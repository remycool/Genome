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
using Cluster.Logs;
using System.Text;

namespace Cluster.Classes
{
    public class Noeud
    {
        public const int PORT_ECOUTE = 9999;
        public const int PORT_ENVOIE = 8888;
        public IPAddress AdresseIP { get; set; }
        public IPAddress OrchestrateurIP { get; set; }
        public Communication Com { get; set; }
        public IBusinessFactory BusinessService { get; set; }
       

        #region EVENT
        public delegate void OperationHandler(object sender, OperationEventArgs resultatEventArgs);
        public event OperationHandler NouvelleOperation;

        public void SignalerNouvelleOperation(Operation o)
        {
            OperationEventArgs e = new OperationEventArgs(o);
            NouvelleOperation?.Invoke(this, e);
        }
        #endregion


        public Noeud(IBusinessFactory BuService)
        {
            AdresseIP = IpConfig.GetLocalIP();
            Com = new Communication(AdresseIP,
                                                        ClusterConstantes.PORT_ECOUTE_TCP, 
                                                        ClusterConstantes.PORT_ENVOIE_TCP,
                                                        ClusterConstantes.PORT_ECOUTE_UDP,
                                                        ClusterConstantes.PORT_ENVOIE_UDP);

            Com.NouvelleReception += onNouvelleReception;

            BusinessService = BuService;
 
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
        public void onNouvelleReception(object sender, ReceptionEventArgs e)
        {
            Operation op = TraitementRequete(e.Noeud);

            if (op.Methode == string.Empty)
                SignalerConnexion();
            else
            {
                string compressedChunk = op.Chunck;
                string decompressedChunk = compressedChunk.Decompress();
                op.Chunck = decompressedChunk;
                IResultat res = (IResultat)ExecuterCalcul(op);
                res.Id = op.Id;
                res.HasValue = true;
                Envoyer(OrchestrateurIP, res);
                //On signale à la vue l'operation réceptionnée par le noeud
                SignalerNouvelleOperation(op);
            }
            
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
            //DALService.UpdateNode(AdresseIP.ToString(), ClusterConstantes.ETAT_CONNECTED, ClusterConstantes.ROLE_NOEUD);
            //obtenir l'IP de l'orchestrateur
            //string ip = DALService.GetOrchestrateurIP();

            //if (!string.IsNullOrEmpty(ip))
            OrchestrateurIP = IPAddress.Parse("10.131.128.74");
            SignalerConnexion();
            Com.RecevoirBroadcastAsync();
        }

        /// <summary>
        /// Permet de mettre à jour le registre du cluster
        /// </summary>
        public void Dispose()
        {
            //Mettre à jour info du noeud courant dans le registre
            // DALService.UpdateNode(AdresseIP.ToString(), ClusterConstantes.ETAT_NOT_CONNECTED, ClusterConstantes.ROLE_NOEUD);
            SignalerDeconnexion();
            if (Com.LocalTcpListener != null)
                Com.LocalTcpListener.Stop();
            Com.LocalUdpListener?.Close();
        }

        private void SignalerDeconnexion()
        {
            Resultat<string> deconnexion = new Resultat<string>() { IpNoeud = AdresseIP.ToString(), Etat = Etat_noeud.Deconnecte, HasValue = false };
            Envoyer(OrchestrateurIP, deconnexion);
        }

        private void SignalerConnexion()
        {
            Resultat<string> connexion = new Resultat<string>() { IpNoeud = AdresseIP.ToString(), Etat = Etat_noeud.Connecte, HasValue = false };
            Envoyer(OrchestrateurIP, connexion);
        }

        /// <summary>
        /// Permet d'envoyer une opération résultante à destination de l'orchestrateur
        /// </summary>
        /// <param name="operation"></param>
        /// <returns>Un objet Operation</returns>
        public void Envoyer(IPAddress remote,IResultat result)
        {
            GestionLog.Log($"Envoie du résultat {result.ToString()}");
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(remote, PORT_ENVOIE);
                TcpClient local = new TcpClient();
                local.Connect(remoteEP);
                byte[] ba = Utility<IResultat>.Serialize(result);
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

        public Operation TraitementRequete(TcpClient remote)
        {
            Operation op = new Operation();
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
                    op = Utility<Operation>.Deserialize(data);
                }
                ns.Close();
                remote.Close();

            }
            return op;
        }


    }
}


