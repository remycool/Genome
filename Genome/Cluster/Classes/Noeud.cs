using Cluster.Classes;
using Cluster.Interfaces;
using Cluster.Protocole;
using Cluster.Utils;
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
        //public const string ORCHESTRATEUR = "10.131.128.243";
        public const string ORCHESTRATEUR = "192.168.0.41";

        public IPAddress AdresseIP { get; set; }
        public IPAddress OrchestrateurIP { get; set; }
        public Communication Com { get; set; }
        public IBusinessFactory BusinessService { get; set; }
        public IDALFactory DALService { get; set; }

        public Noeud(IBusinessFactory BuService, IDALFactory DalService)
        {
            AdresseIP = Utility.GetLocalIP();
            Com = Communication.Instance;
            BusinessService = BuService;
            DALService = DalService;
            Initialize();
        }

        public override string ToString()
        {
            return $"@ IP : {AdresseIP.ToString()}";
        }

        /// <summary>
        /// Permet d'ecouter une operation émanant de l'orchestrateur
        /// </summary>
        /// <returns>un objet operation résultant du traitement effectué sur le noeud</returns>
        public Operation Attente()
        {
            Operation calcul = Com.Recevoir(AdresseIP);
            ExecuterCalcul(ref calcul);
            return Envoyer(calcul);
        }

        /// <summary>
        /// Permet d'executer la méthode du business invoqué par l'opération passée en parametre 
        /// </summary>
        /// <param name="calcul">Objet parametre qui contient la méthode à executer et le morceaux de fichier</param>
        private void ExecuterCalcul(ref Operation calcul)
        {
            //On utilise la réflexion pour obtenir la méthode depuis la factory
            Type type = typeof(IBusinessFactory);
            MethodInfo info = type.GetMethod(calcul.Type);
            string chaine = calcul.Param;
            //On execute la fonction
            calcul = (Operation)info.Invoke(BusinessService, new object[] { chaine });
        }

        /// <summary>
        /// Met à jour l'état du le registre du cluster 
        /// </summary>
        public void Initialize()
        {

            //Mettre à jour info du noeud courant dans le registre
            DALService.UpdateNode(AdresseIP.ToString(), ClusterConstantes.ETAT_CONNECTED, ClusterConstantes.ROLE_NOEUD);
            Console.WriteLine(DALService.GetClusterView());
            //obtenir l'IP de l'orchestrateur
            string ip = DALService.GetOrchestrateurIP();

            if (!string.IsNullOrEmpty(ip))
                OrchestrateurIP = IPAddress.Parse(ip);
        }

        /// <summary>
        /// Permet de mettre à jour le registre du cluster
        /// </summary>
        public void Dispose()
        {

            //Mettre à jour info du noeud courant dans le registre
            DALService.UpdateNode(AdresseIP.ToString(), ClusterConstantes.ETAT_NOT_CONNECTED, ClusterConstantes.ROLE_NOEUD);
            Console.WriteLine(DALService.GetClusterView());

        }

        /// <summary>
        /// Permet d'envoyer une opération résultante à destination de l'orchestrateur
        /// </summary>
        /// <param name="operation"></param>
        /// <returns>Un objet Operation</returns>
        public Operation Envoyer(Operation operation)
        {
            string paramCompressed = string.Empty;
            if (string.IsNullOrEmpty(operation.Param))
                paramCompressed = operation.Param.Compress();
            operation.Param = paramCompressed;
            Com.Envoyer(IPAddress.Parse(ORCHESTRATEUR), operation);
            return Attente();
        }

        public int Map(string chunck)
        {
            throw new NotImplementedException();
        }
    }
}

