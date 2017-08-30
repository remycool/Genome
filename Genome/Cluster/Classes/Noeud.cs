using Cluster.Classes;
using Cluster.Interfaces;
using Cluster.Protocole;
using Cluster.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Cluster.Events;
using System.Net.Sockets;

namespace Cluster.Classes
{
    public class Noeud
    {
        public const int PORT_ECOUTE = 9999;
        public const int PORT_ENVOIE = 8888;

        public IPAddress AdresseIP { get; set; }
        public IPAddress OrchestrateurIP { get; set; }
        public Communication<Resultat, Operation> Com { get; set; }
        public IBusinessFactory BusinessService { get; set; }
        public IDALFactory DALService { get; set; }

        #region EVENT
        public delegate void OperationHandler(object sender, OperationEventArgs resultatEventArgs);
        public event OperationHandler NouvelleOperation;

        public void SignalerNouvelleOperation(Operation o)
        {
           OperationEventArgs e = new OperationEventArgs(o);
            NouvelleOperation?.Invoke(this, e);
        }
        #endregion


        public Noeud(IBusinessFactory BuService, IDALFactory DalService)
        {
            AdresseIP = IpConfig.GetLocalIP();
            Com = new Communication<Resultat, Operation>(AdresseIP, PORT_ECOUTE, PORT_ENVOIE);

            Com.NouvelleReception += onNouvelleReception;
           
            BusinessService = BuService;
            DALService = DalService;
            Initialize();
        }

        public override string ToString()
        {
            return $"@ IP : {AdresseIP.ToString()}";
        }

        /// <summary>
        /// Dès la réception d'une nouvelle opération on éxécute le calcul demandé et on retourne une réponse 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onNouvelleReception(object sender, ReceptionEventArgs<Operation> e)
        {
            string compressedChunk = e.Op.Chunck;
            string decompressedChunk = compressedChunk.Decompress();
            e.Op.Chunck = decompressedChunk;
            Resultat res = (Resultat)ExecuterCalcul(e.Op);
            res.Id = e.Op.Id;

            Envoyer(res);
            SignalerNouvelleOperation(e.Op);
        }

        /// <summary>
        /// Permet d'executer la méthode du business invoqué par l'opération passée en parametre 
        /// </summary>
        /// <param name="calcul">Objet parametre qui contient la méthode à executer et le morceaux de fichier</param>
        private object ExecuterCalcul(Operation calcul)
        {
            //On utilise la réflexion pour obtenir la méthode depuis la factory
            Type type = typeof(IBusinessFactory);
            MethodInfo methode = type.GetMethod(calcul.Methode);
            Type typeRetourMethode = methode.ReturnType;
            string chaine = calcul.Chunck;

            //On éxécute la fonction
            return methode.Invoke(BusinessService, new object[] { chaine });
        }

        /// <summary>
        /// Met à jour l'état du le registre du cluster 
        /// </summary>
        public void Initialize()
        {

            //Mettre à jour info du noeud courant dans le registre
            DALService.UpdateNode(AdresseIP.ToString(), ClusterConstantes.ETAT_CONNECTED, ClusterConstantes.ROLE_NOEUD);
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
            if (Com.LocalListener != null)
                Com.LocalListener.Stop();

        }

        /// <summary>
        /// Permet d'envoyer une opération résultante à destination de l'orchestrateur
        /// </summary>
        /// <param name="operation"></param>
        /// <returns>Un objet Operation</returns>
        public void Envoyer(Resultat result)
        {
            result.IpNoeud = AdresseIP.ToString();
            //On envoie la réponse
            Com.Envoyer(OrchestrateurIP, result);
        }

    }
}


