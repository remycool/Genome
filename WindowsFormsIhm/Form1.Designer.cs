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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.adnTitle = new System.Windows.Forms.Label();
            this.panelTitre = new System.Windows.Forms.Panel();
            this.panelBrowse = new System.Windows.Forms.Panel();
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
            this.buttonReset = new System.Windows.Forms.Button();
            this.panelNode = new System.Windows.Forms.Panel();
            this.labelNodeTitle = new System.Windows.Forms.Label();
            this.panelSelectNode = new System.Windows.Forms.Panel();
            this.listBoxNode = new System.Windows.Forms.ListBox();
            this.buttonValidNode = new System.Windows.Forms.Button();
            this.panelStatNode = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelStatTitre = new System.Windows.Forms.Label();
            this.labelNameCurrentNode = new System.Windows.Forms.Label();
            this.buttonModule1 = new System.Windows.Forms.Button();
            this.buttonNodePanel = new System.Windows.Forms.Button();
            this.panelTitre.SuspendLayout();
            this.panelBrowse.SuspendLayout();
            this.panelModule1.SuspendLayout();
            this.panelNode.SuspendLayout();
            this.panelSelectNode.SuspendLayout();
            this.panelStatNode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // buttonReset
            // 
            this.buttonReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReset.Location = new System.Drawing.Point(336, 521);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(83, 23);
            this.buttonReset.TabIndex = 4;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelNode
            // 
            this.panelNode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNode.Controls.Add(this.panelStatNode);
            this.panelNode.Controls.Add(this.panelSelectNode);
            this.panelNode.Location = new System.Drawing.Point(1, 182);
            this.panelNode.Name = "panelNode";
            this.panelNode.Size = new System.Drawing.Size(787, 279);
            this.panelNode.TabIndex = 5;
            // 
            // labelNodeTitle
            // 
            this.labelNodeTitle.AutoSize = true;
            this.labelNodeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNodeTitle.Location = new System.Drawing.Point(31, 9);
            this.labelNodeTitle.Name = "labelNodeTitle";
            this.labelNodeTitle.Size = new System.Drawing.Size(96, 20);
            this.labelNodeTitle.TabIndex = 0;
            this.labelNodeTitle.Text = "Select Node";
            this.labelNodeTitle.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // panelSelectNode
            // 
            this.panelSelectNode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSelectNode.Controls.Add(this.buttonValidNode);
            this.panelSelectNode.Controls.Add(this.listBoxNode);
            this.panelSelectNode.Controls.Add(this.labelNodeTitle);
            this.panelSelectNode.Location = new System.Drawing.Point(-1, -1);
            this.panelSelectNode.Name = "panelSelectNode";
            this.panelSelectNode.Size = new System.Drawing.Size(182, 279);
            this.panelSelectNode.TabIndex = 1;
            // 
            // listBoxNode
            // 
            this.listBoxNode.FormattingEnabled = true;
            this.listBoxNode.Items.AddRange(new object[] {
            "Node1",
            "Node2"});
            this.listBoxNode.Location = new System.Drawing.Point(25, 56);
            this.listBoxNode.Name = "listBoxNode";
            this.listBoxNode.Size = new System.Drawing.Size(112, 17);
            this.listBoxNode.TabIndex = 2;
            // 
            // buttonValidNode
            // 
            this.buttonValidNode.Location = new System.Drawing.Point(24, 73);
            this.buttonValidNode.Name = "buttonValidNode";
            this.buttonValidNode.Size = new System.Drawing.Size(72, 26);
            this.buttonValidNode.TabIndex = 2;
            this.buttonValidNode.Text = "Submit";
            this.buttonValidNode.UseVisualStyleBackColor = true;
            this.buttonValidNode.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // panelStatNode
            // 
            this.panelStatNode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStatNode.Controls.Add(this.labelNameCurrentNode);
            this.panelStatNode.Controls.Add(this.labelStatTitre);
            this.panelStatNode.Controls.Add(this.pictureBox1);
            this.panelStatNode.Location = new System.Drawing.Point(180, -1);
            this.panelStatNode.Name = "panelStatNode";
            this.panelStatNode.Size = new System.Drawing.Size(602, 279);
            this.panelStatNode.TabIndex = 2;
            this.panelStatNode.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(188, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(409, 268);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // labelStatTitre
            // 
            this.labelStatTitre.AutoSize = true;
            this.labelStatTitre.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatTitre.Location = new System.Drawing.Point(91, 15);
            this.labelStatTitre.Name = "labelStatTitre";
            this.labelStatTitre.Size = new System.Drawing.Size(81, 17);
            this.labelStatTitre.TabIndex = 1;
            this.labelStatTitre.Text = "Statistiques";
            // 
            // labelNameCurrentNode
            // 
            this.labelNameCurrentNode.AutoSize = true;
            this.labelNameCurrentNode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNameCurrentNode.Location = new System.Drawing.Point(29, 16);
            this.labelNameCurrentNode.Name = "labelNameCurrentNode";
            this.labelNameCurrentNode.Size = new System.Drawing.Size(46, 17);
            this.labelNameCurrentNode.TabIndex = 2;
            this.labelNameCurrentNode.Text = "label1";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 637);
            this.Controls.Add(this.panelNode);
            this.Controls.Add(this.buttonReset);
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
            this.panelSelectNode.ResumeLayout(false);
            this.panelSelectNode.PerformLayout();
            this.panelStatNode.ResumeLayout(false);
            this.panelStatNode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label adnTitle;
        private System.Windows.Forms.Panel panelTitre;
        private System.Windows.Forms.Panel panelBrowse;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxPathFile;
        private System.Windows.Forms.Panel panelModule1;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ListView listViewModule1;
        private System.Windows.Forms.ColumnHeader paireDeBase;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button buttonLaunch;
        private System.Windows.Forms.Panel panelNode;
        private System.Windows.Forms.Label labelNodeTitle;
        private System.Windows.Forms.Panel panelSelectNode;
        private System.Windows.Forms.ListBox listBoxNode;
        private System.Windows.Forms.Button buttonValidNode;
        private System.Windows.Forms.Panel panelStatNode;
        private System.Windows.Forms.Label labelStatTitre;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelNameCurrentNode;
        private System.Windows.Forms.Button buttonModule1;
        private System.Windows.Forms.Button buttonNodePanel;
    }
}

