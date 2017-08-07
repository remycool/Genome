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
        /* private TextBox filePath= null;*/
        private long sizeFile;
        private string[] fileTransform = null;
        private string filenameNew;
        private StreamWriter writeInFile;
        public delegate void splitFileEvent();
        public event splitFileEvent OnFileReached;

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

      public void splitFile()
        {
          
           int tour = 1;
           int chunksize = 15;
           var chunk = fileTransform.Take(chunksize);
           var removePrevious = fileTransform.Skip(chunksize);

            while (chunk.Take(1).Count() > 0)
            {
                filenameNew = @"E:\Projet_Cesi\DNA\DNA-Data\"+"adnPart_"+ tour + ".txt";
                using (writeInFile = new StreamWriter(filenameNew))
                    foreach (string element in chunk)
                    {
                        writeInFile.WriteLine(element);  
                        
                    }
                chunk = removePrevious.Take(chunksize);
                removePrevious = removePrevious.Skip(chunksize);
                tour++;

                if (writeInFile.BaseStream == null)
                {
                    Console.WriteLine("Le fihcier à  terminé son processus");
                }
            }
         
        }

       
    }


}
