using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Cluster
{

    public class DisplayData
    {
         
        private string filetPathLog = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private List<string> newList = new ArrayList<string>();
        public delegate void splitFileEvent();
        private string _verifFile;
        private StreamWriter _writeInFile;
        private string[] _Lines;
        public delegate void filePickUpEvent();
        public event splitFileEvent OnFileSplit;
        private static string _pathUser;

        public DisplayData()
        {
            _pathUser = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        //Cette méthode permet de charger un fichier
        public string loadFile(TextBox tbFilePath)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult dialogResult = fileDialog.ShowDialog();
            try
            {
                if (dialogResult == DialogResult.OK)
                {
                    tbFilePath.Text = fileDialog.FileName;
                    splitFile(tranformToArray(tbFilePath.Text));
                }
            }
            catch(Exception e)
            {
                File.AppendAllText(dirLog() + "logPathFile.txt", "Erreur sur le chemin du fichier");
            }                     
            return tbFilePath.Text;
        }

        //Cette méthode permet de vérifier si le fichier chargé est correcte et transform le fichier sous forme de tableau
        public List<string> tranformToArray(string file)
        {
            _verifFile = Path.GetExtension(file);
            try
            {
                if (_verifFile == ".txt")
                {
                    _Lines = File.ReadAllLines(file);
                    if (_Lines.First().Contains("\t"))
                    {
                        if (_Lines.First().Contains("genotype"))
                        {
                            _Lines = _Lines.Skip(1).ToArray();
                            int i = 0;
                            foreach (string line in _Lines)
                            {
                                //récupère les derniers caractère d'une ligne
                                string t = line.Substring(line.Length - 2, 2).Trim();
                                newList.Add(t);
                                Console.WriteLine($"{t}\t : " + i);
                                i++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Avertissement : Votre fichier n'est pas valide manque d'en-tête");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Avertissement : Votre fichier n'est pas valide - manque de tabulation");
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Le chemin vers le fichier est null");
            }
            return newList;
        }

        //Cette méthode permet de divisier le fichien entré par l'utilisateur en plusieurs parties et écris les fichiers dans un dossier spécifique
        public void splitFile(List<string> fileTransform)
        {
            int tour = 1;
            int chunksize = 15;
            var chunk = fileTransform.Take(chunksize);
            var removePrevious = fileTransform.Skip(chunksize);

            while (chunk.Take(1).Count() > 0)
            {                
                string filenameNew = dirCreate() + "adnPart_" + tour + ".txt";
                using (_writeInFile = new StreamWriter(filenameNew))
                    foreach (string element in chunk)
                    {
                        _writeInFile.WriteLine(element);
                    }
                chunk = removePrevious.Take(chunksize);
                removePrevious = removePrevious.Skip(chunksize);
                tour++;
            }
            if (_writeInFile.BaseStream == null)
            {
                if (OnFileSplit != null)
                {
                    OnFileSplit();
                }
            }

        }



        //Cette méthode permet de notifier si les fichier séparer ont été modifier
        public void pickUpFile()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = @"E:\Projet_Cesi\DNA\DNA-Data\SplitFile";
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            
            watcher.Filter = "*.txt";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;
        }

       

        //Cette méthode permet de vérifier si le dossier ou se trouve les fichiers séparer  est vide
        public void isEmpty(string path)
        {
            try
            {
                if (Directory.GetFileSystemEntries(path).Length == 0)
                {
                    if (OnFileSplit != null)
                    {
                        a_FileSplit();
                        Console.WriteLine("Folder is empty");
                    }
                }
                else
                {
                    if (OnFileSplit != null)
                    {
                        a_FileSplit();
                        Console.WriteLine("File exists");
                    }
                }
            }
            catch (Exception e)
            {
                File.AppendAllText(filetPathLog + "logFolder.txt", "Le dossier n'existe pas");
            }
            
        }

        public string dirCreate()
        {
            string pathFile = _pathUser + "/SplitFile/";
            try
            {
                if (Directory.Exists(pathFile))
                {
                    Console.WriteLine("Le dossier existe");
                }
                else
                {
                    DirectoryInfo dir = Directory.CreateDirectory(pathFile);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur : Le chemin spécifié n'existe pas");
            }
            
            return pathFile;
        }

        //Cette méthode permet de créer le dossier de logs
        public string dirLog()
        {
            string pathLog = _pathUser + "/adnLogs/";
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


        /*********************** Events  ******************************************/
        static void a_FileSplit()
        {
            // Console.WriteLine("File created!");
            Console.WriteLine("EventFile :  File has been created");     
        }

        public static void OnChanged(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);         
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }

       

    }

    

    internal class ArrayList<T> : List<string>
    {
    }
}
