﻿using Cluster.Protocole;
using Cluster.Utils;
using System.Collections.Generic;
using System.Net;
using System;
using System.Linq;
using Cluster.Events;
using System.IO;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using Cluster.Exceptions;

namespace Cluster.Classes
{
    public class Orchestrateur
    {
        #region PROPRIETES
        public const int PORT_ECOUTE = 8888;
        public const int PORT_ENVOIE = 9999;
        public IPAddress AdresseIP { get; set; }
        public List<IPAddress> AdressesNoeuds { get; set; }
        public Communication<Operation, Resultat> com { get; set; }
        public List<string> Chuncks { get; set; }
        public IDALFactory DALService { get; set; }
        public Resultat Result { get; set; }
        public Lazy<LazyLoad> Lazy { get; set; }
        public int NbResultatRecus { get; set; }
        public int NbOperationEnvoyes { get; set; }
        #endregion

        #region EVENT
        public delegate void ResultatHandler(object sender, ResultatEventArgs resultatEventArgs);
        public delegate void NoeudConnecteHandler(object sender, NoeudConnecteEventArgs resultatEventArgs);
        public delegate void TraitementTermineHandler(object sender, TraitementTermineEventArgs resultatEventArgs);
        public event ResultatHandler NouveauResultat;
        public event NoeudConnecteHandler NouveauNoeud;
        public event TraitementTermineHandler TraitementTermine;
        public void SignalerNouveauResultat(Resultat r)
        {
            ResultatEventArgs e = new ResultatEventArgs(r);
            NouveauResultat?.Invoke(this, e);
        }
        public void SignalerNoeudConnecte(List<IPAddress> n)
        {
            NoeudConnecteEventArgs e = new NoeudConnecteEventArgs(n);
            NouveauNoeud?.Invoke(this, e);
        }
        public void SignalerTraitementTermine()
        {
            TraitementTermineEventArgs e = new TraitementTermineEventArgs();
            TraitementTermine?.Invoke(this, e);
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
            NbResultatRecus++;
            SignalerNouveauResultat(e.Op);
            if (NbResultatRecus == NbOperationEnvoyes)
                SignalerTraitementTermine();
        }

        public Orchestrateur(IDALFactory DalService)
        {
            AdressesNoeuds = new List<IPAddress>();
            AdresseIP = IpConfig.GetLocalIP();
            com = new Communication<Operation, Resultat>(AdresseIP, 8888, 9999);
            com.NouvelleReception += onNouvelleReception;
            DALService = DalService;
            Lazy = new Lazy<LazyLoad>();
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
        public void RepartirCalcul(string methode)
        {

            //Lazy Loading
            LazyLoad Loaded = Lazy.Value;

            if (Loaded != null)
            {
                foreach (string path in Loaded.Names)
                {
                    string fichier = GetFile(path);
                    ChunkFactory(fichier, methode);
                }
            }
        }

        /// <summary>
        /// Récupère un fichier 
        /// </summary>
        /// <returns></returns>
        private string GetFile(string cheminVersFichier)
        {
            string fileContent = string.Empty;
            using (FileStream fs = new FileStream(cheminVersFichier, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
            {
                fileContent = reader.ReadToEnd();
            }

            return fileContent;
        }

        //public IEnumerable<Operation> ChunkFactoryToReduce(string fileText, string methode)
        //{
        //    int startPos = 0;
        //    int blocksize = 10000000;
        //    var iterations = Math.Round((decimal)(fileText.Length / blocksize));
        //    for (int i = 0; i < iterations - 1; i++)
        //    {

        //        yield return new Operation() { IpNoeud = NOEUD, Chunck = fileText.Substring(startPos, blocksize), Methode = methode };
        //        startPos += blocksize;
        //    }
        //    yield return new Operation() { IpNoeud = NOEUD, Chunck = fileText.Substring(startPos, fileText.Length - startPos), Methode = methode };
        //}

        /// <summary>
        /// Méthode qui découpe la chaine de caractère passée en paramètre et envoye le morceau au noeud avec le nom de la méthode passée en paramètre
        /// </summary>
        /// <param name="fileText"></param>
        /// <param name="methode"></param>
        public void ChunkFactory(string fileText, string methode)
        {
            int startPos = 0;
            int tailleChunk = 100000;
            int posListeNoeud = 0;
            int posDernierNoeudDansListe = AdressesNoeuds.Count-1;
            int tailleFichier = fileText.Length;
            int totalTailleFichierEnvoyee = 0;
            bool decoupageTermine = false;
            string chunk = string.Empty;
            int IdOperation = 1;
            NbResultatRecus = 1;


            while (!decoupageTermine)
            {
                int tailleRestanteFichier = tailleFichier - totalTailleFichierEnvoyee;
                //On vérifie si la taille du morceau est trop grande auquel cas le moceau
                //aura la taille de la taille restante et on précise que c'est fini
                if (tailleChunk > tailleRestanteFichier)
                {
                    tailleChunk = tailleFichier - totalTailleFichierEnvoyee;
                    decoupageTermine = true;
                }

                chunk = fileText.Substring(startPos, tailleChunk);
                totalTailleFichierEnvoyee += chunk.Length;
                //On compresse les données avant envoie au noeud
                string compressedChunk = chunk.Compress();
                //On réupère l'adresse du noeud auquel envoyer l'opération 
                IPAddress adresseNoeud = SelectNoeud(posListeNoeud);
                //On envoie l'opération au noeud
                com.Envoyer(adresseNoeud, new Operation() { Id = IdOperation, IpNoeud = adresseNoeud.ToString(), Chunck = compressedChunk, Methode = methode });
                //On précise ici parmi la liste des noeuds quel sera le prochain à recevoir une opération
                if (posListeNoeud == posDernierNoeudDansListe)
                    posListeNoeud = 0;
                else
                    posListeNoeud++;

                startPos += tailleChunk;
                IdOperation++;

            }
            NbOperationEnvoyes = IdOperation;
        }

        /// <summary>
        /// Met à jour les informations dans le registre Cluster et récupère
        /// les adresses IP de tous les noeuds connectés
        /// </summary>
        public void Initialize()
        {
            //Mettre à jour info du noeud courant dans le registre
            DALService.UpdateNode(AdresseIP.ToString(), ClusterConstantes.ETAT_CONNECTED, ClusterConstantes.ROLE_ORCHESTRATEUR);
            //obtenir l'IP des noeuds connectés
            VerifierNoeudConnectes();
        }

        /// <summary>
        /// Vérifie l'état du cluster
        /// </summary>
        private void VerifierNoeudConnectes()
        {
            AdressesNoeuds = DALService.GetAllNodeIPs();
            SignalerNoeudConnecte(AdressesNoeuds);
        }

        /// <summary>
        /// Sélectionne un noeud parmi une liste
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private IPAddress SelectNoeud(int i)
        {
            //TEST
            //List<IPAddress> add = new List<IPAddress>() { IPAddress.Parse("192.168.0.25"), IPAddress.Parse("192.168.0.21"), IPAddress.Parse("192.168.0.23") };
            //List<IPAddress> add = new List<IPAddress>() { IPAddress.Parse("10.131.128.74"), IPAddress.Parse("10.131.128.90") };
            //
            if (i > AdressesNoeuds.Count - 1)
                throw new Exception($"Aucun noeud trouvé a l'emplacement {i} de la liste");
            return AdressesNoeuds[i];
        }

        /// <summary>
        /// Libère les ressources
        /// </summary>
        public void Dispose()
        {

            if (com.LocalListener != null)
                com.LocalListener.Stop();
            //Mettre à jour info du noeud courant dans le registre
            DALService.UpdateNode(AdresseIP.ToString(), ClusterConstantes.ETAT_NOT_CONNECTED, ClusterConstantes.ROLE_ORCHESTRATEUR);
            DALService.Dispose();
        }


    }


}

