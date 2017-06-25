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
            IPEndPoint noeud = new IPEndPoint(remote, PORT);
            TcpClient orchestrateur = new TcpClient();
            orchestrateur.Connect(noeud);

            byte[] ba = Utility.Serialize(obj);
            using (NetworkStream ns = orchestrateur.GetStream())
            {
                ns.Write(ba, 0, ba.Length);
            };


            orchestrateur.Close();
        }

        public IClusterizable Recevoir()
        {
            //Initialisation du listener
            TcpListener orchestrateur = new TcpListener(AdresseIpLocale, PORT);

            //Débuter l'écoute
            orchestrateur.Start();
            //Console.WriteLine("En attente d'un resultat.....\n");


            TcpClient noeud = orchestrateur.AcceptTcpClient();
            NetworkStream ns = noeud.GetStream();

            IClusterizable obj = null;
            int i = 0;
            byte[] resultFromNoeud = new byte[1024];
            string data = string.Empty;
            //Lecture du flux
            while ((i = ns.Read(resultFromNoeud, 0, resultFromNoeud.Length)) != 0)
            {
                data = Encoding.UTF8.GetString(resultFromNoeud, 0, i);
                //Console.WriteLine($"Donnees : {data}");
            }

            obj = Utility.Deserialize(data);

            return obj;
        }

    }
}
