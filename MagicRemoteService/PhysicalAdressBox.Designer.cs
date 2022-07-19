
namespace MagicRemoteService {
	partial class MacAddressBox {
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
			this.lDot3 = new System.Windows.Forms.Label();
			this.lDot2 = new System.Windows.Forms.Label();
			this.lDot1 = new System.Windows.Forms.Label();
			this.lDot4 = new System.Windows.Forms.Label();
			this.lDot5 = new System.Windows.Forms.Label();
			this.ttFormating = new System.Windows.Forms.ToolTip(this.components);
			this.dbbByte5 = new MagicRemoteService.DecimalByteBox();
			this.dbbByte4 = new MagicRemoteService.DecimalByteBox();
			this.dbbByte3 = new MagicRemoteService.DecimalByteBox();
			this.dbbByte2 = new MagicRemoteService.DecimalByteBox();
			this.dbbByte1 = new MagicRemoteService.DecimalByteBox();
			this.dbbByte0 = new MagicRemoteService.DecimalByteBox();
			this.SuspendLayout();
			// 
			// lDot3
			// 
			this.lDot3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lDot3.AutoSize = true;
			this.lDot3.Location = new System.Drawing.Point(70, 3);
			this.lDot3.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.lDot3.Name = "lDot3";
			this.lDot3.Size = new System.Drawing.Size(10, 13);
			this.lDot3.TabIndex = 12;
			this.lDot3.Text = ":";
			this.lDot3.Click += new System.EventHandler(this.MacAddressBox_Click);
			// 
			// lDot2
			// 
			this.lDot2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lDot2.AutoSize = true;
			this.lDot2.Location = new System.Drawing.Point(45, 3);
			this.lDot2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.lDot2.Name = "lDot2";
			this.lDot2.Size = new System.Drawing.Size(10, 13);
			this.lDot2.TabIndex = 11;
			this.lDot2.Text = ":";
			this.lDot2.Click += new System.EventHandler(this.MacAddressBox_Click);
			// 
			// lDot1
			// 
			this.lDot1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lDot1.AutoSize = true;
			this.lDot1.Location = new System.Drawing.Point(20, 3);
			this.lDot1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.lDot1.Name = "lDot1";
			this.lDot1.Size = new System.Drawing.Size(10, 13);
			this.lDot1.TabIndex = 8;
			this.lDot1.Text = ":";
			this.lDot1.Click += new System.EventHandler(this.MacAddressBox_Click);
			// 
			// lDot4
			// 
			this.lDot4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lDot4.AutoSize = true;
			this.lDot4.Location = new System.Drawing.Point(95, 3);
			this.lDot4.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.lDot4.Name = "lDot4";
			this.lDot4.Size = new System.Drawing.Size(10, 13);
			this.lDot4.TabIndex = 14;
			this.lDot4.Text = ":";
			this.lDot4.Click += new System.EventHandler(this.MacAddressBox_Click);
			// 
			// lDot5
			// 
			this.lDot5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lDot5.AutoSize = true;
			this.lDot5.Location = new System.Drawing.Point(120, 3);
			this.lDot5.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.lDot5.Name = "lDot5";
			this.lDot5.Size = new System.Drawing.Size(10, 13);
			this.lDot5.TabIndex = 16;
			this.lDot5.Text = ":";
			this.lDot5.Click += new System.EventHandler(this.MacAddressBox_Click);
			// 
			// ttFormating
			// 
			this.ttFormating.IsBalloon = true;
			this.ttFormating.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
			// 
			// dbbByte5
			// 
			this.dbbByte5.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dbbByte5.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dbbByte5.Location = new System.Drawing.Point(130, 3);
			this.dbbByte5.Margin = new System.Windows.Forms.Padding(0, 3, 5, 2);
			this.dbbByte5.MaxLength = 2;
			this.dbbByte5.Name = "dbbByte5";
			this.dbbByte5.Size = new System.Drawing.Size(15, 13);
			this.dbbByte5.TabIndex = 17;
			this.dbbByte5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dbbByte5.EventPaste += new System.EventHandler(this.dbbByte_EventPaste);
			this.dbbByte5.Click += new System.EventHandler(this.MacAddressBox_Click);
			this.dbbByte5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dbbByte_KeyPress);
			// 
			// dbbByte4
			// 
			this.dbbByte4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dbbByte4.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dbbByte4.Location = new System.Drawing.Point(105, 3);
			this.dbbByte4.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.dbbByte4.MaxLength = 2;
			this.dbbByte4.Name = "dbbByte4";
			this.dbbByte4.Size = new System.Drawing.Size(15, 13);
			this.dbbByte4.TabIndex = 15;
			this.dbbByte4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dbbByte4.EventPaste += new System.EventHandler(this.dbbByte_EventPaste);
			this.dbbByte4.Click += new System.EventHandler(this.MacAddressBox_Click);
			this.dbbByte4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dbbByte_KeyPress);
			// 
			// dbbByte3
			// 
			this.dbbByte3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dbbByte3.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dbbByte3.Location = new System.Drawing.Point(80, 3);
			this.dbbByte3.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.dbbByte3.MaxLength = 2;
			this.dbbByte3.Name = "dbbByte3";
			this.dbbByte3.Size = new System.Drawing.Size(15, 13);
			this.dbbByte3.TabIndex = 13;
			this.dbbByte3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dbbByte3.EventPaste += new System.EventHandler(this.dbbByte_EventPaste);
			this.dbbByte3.Click += new System.EventHandler(this.MacAddressBox_Click);
			this.dbbByte3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dbbByte_KeyPress);
			// 
			// dbbByte2
			// 
			this.dbbByte2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dbbByte2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dbbByte2.Location = new System.Drawing.Point(55, 3);
			this.dbbByte2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.dbbByte2.MaxLength = 2;
			this.dbbByte2.Name = "dbbByte2";
			this.dbbByte2.Size = new System.Drawing.Size(15, 13);
			this.dbbByte2.TabIndex = 10;
			this.dbbByte2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dbbByte2.EventPaste += new System.EventHandler(this.dbbByte_EventPaste);
			this.dbbByte2.Click += new System.EventHandler(this.MacAddressBox_Click);
			this.dbbByte2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dbbByte_KeyPress);
			// 
			// dbbByte1
			// 
			this.dbbByte1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dbbByte1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dbbByte1.Location = new System.Drawing.Point(30, 3);
			this.dbbByte1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
			this.dbbByte1.MaxLength = 2;
			this.dbbByte1.Name = "dbbByte1";
			this.dbbByte1.Size = new System.Drawing.Size(15, 13);
			this.dbbByte1.TabIndex = 9;
			this.dbbByte1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dbbByte1.EventPaste += new System.EventHandler(this.dbbByte_EventPaste);
			this.dbbByte1.Click += new System.EventHandler(this.MacAddressBox_Click);
			this.dbbByte1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dbbByte_KeyPress);
			// 
			// dbbByte0
			// 
			this.dbbByte0.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.dbbByte0.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dbbByte0.Location = new System.Drawing.Point(5, 3);
			this.dbbByte0.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
			this.dbbByte0.MaxLength = 2;
			this.dbbByte0.Name = "dbbByte0";
			this.dbbByte0.Size = new System.Drawing.Size(15, 13);
			this.dbbByte0.TabIndex = 7;
			this.dbbByte0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.dbbByte0.EventPaste += new System.EventHandler(this.dbbByte_EventPaste);
			this.dbbByte0.Click += new System.EventHandler(this.MacAddressBox_Click);
			this.dbbByte0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dbbByte_KeyPress);
			// 
			// MacAddressBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.dbbByte5);
			this.Controls.Add(this.lDot5);
			this.Controls.Add(this.dbbByte4);
			this.Controls.Add(this.lDot4);
			this.Controls.Add(this.dbbByte3);
			this.Controls.Add(this.lDot3);
			this.Controls.Add(this.dbbByte2);
			this.Controls.Add(this.lDot2);
			this.Controls.Add(this.dbbByte1);
			this.Controls.Add(this.lDot1);
			this.Controls.Add(this.dbbByte0);
			this.ForeColor = System.Drawing.SystemColors.WindowText;
			this.MinimumSize = new System.Drawing.Size(152, 20);
			this.Name = "MacAddressBox";
			this.Size = new System.Drawing.Size(150, 18);
			this.Click += new System.EventHandler(this.MacAddressBox_Click);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DecimalByteBox dbbByte3;
		private System.Windows.Forms.Label lDot3;
		private DecimalByteBox dbbByte2;
		private System.Windows.Forms.Label lDot2;
		private DecimalByteBox dbbByte1;
		private System.Windows.Forms.Label lDot1;
		private DecimalByteBox dbbByte0;
		private DecimalByteBox dbbByte4;
		private System.Windows.Forms.Label lDot4;
		private DecimalByteBox dbbByte5;
		private System.Windows.Forms.Label lDot5;
		private System.Windows.Forms.ToolTip ttFormating;
	}
}
