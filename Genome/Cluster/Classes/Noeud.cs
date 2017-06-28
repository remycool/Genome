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
    public class Noeud : ICalculable
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
            Operation calcul = Com.Recevoir(AdresseIP);
            ExecuterCalcul(ref calcul);
            Com.Envoyer(IPAddress.Parse(ORCHESTRATEUR), calcul);
        }

        private void ExecuterCalcul( ref Operation calcul)
        {
            //executer la methode invoquée en utilisant la réflexion
            Type type = typeof(ICalculable);
            MethodInfo info = type.GetMethod(calcul.Type);
            //Transformer le tableau de byte en string
            string chaine = Convert.ToString(calcul.Param);
            calcul = (Operation)info.Invoke(this, new object[] { 'A', chaine });
        }

        Operation ICalculable.CountChars(char charToCount, string chaine)
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

            return new Operation { Resultat = count, TempsExecution = sw.ElapsedMilliseconds };
        }
    }
}
