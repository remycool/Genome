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
        private string arrFromFile;
        private long sizeFile;
        private string[] lines = null;

        

        public DisplayIhm(){
            //textToArray = System.IO.File.ReadAllLines("@"+ loadFile(filePath));
            Console.WriteLine("List Generated:");
            lines = File.ReadLines(@"E:\Projet_Cesi\DNA\DNA-Data\test.txt").ToArray();
            /*sizeFile = new System.IO.FileInfo(@"E:\Projet_Cesi\DNA\DNA-Data\test.txt").Length;
            arrFromFile = "ok "; */
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



        public string[] hyi
        {
            get
            {
               
                return lines;
            }
            
        }

        public long meth
        {
            get
            {

                return sizeFile;
            }

        }

      public void regroup()
        {
          


           /// List<string> ss = File.ReadLines(@"E:\Projet_Cesi\DNA\DNA-Data\test.txt").ToList();
          //  int cycle = 1;
            int chunksize = 10;

            var chunk = lines.Take(chunksize);
            var rem = lines.Skip(chunksize);
            foreach (string s in lines)
            {
                Console.WriteLine(s);
                Console.ReadKey();
            }
          //  Console.WriteLine("c");
            /*while (chunk.Take(3).Count() <10)
            {
                 string filename = @"E:\Projet_Cesi\DNA\DNA-Data\test.txt";
                 using (StreamWriter sw = new StreamWriter(filename))
                 {
                     foreach (string line in chunk)
                     {
                        // sw.WriteLine(line);                    
                         Console.WriteLine(line);
                     }
                 }
                 chunk = rem.Take(chunksize);
                 rem = rem.Skip(chunksize);
                 cycle++;
             }*/
        }

       
    }


}
