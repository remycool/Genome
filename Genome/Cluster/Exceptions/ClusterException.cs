using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Cluster.Utils;

namespace Cluster.Exceptions
{
    public class ClusterException : Exception
    {
        public string ErreurOrigine { get; set; }
        public string MessageSpec { get; set; }

        public ClusterException(string message):base()
        {
            ErreurOrigine = $"{Message}\n";
            MessageSpec = message;
        }

        /// <summary>
        /// Notifie l'exception dans un fichier texte 
        /// </summary>
        public void Log(string message,string stackTrace)
        {
            string messageBuilder = $"\n{DateTime.Now} - {MessageSpec}\n {message}\n" + ErreurOrigine +  StackTrace;
            File.AppendAllText(ClusterConstantes.LOG_DIR + "logPathFile.txt", messageBuilder);
        }
    }
}
