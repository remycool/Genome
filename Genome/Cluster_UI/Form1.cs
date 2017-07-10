﻿using Cluster.Classes;
using Cluster.Utils;
using Genome.GenomeBusiness;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Cluster_UI
{
    public partial class Form1 : Form
    {
        public Orchestrateur O { get; set; }
        public Noeud N { get; set; }
        IBusinessFactory Service { get; set; }

        public Form1()
        {
            Service = new BusinessFactory(new GenomeBusiness());
            InitializeComponent();
            Calcul1_Btn.Enabled = false;
        }

        private void Orchestrateur_Btn_Click(object sender, EventArgs e)
        {
            O = new Orchestrateur();
            Noeud_Btn.Enabled = false;
            Calcul1_Btn.Enabled = true;
            Orchestrateur_Btn.BackColor = Color.DarkGray;
            AdresseIP_Lbl.Text = O.ToString();
        }

        private void Noeud_Btn_Click(object sender, EventArgs e)
        {
            N = new Noeud(Service);
            Orchestrateur_Btn.Enabled = false;
            Noeud_Btn.BackColor = Color.DarkGray;
            AdresseIP_Lbl.Text = N.ToString();
            try
            {
                N.AttenteCalcul();
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
                Stopwatch sw = Stopwatch.StartNew();
                Operation retour = O.EnvoyerCalcul(new Operation { Type = "GetCalcul1", Param = fileContentZip });
                sw.Stop();
                Resultat_Lbl.Text = $"Temps réseau : {sw.ElapsedMilliseconds} ms\n" + retour.ToString();
            }
            catch (Exception ex)
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
                using (StreamReader reader = new StreamReader(fs,Encoding.UTF8))
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
                Stopwatch sw = Stopwatch.StartNew();
                Operation retour = O.EnvoyerCalcul(new Operation { Type = "GetCalcul2", Param = fileContentZip });
                sw.Stop();
                Resultat_Lbl.Text = $"Temps réseau : {sw.ElapsedMilliseconds} ms\n" + retour.ToString();
            }
            catch (Exception ex)
            {
                string err = $"{ex.Message} \n{ex.StackTrace}";
                MessageBox.Show(err);
            }
        }
    }
}