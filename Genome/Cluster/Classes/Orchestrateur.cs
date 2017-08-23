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
        public const string NOEUD = "192.168.0.21";
        public IPAddress AdresseIP { get; set; }
        public List<IPAddress> AdressesNoeuds { get; set; }
        public Communication com { get; set; }
        public List<string> Chuncks { get; set; }
        public IDALFactory DALService { get; set; }
        public MapReduce<Operation,Operation> MapRed { get; set; }
        #endregion

        public Orchestrateur(IDALFactory DalService)
        {
            AdressesNoeuds = new List<IPAddress>();
            AdresseIP = Utility.GetLocalIP();
            com = new Communication(AdresseIP);
            DALService = DalService;
            Initialize();
        }

        public override string ToString()
        {
            return $"@ IP : {AdresseIP.ToString()}";
        }
       
        /// <summary>
        /// Permet d'envoyer un objet operation sur le réseau TCP/IP
        /// </summary>
        /// <param name="op">L'objet Operation qui contient la fonction à invoquer et le morceau de fichier</param>
        /// <returns>Le résultat de l'opération demandée depuis le noeud distant</returns>
        public Operation Envoyer(Operation op)
        {
            com.Envoyer(IPAddress.Parse(op.IpNoeud), op);
            return Attente();
        }

        public void RepartirCalcul(string fileText, string methode)
        {
            MapRed.mapReduce(ChunkFactory(fileText, methode), c => Envoyer(c));
        }

        public IEnumerable<Operation> ChunkFactory(string fileText , string methode)
        {
            int startPos = 0;
            int blocksize = 10000000;
            var iterations = Math.Round((decimal)(fileText.Length / blocksize));
            for (int i = 0; i < iterations - 1; i++)
            {
                yield return new Operation() { IpNoeud = NOEUD, Param = fileText.Substring(startPos, blocksize), Type = methode }; 
                startPos += blocksize;
            }
            yield return new Operation() { IpNoeud = NOEUD, Param = fileText.Substring(startPos, fileText.Length - startPos), Type = methode }; 
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
            MapRed = new MapReduce<Operation, Operation>();
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

        public int CountChars(string chunk, char charToCount)
        {
            throw new NotImplementedException();
        }
    }
}

