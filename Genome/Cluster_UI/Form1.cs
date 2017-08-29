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
       
        public Noeud N { get; set; }
        IBusinessFactory ServiceBusiness { get; set; }
        IDALFactory ServiceDAL { get; set; }

        public Form1()
        {
            ServiceBusiness = new BusinessFactory(new GenomeBusiness());
            ServiceDAL = new DALFactory(new ClusterDAL());
            try
            {
                N = new Noeud(ServiceBusiness, ServiceDAL);
                N.NouvelleOperation += onOperationReceived;
            }
            catch(ClusterException ex)
            {
                string message= "Erreur à l'initialisation du noeud - Veuillez consulter le log dans C:/ pour plus d'informations";
                ex.Log(message,ex.StackTrace);
                MessageBox.Show(message);
            }
           
            InitializeComponent();

        }

        private void Noeud_Btn_Click(object sender, EventArgs e)
        {
           
                Noeud_Btn.BackColor = Color.DarkGray;
                AdresseIP_Lbl.Text = N.ToString();
        }

        /// <summary>
        /// Dés que l'event résultant d'un calcul est receptionné on met à jour l'affichage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void onOperationReceived(object sender, OperationEventArgs e)
        {
            Invoke(new MethodInvoker(() => { richTextBox_Result.AppendText($"{e.Op.ToString()}");}));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (N != null)
                N.Dispose();
        }
    }
}
