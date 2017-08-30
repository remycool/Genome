using Cluster.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cluster.Logs
{
    class GestionLog
    {
        public static void Log(string message)
        {
            string messageBuilder = $"\n{DateTime.Now} {message}";
            string filetPathLog = ClusterConstantes.LOG_DIR;
            string pathLog = filetPathLog + "/adnLogs/";
            if (!Directory.Exists(pathLog))
            {
                DirectoryInfo dir = Directory.CreateDirectory(pathLog);
            }
            File.AppendAllText(pathLog + "logPathFile.txt", messageBuilder);
        }
    }
}
