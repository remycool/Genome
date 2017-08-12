using FileManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DisplayIhm
{

    public class DisplayIhm
    {
     
        private long sizeFile;
        private string[] fileTransform = null;
        private  string filenameNew;
        private static StreamWriter writeInFile;
        public delegate void splitFileEvent();
        public event splitFileEvent OnFileSplit;
   

        public DisplayIhm(){
            Console.WriteLine("List Generated:");
            fileTransform = File.ReadLines(@"E:\Projet_Cesi\DNA\DNA-Data\test.txt").ToArray();
            sizeFile = new System.IO.FileInfo(@"E:\Projet_Cesi\DNA\DNA-Data\test.txt").Length; 
        }


        public string loadFile(TextBox tbFilePath)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult dialogResult = fileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                tbFilePath.Text = fileDialog.FileName;
                splitFile(File.ReadLines(tbFilePath.Text).ToArray());
                pickUpFile();
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
      //Cette méthode permet de divisier le fichien entré par l'utilisateur en plusieur partie
      public void splitFile(string[] fileTransform)
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

              // Console.WriteLine("Le fihcier à  terminé son processus");
                //OnFileSplit();
                MessageBox.Show("Découpage du fichier  Terminé");
            }
        }
    
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
            MessageBox.Show("Ok ");

        }

        public static void OnChanged(object source, FileSystemEventArgs e)
        {
            //Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            MessageBox.Show("File: " + e.FullPath + " " + e.ChangeType);

        }
        public void isEmpty(string path)
        {
            if (Directory.GetFileSystemEntries(path).Length == 0)
            {
                MessageBox.Show("File is not there");
            }
            else
            {
                MessageBox.Show("File exist");
            }
        }

    
    }


}
