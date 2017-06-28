using Cluster.Classes;
using Cluster.Protocole;
using Cluster.Utils;
using System.Collections.Generic;
using System.Net;

namespace Cluster
{
    public class Orchestrateur
    {
        public const int PORT = 8888;
        public const string NOEUD = "192.168.0.25";
        public IPAddress AdresseIP { get; set; }
        public List<IPAddress> AdressesNoeuds { get; set; }
        public Communication com { get; set; }

        public Orchestrateur()
        {
            AdressesNoeuds = new List<IPAddress>();
            AdresseIP = Utility.GetLocalIP();
            com = Communication.Instance;
        }

        public override string ToString()
        {
            return $"@ IP : {AdresseIP.ToString()}";
        }

        public Operation EnvoyerCalcul(Operation op)
        {
            com.Envoyer(IPAddress.Parse(NOEUD), op);
            return AttenteResultatCalcul();
        }

        public Operation AttenteResultatCalcul()
        {
            return com.Recevoir(AdresseIP);
        }
       
    }
}
