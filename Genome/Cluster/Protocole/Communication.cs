using Cluster.Interfaces;
using Cluster.Utils;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Cluster.Protocole
{
    public class Communication
    {
        private static Communication instance;
        private static object syncRoot = new object();
        public IPAddress AdresseIpLocale { get; set; }
        public const int PORT = 8888;



        private Communication() {

            
        }

        public static Communication Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Communication();
                    }
                }

                return instance;
            }
        }


        public void Envoyer(IPAddress remote, IClusterizable obj)
        {
            IPEndPoint remoteEP = new IPEndPoint(remote, PORT);
            TcpClient local = new TcpClient();
            local.Connect(remoteEP);

            byte[] ba = Utility.Serialize(obj);
            using (NetworkStream ns = local.GetStream())
            {
                ns.Write(ba, 0, ba.Length);
            };


            local.Close();
        }

        public IClusterizable Recevoir()
        {
            //Initialisation du listener
            TcpListener localListener = new TcpListener(AdresseIpLocale, PORT);

            //Débuter l'écoute
            localListener.Start();
            //Console.WriteLine("En attente d'un resultat.....\n");


            TcpClient remote = localListener.AcceptTcpClient();
            NetworkStream ns = remote.GetStream();

            IClusterizable obj = null;
            int i = 0;
            byte[] remoteData = new byte[1024];
            string data = string.Empty;
            //Lecture du flux
            while ((i = ns.Read(remoteData, 0, remoteData.Length)) != 0)
            {
                data = Encoding.UTF8.GetString(remoteData, 0, i);
                //Console.WriteLine($"Donnees : {data}");
            }

            obj = Utility.Deserialize(data);

            return obj;
        }

    }
}
