
namespace MagicRemoteService {
	partial class TVAdder {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TVAdder));
			this.tlpTV = new System.Windows.Forms.TableLayoutPanel();
			this.labKeyServer = new System.Windows.Forms.Label();
			this.pnlPrivateKey = new System.Windows.Forms.Panel();
			this.tbPrivateKey = new System.Windows.Forms.TextBox();
			this.labPrivateKey = new System.Windows.Forms.Label();
			this.pnlPassphrase = new System.Windows.Forms.Panel();
			this.labPassphrase = new System.Windows.Forms.Label();
			this.tbPassphrase = new System.Windows.Forms.TextBox();
			this.pnlName = new System.Windows.Forms.Panel();
			this.labName = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.pnlDescription = new System.Windows.Forms.Panel();
			this.labDescription = new System.Windows.Forms.Label();
			this.tbDescription = new System.Windows.Forms.TextBox();
			this.pnlIP = new System.Windows.Forms.Panel();
			this.iabIP = new MagicRemoteService.IPAddressBox();
			this.labIP = new System.Windows.Forms.Label();
			this.pnlPort = new System.Windows.Forms.Panel();
			this.labPort = new System.Windows.Forms.Label();
			this.nbPort = new System.Windows.Forms.NumericUpDown();
			this.pnlUser = new System.Windows.Forms.Panel();
			this.labUser = new System.Windows.Forms.Label();
			this.tbUser = new System.Windows.Forms.TextBox();
			this.pnlPassword = new System.Windows.Forms.Panel();
			this.labPassword = new System.Windows.Forms.Label();
			this.tbPassword = new System.Windows.Forms.TextBox();
			this.cbAdvanced = new System.Windows.Forms.CheckBox();
			this.btnConfirm = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tlpTV.SuspendLayout();
			this.pnlPrivateKey.SuspendLayout();
			this.pnlPassphrase.SuspendLayout();
			this.pnlName.SuspendLayout();
			this.pnlDescription.SuspendLayout();
			this.pnlIP.SuspendLayout();
			this.pnlPort.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nbPort)).BeginInit();
			this.pnlUser.SuspendLayout();
			this.pnlPassword.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpTV
			// 
			resources.ApplyResources(this.tlpTV, "tlpTV");
			this.tlpTV.Controls.Add(this.labKeyServer, 0, 9);
			this.tlpTV.Controls.Add(this.pnlPrivateKey, 0, 8);
			this.tlpTV.Controls.Add(this.pnlPassphrase, 0, 6);
			this.tlpTV.Controls.Add(this.pnlName, 0, 0);
			this.tlpTV.Controls.Add(this.pnlDescription, 0, 1);
			this.tlpTV.Controls.Add(this.pnlIP, 0, 2);
			this.tlpTV.Controls.Add(this.pnlPort, 0, 3);
			this.tlpTV.Controls.Add(this.pnlUser, 0, 4);
			this.tlpTV.Controls.Add(this.pnlPassword, 0, 5);
			this.tlpTV.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.tlpTV.Name = "tlpTV";
			// 
			// labKeyServer
			// 
			resources.ApplyResources(this.labKeyServer, "labKeyServer");
			this.labKeyServer.Name = "labKeyServer";
			// 
			// pnlPrivateKey
			// 
			resources.ApplyResources(this.pnlPrivateKey, "pnlPrivateKey");
			this.pnlPrivateKey.Controls.Add(this.tbPrivateKey);
			this.pnlPrivateKey.Controls.Add(this.labPrivateKey);
			this.pnlPrivateKey.Name = "pnlPrivateKey";
			// 
			// tbPrivateKey
			// 
			resources.ApplyResources(this.tbPrivateKey, "tbPrivateKey");
			this.tbPrivateKey.Name = "tbPrivateKey";
			this.tbPrivateKey.TextChanged += new System.EventHandler(this.PrivateKey_TextChanged);
			// 
			// labPrivateKey
			// 
			resources.ApplyResources(this.labPrivateKey, "labPrivateKey");
			this.labPrivateKey.Name = "labPrivateKey";
			// 
			// pnlPassphrase
			// 
			resources.ApplyResources(this.pnlPassphrase, "pnlPassphrase");
			this.pnlPassphrase.Controls.Add(this.labPassphrase);
			this.pnlPassphrase.Controls.Add(this.tbPassphrase);
			this.pnlPassphrase.Name = "pnlPassphrase";
			// 
			// labPassphrase
			// 
			resources.ApplyResources(this.labPassphrase, "labPassphrase");
			this.labPassphrase.Name = "labPassphrase";
			// 
			// tbPassphrase
			// 
			resources.ApplyResources(this.tbPassphrase, "tbPassphrase");
			this.tbPassphrase.Name = "tbPassphrase";
			this.tbPassphrase.TextChanged += new System.EventHandler(this.Passphrase_TextChanged);
			// 
			// pnlName
			// 
			resources.ApplyResources(this.pnlName, "pnlName");
			this.pnlName.Controls.Add(this.labName);
			this.pnlName.Controls.Add(this.tbName);
			this.pnlName.Name = "pnlName";
			// 
			// labName
			// 
			resources.ApplyResources(this.labName, "labName");
			this.labName.Name = "labName";
			// 
			// tbName
			// 
			resources.ApplyResources(this.tbName, "tbName");
			this.tbName.Name = "tbName";
			this.tbName.TextChanged += new System.EventHandler(this.Name_TextChanged);
			// 
			// pnlDescription
			// 
			resources.ApplyResources(this.pnlDescription, "pnlDescription");
			this.pnlDescription.Controls.Add(this.labDescription);
			this.pnlDescription.Controls.Add(this.tbDescription);
			this.pnlDescription.Name = "pnlDescription";
			// 
			// labDescription
			// 
			resources.ApplyResources(this.labDescription, "labDescription");
			this.labDescription.Name = "labDescription";
			// 
			// tbDescription
			// 
			resources.ApplyResources(this.tbDescription, "tbDescription");
			this.tbDescription.Name = "tbDescription";
			this.tbDescription.TextChanged += new System.EventHandler(this.Description_TextChanged);
			// 
			// pnlIP
			// 
			resources.ApplyResources(this.pnlIP, "pnlIP");
			this.pnlIP.Controls.Add(this.iabIP);
			this.pnlIP.Controls.Add(this.labIP);
			this.pnlIP.Name = "pnlIP";
			// 
			// iabIP
			// 
			resources.ApplyResources(this.iabIP, "iabIP");
			this.iabIP.BackColor = System.Drawing.SystemColors.Window;
			this.iabIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iabIP.ForeColor = System.Drawing.SystemColors.WindowText;
			this.iabIP.Name = "iabIP";
			this.iabIP.Value = null;
			this.iabIP.ehValueChanged += new System.EventHandler(this.IP_ValueChanged);
			// 
			// labIP
			// 
			resources.ApplyResources(this.labIP, "labIP");
			this.labIP.Name = "labIP";
			// 
			// pnlPort
			// 
			resources.ApplyResources(this.pnlPort, "pnlPort");
			this.pnlPort.Controls.Add(this.labPort);
			this.pnlPort.Controls.Add(this.nbPort);
			this.pnlPort.Name = "pnlPort";
			// 
			// labPort
			// 
			resources.ApplyResources(this.labPort, "labPort");
			this.labPort.Name = "labPort";
			// 
			// nbPort
			// 
			resources.ApplyResources(this.nbPort, "nbPort");
			this.nbPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nbPort.Name = "nbPort";
			this.nbPort.ValueChanged += new System.EventHandler(this.Port_ValueChanged);
			// 
			// pnlUser
			// 
			resources.ApplyResources(this.pnlUser, "pnlUser");
			this.pnlUser.Controls.Add(this.labUser);
			this.pnlUser.Controls.Add(this.tbUser);
			this.pnlUser.Name = "pnlUser";
			// 
			// labUser
			// 
			resources.ApplyResources(this.labUser, "labUser");
			this.labUser.Name = "labUser";
			// 
			// tbUser
			// 
			resources.ApplyResources(this.tbUser, "tbUser");
			this.tbUser.Name = "tbUser";
			this.tbUser.TextChanged += new System.EventHandler(this.User_TextChanged);
			// 
			// pnlPassword
			// 
			resources.ApplyResources(this.pnlPassword, "pnlPassword");
			this.pnlPassword.Controls.Add(this.labPassword);
			this.pnlPassword.Controls.Add(this.tbPassword);
			this.pnlPassword.Name = "pnlPassword";
			// 
			// labPassword
			// 
			resources.ApplyResources(this.labPassword, "labPassword");
			this.labPassword.Name = "labPassword";
			// 
			// tbPassword
			// 
			resources.ApplyResources(this.tbPassword, "tbPassword");
			this.tbPassword.Name = "tbPassword";
			this.tbPassword.TextChanged += new System.EventHandler(this.Password_TextChanged);
			// 
			// cbAdvanced
			// 
			resources.ApplyResources(this.cbAdvanced, "cbAdvanced");
			this.cbAdvanced.Name = "cbAdvanced";
			this.cbAdvanced.UseVisualStyleBackColor = true;
			this.cbAdvanced.CheckedChanged += new System.EventHandler(this.Advanced_CheckedChanged);
			// 
			// btnConfirm
			// 
			resources.ApplyResources(this.btnConfirm, "btnConfirm");
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.UseVisualStyleBackColor = true;
			this.btnConfirm.Click += new System.EventHandler(this.Confirm_Click);
			// 
			// btnCancel
			// 
			resources.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// TVAdder
			// 
			this.AcceptButton = this.btnConfirm;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cbAdvanced);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnConfirm);
			this.Controls.Add(this.tlpTV);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = global::MagicRemoteService.Properties.Resources.MagicRemoteServiceIcon;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TVAdder";
			this.tlpTV.ResumeLayout(false);
			this.tlpTV.PerformLayout();
			this.pnlPrivateKey.ResumeLayout(false);
			this.pnlPrivateKey.PerformLayout();
			this.pnlPassphrase.ResumeLayout(false);
			this.pnlPassphrase.PerformLayout();
			this.pnlName.ResumeLayout(false);
			this.pnlName.PerformLayout();
			this.pnlDescription.ResumeLayout(false);
			this.pnlDescription.PerformLayout();
			this.pnlIP.ResumeLayout(false);
			this.pnlIP.PerformLayout();
			this.pnlPort.ResumeLayout(false);
			this.pnlPort.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nbPort)).EndInit();
			this.pnlUser.ResumeLayout(false);
			this.pnlUser.PerformLayout();
			this.pnlPassword.ResumeLayout(false);
			this.pnlPassword.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TableLayoutPanel tlpTV;
		private System.Windows.Forms.Panel pnlName;
		private System.Windows.Forms.Label labName;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.Panel pnlDescription;
		private System.Windows.Forms.Label labDescription;
		private System.Windows.Forms.TextBox tbDescription;
		private System.Windows.Forms.Panel pnlIP;
		private System.Windows.Forms.Label labIP;
		private IPAddressBox iabIP;
		private System.Windows.Forms.Panel pnlPort;
		private System.Windows.Forms.Label labPort;
		private System.Windows.Forms.NumericUpDown nbPort;
		private System.Windows.Forms.Panel pnlUser;
		private System.Windows.Forms.Label labUser;
		private System.Windows.Forms.TextBox tbUser;
		private System.Windows.Forms.Panel pnlPassword;
		private System.Windows.Forms.Label labPassword;
		private System.Windows.Forms.TextBox tbPassword;
		private System.Windows.Forms.Panel pnlPassphrase;
		private System.Windows.Forms.Label labPassphrase;
		private System.Windows.Forms.TextBox tbPassphrase;
		private System.Windows.Forms.Panel pnlPrivateKey;
		private System.Windows.Forms.Label labPrivateKey;
		private System.Windows.Forms.TextBox tbPrivateKey;
		private System.Windows.Forms.Label labKeyServer;
		private System.Windows.Forms.CheckBox cbAdvanced;
		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.Button btnCancel;
	}
}