﻿using Cluster.Classes;
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
        public const int PORT = 8888;
        public const string ORCHESTRATEUR = "192.168.0.21";

        public IPAddress AdresseIP { get; set; }
        public IPAddress OrchestrateurIP { get; set; }
        public Communication<Resultat, Operation> Com { get; set; }
        public IBusinessFactory BusinessService { get; set; }
        public IDALFactory DALService { get; set; }

        public Noeud(IBusinessFactory BuService, IDALFactory DalService)
        {
            AdresseIP = IpConfig.GetLocalIP();
            Com = new Communication<Resultat, Operation>(AdresseIP, 9999, 8888);
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
            Resultat res = (Resultat)ExecuterCalcul(e.Op);
            Envoyer(res);
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
            if (Com.LocalListener != null)
                Com.LocalListener.Stop();
            Console.WriteLine(DALService.GetClusterView());

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
            Com.Envoyer(IPAddress.Parse("192.168.0.25"), result);
        }
       
    }
}


