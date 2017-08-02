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

       // private List<string> lines = null;

        public DisplayIhm(){
            //textToArray = System.IO.File.ReadAllLines("@"+ loadFile(filePath));
            Console.WriteLine("List Generated:");
           // lines = File.ReadLines(@"E:\Projet_Cesi\DNA\DNA-Data\test.txt").ToList();

            arrFromFile = "ok ";







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



        public string hyi
        {
            get
            {
               
                return arrFromFile;
            }
            
        }
    }


}
