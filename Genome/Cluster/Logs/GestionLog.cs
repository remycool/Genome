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
        private string _filetPathLog;

        public GestionLog()
        {
             Initialiser();
             _filetPathLog = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public string Initialiser()
        {
            string pathLog = _filetPathLog + "/adnLogs/";
                if (!Directory.Exists(pathLog))
                {
                    DirectoryInfo dir = Directory.CreateDirectory(pathLog);
                }
            return pathLog;
        }
    }
}
