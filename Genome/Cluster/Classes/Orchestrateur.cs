using Cluster.Interfaces;
using Cluster.Protocole;
using Cluster.Utils;
using System.Collections.Generic;
using System.Net;
using System;
using Cluster.Exceptions;
using System.Threading.Tasks;
using System.Linq;

namespace Cluster.Classes
{
    public class Orchestrateur : INoeud
    {
        #region PROPRIETES
        public const int PORT = 8888;
        //public const string NOEUD = "192.168.0.25";
        public const string NOEUD = "10.131.129.3";
        public IPAddress AdresseIP { get; set; }
        public List<IPAddress> AdressesNoeuds { get; set; }
        public Communication com { get; set; }
        public List<string> Chuncks { get; set; }
        public IDALFactory DALService { get; set; }
        #endregion

        public Orchestrateur(IDALFactory DalService)
        {
            AdressesNoeuds = new List<IPAddress>();
            AdresseIP = Utility.GetLocalIP();
            com = Communication.Instance;
            DALService = DalService;
            Initialize();
        }

        public override string ToString()
        {
            return $"@ IP : {AdresseIP.ToString()}";
        }

        public async void Map(string chunck)
        {

            //Découper le fichier, associer chaque morceau à un calcul l'envoyer à une adresse IP 
            MapReduce mr = new MapReduce();
            List<Operation> ops = new List<Operation>();
            ops.Add(new Operation { Type = "GetCalcul1", Param = chunck });
            Operation result = await mr.MapRed<Operation, Operation, Operation>(ops, op => Envoyer(op), r => Reduce(r));

        }

       
        

        /// <summary>
        /// Permet d'envoyer un objet opérateur sur le réseau TCP/IP
        /// </summary>
        /// <param name="op">L'objet Operation qui contient la fonction à invoquer et le morceau de fichier</param>
        /// <returns>Le résultat de l'opération demandée depuis le noeud distant</returns>
        public Operation Envoyer(Operation op)
        {
            com.Envoyer(IPAddress.Parse(NOEUD), op);
            return Attente();
        }

        public Operation Attente()
        {
            return com.Recevoir(AdresseIP);
        }

        /// <summary>
        /// Met à jour les informations dans le registre Cluster et récupère
        /// les adresses IP de tous les noeuds connectés
        /// </summary>
        public void Initialize()
        {
            //Mettre à jour info du noeud courant dans le registre
            DALService.UpdateNode(AdresseIP.ToString(), ClusterConstantes.ETAT_CONNECTED, ClusterConstantes.ROLE_ORCHESTRATEUR);
            Console.WriteLine(DALService.GetClusterView());
            //obtenir l'IP des noeuds connectés
            AdressesNoeuds = DALService.GetAllNodeIPs();
        }

        public void AttenteCalcul()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //Mettre à jour info du noeud courant dans le registre

            DALService.UpdateNode(AdresseIP.ToString(), ClusterConstantes.ETAT_NOT_CONNECTED, ClusterConstantes.ROLE_ORCHESTRATEUR);
            Console.WriteLine(DALService.GetClusterView());
            DALService.Dispose();
        }
    }
}

