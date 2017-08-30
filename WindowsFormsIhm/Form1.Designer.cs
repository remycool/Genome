namespace WindowsFormsIhm
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.adnTitle = new System.Windows.Forms.Label();
            this.panelTitre = new System.Windows.Forms.Panel();
            this.panelBrowse = new System.Windows.Forms.Panel();
            this.listeNoeud_label = new System.Windows.Forms.Label();
            this.NbNoeudConnecte_label = new System.Windows.Forms.Label();
            this.FichierSelectionne = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.panelModule1 = new System.Windows.Forms.Panel();
            this.panelNode = new System.Windows.Forms.Panel();
            this.panelAffichResult = new System.Windows.Forms.Panel();
            this.TraitementTermine_label = new System.Windows.Forms.Label();
            this.richTextBox_result = new System.Windows.Forms.RichTextBox();
            this.labelButtonClick = new System.Windows.Forms.Label();
            this.panelButton = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.buttonPaireDeBase = new System.Windows.Forms.Button();
            this.buttonBaseInconnue = new System.Windows.Forms.Button();
            this.buttonOccurence = new System.Windows.Forms.Button();
            this.panelTitre.SuspendLayout();
            this.panelBrowse.SuspendLayout();
            this.panelNode.SuspendLayout();
            this.panelAffichResult.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // adnTitle
            // 
            this.adnTitle.AutoSize = true;
            this.adnTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adnTitle.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.adnTitle.Location = new System.Drawing.Point(395, 8);
            this.adnTitle.Name = "adnTitle";
            this.adnTitle.Size = new System.Drawing.Size(57, 25);
            this.adnTitle.TabIndex = 0;
            this.adnTitle.Text = "ADN";
            // 
            // panelTitre
            // 
            this.panelTitre.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panelTitre.Controls.Add(this.adnTitle);
            this.panelTitre.ForeColor = System.Drawing.SystemColors.Desktop;
            this.panelTitre.Location = new System.Drawing.Point(0, 1);
            this.panelTitre.Name = "panelTitre";
            this.panelTitre.Size = new System.Drawing.Size(847, 50);
            this.panelTitre.TabIndex = 1;
            // 
            // panelBrowse
            // 
            this.panelBrowse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBrowse.Controls.Add(this.listeNoeud_label);
            this.panelBrowse.Controls.Add(this.NbNoeudConnecte_label);
            this.panelBrowse.Controls.Add(this.FichierSelectionne);
            this.panelBrowse.Controls.Add(this.buttonBrowse);
            this.panelBrowse.Location = new System.Drawing.Point(0, 50);
            this.panelBrowse.Name = "panelBrowse";
            this.panelBrowse.Size = new System.Drawing.Size(847, 131);
            this.panelBrowse.TabIndex = 2;
            // 
            // listeNoeud_label
            // 
            this.listeNoeud_label.AutoSize = true;
            this.listeNoeud_label.Location = new System.Drawing.Point(11, 51);
            this.listeNoeud_label.Name = "listeNoeud_label";
            this.listeNoeud_label.Size = new System.Drawing.Size(0, 13);
            this.listeNoeud_label.TabIndex = 4;
            // 
            // NbNoeudConnecte_label
            // 
            this.NbNoeudConnecte_label.AutoSize = true;
            this.NbNoeudConnecte_label.Location = new System.Drawing.Point(11, 28);
            this.NbNoeudConnecte_label.Name = "NbNoeudConnecte_label";
            this.NbNoeudConnecte_label.Size = new System.Drawing.Size(97, 13);
            this.NbNoeudConnecte_label.TabIndex = 3;
            this.NbNoeudConnecte_label.Text = "Noeuds connectés";
            // 
            // FichierSelectionne
            // 
            this.FichierSelectionne.AutoSize = true;
            this.FichierSelectionne.Location = new System.Drawing.Point(312, 96);
            this.FichierSelectionne.Name = "FichierSelectionne";
            this.FichierSelectionne.Size = new System.Drawing.Size(0, 13);
            this.FichierSelectionne.TabIndex = 2;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.BackColor = System.Drawing.SystemColors.MenuBar;
            this.buttonBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowse.ForeColor = System.Drawing.SystemColors.InfoText;
            this.buttonBrowse.Location = new System.Drawing.Point(557, 28);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(211, 58);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "Sélectionner un fichier";
            this.buttonBrowse.UseVisualStyleBackColor = false;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // panelModule1
            // 
            this.panelModule1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelModule1.Location = new System.Drawing.Point(0, 190);
            this.panelModule1.Name = "panelModule1";
            this.panelModule1.Size = new System.Drawing.Size(847, 417);
            this.panelModule1.TabIndex = 3;
            this.panelModule1.Visible = false;
            // 
            // panelNode
            // 
            this.panelNode.Controls.Add(this.panelAffichResult);
            this.panelNode.Controls.Add(this.panelButton);
            this.panelNode.Location = new System.Drawing.Point(1, 182);
            this.panelNode.Name = "panelNode";
            this.panelNode.Size = new System.Drawing.Size(846, 422);
            this.panelNode.TabIndex = 5;
            // 
            // panelAffichResult
            // 
            this.panelAffichResult.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelAffichResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAffichResult.Controls.Add(this.TraitementTermine_label);
            this.panelAffichResult.Controls.Add(this.richTextBox_result);
            this.panelAffichResult.Controls.Add(this.labelButtonClick);
            this.panelAffichResult.Location = new System.Drawing.Point(208, 0);
            this.panelAffichResult.Name = "panelAffichResult";
            this.panelAffichResult.Size = new System.Drawing.Size(637, 422);
            this.panelAffichResult.TabIndex = 0;
            this.panelAffichResult.Visible = false;
            // 
            // TraitementTermine_label
            // 
            this.TraitementTermine_label.AutoSize = true;
            this.TraitementTermine_label.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TraitementTermine_label.ForeColor = System.Drawing.Color.Lime;
            this.TraitementTermine_label.Location = new System.Drawing.Point(349, 26);
            this.TraitementTermine_label.Name = "TraitementTermine_label";
            this.TraitementTermine_label.Size = new System.Drawing.Size(0, 19);
            this.TraitementTermine_label.TabIndex = 2;
            // 
            // richTextBox_result
            // 
            this.richTextBox_result.Location = new System.Drawing.Point(10, 52);
            this.richTextBox_result.Name = "richTextBox_result";
            this.richTextBox_result.Size = new System.Drawing.Size(616, 352);
            this.richTextBox_result.TabIndex = 1;
            this.richTextBox_result.Text = "";
            // 
            // labelButtonClick
            // 
            this.labelButtonClick.AutoSize = true;
            this.labelButtonClick.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelButtonClick.Location = new System.Drawing.Point(7, 21);
            this.labelButtonClick.Name = "labelButtonClick";
            this.labelButtonClick.Size = new System.Drawing.Size(46, 18);
            this.labelButtonClick.TabIndex = 0;
            this.labelButtonClick.Text = "label1";
            // 
            // panelButton
            // 
            this.panelButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelButton.Controls.Add(this.button5);
            this.panelButton.Controls.Add(this.buttonPaireDeBase);
            this.panelButton.Controls.Add(this.buttonBaseInconnue);
            this.panelButton.Controls.Add(this.buttonOccurence);
            this.panelButton.Location = new System.Drawing.Point(1, 0);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(201, 421);
            this.panelButton.TabIndex = 5;
            this.panelButton.Visible = false;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(35, 135);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(122, 40);
            this.button5.TabIndex = 4;
            this.button5.Text = "Occurrence base la plus frequente";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // buttonPaireDeBase
            // 
            this.buttonPaireDeBase.Location = new System.Drawing.Point(35, 34);
            this.buttonPaireDeBase.Name = "buttonPaireDeBase";
            this.buttonPaireDeBase.Size = new System.Drawing.Size(122, 27);
            this.buttonPaireDeBase.TabIndex = 1;
            this.buttonPaireDeBase.Text = "Total Paires de bases";
            this.buttonPaireDeBase.UseVisualStyleBackColor = true;
            this.buttonPaireDeBase.Click += new System.EventHandler(this.buttonPaireDeBase_Click);
            // 
            // buttonBaseInconnue
            // 
            this.buttonBaseInconnue.Location = new System.Drawing.Point(35, 101);
            this.buttonBaseInconnue.Name = "buttonBaseInconnue";
            this.buttonBaseInconnue.Size = new System.Drawing.Size(122, 27);
            this.buttonBaseInconnue.TabIndex = 3;
            this.buttonBaseInconnue.Text = "Base Inconnue";
            this.buttonBaseInconnue.UseVisualStyleBackColor = true;
            this.buttonBaseInconnue.Click += new System.EventHandler(this.buttonBaseInconnue_Click);
            // 
            // buttonOccurence
            // 
            this.buttonOccurence.Location = new System.Drawing.Point(35, 69);
            this.buttonOccurence.Name = "buttonOccurence";
            this.buttonOccurence.Size = new System.Drawing.Size(122, 27);
            this.buttonOccurence.TabIndex = 2;
            this.buttonOccurence.Text = "Occurrence des bases";
            this.buttonOccurence.UseVisualStyleBackColor = true;
            this.buttonOccurence.Click += new System.EventHandler(this.buttonOccurence_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 637);
            this.Controls.Add(this.panelNode);
            this.Controls.Add(this.panelModule1);
            this.Controls.Add(this.panelBrowse);
            this.Controls.Add(this.panelTitre);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panelTitre.ResumeLayout(false);
            this.panelTitre.PerformLayout();
            this.panelBrowse.ResumeLayout(false);
            this.panelBrowse.PerformLayout();
            this.panelNode.ResumeLayout(false);
            this.panelAffichResult.ResumeLayout(false);
            this.panelAffichResult.PerformLayout();
            this.panelButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label adnTitle;
        private System.Windows.Forms.Panel panelTitre;
        private System.Windows.Forms.Panel panelBrowse;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Panel panelModule1;
        private System.Windows.Forms.Panel panelNode;
        private System.Windows.Forms.Panel panelAffichResult;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonBaseInconnue;
        private System.Windows.Forms.Button buttonOccurence;
        private System.Windows.Forms.Button buttonPaireDeBase;
        private System.Windows.Forms.Label labelButtonClick;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.RichTextBox richTextBox_result;
        private System.Windows.Forms.Label FichierSelectionne;
        private System.Windows.Forms.Label TraitementTermine_label;
        private System.Windows.Forms.Label NbNoeudConnecte_label;
        private System.Windows.Forms.Label listeNoeud_label;
    }
}

