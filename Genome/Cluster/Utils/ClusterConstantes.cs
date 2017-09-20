using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Utils
{
    public class ClusterConstantes
    {
        public const int ROLE_ORCHESTRATEUR = 2;
        public const int ROLE_NOEUD = 1;
        public const int ETAT_CONNECTED = 4;
        public const int ETAT_NOT_CONNECTED = 7;
        public const int PORT_ECOUTE_TCP = 9999;
        public const int PORT_ENVOIE_TCP = 8888;
        public const int PORT_ECOUTE_UDP = 9998;
        public const int PORT_ENVOIE_UDP = 8887;

    }
}
