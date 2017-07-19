using Cluster.Classes;
using Cluster.Interfaces;
using Cluster.Protocole;
using Cluster.Utils;
using Cluster_DAL;
using System;
using System.Net;
using System.Reflection;
using System.Threading;

namespace Cluster.Classes
{
    public class Noeud : INoeud
    {
        public const int PORT = 8888;
        // public const string ORCHESTRATEUR = "192.168.0.21";
        public const string ORCHESTRATEUR = "10.131.128.243";


        public IPAddress AdresseIP { get; set; }
        public IPAddress OrchestrateurIP { get; set; }
        public Communication Com { get; set; }
        public IBusinessFactory Service { get; set; }

        public Noeud(IBusinessFactory service)
        {
            AdresseIP = Utility.GetLocalIP();
            Com = Communication.Instance;
            Service = service;
            Initialize();
        }



        public override string ToString()
        {
            return $"@ IP : {AdresseIP.ToString()}";
        }

        public void AttenteCalcul()
        {
            Operation calcul = Com.Recevoir(AdresseIP);
            using(ClusterDAL dal = new ClusterDAL("MYSQL"))
            {
                Thread th = new Thread(()=>dal.UpdateCluster(AdresseIP.ToString(), etat.workInProgress, role.noeud));
                th.Start();
            }
            
            ExecuterCalcul(ref calcul);

            Com.Envoyer(IPAddress.Parse(ORCHESTRATEUR), calcul);
        }

        private void ExecuterCalcul(ref Operation calcul)
        {
            //executer la methode invoquée en utilisant la réflexion
            Type type = typeof(IBusinessFactory);
            MethodInfo info = type.GetMethod(calcul.Type);
            //Transformer le tableau de byte en string
            string chaine = calcul.Param;
            calcul = (Operation)info.Invoke(Service, new object[] { chaine });
        }
        
        /// <summary>
        /// Met à jour l'état du le registre du cluster 
        /// </summary>
        public void Initialize()
        {
            using (ClusterDAL dal = new ClusterDAL("MYSQL"))
            {
                //Mettre à jour info du noeud courant dans le registre
                dal.UpdateCluster(AdresseIP.ToString(), etat.connected, role.noeud);
                Console.WriteLine(dal.GetClusterRegistry());
                //obtenir l'IP de l'orchestrateur
                string ip = dal.GetOrchestrateurIp();

                if (!string.IsNullOrEmpty(ip))
                    OrchestrateurIP = IPAddress.Parse(ip);
            }
        }

        /// <summary>
        /// Met à jour l'état du noeud à "déconnecté" dans le registre du cluster 
        /// </summary>
        public void Close()
        {
            using (ClusterDAL dal = new ClusterDAL("MYSQL"))
            {
                //Mettre à jour info du noeud courant dans le registre
                dal.UpdateCluster(AdresseIP.ToString(), etat.notConnected, role.noeud);
                Console.WriteLine(dal.GetClusterRegistry());
            }
        }

        public Operation EnvoyerCalcul(Operation operation)
        {
            throw new NotImplementedException();
        }
    }
}
