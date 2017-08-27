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
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxPathFile = new System.Windows.Forms.TextBox();
            this.panelModule1 = new System.Windows.Forms.Panel();
            this.panelNode = new System.Windows.Forms.Panel();
            this.buttonNode = new System.Windows.Forms.Button();
            this.buttonOrchestrateur = new System.Windows.Forms.Button();
            this.panelAffichResult = new System.Windows.Forms.Panel();
            this.buttonPaireDeBase = new System.Windows.Forms.Button();
            this.buttonOccurence = new System.Windows.Forms.Button();
            this.buttonBaseInconnue = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.labelButtonClick = new System.Windows.Forms.Label();
            this.panelButton = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelTitre.SuspendLayout();
            this.panelBrowse.SuspendLayout();
            this.panelNode.SuspendLayout();
            this.panelAffichResult.SuspendLayout();
            this.panelButton.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.adnTitle.Click += new System.EventHandler(this.label1_Click);
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
            this.panelBrowse.Controls.Add(this.buttonBrowse);
            this.panelBrowse.Controls.Add(this.textBoxPathFile);
            this.panelBrowse.Location = new System.Drawing.Point(0, 50);
            this.panelBrowse.Name = "panelBrowse";
            this.panelBrowse.Size = new System.Drawing.Size(847, 131);
            this.panelBrowse.TabIndex = 2;
            this.panelBrowse.UseWaitCursor = true;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.BackColor = System.Drawing.SystemColors.MenuBar;
            this.buttonBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowse.ForeColor = System.Drawing.SystemColors.InfoText;
            this.buttonBrowse.Location = new System.Drawing.Point(370, 45);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(72, 31);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = false;
            this.buttonBrowse.UseWaitCursor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxPathFile
            // 
            this.textBoxPathFile.Location = new System.Drawing.Point(308, 19);
            this.textBoxPathFile.Name = "textBoxPathFile";
            this.textBoxPathFile.Size = new System.Drawing.Size(190, 20);
            this.textBoxPathFile.TabIndex = 0;
            this.textBoxPathFile.UseWaitCursor = true;
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
            this.panelNode.Controls.Add(this.panel1);
            this.panelNode.Controls.Add(this.panelAffichResult);
            this.panelNode.Controls.Add(this.panelButton);
            this.panelNode.Location = new System.Drawing.Point(1, 182);
            this.panelNode.Name = "panelNode";
            this.panelNode.Size = new System.Drawing.Size(846, 422);
            this.panelNode.TabIndex = 5;
            // 
            // buttonNode
            // 
            this.buttonNode.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNode.Location = new System.Drawing.Point(501, 42);
            this.buttonNode.Name = "buttonNode";
            this.buttonNode.Size = new System.Drawing.Size(209, 78);
            this.buttonNode.TabIndex = 1;
            this.buttonNode.Text = "Node";
            this.buttonNode.UseVisualStyleBackColor = false;
            // 
            // buttonOrchestrateur
            // 
            this.buttonOrchestrateur.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonOrchestrateur.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOrchestrateur.Location = new System.Drawing.Point(21, 42);
            this.buttonOrchestrateur.Name = "buttonOrchestrateur";
            this.buttonOrchestrateur.Size = new System.Drawing.Size(205, 78);
            this.buttonOrchestrateur.TabIndex = 0;
            this.buttonOrchestrateur.Text = "Orchestrateur";
            this.buttonOrchestrateur.UseVisualStyleBackColor = false;
            // 
            // panelAffichResult
            // 
            this.panelAffichResult.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panelAffichResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAffichResult.Controls.Add(this.labelButtonClick);
            this.panelAffichResult.Location = new System.Drawing.Point(200, 152);
            this.panelAffichResult.Name = "panelAffichResult";
            this.panelAffichResult.Size = new System.Drawing.Size(645, 270);
            this.panelAffichResult.TabIndex = 0;
            this.panelAffichResult.Visible = false;
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
            // labelButtonClick
            // 
            this.labelButtonClick.AutoSize = true;
            this.labelButtonClick.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelButtonClick.Location = new System.Drawing.Point(35, 20);
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
            this.panelButton.Location = new System.Drawing.Point(1, 152);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(201, 269);
            this.panelButton.TabIndex = 5;
            this.panelButton.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonNode);
            this.panel1.Controls.Add(this.buttonOrchestrateur);
            this.panel1.Location = new System.Drawing.Point(1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(844, 154);
            this.panel1.TabIndex = 6;
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
            this.panelTitre.ResumeLayout(false);
            this.panelTitre.PerformLayout();
            this.panelBrowse.ResumeLayout(false);
            this.panelBrowse.PerformLayout();
            this.panelNode.ResumeLayout(false);
            this.panelAffichResult.ResumeLayout(false);
            this.panelAffichResult.PerformLayout();
            this.panelButton.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label adnTitle;
        private System.Windows.Forms.Panel panelTitre;
        private System.Windows.Forms.Panel panelBrowse;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxPathFile;
        private System.Windows.Forms.Panel panelModule1;
        private System.Windows.Forms.Panel panelNode;
        private System.Windows.Forms.Button buttonNode;
        private System.Windows.Forms.Button buttonOrchestrateur;
        private System.Windows.Forms.Panel panelAffichResult;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonBaseInconnue;
        private System.Windows.Forms.Button buttonOccurence;
        private System.Windows.Forms.Button buttonPaireDeBase;
        private System.Windows.Forms.Label labelButtonClick;
        private System.Windows.Forms.Panel panelButton;
        private System.Windows.Forms.Panel panel1;
    }
}

