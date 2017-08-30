using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using Cluster.Exceptions;

namespace Cluster
{

    public class DisplayData
    {

        private List<string> newList = new List<string>();
        public delegate void splitFileEvent();
        private string _verifFile;
        private StreamWriter _writeInFile;

        public delegate void filePickUpEvent();
        public event splitFileEvent OnFileSplit;
        private static string _pathSplitFile;
        private static string _pathUser;

        public DisplayData()
        {
            _pathUser = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _pathSplitFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/SplitFile/";
            DirCreate();
        }

        /// <summary>
        /// Cette méthode permet de charger un fichier
        /// </summary>
        /// <param name="tbFilePath"></param>
        /// <returns></returns>
        public string loadFile(string tbFilePath)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult dialogResult = fileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                SplitFile(tranformToArray(tbFilePath));
            }

            return tbFilePath;
        }

        /// <summary>
        /// Cette méthode permet de vérifier si le fichier chargé est correct, 
        /// enlève toutes les informations inutiles et garde les données exploitables
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public List<string> tranformToArray(string file)
        {
            IEnumerable<string> lines;
            VerifierFichier(file);
            lines = File.ReadLines(file).Skip(1);
            Parallel.ForEach(lines, (line) =>
            {
                //récupère les derniers caractère d'une ligne
                string t = line.Substring(line.Length - 2, 2).Trim();
                newList.Add(t);

            });
            return newList;
        }

        /// <summary>
        /// Effectue des vérifications sur le fichier
        /// </summary>
        /// <param name="fichier"></param>
        /// <returns></returns>
        private void VerifierFichier(string fichier)
        {
            _verifFile = Path.GetExtension(fichier);
            string premiereLigne = File.ReadLines(fichier).First();
            //Test de l'extension
            if (_verifFile != ".txt")
            {
                throw new ClusterException("Le fichier sélectionné n'est pas un fichier .txt");
            }
            if (!premiereLigne.Contains("\t") || !premiereLigne.Contains("genotype"))
            {
                throw new ClusterException("Le fichier sélectionné n'est pas au bon format");
            }
        }

        /// <summary>
        /// Cette méthode permet de divisier le fichien entré par l'utilisateur en plusieurs parties et écris les fichiers dans un dossier spécifique
        /// </summary>
        /// <param name="fileTransform"></param>
        public void SplitFile(List<string> fileTransform)
        {
            int tour = 1;
            int chunksize = 1000000;
            var chunk = fileTransform.Take(chunksize);
            var removePrevious = fileTransform.Skip(chunksize);

            while (chunk.Take(1).Count() > 0)
            {

                string filenameNew = $"{_pathSplitFile}adnPart_" + tour + ".txt";
                using (_writeInFile = new StreamWriter(filenameNew))
                {
                    foreach (string c in chunk)
                    {
                        _writeInFile.WriteLine(c);
                    }
                }

                chunk = removePrevious.Take(chunksize);
                removePrevious = removePrevious.Skip(chunksize);
                tour++;
                if (_writeInFile.BaseStream == null)
                {
                    OnFileSplit?.Invoke();
                }
            }

        }

        /// <summary>
        /// Créé le répertoire qui accueillera les fichiers séparés
        /// </summary>
        /// <returns></returns>
        private void DirCreate()
        {
            if (!Directory.Exists(_pathSplitFile))
            {
                DirectoryInfo dir = Directory.CreateDirectory(_pathSplitFile);
            }
        }

        /// <summary>
        /// Cette méthode permet de créer le dossier de logs
        /// </summary>
        /// <returns></returns>
        public string dirLog()
        {
            string pathLog = _pathUser + "/adnLogs/";

            if (!Directory.Exists(pathLog))
            {
                DirectoryInfo dir = Directory.CreateDirectory(pathLog);
            }

            return pathLog;
        }


    }
}
