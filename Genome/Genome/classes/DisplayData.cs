
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;


namespace DisplayIhm
{

    public class DisplayIhm
    {
     
        private long sizeFile;
        private string[] fileTransform = null;
        private  string filenameNew;
        private List<string> allSplitFile ;
        private static StreamWriter writeInFile;
        public delegate void splitFileEvent();
        private static string targetDirectory = @"E:\Projet_Cesi\DNA\DNA-Data\SplitFile";
        public delegate void filePickUpEvent();
        public event splitFileEvent OnFileSplit;
   

        public DisplayIhm(){
            Console.WriteLine("List Generated:");
            fileTransform = File.ReadLines(@"E:\Projet_Cesi\DNA\DNA-Data\test.txt").ToArray();
            sizeFile = new System.IO.FileInfo(@"E:\Projet_Cesi\DNA\DNA-Data\test.txt").Length;

        }

        //Cette méthode permet de charger un fichier
        public string loadFile(System.Windows.Forms.TextBox tbFilePath)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            DialogResult dialogResult = fileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                tbFilePath.Text = fileDialog.FileName;
                splitFile(File.ReadLines(tbFilePath.Text).ToList());            
            }
            
            return tbFilePath.Text;
        }


        public long meth
        {
            get
            {

                return sizeFile;
            }

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
                filenameNew = @"E:\Projet_Cesi\DNA\DNA-Data\SplitFile\" + "adnPart_"+ tour + ".txt";
                using (writeInFile = new StreamWriter(filenameNew))
                    foreach (string element in chunk)
                    {
                        writeInFile.WriteLine(element);
                    }
                chunk = removePrevious.Take(chunksize);
                removePrevious = removePrevious.Skip(chunksize);
                tour++;
            }
            if (writeInFile.BaseStream == null)
            {
                if(OnFileSplit != null)
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
            if (Directory.GetFileSystemEntries(path).Length == 0)
            {
                if (OnFileSplit != null)
                {
                    a_FileSplit();
                    MessageBox.Show("Folder is empty");
                }
               
            }
            else
            {
                if (OnFileSplit != null)
                {
                    a_FileSplit();
                    MessageBox.Show("File exists");
                }
               
            }
        }

        //test
        public List<string>  listFileFolder_Node()
        {
            allSplitFile = new ArrayList<string>();
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string tmp in fileEntries)
            {
                string fileName = Path.GetFileName(tmp);
                allSplitFile.Add(tmp);
                MessageBox.Show(fileName);
            }
            return allSplitFile;
        }



        /*********************** Events  ******************************************/
        static void a_FileSplit()
        {
            // Console.WriteLine("File created!");
            
            MessageBox.Show("EventFile :  File has been created");
         
        }

        public static void OnChanged(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);         
            MessageBox.Show("File: " + e.FullPath + " " + e.ChangeType);
        }

    }

    internal class ArrayList<T> : List<string>
    {
    }
}
