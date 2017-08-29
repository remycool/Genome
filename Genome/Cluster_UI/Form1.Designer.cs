namespace Cluster_UI
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
            this.Noeud_Btn = new System.Windows.Forms.Button();
            this.Resultat_Lbl = new System.Windows.Forms.Label();
            this.AdresseIP_Lbl = new System.Windows.Forms.Label();
            this.richTextBox_Result = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Noeud_Btn
            // 
            this.Noeud_Btn.Location = new System.Drawing.Point(216, 25);
            this.Noeud_Btn.Name = "Noeud_Btn";
            this.Noeud_Btn.Size = new System.Drawing.Size(204, 75);
            this.Noeud_Btn.TabIndex = 1;
            this.Noeud_Btn.Text = "Noeud";
            this.Noeud_Btn.UseVisualStyleBackColor = true;
            this.Noeud_Btn.Click += new System.EventHandler(this.Noeud_Btn_Click);
            // 
            // Resultat_Lbl
            // 
            this.Resultat_Lbl.AutoSize = true;
            this.Resultat_Lbl.Location = new System.Drawing.Point(367, 204);
            this.Resultat_Lbl.Name = "Resultat_Lbl";
            this.Resultat_Lbl.Size = new System.Drawing.Size(0, 13);
            this.Resultat_Lbl.TabIndex = 3;
            // 
            // AdresseIP_Lbl
            // 
            this.AdresseIP_Lbl.AutoSize = true;
            this.AdresseIP_Lbl.Location = new System.Drawing.Point(272, 9);
            this.AdresseIP_Lbl.Name = "AdresseIP_Lbl";
            this.AdresseIP_Lbl.Size = new System.Drawing.Size(0, 13);
            this.AdresseIP_Lbl.TabIndex = 4;
            // 
            // richTextBox_Result
            // 
            this.richTextBox_Result.Location = new System.Drawing.Point(12, 106);
            this.richTextBox_Result.Name = "richTextBox_Result";
            this.richTextBox_Result.Size = new System.Drawing.Size(640, 198);
            this.richTextBox_Result.TabIndex = 6;
            this.richTextBox_Result.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 316);
            this.Controls.Add(this.richTextBox_Result);
            this.Controls.Add(this.AdresseIP_Lbl);
            this.Controls.Add(this.Resultat_Lbl);
            this.Controls.Add(this.Noeud_Btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Noeud_Btn;
        private System.Windows.Forms.Label Resultat_Lbl;
        private System.Windows.Forms.Label AdresseIP_Lbl;
        private System.Windows.Forms.RichTextBox richTextBox_Result;
    }
}

