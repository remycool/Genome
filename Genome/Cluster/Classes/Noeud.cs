using Cluster.Classes;
using Cluster.Interfaces;
using Cluster.Protocole;
using Cluster.Utils;
using System;
using System.Net;
using System.Reflection;

namespace Cluster.Classes
{
    public class Noeud 
    {
        public const int PORT = 8888;
        public const string ORCHESTRATEUR = "192.168.0.21";


        public IPAddress AdresseIP { get; set; }
        public IPAddress OrchestrateurIP { get; set; }
        public Communication Com { get; set; }
        public IBusinessFactory Service { get; set; }

        public Noeud(IBusinessFactory service)
        {
            AdresseIP = Utility.GetLocalIP();
            Com = Communication.Instance;
            Service = service;
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
            Type type = typeof(IBusinessFactory);
            MethodInfo info = type.GetMethod(calcul.Type);
            //Transformer le tableau de byte en string
            string chaine = Convert.ToString(calcul.Param);
            calcul = (Operation)info.Invoke(Service, new object[] { chaine });
        }

        //Operation ICalculable.CountChars(char charToCount, string chaine)
        //{
        //    Console.WriteLine("Calcul en cours");
        //    //On lance le chrono
        //    Stopwatch sw = Stopwatch.StartNew();
        //    //opération
        //    int count = 0;
        //    for (int i = 0; i < chaine.Length; i++)
        //    {
        //        if (chaine[i] == charToCount)
        //            count++;
        //    }

        //    Thread.Sleep(1000);
        //    //On arrête le chrono
        //    sw.Stop();

        //    return new Operation { Resultat = count, TempsExecution = sw.ElapsedMilliseconds };
        //}
    }
}
