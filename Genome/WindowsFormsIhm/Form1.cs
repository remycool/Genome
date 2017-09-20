using Cluster;
using Cluster.Classes;
using Cluster.Events;
using Cluster.Exceptions;
using Cluster.Protocole;
using Genome;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsIhm
{

    public partial class Form1 : Form
    {
        #region PROPRIETES
        public DisplayData meth { get; set; }
        public Communication Com { get; set; } 
        #endregion

        public Form1()
        {
            InitializeComponent();
            try
            {
                meth = new DisplayData();

            }
            catch (ClusterException ex)
            {
                string message = "Erreur à l'initialisation de l'orchestrateur - Veuillez consulter le log dans C:/ pour plus d'informations";
                ex.Log(message, ex.StackTrace);
                MessageBox.Show(message);
            }
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
                    FichierSelectionne.Text = fileDialog.FileName;
                    List<string> fichiersDecoupes = meth.tranformToArray(fileDialog.FileName);
                    meth.SplitFile(fichiersDecoupes);
                    panelButton.Visible = true;
                }
            }
            catch (ClusterException ex)
            {
                string message = "L'ouverture du fichier n'a pas fonctionné, veuillez consulter le log dans C:/ pour plus d'informations";
                ex.Log(ex.Message, ex.StackTrace);
                MessageBox.Show(message);
            }

        }

        /// <summary>
        /// Dés que l'event résultant d'un calcul est receptionné on met à jour l'affichage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public void onResultatChanged(object sender, ResultatEventArgs<T> e)
        //{
        //    //Invoke(new MethodInvoker(() =>
        //    //{
        //    //    if (string.IsNullOrEmpty(e.Op.Erreur))
        //    //        richTextBox_result.AppendText($"{e.Op.ToString()}");
        //    //    else
        //    //        richTextBox_result.AppendText($"{e.Op.Erreur}");
        //    //}));
        //}

        /// <summary>
        /// On notifie à la vue dès que le le traitement est terminé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onTraitementTermine(object sender, TraitementTermineEventArgs e)
        {
            //Invoke(new MethodInvoker(() =>
            //{
            //    TraitementTermine_label.Text = $"Traitement terminé! ";
            //    MessageBox.Show($"Résultat = {Orch.Result.Valeur} en {Orch.Result.TempsExecution} ms");
            //}));
        }

        /// <summary>
        /// On notifie la vue des noeuds connectés
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onNouveauNoeudConnecte(object sender, NoeudConnecteEventArgs e)
        {
            Invoke(new MethodInvoker(() =>
            {
                NbNoeudConnecte_label.Text = string.Empty;
                NbNoeudConnecte_label.Text = $"Noeuds connectes {e.Noeuds.Count}";
                listeNoeud_label.ResetText();
                foreach (IPAddress a in e.Noeuds)
                {
                    listeNoeud_label.Text += $"\n{a.ToString()}";
                }
            }));
        }

        /// <summary>
        /// Lance le comptage des paires de bases dans le fichier chargé
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPaireDeBase_Click(object sender, EventArgs e)
        {
            panelAffichResult.Visible = true;
            richTextBox_result.Clear();
            //try
            //{
            //    Orch.RepartirCalcul("GetCalcul4");
            //}
            //catch (ClusterException ex)
            //{
            //    string message = "Erreur lors du lancement du calcul - Veuillez consulter le log dans C:/ pour plus d'informations";
            //    ex.Log(message, ex.StackTrace);
            //    MessageBox.Show(message);
            //}

        }

        /// <summary>
        /// Lance le comptage du nombre de bases A,T,G,C contenu dans le fichier génome
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOccurence_Click(object sender, EventArgs e)
        {
            panelAffichResult.Visible = true;
            Orchestrateur<Base> o = new Orchestrateur<Base>(Com);
            //Abonnement à l'évènement
            //o.NouveauResultat+= onResultatChanged;
            o.TraitementTermine += onTraitementTermine;
            o.NouveauNoeud += onNouveauNoeudConnecte;
            try
            {
                o.RepartirCalcul("GetCalcul");
            }
            catch (ClusterException ex)
            {
                string message = "Erreur lors du lancement du calcul - Veuillez consulter le log dans C:/ pour plus d'informations";
                ex.Log(message, ex.StackTrace);
                MessageBox.Show(message);
            }
        }

        /// <summary>
        /// Lance le comptage du nombre de bases inconnues contenu dans le fichier génome
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBaseInconnue_Click(object sender, EventArgs e)
        {
            //panelAffichResult.Visible = true;
            //richTextBox_result.Clear();
            //try
            //{
            //    Orch.RepartirCalcul("GetCalcul5");
            //}
            //catch (ClusterException ex)
            //{
            //    string message = "Erreur lors du lancement du calcul - Veuillez consulter le log dans C:/ pour plus d'informations";
            //    ex.Log(message, ex.StackTrace);
            //    MessageBox.Show(message);
            //}
        }

        /// <summary>
        /// lance le comptage du nombre d’occurrence de la séquence de 4 bases la plus fréquente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            panelAffichResult.Visible = true;
            //try
            //{
            //    Orch.RepartirCalcul("GetCalcul10");
            //}
            //catch (ClusterException ex)
            //{
            //    string message = "Erreur lors du lancement du calcul - Veuillez consulter le log dans C:/ pour plus d'informations";
            //    ex.Log(message, ex.StackTrace);
            //    MessageBox.Show(message);
            //}
        }

        /// <summary>
        /// A l'ouverture du form on initialise l'objet Orchestrateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// A la fermeture du form on appelle la méthode dispose de la classe Orchestrateur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

    }
}
