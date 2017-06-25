using Cluster.Classes;
using Cluster.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Script.Serialization;

namespace Cluster
{
    public class Orchestrateur
    {
        public const int PORT = 8888;
        public const string NOEUD = "192.168.0.25";
        public IPAddress AdresseIP { get; set; }
        public List<IPAddress> AdressesNoeuds { get; set; }

        public Orchestrateur()
        {
            AdressesNoeuds = new List<IPAddress>();
            AdresseIP = Utility.GetLocalIP();
        }

        /// <summary>
        /// Permet de rechercher les noeuds connectés
        /// </summary>
        //public void RechercherNoeud()
        //{
        //    Console.ForegroundColor = ConsoleColor.DarkGreen;

        //    Console.WriteLine($"Recherche de noeuds");

        //    int nbAppel = 0;
        //    UdpClient orchestrateur = new UdpClient(PORT);
        //    byte[] dataOrchestrateur = Encoding.ASCII.GetBytes(AdresseIP.ToString());
        //    IPEndPoint noeud = new IPEndPoint(IPAddress.Broadcast, PORT);
        //    orchestrateur.EnableBroadcast = true;
        //    while (true)
        //    {

        //        orchestrateur.Send(dataOrchestrateur, dataOrchestrateur.Length, new IPEndPoint(IPAddress.Broadcast, PORT));
        //        byte[] dataNoeud = orchestrateur.Receive(ref noeud);

        //        //Si le noeud n'est pas null ET qu'il n'est pas déjà inclu dans la liste 
        //        if (noeud.Address != null && !AdressesNoeuds.Exists(a => a.Equals(noeud.Address)))
        //        {
        //            AdressesNoeuds.Add(noeud.Address);
        //            Console.WriteLine($"Noeud {noeud.Address.ToString()} a répondu");
        //        }


        //        nbAppel++;
        //    }
        //    orchestrateur.Close();
        //    Console.ReadKey();

        //}

        //public void Ecouter()
        //{
        //    Console.ForegroundColor = ConsoleColor.DarkCyan;

        //    TcpListener orchestrateurListener = new TcpListener(AdresseIP, PORT);

        //    orchestrateurListener.Start();

        //    Console.WriteLine("\n\nEn attente.....");

        //    Socket s = orchestrateurListener.AcceptSocket();

        //    byte[] b = new byte[100];
        //    int k = s.Receive(b);
        //    Console.WriteLine($">>>>>> nouveau message de { s.RemoteEndPoint}\n\n");
        //    for (int i = 0; i < k; i++)
        //        Console.Write(Convert.ToChar(b[i]));

        //    ASCIIEncoding asen = new ASCIIEncoding();
        //    s.Send(asen.GetBytes("Le message a bien été reçu par l'orchestrateur."));
        //    Console.WriteLine("\n Accusé de reception envoyé");

        //    s.Close();
        //    orchestrateurListener.Stop();
        //    Communiquer(IPAddress.Parse(s.RemoteEndPoint.ToString()));
        //}

        //public void Communiquer(IPAddress adresseNoeud)
        //{

        //    Console.ForegroundColor = ConsoleColor.DarkYellow;
        //    TcpClient tcpclnt = new TcpClient();

        //    Console.WriteLine("\n\nConnexion.....");

        //    tcpclnt.Connect(adresseNoeud.ToString(), PORT);

        //    Console.WriteLine("OK\n\n");
        //    Console.Write("Entrer un message : ");

        //    string str = Console.ReadLine();
        //    Stream stm = tcpclnt.GetStream();

        //    ASCIIEncoding asen = new ASCIIEncoding();
        //    byte[] ba = asen.GetBytes(str);
        //    Console.WriteLine("\nEnvoie >>>>>\n");

        //    stm.Write(ba, 0, ba.Length);

        //    byte[] bb = new byte[100];
        //    int k = stm.Read(bb, 0, 100);

        //    for (int i = 0; i < k; i++)
        //        Console.Write(Convert.ToChar(bb[i]));

        //    tcpclnt.Close();
        //    Ecouter();

        //}


        public void EnvoyerCalcul(Calcul<byte[]> calcul)
        {

         //   Console.WriteLine($"Calcul envoyé par l'orchestrateur : {calcul.Type} {calcul.Nb1} et {calcul.Nb2}");
            IPEndPoint IPNoeud = new IPEndPoint(IPAddress.Parse(NOEUD), PORT);
            TcpClient orchestrateur = new TcpClient();
            orchestrateur.Connect(IPNoeud);

            byte[] ba = Utility.Serialize(calcul);
            using (NetworkStream ns = orchestrateur.GetStream())
            {
                ns.Write(ba, 0, ba.Length);
            };


            orchestrateur.Close();
            AttenteResultatCalcul();
        }

        public void AttenteResultatCalcul()
        {
            //Initialisation du listener
            TcpListener orchestrateur = new TcpListener(AdresseIP, PORT);

            //Débuter l'écoute
            orchestrateur.Start();
            Console.WriteLine("En attente d'un resultat.....\n");


            TcpClient noeud = orchestrateur.AcceptTcpClient();
            NetworkStream ns = noeud.GetStream();

            ResultatCalcul resultat = RecevoirResultatCalcul(ns);
            AfficherResultatCalcul(resultat);
        }

        public ResultatCalcul RecevoirResultatCalcul(NetworkStream ns)
        {
            ResultatCalcul resultat = null;
            int i = 0;
            byte[] resultFromNoeud = new byte[1024];
            string data = string.Empty;
            //Lecture du flux
            while ((i = ns.Read(resultFromNoeud, 0, resultFromNoeud.Length)) != 0)
            {
                data = Encoding.UTF8.GetString(resultFromNoeud, 0, i);
                Console.WriteLine($"Donnees : {data}");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            resultat = js.Deserialize<ResultatCalcul>(data);


            return resultat;
        }

        public void AfficherResultatCalcul(ResultatCalcul r)
        {
          //  Console.WriteLine($">>> Résultat : {r.Resultat} effectué en {r.TempsExecution} ms");

            //Console.ReadKey();
        }
    }
}
