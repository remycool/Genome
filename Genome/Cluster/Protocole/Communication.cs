using Cluster.Classes;
using Cluster.Interfaces;
using Cluster.Utils;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Cluster.Protocole
{
    public class Communication
    {
        public IPAddress AdresseIpLocale { get; set; }
        public int PortEcoute { get; set; }
        public int PortEnvoie { get; set; }
        public TcpListener LocalListener { get; set; }

        public delegate void OperationHandler(object sender, OperationEventArgs operationEventArgs);
        public event OperationHandler NouvelleOperation;

        public void SignalerNouvelleOperation(Operation o)
        {
            OperationEventArgs e = new OperationEventArgs(o);
            NouvelleOperation?.Invoke(this, e);
        }

        public Communication(IPAddress adressIpLocale, int portIn, int portOut)
        {
            AdresseIpLocale = adressIpLocale;

            PortEcoute = portIn;
            PortEnvoie = portOut;
            LocalListener = new TcpListener(AdresseIpLocale, PortEcoute);
            LocalListener.Start();
        }

        public void Envoyer(IPAddress remote, Operation obj)
        {

            try
            {
                IPEndPoint remoteEP = new IPEndPoint(remote, PortEnvoie);
                TcpClient local = new TcpClient();
                local.Connect(remoteEP);
                byte[] ba = Utility.Serialize(obj);
                using (NetworkStream ns = local.GetStream())
                {
                    ns.Write(ba, 0, ba.Length);
                };
                local.Close();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + ex.StackTrace);
            }

        }

        public void Recevoir()
        {
            Operation obj = null;

            try
            {
                while (true)
                {
                    using (TcpClient remote = LocalListener.AcceptTcpClient())
                    using (NetworkStream ns = remote.GetStream())
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
                            obj = Utility.Deserialize(data);
                        }
                    }

                    //Décompresser le fichier
                    if (!string.IsNullOrEmpty(obj.Param))
                    {
                        string unzipFile = obj.Param.Decompress();
                        obj.Param = string.Empty;
                        obj.Param = unzipFile;
                    }
                    SignalerNouvelleOperation(obj);
                }


            }
            catch (Exception ex)
            {
                Console.Write(ex.Message + ex.StackTrace);
            }


        }

    }

    public class OperationEventArgs
    {
        public Operation Op;
        public OperationEventArgs(Operation o) { Op = o; }
    }
}
