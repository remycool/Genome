using Cluster.Classes;
using Cluster.Interfaces;
using Cluster.Protocole;
using Cluster.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cluster
{
    public class Noeud:ICalculable
    {
        public const int PORT = 8888;
        public const string ORCHESTRATEUR = "192.168.0.21";


        public IPAddress AdresseIP { get; set; }
        public IPAddress OrchestrateurIP { get; set; }
        public Communication Com { get; set; }

        public Noeud()
        {
            AdresseIP = Utility.GetLocalIP();
            Console.WriteLine($"Adresse locale : {AdresseIP.ToString()}");
            Com = Communication.Instance;
        }

        public override string ToString()
        {
            return $"@ IP : {AdresseIP.ToString()}";
        }

        public void AttenteCalcul()
        {
            //string data = string.Empty;
            //byte[] morceau = new byte[1024];
            ////Initialisation du listener
            //TcpListener noeud = new TcpListener(AdresseIP, PORT);

            ////Débuter l'écoute
            //noeud.Start();

            ////Acceptation d'une demande de connexion
            //TcpClient orchestrateur = noeud.AcceptTcpClient();
            //Console.WriteLine("Contact avec l'orchestrateur établi...Réception Calcul");

            ////Recevoir les données de l'orchestrateur
            //NetworkStream ns = orchestrateur.GetStream();

            //int i = 0;

            ////Lecture du flux
            //while ((i = ns.Read(morceau, 0, morceau.Length)) != 0)
            //{
            //    data = Encoding.UTF8.GetString(morceau, 0, i);
            //    Console.WriteLine($"Donnees : {data}");
            //}

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //Calcul c = js.Deserialize<Calcul>(data);
            //Console.WriteLine($"Objet calcul : \nTypeOperation : {c.TypeOperation},\nNb1:{c.Nb1} ,\nNb2:{c.Nb2}");
            ////Calcul nouveauCalcul = RecevoirCalcul();
            //ResultatCalcul resultat = ExecuterCalcul(c);
            //orchestrateur.Close();
            //noeud.Stop();
            //EnvoyerResultat(resultat);

            Calcul < byte[]> calcul = (Calcul<byte[]>) Com.Recevoir();
            ResultatCalcul resultat = ExecuterCalcul(calcul);
            Com.Envoyer(IPAddress.Parse(ORCHESTRATEUR), resultat);


        }

        private ResultatCalcul ExecuterCalcul(Calcul<byte[]> nouveauCalcul)
        {
            //executer la methode invoquée en utilisant la réflexion
            Type type = typeof(ICalculable);
            MethodInfo info = type.GetMethod(nouveauCalcul.Type);
            //Transformer le tableau de byte en string
            string chaine = Convert.ToString(nouveauCalcul.Param);


            return (ResultatCalcul)info.Invoke(this, new object[] { 'A', chaine });
        }

      

        public void EnvoyerResultat(ResultatCalcul r)
        {
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string objetResultat = js.Serialize(r);
            //byte[] data = Encoding.UTF8.GetBytes(objetResultat);
            ////string result = $"le résultat est : {r.Resultat} ( {r.TempsExecution} ms)";
            //IPEndPoint orchestrateur = new IPEndPoint(IPAddress.Parse(ORCHESTRATEUR), PORT);
            ////byte[] data = Encoding.UTF8.GetBytes(result);
            //TcpClient noeud = new TcpClient();
            //noeud.Connect(orchestrateur);
            //using (NetworkStream ns = noeud.GetStream())
            //{

            //    ns.Write(data, 0, data.Length);
            //}
            //noeud.Close();

        }


        IClusterizable ICalculable.CountChars(char charToCount, string chaine)
        {
            Console.WriteLine("Calcul en cours");
            //On lance le chrono
            Stopwatch sw = Stopwatch.StartNew();
            //opération
            int count = 0;
            for (int i = 0; i < chaine.Length; i++)
            {
                if (chaine[i] == charToCount)
                    count++;
            }

            Thread.Sleep(1000);
            //On arrête le chrono
            sw.Stop();

            return new ResultatCalcul { Resultat = count, TempsExecution = sw.ElapsedMilliseconds };
        }
    }
}
