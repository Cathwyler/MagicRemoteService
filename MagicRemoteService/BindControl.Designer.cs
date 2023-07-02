
namespace MagicRemoteService {
	partial class BindControl {
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent() {
			this.btnBind = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnBind
			// 
			this.btnBind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBind.Location = new System.Drawing.Point(0, 0);
			this.btnBind.Margin = new System.Windows.Forms.Padding(0);
			this.btnBind.Name = "btnBind";
			this.btnBind.Size = new System.Drawing.Size(100, 23);
			this.btnBind.TabIndex = 0;
			this.btnBind.UseVisualStyleBackColor = true;
			this.btnBind.Click += new System.EventHandler(this.btnBind_Click);
			// 
			// BindControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.btnBind);
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.Name = "BindControl";
			this.Size = new System.Drawing.Size(100, 23);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnBind;
	}
}
