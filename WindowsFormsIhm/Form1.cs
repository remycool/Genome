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
            meth.OnFileSplit += a_FileSplit;
            meth.loadFile(textBoxPathFile);
            this.buttonLaunch.Visible = true;
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panelModule1.Visible = true;
            buttonModule1.Visible = true;
            meth = new DisplayIhm.DisplayIhm();
            meth.OnFileSplit += a_FileSplit;
            meth.isEmpty(@"E:\Projet_Cesi\DNA\DNA-Data\SplitFile");
            
           /* meth.OnFileSplit += new DisplayIhm.DisplayIhm.splitFileEvent(a_FileSplit);
            string[] transformFile =  File.ReadLines(textBoxPathFile.Text).ToArray();
            dgPointer pAdder = new dgPointer(meth.splitFile);
            pAdder(transformFile);*/
           //FileManagement.geneomeFonction.arrayFromFile(textBoxPathFile.Text);

            // meth.pickUpFile();
        }

        static void a_FileSplit()
        {
           // Console.WriteLine("File created!");
            MessageBox.Show("EventFile :  File has been created");
        }


        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            if(textBoxPathFile.Text == "")
            {
                MessageBox.Show("Select File");
            }
            else
            {
                if (listBoxNode.SelectedIndex == -1)
                {
                    
                     MessageBox.Show("Select Node !");
                }
                else
                {
                    
                    string curItem = this.listBoxNode.SelectedItem.ToString();
                    //  MessageBox.Show(curItem);
                    labelNameCurrentNode.Text = curItem;
                    this.panelStatNode.Visible = true;
                    this.buttonModule1.Visible = true;
                }
                
            }
           
        }

        private void buttonModule1_Click(object sender, EventArgs e)
        {
            this.panelModule1.Visible = true;
            this.panelNode.Visible = false;
            this.buttonNodePanel.Visible = true;
        }

        private void buttonNodePanel_Click(object sender, EventArgs e)
        {
            this.panelNode.Visible = true;
            this.panelModule1.Visible = false;
        }
    }
}
