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
            this.buttonNodePanel = new System.Windows.Forms.Button();
            this.buttonModule1 = new System.Windows.Forms.Button();
            this.buttonLaunch = new System.Windows.Forms.Button();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxPathFile = new System.Windows.Forms.TextBox();
            this.panelModule1 = new System.Windows.Forms.Panel();
            this.listViewModule1 = new System.Windows.Forms.ListView();
            this.paireDeBase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelNode = new System.Windows.Forms.Panel();
            this.buttonOrchestrateur = new System.Windows.Forms.Button();
            this.buttonNode = new System.Windows.Forms.Button();
            this.panelTitre.SuspendLayout();
            this.panelBrowse.SuspendLayout();
            this.panelModule1.SuspendLayout();
            this.panelNode.SuspendLayout();
            this.SuspendLayout();
            // 
            // adnTitle
            // 
            this.adnTitle.AutoSize = true;
            this.adnTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adnTitle.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.adnTitle.Location = new System.Drawing.Point(331, 8);
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
            this.panelTitre.Size = new System.Drawing.Size(788, 50);
            this.panelTitre.TabIndex = 1;
            // 
            // panelBrowse
            // 
            this.panelBrowse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBrowse.Controls.Add(this.buttonNodePanel);
            this.panelBrowse.Controls.Add(this.buttonModule1);
            this.panelBrowse.Controls.Add(this.buttonLaunch);
            this.panelBrowse.Controls.Add(this.buttonBrowse);
            this.panelBrowse.Controls.Add(this.textBoxPathFile);
            this.panelBrowse.Location = new System.Drawing.Point(0, 50);
            this.panelBrowse.Name = "panelBrowse";
            this.panelBrowse.Size = new System.Drawing.Size(788, 131);
            this.panelBrowse.TabIndex = 2;
            this.panelBrowse.UseWaitCursor = true;
            // 
            // buttonNodePanel
            // 
            this.buttonNodePanel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonNodePanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNodePanel.Location = new System.Drawing.Point(224, 101);
            this.buttonNodePanel.Name = "buttonNodePanel";
            this.buttonNodePanel.Size = new System.Drawing.Size(134, 28);
            this.buttonNodePanel.TabIndex = 4;
            this.buttonNodePanel.Text = "Node Window";
            this.buttonNodePanel.UseVisualStyleBackColor = false;
            this.buttonNodePanel.UseWaitCursor = true;
            this.buttonNodePanel.Visible = false;
            this.buttonNodePanel.Click += new System.EventHandler(this.buttonNodePanel_Click);
            // 
            // buttonModule1
            // 
            this.buttonModule1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonModule1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonModule1.Location = new System.Drawing.Point(110, 101);
            this.buttonModule1.Name = "buttonModule1";
            this.buttonModule1.Size = new System.Drawing.Size(92, 27);
            this.buttonModule1.TabIndex = 3;
            this.buttonModule1.Text = "Module 1";
            this.buttonModule1.UseVisualStyleBackColor = false;
            this.buttonModule1.UseWaitCursor = true;
            this.buttonModule1.Visible = false;
            this.buttonModule1.Click += new System.EventHandler(this.buttonModule1_Click);
            // 
            // buttonLaunch
            // 
            this.buttonLaunch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonLaunch.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLaunch.Location = new System.Drawing.Point(3, 100);
            this.buttonLaunch.Name = "buttonLaunch";
            this.buttonLaunch.Size = new System.Drawing.Size(89, 28);
            this.buttonLaunch.TabIndex = 2;
            this.buttonLaunch.Text = "Launch";
            this.buttonLaunch.UseVisualStyleBackColor = false;
            this.buttonLaunch.UseWaitCursor = true;
            this.buttonLaunch.Visible = false;
            this.buttonLaunch.Click += new System.EventHandler(this.button1_Click);
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
            this.panelModule1.Controls.Add(this.listViewModule1);
            this.panelModule1.Location = new System.Drawing.Point(0, 190);
            this.panelModule1.Name = "panelModule1";
            this.panelModule1.Size = new System.Drawing.Size(788, 271);
            this.panelModule1.TabIndex = 3;
            this.panelModule1.Visible = false;
            // 
            // listViewModule1
            // 
            this.listViewModule1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.paireDeBase,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listViewModule1.FullRowSelect = true;
            this.listViewModule1.GridLines = true;
            this.listViewModule1.Location = new System.Drawing.Point(28, 0);
            this.listViewModule1.Name = "listViewModule1";
            this.listViewModule1.Size = new System.Drawing.Size(741, 271);
            this.listViewModule1.TabIndex = 0;
            this.listViewModule1.UseCompatibleStateImageBehavior = false;
            this.listViewModule1.View = System.Windows.Forms.View.Details;
            // 
            // paireDeBase
            // 
            this.paireDeBase.Text = "Nombre paire de base";
            this.paireDeBase.Width = 125;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Occurrence A, T, G, C";
            this.columnHeader1.Width = 137;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "% total Occurrence";
            this.columnHeader2.Width = 119;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Nombre de Base Inconnues";
            this.columnHeader3.Width = 163;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "occurrence séquence de 4 bases";
            this.columnHeader4.Width = 193;
            // 
            // panelNode
            // 
            this.panelNode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNode.Controls.Add(this.buttonNode);
            this.panelNode.Controls.Add(this.buttonOrchestrateur);
            this.panelNode.Location = new System.Drawing.Point(1, 182);
            this.panelNode.Name = "panelNode";
            this.panelNode.Size = new System.Drawing.Size(787, 333);
            this.panelNode.TabIndex = 5;
            // 
            // buttonOrchestrateur
            // 
            this.buttonOrchestrateur.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonOrchestrateur.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOrchestrateur.Location = new System.Drawing.Point(31, 49);
            this.buttonOrchestrateur.Name = "buttonOrchestrateur";
            this.buttonOrchestrateur.Size = new System.Drawing.Size(205, 78);
            this.buttonOrchestrateur.TabIndex = 0;
            this.buttonOrchestrateur.Text = "Orchestrateur";
            this.buttonOrchestrateur.UseVisualStyleBackColor = false;
            // 
            // buttonNode
            // 
            this.buttonNode.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNode.Location = new System.Drawing.Point(511, 49);
            this.buttonNode.Name = "buttonNode";
            this.buttonNode.Size = new System.Drawing.Size(209, 78);
            this.buttonNode.TabIndex = 1;
            this.buttonNode.Text = "Noeux";
            this.buttonNode.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 637);
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
            this.panelModule1.ResumeLayout(false);
            this.panelNode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label adnTitle;
        private System.Windows.Forms.Panel panelTitre;
        private System.Windows.Forms.Panel panelBrowse;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxPathFile;
        private System.Windows.Forms.Panel panelModule1;
        private System.Windows.Forms.ListView listViewModule1;
        private System.Windows.Forms.ColumnHeader paireDeBase;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonLaunch;
        private System.Windows.Forms.Panel panelNode;
        private System.Windows.Forms.Button buttonModule1;
        private System.Windows.Forms.Button buttonNodePanel;
        private System.Windows.Forms.Button buttonNode;
        private System.Windows.Forms.Button buttonOrchestrateur;
    }
}

