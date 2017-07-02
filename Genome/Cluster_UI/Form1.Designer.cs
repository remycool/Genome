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
            this.Orchestrateur_Btn = new System.Windows.Forms.Button();
            this.Noeud_Btn = new System.Windows.Forms.Button();
            this.Calcul_Btn = new System.Windows.Forms.Button();
            this.Resultat_Lbl = new System.Windows.Forms.Label();
            this.AdresseIP_Lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Orchestrateur_Btn
            // 
            this.Orchestrateur_Btn.Location = new System.Drawing.Point(83, 71);
            this.Orchestrateur_Btn.Name = "Orchestrateur_Btn";
            this.Orchestrateur_Btn.Size = new System.Drawing.Size(204, 75);
            this.Orchestrateur_Btn.TabIndex = 0;
            this.Orchestrateur_Btn.Text = "Orchestrateur";
            this.Orchestrateur_Btn.UseVisualStyleBackColor = true;
            this.Orchestrateur_Btn.Click += new System.EventHandler(this.Orchestrateur_Btn_Click);
            // 
            // Noeud_Btn
            // 
            this.Noeud_Btn.Location = new System.Drawing.Point(367, 71);
            this.Noeud_Btn.Name = "Noeud_Btn";
            this.Noeud_Btn.Size = new System.Drawing.Size(204, 75);
            this.Noeud_Btn.TabIndex = 1;
            this.Noeud_Btn.Text = "Noeud";
            this.Noeud_Btn.UseVisualStyleBackColor = true;
            this.Noeud_Btn.Click += new System.EventHandler(this.Noeud_Btn_Click);
            // 
            // Calcul_Btn
            // 
            this.Calcul_Btn.Location = new System.Drawing.Point(83, 204);
            this.Calcul_Btn.Name = "Calcul_Btn";
            this.Calcul_Btn.Size = new System.Drawing.Size(204, 75);
            this.Calcul_Btn.TabIndex = 2;
            this.Calcul_Btn.Text = "Lancer Calcul";
            this.Calcul_Btn.UseVisualStyleBackColor = true;
            this.Calcul_Btn.Click += new System.EventHandler(this.LancerCalcul_Btn_Click);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 351);
            this.Controls.Add(this.AdresseIP_Lbl);
            this.Controls.Add(this.Resultat_Lbl);
            this.Controls.Add(this.Calcul_Btn);
            this.Controls.Add(this.Noeud_Btn);
            this.Controls.Add(this.Orchestrateur_Btn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Orchestrateur_Btn;
        private System.Windows.Forms.Button Noeud_Btn;
        private System.Windows.Forms.Button Calcul_Btn;
        private System.Windows.Forms.Label Resultat_Lbl;
        private System.Windows.Forms.Label AdresseIP_Lbl;
    }
}

