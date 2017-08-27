
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;


namespace Genome
{

    public class DisplayData
    {
     
        private  string filenameNew;
        private string filetPathLog = @"E:\Projet_Cesi\DNA\logs\";
        private List<string> allSplitFile = null ;
        private static StreamWriter writeInFile;
        public delegate void splitFileEvent();
        
        public delegate void filePickUpEvent();
        public event splitFileEvent OnFileSplit;
   

        public DisplayData()
        {
            
        }


        //Cette méthode permet de charger un fichier
        public string loadFile(System.Windows.Forms.TextBox tbFilePath)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            DialogResult dialogResult = fileDialog.ShowDialog();
            try
            {
                if (dialogResult == DialogResult.OK)
                {
                    tbFilePath.Text = fileDialog.FileName;
                }
            }catch(Exception e)
            {
                File.AppendAllText(filetPathLog + "logPathFile.txt", "Erreur sur le chemin du fichier");
            }
           
            
            return tbFilePath.Text;
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
