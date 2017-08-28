using Cluster;
using Cluster.Utils;
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
        DisplayData meth;
        
        public Form1()
        {
            InitializeComponent();
            meth = new DisplayData();
        }



        /// <summary>
        /// Effectue le chargement du fichier, transforme le fichier initial et répartit dans plusieurs fichiers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBrowse_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult dialogResult = fileDialog.ShowDialog();
           
            try
            {
                if (dialogResult == DialogResult.OK)
                {
                    List<string> fichiersDecoupes = meth.tranformToArray(fileDialog.FileName);
                    meth.SplitFile(fichiersDecoupes);
                }
            }
            catch (Exception ex)
            {
                File.AppendAllText(ClusterConstantes.LOG_DIR + "logPathFile.txt", "Erreur sur le chemin du fichier");
            }
            panelButton.Visible = true;
        }

        private void buttonModule1_Click(object sender, EventArgs e)
        {
            this.panelModule1.Visible = true;
            this.panelNode.Visible = false;
            
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
