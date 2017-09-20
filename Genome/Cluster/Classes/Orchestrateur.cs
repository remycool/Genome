using Cluster.Protocole;
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
using System.Collections.Concurrent;
using Cluster.Logs;

namespace Cluster.Classes
{
    public class Orchestrateur<T>
    {
        #region PROPRIETES
        public const int PORT_ECOUTE = 8888;
        public const int PORT_ENVOIE = 9999;
        public IPAddress AdresseIP { get; set; }
        public List<IPAddress> AdressesNoeuds { get; set; }
        public ConcurrentDictionary<IPAddress, Etat_noeud> Noeuds { get; set; }
        public Communication Com { get; set; }
        public List<string> Chuncks { get; set; }
        public Resultat<T> ResultatGlobal { get; set; }
        public Lazy<LazyLoad> Lazy { get; set; }
        public int NbResultatRecus { get; set; }
        public int NbOperationEnvoyes { get; set; }
        #endregion

        #region EVENT
        public delegate void ResultatHandler(object sender, ResultatEventArgs<T> resultatEventArgs);
        public delegate void NoeudConnecteHandler(object sender, NoeudConnecteEventArgs resultatEventArgs);
        public delegate void TraitementTermineHandler(object sender, TraitementTermineEventArgs resultatEventArgs);
        public event ResultatHandler NouveauResultat;
        public event NoeudConnecteHandler NouveauNoeud;
        public event TraitementTermineHandler TraitementTermine;
        public void SignalerNouveauResultat(Resultat<T> r)
        {
            ResultatEventArgs<T> e = new ResultatEventArgs<T>(r);
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
        public void onNouvelleReception(object sender, ReceptionEventArgs e)
        {
            Resultat<T> nouveauResultat = TraitementRequete(e.Noeud);


            //On cherche à savoir si c'est une reception indiquant une connexion
            //ou une deconnexion de noeud
            if (!nouveauResultat.HasValue)
            {
                IPAddress adresseNoeud = IPAddress.Parse(nouveauResultat.IpNoeud);
                MettreAJourNoeudsConnectes(adresseNoeud, nouveauResultat.Etat);
            }
            else
            {
                if (ResultatGlobal == null)
                    ResultatGlobal = nouveauResultat;
                else
                    ResultatGlobal += nouveauResultat;
                NbResultatRecus++;
                //SignalerNouveauResultat(nouveauResultat);
                if (NbResultatRecus == NbOperationEnvoyes)
                    SignalerTraitementTermine();
            }
        }

        /// <summary>
        /// Met à jour le dictionnaire des noeuds ou ajoute un noeud au dictionnaire et le signale à la vue
        /// </summary>
        /// <param name="ipNoeud"></param>
        /// <param name="etat"></param>
        public void MettreAJourNoeudsConnectes(IPAddress ipNoeud, Etat_noeud etat)
        {
            if (ipNoeud == null)
                throw new ClusterException("Impossible d'ajouter le noeud dans la liste");
            //Si c'est un nouveau noeud on l'ajoute au dictionnaire sinon on met à jour la liste
            Noeuds?.AddOrUpdate(ipNoeud, etat, (k, v) => v = etat);
            var noeuds = Noeuds.ToArray();
            List<IPAddress> noeudsConnectes = new List<IPAddress>();
            foreach(var item in noeuds)
            {
                if(item.Value == Etat_noeud.Connecte)
                {
                    noeudsConnectes.Add(item.Key);
                } 
            }
            SignalerNoeudConnecte(noeudsConnectes);

        }

        public Orchestrateur(Communication com)
        {
            AdressesNoeuds = new List<IPAddress>();
            Noeuds = new ConcurrentDictionary<IPAddress, Etat_noeud>();
            AdresseIP = IpConfig.GetLocalIP();
            Com = com; 
            Com.NouvelleReception += onNouvelleReception;
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
        //public void Envoyer(Operation op)
        //{
        //    Com.Envoyer(IPAddress.Parse(op.IpNoeud), op);
        //}

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
            int posDernierNoeudDansListe = AdressesNoeuds.Count - 1;
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
                Envoyer(adresseNoeud, new Operation() { Id = IdOperation, IpNoeud = adresseNoeud.ToString(), Chunck = compressedChunk, Methode = methode });
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
            //obtenir l'IP des noeuds connectés
            VerifierNoeudConnectes();
        }

        /// <summary>
        /// Vérifie l'état du cluster
        /// </summary>
        private void VerifierNoeudConnectes()
        {
            IPAddress IpNoeudDejeConnecte= Com.EnvoyerBroadcast();
            MettreAJourNoeudsConnectes(IpNoeudDejeConnecte, Etat_noeud.Connecte);
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

            if (Com.LocalTcpListener != null)
                Com.LocalTcpListener.Stop();
        }

        /// <summary>
        /// Sérialise un objet et le transmet à l'adresse passée en paramètre via TCP
        /// </summary>
        /// <param name="remote"></param>
        /// <param name="obj"></param>
        public void Envoyer(IPAddress remote, Operation op)
        {
            GestionLog.Log($"Envoie de l'opération {op.ToString()}");
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(remote, PORT_ENVOIE);
                TcpClient local = new TcpClient();
                local.Connect(remoteEP);
                byte[] ba = Utility<Operation>.Serialize(op);
                using (NetworkStream ns = local.GetStream())
                {
                    ns.Write(ba, 0, ba.Length);
                    ns.Close();
                };
                local.Close();
            }
            catch (Exception ex)
            {
                GestionLog.Log($"{ex.Message} \n {ex.StackTrace}");
            }

        }

        /// <summary>
        /// Lit le flux de donnée, désérialise l'objet transmit et signale sa présence
        /// </summary>
        /// <param name="remote"></param>
        public Resultat<T> TraitementRequete(TcpClient remote)
        {
            Resultat<T> res = new Resultat<T>();
            using (NetworkStream ns = remote.GetStream())
            {
                int i = 0;
                byte[] remoteData = new byte[1024];
                string data = string.Empty;
                //Lecture du flux
                if (ns.CanRead)
                {
                    while ((i = ns.Read(remoteData, 0, remoteData.Length)) != 0)
                    {
                        data += Encoding.UTF8.GetString(remoteData, 0, i);
                    }
                    res = Utility<Resultat<T>>.Deserialize(data);
                }
                ns.Close();
                remote.Close();
               
            }
            return res;
        }

    }


}

