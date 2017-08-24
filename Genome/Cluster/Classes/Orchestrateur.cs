using Cluster.Interfaces;
using Cluster.Protocole;
using Cluster.Utils;
using System.Collections.Generic;
using System.Net;
using System;
using Cluster.Exceptions;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace Cluster.Classes
{
    public class Orchestrateur : INoeud
    {
        #region PROPRIETES
        public const string NOEUD = "192.168.0.25";
        public IPAddress AdresseIP { get; set; }
        public List<IPAddress> AdressesNoeuds { get; set; }
        public Communication com { get; set; }
        public List<string> Chuncks { get; set; }
        public IDALFactory DALService { get; set; }
        public MapReduce<Operation, Operation> MapRed { get; set; }
        public Operation Resultat { get; set; }


        #endregion

        public delegate void ResultatHandler(object sender, ResultatEventArgs resultatEventArgs);
        public event ResultatHandler NouveauResultat;

        public void SignalerNouveauResultat(Operation r)
        {
            ResultatEventArgs e = new ResultatEventArgs(r);
            NouveauResultat?.Invoke(this, e);
        }




        /// <summary>
        /// On additionne les résultats dès que l'orchestrateur réceptionne un retour d'opération
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onNouvelleOperation(object sender, OperationEventArgs e)
        {
            if (Resultat == null)
                Resultat = e.Op;
            else
                Resultat += e.Op;
            SignalerNouveauResultat(Resultat);
        }


        public Orchestrateur(IDALFactory DalService)
        {
            AdressesNoeuds = new List<IPAddress>();
            AdresseIP = Utility.GetLocalIP();
            com = new Communication(AdresseIP, 8888, 9999);
            com.NouvelleOperation += onNouvelleOperation;
            Thread ecouteOrchestrateur = new Thread(new ThreadStart(com.Recevoir));
            ecouteOrchestrateur.Start();
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
        public void Envoyer(Operation op)
        {
            com.Envoyer(IPAddress.Parse(op.IpNoeud), op);
        }

        public void RepartirCalcul(string fileText, string methode)
        {
            // MapRed.mapReduce(ChunkFactoryToReduce(fileText, methode), c => Envoyer(c));
            //ChunkFactory(fileText, methode);
            foreach (IPAddress a in AdressesNoeuds)
            {
                //IPAddress remote = IPAddress.Parse(NOEUD);
                com.Envoyer(a, new Operation() { IpNoeud = a.ToString(), Param = fileText, Type = methode });
            }
        }

        public IEnumerable<Operation> ChunkFactoryToReduce(string fileText, string methode)
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

        /// <summary>
        /// Méthode qui découpe la chaine de caractère passée en paramètre et envoye le morceau au noeud avec le nom de la méthode passée en paramètre
        /// </summary>
        /// <param name="fileText"></param>
        /// <param name="methode"></param>
        public void ChunkFactory(string fileText, string methode)
        {
            int startPos = 0;
            int blocksize = 1000;
            int posListeNoeud = 0;
            int posDernierNoeudDansListe = AdressesNoeuds.Count - 1;
            var iterations = Math.Round((decimal)(fileText.Length / blocksize));
            for (int i = 0; i < iterations - 1; i++)
            {
                IPAddress adresseNoeud = SelectNoeud(posListeNoeud);
                com.Envoyer(adresseNoeud, new Operation() { IpNoeud = adresseNoeud.ToString(), Param = fileText.Substring(startPos, blocksize), Type = methode });
                if (posListeNoeud == posDernierNoeudDansListe)
                    posListeNoeud = 0;
                else
                    posListeNoeud++;
                startPos += blocksize;
            }
        }


        //public Operation Recevoir()
        //{
        //   return Resultat += com.Recevoir(AdresseIP);
        //}

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

        private IPAddress SelectNoeud(int i)
        {
            if (i > AdressesNoeuds.Count - 1)
                throw new Exception($"Aucun noeud trouvé a l'emplacement {i} de la liste");
            return AdressesNoeuds[i];
        }

        public void AttenteCalcul()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

            if (com.LocalListener != null)
                com.LocalListener.Stop();
            //Mettre à jour info du noeud courant dans le registre
            DALService.UpdateNode(AdresseIP.ToString(), ClusterConstantes.ETAT_NOT_CONNECTED, ClusterConstantes.ROLE_ORCHESTRATEUR);
            Console.WriteLine(DALService.GetClusterView());
            DALService.Dispose();
        }

        public Operation GetResultat()
        {
            return Resultat;
        }
    }
    public class ResultatEventArgs
    {
        public Operation Op;
        public ResultatEventArgs(Operation o) { Op = o; }
    }
}

