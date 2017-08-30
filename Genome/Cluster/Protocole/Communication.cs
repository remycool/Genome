using Cluster.Classes;
using Cluster.Events;
using Cluster.Exceptions;
using Cluster.Interfaces;
using Cluster.Utils;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Cluster.Protocole
{
    public class Communication<T, U>
    {
        public IPAddress AdresseIpLocale { get; set; }
        public int PortEcoute { get; set; }
        public int PortEnvoie { get; set; }
        public TcpListenerCluster LocalListener { get; set; }


        #region GESTION EVENEMENT
        public delegate void ReceptionHandler(object sender, ReceptionEventArgs<U> operationEventArgs);
        public event ReceptionHandler NouvelleReception;

        public void SignalerNouvelleReception(U o)
        {
            ReceptionEventArgs<U> e = new ReceptionEventArgs<U>(o);
            NouvelleReception?.Invoke(this, e);
        }
        #endregion

        public Communication(IPAddress adressIpLocale, int portIn, int portOut)
        {
            AdresseIpLocale = adressIpLocale;

            PortEcoute = portIn;
            PortEnvoie = portOut;
            LocalListener = new TcpListenerCluster(AdresseIpLocale, PortEcoute);
            LocalListener.Start();
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
                IPEndPoint remoteEP = new IPEndPoint(remote, PortEnvoie);
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
                Console.Write(ex.Message + ex.StackTrace);
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
            if (LocalListener.Active)
            {
                TcpClient client = LocalListener.EndAcceptTcpClient(asyncResult);
                if (client != null)
                    TraitementRequete(client);
                RecevoirAsync();
            }
            
        }

        /// <summary>
        /// Initialise un TcpListener et créé un nouveau thread
        /// </summary>
        //public void Recevoir()
        //{
        //    LocalListener = new TcpListener(AdresseIpLocale, PortEcoute);
        //    LocalListener.Start();

        //    try
        //    {
        //        while (true)
        //        {
        //            TcpClient remote = LocalListener.AcceptTcpClient();
        //            ThreadPool.QueueUserWorkItem(new WaitCallback(TraitementRequete), remote);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.Write(ex.Message + ex.StackTrace);
        //    }
        //    finally
        //    {
        //        LocalListener.Stop();
        //    }
        //}

        public void RecevoirAsync()
        {
            if (LocalListener.Active)
                LocalListener.BeginAcceptTcpClient(new AsyncCallback(OnClientConnected), null);
        }
    }


}
