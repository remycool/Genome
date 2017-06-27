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
            byte[] fileContent = GetFile();

            try {
                O.EnvoyerCalcul(new Calcul<byte[]> { Type = "CountChars", Param = fileContent });
            }
            catch(Exception ex)
            {
                string err = $"{ex.Message} \n{ex.StackTrace}";
                MessageBox.Show(err);
            }
            
        }

        private byte[] GetFile()
        {
            string emplacement = Assembly.GetExecutingAssembly().Location;
            string repertoire = Path.GetDirectoryName(emplacement);
            string cheminVersFichier = Path.Combine(repertoire, @"genome-kukushkin.txt");

            using (FileStream fs = new FileStream(cheminVersFichier, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs, Encoding.UTF8))
            {
                string fileContent = reader.ReadToEnd();
                return Encoding.UTF8.GetBytes(fileContent);
            }
        }
    }
}
