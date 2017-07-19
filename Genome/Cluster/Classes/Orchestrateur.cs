using Cluster.Interfaces;
using Cluster.Protocole;
using Cluster.Utils;
using System.Collections.Generic;
using System.Net;
using System;
using Cluster_DAL;

namespace Cluster.Classes
{
    public class Orchestrateur : INoeud
    {
        public const int PORT = 8888;
        //public const string NOEUD = "192.168.0.25";
        public const string NOEUD = "10.131.129.3";
        public IPAddress AdresseIP { get; set; }
        public List<IPAddress> AdressesNoeuds { get; set; }
        public Communication com { get; set; }
        public List<string> Chuncks { get; set; }

        public Orchestrateur()
        {
            AdressesNoeuds = new List<IPAddress>();
            AdresseIP = Utility.GetLocalIP();
            com = Communication.Instance;
            Initialize();
        }

        public override string ToString()
        {
            return $"@ IP : {AdresseIP.ToString()}";
        }

        public void Map()
        {
            //Découper le fichier

        }

        public Operation EnvoyerCalcul(Operation op)
        {
            com.Envoyer(IPAddress.Parse(NOEUD), op);
            return AttenteResultatCalcul();
        }

        public Operation AttenteResultatCalcul()
        {
            return com.Recevoir(AdresseIP);
        }

        public void Initialize()
        {

            using (ClusterDAL dal = new ClusterDAL("MYSQL"))
            {
                //Mettre à jour info du noeud courant dans le registre
                dal.UpdateCluster(AdresseIP.ToString(), etat.connected, role.orchestrateur);
                Console.WriteLine(dal.GetClusterRegistry());
                //obtenir l'IP des noeuds connectés
                AdressesNoeuds = dal.GetAllNodeIPs();
            }
        }

        public void Close()
        {
            using (ClusterDAL dal = new ClusterDAL("MYSQL"))
            {
                //Mettre à jour info du noeud courant dans le registre
                dal.UpdateCluster(AdresseIP.ToString(), etat.notConnected, role.orchestrateur);
                Console.WriteLine(dal.GetClusterRegistry());
            }
        }

        public void AttenteCalcul()
        {
            throw new NotImplementedException();
        }
    }
}
