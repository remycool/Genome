using Cluster.Classes;
using Cluster.Interfaces;
using Cluster.Utils;
using Cluster.Events;
using Cluster.Exceptions;
using Genome.GenomeBusiness;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Cluster_DAL;

namespace Cluster_UI
{
    public partial class Form1 : Form
    {
        public Orchestrateur O { get; set; }
        public Noeud N { get; set; }
        //public INoeud N { get; set; }
        IBusinessFactory ServiceBusiness { get; set; }
        IDALFactory ServiceDAL { get; set; }

        public Form1()
        {
            ServiceBusiness = new BusinessFactory(new GenomeBusiness());
            ServiceDAL = new DALFactory(new ClusterDAL());
            InitializeComponent();
            Calcul1_Btn.Enabled = false;
            Calcul2_Btn.Enabled = false;
        }

        private void Orchestrateur_Btn_Click(object sender, EventArgs e)
        {
            O = new Orchestrateur(ServiceDAL);
            O.NouveauResultat += onResultatChanged;
            Noeud_Btn.Enabled = false;
            Calcul1_Btn.Enabled = true;
            Calcul2_Btn.Enabled = true;
            Orchestrateur_Btn.BackColor = Color.DarkGray;
            AdresseIP_Lbl.Text = O.ToString();
        }

        private void Noeud_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                string file = GetFile();
                Console.WriteLine(file.Length);
                N = new Noeud(ServiceBusiness, ServiceDAL);
                Orchestrateur_Btn.Enabled = false;
                Noeud_Btn.BackColor = Color.DarkGray;
                AdresseIP_Lbl.Text = N.ToString();
            }
            catch (ClusterException ex)
            {
                string err = $"{ex.Message} \n{ex.StackTrace}";
                ex.Log();
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Dés que l'event résultant d'un calcul est receptionné on met à jour l'affichage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onResultatChanged(object sender, ResultatEventArgs e)
        {
            Invoke(new MethodInvoker(() => { richTextBox_Result.AppendText($"{e.Op.ToString()}");}));
        }

        private void Calcul1_Btn_Click(object sender, EventArgs e)
        {
            string file = GetFile();
            string fileContentZip = file.Compress();

            try
            {
                O.RepartirCalcul(file, "GetCalcul1");
            }
            catch (ClusterException ex)
            {
                ex.Log();
                MessageBox.Show(ex.Message);
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
            catch (Exception ex)
            {
                string err = $"{ex.Message} \n{ex.StackTrace}";
                MessageBox.Show(err);
            }
            return fileContent;
        }

        private void Calcul2_Btn_Click(object sender, EventArgs e)
        {
            string file = GetFile();
            string fileContentZip = file.Compress();

            try
            {
                O.Envoyer(new Operation { Methode = "GetCalcul2", Chunck = fileContentZip });
               // Resultat_Lbl.Text = $"{N.GetResultat()}";
            }
            catch (Exception ex)
            {
                string err = $"{ex.Message} \n{ex.StackTrace}";
                MessageBox.Show(err);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (N != null)
                N.Dispose();
            if (O != null)
                O.Dispose();
        }
    }
}
