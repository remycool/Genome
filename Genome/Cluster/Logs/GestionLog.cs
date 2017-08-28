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
        private string _filetPathLog = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public GestionLog()
        {
             Initialiser();
             _filetPathLog = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public string Initialiser()
        {
            string pathLog = _filetPathLog + "/adnLogs/";
            try
            {
                if (Directory.Exists(pathLog))
                {
                    Console.WriteLine("Le dossier existe");
                }
                else
                {
                    DirectoryInfo dir = Directory.CreateDirectory(pathLog);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : Le chemin spécifié n'existe pas");
            }
            return pathLog;
        }
    }
}
