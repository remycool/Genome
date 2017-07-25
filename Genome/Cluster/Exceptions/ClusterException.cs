using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Exceptions
{
    public class ClusterException : Exception
    {
        public string Erreur { get; set; }
        public ClusterException():base()
        {
            Erreur = $"Une erreur a eu lieu dans le composant Cluster : {Message}";
        }

        public void Log()
        {
            EventLog clusterLog = new EventLog();
            if (!EventLog.SourceExists("Cluster"))
                EventLog.CreateEventSource("Cluster", "Application");

            clusterLog.Source = "Cluster";
            clusterLog.WriteEntry(Erreur);
        }
              
    }
}
