using DisplayIhm;
using FileManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsIhm
{
    public delegate void dgPointer(string[] transformFile);

    public partial class Form1 : Form
    {
        DisplayIhm.DisplayIhm meth;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            meth = new DisplayIhm.DisplayIhm();
            meth.loadFile(textBoxPathFile);
            buttonBrowse.Text = "Launch";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            meth = new DisplayIhm.DisplayIhm();
            meth.isEmpty(@"E:\Projet_Cesi\DNA\DNA-Data\SplitFile");
           /* meth.OnFileSplit += new DisplayIhm.DisplayIhm.splitFileEvent(a_FileSplit);
            string[] transformFile =  File.ReadLines(textBoxPathFile.Text).ToArray();
            dgPointer pAdder = new dgPointer(meth.splitFile);
            pAdder(transformFile);*/
           // FileManagement.geneomeFonction.arrayFromFile(textBoxPathFile.Text);

            // meth.pickUpFile();
        }

        static void a_FileSplit()
        {
           // Console.WriteLine("File created!");
            MessageBox.Show("File created!");
        }


    }
}
