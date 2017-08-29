using System.Collections.Generic;
using System.Net;

namespace Cluster.Events
{
    public class NoeudConnecteEventArgs
    {
        public List<IPAddress> Noeuds;
        public NoeudConnecteEventArgs(List<IPAddress> n) { Noeuds = n; }
    }
}
