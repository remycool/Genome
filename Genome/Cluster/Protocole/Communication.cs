using Cluster.Classes;
using Cluster.Events;
using Cluster.Exceptions;
using Cluster.Interfaces;
using Cluster.Logs;
using Cluster.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Cluster.Protocole
{
    public class Communication<T, U>
    {
        public IPAddress AdresseIpLocale { get; set; }
        public int PortEcouteTCP { get; set; }
        public int PortEnvoieTCP { get; set; }
        public int PortEcouteUDP { get; set; }
        public int PortEnvoieUDP { get; set; }
        public TcpListenerCluster LocalTcpListener { get; set; }
        public UdpClientCluster LocalUdpListener { get; set; }


        #region GESTION EVENEMENT
        public delegate void ReceptionHandler(object sender, ReceptionEventArgs<U> operationEventArgs);
        public event ReceptionHandler NouvelleReception;

        public void SignalerNouvelleReception(U o)
        {
            ReceptionEventArgs<U> e = new ReceptionEventArgs<U>(o);
            NouvelleReception?.Invoke(this, e);
        }
        #endregion

        public Communication(IPAddress adressIpLocale, int portIn, int portOut, int portUdpIn, int portUdpOut)
        {
            AdresseIpLocale = adressIpLocale;
            
            PortEcouteTCP = portIn;
            PortEnvoieTCP = portOut;
            PortEcouteUDP = portUdpIn;
            PortEnvoieUDP = portUdpOut;
            LocalUdpListener = new UdpClientCluster(PortEcouteUDP);
            
            LocalTcpListener = new TcpListenerCluster(AdresseIpLocale, PortEcouteTCP);
            LocalTcpListener.Start();
            RecevoirAsync();
        }

        /// <summary>
        /// Sérialise un objet et le transmet à l'adresse passée en paramètre via TCP
        /// </summary>
        /// <param name="remote"></param>
        /// <param name="obj"></param>
        public void Envoyer(IPAddress remote, T obj)
        {
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(remote, PortEnvoieTCP);
                TcpClient local = new TcpClient();
                local.Connect(remoteEP);
                byte[] ba = Utility<T>.Serialize(obj);
                using (NetworkStream ns = local.GetStream())
                {
                    ns.Write(ba, 0, ba.Length);
                    ns.Close();
                };

                local.Close();

            }
            catch (Exception ex)
            {
                GestionLog.Log($"{ex.Message} \n {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Lit le flux de donnée, désérialise l'objet transmit et signale sa présence
        /// </summary>
        /// <param name="remote"></param>
        public void TraitementRequete(object remote)
        {
            TcpClient client = (TcpClient)remote;
            U obj = default(U);
            using (NetworkStream ns = client.GetStream())
            {
                int i = 0;
                byte[] remoteData = new byte[1024];
                string data = string.Empty;
                //Lecture du flux
                if (ns.CanRead)
                {
                    while ((i = ns.Read(remoteData, 0, remoteData.Length)) != 0)
                    {
                        data += Encoding.UTF8.GetString(remoteData, 0, i);
                    }
                    obj = Utility<U>.Deserialize(data);
                }
                ns.Close();
                client.Close();
                SignalerNouvelleReception(obj);
            }
        }

        public void OnClientConnected(IAsyncResult asyncResult)
        {
            if (LocalTcpListener.Active)
            {
                TcpClient client = LocalTcpListener.EndAcceptTcpClient(asyncResult);
                if (client != null)
                    TraitementRequete(client);
                RecevoirAsync();
            }

        }

        public void RecevoirAsync()
        {
            if (LocalTcpListener.Active)
                LocalTcpListener.BeginAcceptTcpClient(new AsyncCallback(OnClientConnected), null);
        }

        #region BROADCAST

        public IPAddress EnvoyerBroadcast()
        {
       
            byte [] donneesEnvoyees = Encoding.ASCII.GetBytes("Orchestrateur connecte!!!");
            IPEndPoint distant = new IPEndPoint(IPAddress.Any, PortEnvoieUDP);

            LocalUdpListener.EnableBroadcast = true;
            LocalUdpListener.Send(donneesEnvoyees, donneesEnvoyees.Length, new IPEndPoint(IPAddress.Broadcast, PortEnvoieUDP));

            byte[] donneesDistantes = LocalUdpListener.Receive(ref distant);
            string ServerResponse = Encoding.ASCII.GetString(donneesDistantes);

            return distant.Address;
        }

        public void RecevoirBroadcastAsync()
        {

            try
            {
                LocalUdpListener.BeginReceive(new AsyncCallback(OnAnswerToBroadcast), null);
            }
            catch (Exception ex)
            {
                GestionLog.Log($"{ex.Message} \n {ex.StackTrace}");
            }
        }

        private void OnAnswerToBroadcast(IAsyncResult result)
        {
            
                IPEndPoint distant = new IPEndPoint(IPAddress.Any, PortEnvoieUDP);
                byte[] donneesRecues = LocalUdpListener.EndReceive(result, ref distant);
                byte[] donneesrenvoyees = Encoding.ASCII.GetBytes("Ok bien reçu!");
                LocalUdpListener.Send(donneesrenvoyees, donneesrenvoyees.Length, distant);
                LocalUdpListener.BeginReceive(new AsyncCallback(OnAnswerToBroadcast), null);

        }

        #endregion
    }


}
