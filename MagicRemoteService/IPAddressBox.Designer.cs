
namespace MagicRemoteService {
	partial class IPAddressBox {
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
			this.components = new System.ComponentModel.Container();
			this.lDot1 = new System.Windows.Forms.Label();
			this.lDot2 = new System.Windows.Forms.Label();
			this.lDot3 = new System.Windows.Forms.Label();
			this.ttFormating = new System.Windows.Forms.ToolTip(this.components);
			this.dbbByte3 = new MagicRemoteService.DecimalByteBox();
			this.dbbByte2 = new MagicRemoteService.DecimalByteBox();
			this.dbbByte1 = new MagicRemoteService.DecimalByteBox();
			this.dbbByte0 = new MagicRemoteService.DecimalByteBox();
			this.SuspendLayout();
			// 
			// lDot1
			// 
			this.lDot1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lDot1.AutoSize = true;
			this.lDot1.Location = new System.Drawing.Point(30, 3);
			this.lDot1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.lDot1.Name = "lDot1";
			this.lDot1.Size = new System.Drawing.Size(10, 13);
			this.lDot1.TabIndex = 1;
			this.lDot1.Text = ".";
			this.lDot1.Click += new System.EventHandler(this.IPAddressBox_Click);
			// 
			// lDot2
			// 
			this.lDot2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lDot2.AutoSize = true;
			this.lDot2.Location = new System.Drawing.Point(65, 3);
			this.lDot2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.lDot2.Name = "lDot2";
			this.lDot2.Size = new System.Drawing.Size(10, 13);
			this.lDot2.TabIndex = 4;
			this.lDot2.Text = ".";
			this.lDot2.Click += new System.EventHandler(this.IPAddressBox_Click);
			// 
			// lDot3
			// 
			this.lDot3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lDot3.AutoSize = true;
			this.lDot3.Location = new System.Drawing.Point(100, 3);
			this.lDot3.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.lDot3.Name = "lDot3";
			this.lDot3.Size = new System.Drawing.Size(10, 13);
			this.lDot3.TabIndex = 5;
			this.lDot3.Text = ".";
			this.lDot3.Click += new System.EventHandler(this.IPAddressBox_Click);
			// 
			// ttFormating
			// 
			this.ttFormating.IsBalloon = true;
			this.ttFormating.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
			// 
			// dbbByte3
			// 
			this.dbbByte3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dbbByte3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dbbByte3.Location = new System.Drawing.Point(110, 3);
			this.dbbByte3.Margin = new System.Windows.Forms.Padding(0, 3, 5, 2);
			this.dbbByte3.MaxLength = 3;
			this.dbbByte3.Name = "dbbByte3";
			this.dbbByte3.Size = new System.Drawing.Size(25, 13);
			this.dbbByte3.TabIndex = 6;
			this.dbbByte3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dbbByte3.EventPaste += new System.EventHandler(this.dbbByte_EventPaste);
			this.dbbByte3.Click += new System.EventHandler(this.IPAddressBox_Click);
			this.dbbByte3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dbbByte_KeyPress);
			// 
			// dbbByte2
			// 
			this.dbbByte2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dbbByte2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dbbByte2.Location = new System.Drawing.Point(75, 3);
			this.dbbByte2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.dbbByte2.MaxLength = 3;
			this.dbbByte2.Name = "dbbByte2";
			this.dbbByte2.Size = new System.Drawing.Size(25, 13);
			this.dbbByte2.TabIndex = 3;
			this.dbbByte2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dbbByte2.EventPaste += new System.EventHandler(this.dbbByte_EventPaste);
			this.dbbByte2.Click += new System.EventHandler(this.IPAddressBox_Click);
			this.dbbByte2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dbbByte_KeyPress);
			// 
			// dbbByte1
			// 
			this.dbbByte1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dbbByte1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dbbByte1.Location = new System.Drawing.Point(40, 3);
			this.dbbByte1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.dbbByte1.MaxLength = 3;
			this.dbbByte1.Name = "dbbByte1";
			this.dbbByte1.Size = new System.Drawing.Size(25, 13);
			this.dbbByte1.TabIndex = 2;
			this.dbbByte1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dbbByte1.EventPaste += new System.EventHandler(this.dbbByte_EventPaste);
			this.dbbByte1.Click += new System.EventHandler(this.IPAddressBox_Click);
			this.dbbByte1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dbbByte_KeyPress);
			// 
			// dbbByte0
			// 
			this.dbbByte0.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dbbByte0.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dbbByte0.Location = new System.Drawing.Point(5, 3);
			this.dbbByte0.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
			this.dbbByte0.MaxLength = 3;
			this.dbbByte0.Name = "dbbByte0";
			this.dbbByte0.Size = new System.Drawing.Size(25, 13);
			this.dbbByte0.TabIndex = 0;
			this.dbbByte0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dbbByte0.EventPaste += new System.EventHandler(this.dbbByte_EventPaste);
			this.dbbByte0.Click += new System.EventHandler(this.IPAddressBox_Click);
			this.dbbByte0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dbbByte_KeyPress);
			// 
			// IPAddressBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.dbbByte3);
			this.Controls.Add(this.lDot3);
			this.Controls.Add(this.dbbByte2);
			this.Controls.Add(this.lDot2);
			this.Controls.Add(this.dbbByte1);
			this.Controls.Add(this.lDot1);
			this.Controls.Add(this.dbbByte0);
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.MinimumSize = new System.Drawing.Size(142, 20);
			this.Name = "IPAddressBox";
			this.Size = new System.Drawing.Size(140, 18);
			this.Click += new System.EventHandler(this.IPAddressBox_Click);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private MagicRemoteService.DecimalByteBox dbbByte0;
		private System.Windows.Forms.Label lDot1;
		private MagicRemoteService.DecimalByteBox dbbByte1;
		private System.Windows.Forms.Label lDot2;
		private MagicRemoteService.DecimalByteBox dbbByte2;
		private System.Windows.Forms.Label lDot3;
		private MagicRemoteService.DecimalByteBox dbbByte3;
		private System.Windows.Forms.ToolTip ttFormating;
	}
}
