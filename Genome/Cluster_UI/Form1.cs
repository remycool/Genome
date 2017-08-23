﻿using Cluster.Classes;
using Cluster.Interfaces;
using Cluster.Utils;
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
        //public Orchestrateur O { get; set; }
        //public Noeud N { get; set; }
        public INoeud N { get; set; }
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
            N = new Orchestrateur(ServiceDAL);
            Noeud_Btn.Enabled = false;
            Calcul1_Btn.Enabled = true;
            Calcul2_Btn.Enabled = true;
            Orchestrateur_Btn.BackColor = Color.DarkGray;
            AdresseIP_Lbl.Text = N.ToString();
        }

        private void Noeud_Btn_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox_Results.Clear();
                string file = GetFile();
                Console.WriteLine(file.Length);
                N = new Noeud(ServiceBusiness, ServiceDAL);
                Orchestrateur_Btn.Enabled = false;
                Noeud_Btn.BackColor = Color.DarkGray;
                AdresseIP_Lbl.Text = N.ToString();              
               
                N.Attente();
            }
            catch (Exception ex)
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

        private void Calcul1_Btn_Click(object sender, EventArgs e)
        {
            string file = GetFile();
            string fileContentZip = file.Compress();

            try
            {
                N.RepartirCalcul(file, "GetCalcul1");
                //Operation retour1 = N.Envoyer(new Operation { Type = "GetCalcul1", Param = fileContentZip });
                richTextBox_Results.AppendText($"");
                //Resultat_Lbl.Text = retour1.ToString();
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
                Operation retour = N.Envoyer(new Operation { Type = "GetCalcul2", Param = fileContentZip });
                Resultat_Lbl.Text = $"{retour.Param.Substring(0,150)}";
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
        }
    }
}
