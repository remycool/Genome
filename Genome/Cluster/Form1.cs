using Cluster.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cluster
{
    public partial class Form1 : Form
    {
        public Orchestrateur O { get; set; }
        public Noeud N { get; set; }

        public Form1()
        {
            InitializeComponent();
            Calcul_Btn.Enabled = false;
            O = new Orchestrateur();
            N = new Noeud();
        }

        private void Orchestrateur_Btn_Click(object sender, EventArgs e)
        {
            //Logique d'interface
            Noeud_Btn.Enabled = false;
            Calcul_Btn.Enabled = true;
            Orchestrateur_Btn.BackColor = Color.DarkGray;

            AdresseIP_Lbl.Text = O.ToString();
        }

        private void Noeud_Btn_Click(object sender, EventArgs e)
        {
            Orchestrateur_Btn.Enabled = false;
            Noeud_Btn.BackColor = Color.DarkGray;
            AdresseIP_Lbl.Text = N.ToString();
            try
            {
                N.AttenteCalcul();
            }
            catch(Exception ex)
            {
                string err = $"{ex.Message} \n{ex.StackTrace}";
                MessageBox.Show(err);
            }
        }

        private void ToggleBackColor(Button btn)
        {
            if (btn.BackColor == Color.DarkGray)
                btn.BackColor = default(Color);
            else
                btn.BackColor = Color.DarkGray;
        }

        private void LancerCalcul_Btn_Click(object sender, EventArgs e)
        {
           string fileContent = GetFile();

            try {
                Operation retour = O.EnvoyerCalcul(new Operation { Type = "CountChars", Param = fileContent });
                Resultat_Lbl.Text = retour.ToString();
            }
            catch(Exception ex)
            {
                string err = $"{ex.Message} \n{ex.StackTrace}";
                MessageBox.Show(err);
            }
            
        }

        private string GetFile()
        {
            string emplacement = Assembly.GetExecutingAssembly().Location;
            string repertoire = Path.GetDirectoryName(emplacement);
            string cheminVersFichier = Path.Combine(repertoire, @"genome-kukushkin.txt");
            string fileContent = string.Empty;

            try
            {
                using (FileStream fs = new FileStream(cheminVersFichier, FileMode.Open, FileAccess.Read))
                using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
                {
                    fileContent = reader.ReadToEnd();
                }
            }
            catch(Exception ex)
            {
                string err = $"{ex.Message} \n{ex.StackTrace}";
                MessageBox.Show(err);
            }

            return fileContent;
        }
    }
}
