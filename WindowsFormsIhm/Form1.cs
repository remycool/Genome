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
        private string filetPathLog = @"E:\Projet_Cesi\DNA\logs\";
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
          
            this.panelButton.Visible = true;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            this.panelModule1.Visible = true;
            Lazy<Genome.LazyLoad> lazy = new Lazy<Genome.LazyLoad>();

            //Vérifie si la varibl lazy contient des donées
            Console.WriteLine("Data Loaded : " + lazy.IsValueCreated);
            Genome.LazyLoad lazyLoadingValue = lazy.Value;
            try
            {
                if (lazyLoadingValue != null)
                {
                    /*foreach (string tmp in lazyLoadingValue.Names)
                    {
                        MessageBox.Show("Data présente : " + tmp);                       
                    }*/
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(filetPathLog + "logCsharp.txt", "Erreur sur la veleur du lazy");
            }
                   
        }




        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
                    
        }
     

        private void buttonModule1_Click(object sender, EventArgs e)
        {
            this.panelModule1.Visible = true;
            this.panelNode.Visible = false;
            
        }

        private void buttonNodePanel_Click(object sender, EventArgs e)
        {
            this.panelNode.Visible = true;
            this.panelModule1.Visible = false;
        }

        private void buttonPaireDeBase_Click(object sender, EventArgs e)
        {
            this.panelAffichResult.Visible = true;
            this.labelButtonClick.Text = "Nombre total de paires de bases";
        }

        private void buttonOccurence_Click(object sender, EventArgs e)
        {
            this.panelAffichResult.Visible = true;
            this.labelButtonClick.Text = "Nombre d’occurrence des bases A, T, G ou C dans le génome et pourcentage relatif au total ";
        }

        private void buttonBaseInconnue_Click(object sender, EventArgs e)
        {
            this.panelAffichResult.Visible = true;
            this.labelButtonClick.Text = " Nombre de bases inconnues(le tiret)";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.panelAffichResult.Visible = true;
            this.labelButtonClick.Text = " Nombre d’occurrence de la séquence de 4 bases la plus fréquente ";
        }
    }
}
