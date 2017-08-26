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
using Cluster.Events;

namespace Cluster.Classes
{
    public class Orchestrateur
    {
        #region PROPRIETES
        public const string NOEUD = "192.168.0.25";
        public IPAddress AdresseIP { get; set; }
        public List<IPAddress> AdressesNoeuds { get; set; }
        public Communication<Operation, Resultat> com { get; set; }
        public List<string> Chuncks { get; set; }
        public IDALFactory DALService { get; set; }
        //public MapReduce<Operation, Operation> MapRed { get; set; }
        public Resultat Result { get; set; }


        #endregion

        #region EVENT
        public delegate void ResultatHandler(object sender, ResultatEventArgs resultatEventArgs);
        public event ResultatHandler NouveauResultat;

        public void SignalerNouveauResultat(Resultat r)
        {
            ResultatEventArgs e = new ResultatEventArgs(r);
            NouveauResultat?.Invoke(this, e);
        }
        #endregion


        /// <summary>
        /// On additionne les résultats dès que l'orchestrateur réceptionne un retour d'opération
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onNouvelleReception(object sender, ReceptionEventArgs<Resultat> e)
        {
            if (Result == null)
                Result = e.Op;
            else
                Result += e.Op;
            
            Console.WriteLine($"reçu de {e.Op.IpNoeud} Total = {Result.Valeur}");
            SignalerNouveauResultat(Result);
        }

        public Orchestrateur(IDALFactory DalService)
        {
            AdressesNoeuds = new List<IPAddress>();
            AdresseIP = IpConfig.GetLocalIP();
            com = new Communication<Operation, Resultat>(AdresseIP, 8888, 9999);
            com.NouvelleReception += onNouvelleReception;
            //Thread ecouteOrchestrateur = new Thread(com.RecevoirAsync);
            //ecouteOrchestrateur.Start();
            DALService = DalService;
            Initialize();
        }

        /// <summary>
        /// Affiche l'adresse IP de l'objet
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Appelle la méthode qui distribue un morceau du fichier passé en paramètre au noeuds 
        /// </summary>
        /// <param name="fileText"></param>
        /// <param name="methode"></param>
        public void RepartirCalcul(string fileText, string methode)
        {
            ChunkFactory(fileText, methode);
        }

        public IEnumerable<Operation> ChunkFactoryToReduce(string fileText, string methode)
        {
            int startPos = 0;
            int blocksize = 10000000;
            var iterations = Math.Round((decimal)(fileText.Length / blocksize));
            for (int i = 0; i < iterations - 1; i++)
            {

                yield return new Operation() { IpNoeud = NOEUD, Chunck = fileText.Substring(startPos, blocksize), Methode = methode };
                startPos += blocksize;
            }
            yield return new Operation() { IpNoeud = NOEUD, Chunck = fileText.Substring(startPos, fileText.Length - startPos), Methode = methode };
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
            int posDernierNoeudDansListe = 0;
            var iterations = Math.Round((decimal)(fileText.Length / blocksize));
            for (int i = 0; i < iterations - 1; i++)
            {
                IPAddress adresseNoeud = SelectNoeud(posListeNoeud);

                com.Envoyer(adresseNoeud, new Operation() { IpNoeud = adresseNoeud.ToString(), Chunck = fileText.Substring(startPos, blocksize), Methode = methode });
                if (posListeNoeud == posDernierNoeudDansListe)
                    posListeNoeud = 0;
                else
                    posListeNoeud++;
                startPos += blocksize;
            }
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

        private IPAddress SelectNoeud(int i)
        {
            List<IPAddress> add = new List<IPAddress>() { IPAddress.Parse("192.168.0.21")};
            if (i > add.Count - 1)
                throw new Exception($"Aucun noeud trouvé a l'emplacement {i} de la liste");
            return add[i];
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


    }

    public class ResultatEventArgs
    {
        public Resultat Op;
        public ResultatEventArgs(Resultat r) { Op = r; }
    }
}

