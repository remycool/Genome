using Cluster.Protocole;
using Cluster.Utils;
using System.Collections.Generic;
using System.Net;
using System;
using System.Linq;
using Cluster.Events;
using System.IO;
using System.Net.Sockets;

namespace Cluster.Classes
{
    public class Orchestrateur
    {
        #region PROPRIETES
        public const string NOEUD = "";
        public IPAddress AdresseIP { get; set; }
        public List<IPAddress> AdressesNoeuds { get; set; }
        public Communication<Operation, Resultat> com { get; set; }
        public List<string> Chuncks { get; set; }
        public IDALFactory DALService { get; set; }
        //public MapReduce<Operation, Operation> MapRed { get; set; }
        public Resultat Result { get; set; }
        //public Lazy<LazyLoading> Lazy { get; set; }

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
            
            Console.WriteLine($"Retour operation {e.Op.Id} Résultat reçu de {e.Op.Valeur} reçu de {e.Op.IpNoeud} Total = {Result.Valeur}");
            SignalerNouveauResultat(e.Op);
        }

        public Orchestrateur(IDALFactory DalService)
        {
            AdressesNoeuds = new List<IPAddress>();
            AdresseIP = IpConfig.GetLocalIP();
            com = new Communication<Operation, Resultat>(AdresseIP, 8888, 9999);
            com.NouvelleReception += onNouvelleReception;
            DALService = DalService;
            //Lazy = new Lazy<LazyLoading>();
            Initialize();

            //string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //string finalPath = pathUser + "/SplitFile"; 
            //if (!Directory.Exists(finalPath))
            //{
            //    DirectoryInfo di = Directory.CreateDirectory(finalPath);
            //}

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
            ////Lazy Loading
            //LazyLoad Loaded = Lazy.Value;
            //try
            //{
            //    if (Loaded != null)
            //    {
            //        foreach (string path in Loaded.Names)
            //        {
            //            string fichier = GetFile(path);
            //            ChunkFactory(fileText, methode);
            //        }
            //    }
            //}


            //TEST
            int expectedCount = fileText.Count(c => c == 'C');
            Console.WriteLine($"Résultat attendu = {expectedCount}");
            //
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
            int tailleChunk = 100000;
            int posListeNoeud = 0;
            int posDernierNoeudDansListe = 1;
            int tailleFichier = fileText.Length;
            int totalTailleFichierEnvoyee = 0;
            bool decoupageTermine = false;
            string chunk = string.Empty;
            int IdOperation = 1;

            while (!decoupageTermine)
            {
                int tailleRestanteFichier = tailleFichier - totalTailleFichierEnvoyee;
                //On vérifie si la taille du morceau est trop grande auquel cas le moceau
                //aura la taille de la taille restante et on précise que c'est fini
                if ( tailleChunk > tailleRestanteFichier)
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
                com.Envoyer(adresseNoeud, new Operation() { Id = IdOperation , IpNoeud = adresseNoeud.ToString(), Chunck = compressedChunk, Methode = methode });
                //On précise ici parmi la liste des noeuds quel sera le prochain à recevoir une opération
                if (posListeNoeud == posDernierNoeudDansListe)
                    posListeNoeud = 0;
                else
                    posListeNoeud++;

                startPos += tailleChunk;
                //TEST
                Console.WriteLine($" Operation : {IdOperation} envoyée");
                //
                IdOperation++;
                
            }
           //TEST
            Console.WriteLine($"Tous les morceaux de fichier ont été envoyés total envoyé : {totalTailleFichierEnvoyee} Octets taille du dernier fichier {tailleChunk}");
           //
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

        /// <summary>
        /// Méthode qui va chercher le fichier et le charger en mémoire
        /// </summary>
        /// <returns></returns>
        //private string GetFile(string cheminVersFichier)
        //{
        //    string emplacement = Assembly.GetExecutingAssembly().Location;
        //    string repertoire = Path.GetDirectoryName(emplacement);
        //    string cheminVersFichier = Path.Combine(repertoire, @"genome-kukushkin.txt");
        //    string fileContent = string.Empty;

        //    try
        //    {
        //        using (FileStream fs = new FileStream(cheminVersFichier, FileMode.Open, FileAccess.Read))
        //        using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
        //        {
        //            fileContent = reader.ReadToEnd();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string err = $"{ex.Message} \n{ex.StackTrace}";
        //        MessageBox.Show(err);
        //    }
        //    return fileContent;
        //}

        
        private IPAddress SelectNoeud(int i)
        {
            //TEST
            //List<IPAddress> add = new List<IPAddress>() { IPAddress.Parse("192.168.0.25"), IPAddress.Parse("192.168.0.21"), IPAddress.Parse("192.168.0.23") };
            List<IPAddress> add = new List<IPAddress>() { IPAddress.Parse("10.131.128.74"), IPAddress.Parse("10.131.128.90") };
            //
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

   
}

