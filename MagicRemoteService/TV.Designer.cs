
namespace MagicRemoteService {
	partial class TV {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TV));
			this.btnConfirm = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.labIP = new System.Windows.Forms.Label();
			this.labTVPort = new System.Windows.Forms.Label();
			this.numboxSendPort = new System.Windows.Forms.NumericUpDown();
			this.labDescription = new System.Windows.Forms.Label();
			this.tbDescription = new System.Windows.Forms.TextBox();
			this.labUser = new System.Windows.Forms.Label();
			this.tbUser = new System.Windows.Forms.TextBox();
			this.labPassword = new System.Windows.Forms.Label();
			this.tbPassword = new System.Windows.Forms.TextBox();
			this.labPassphrase = new System.Windows.Forms.Label();
			this.tbPassphrase = new System.Windows.Forms.TextBox();
			this.labPrivateKey = new System.Windows.Forms.Label();
			this.tbPrivateKey = new System.Windows.Forms.TextBox();
			this.tlpTV = new System.Windows.Forms.TableLayoutPanel();
			this.labKeyServer = new System.Windows.Forms.Label();
			this.pnlPrivateKey = new System.Windows.Forms.Panel();
			this.pnlPassphrase = new System.Windows.Forms.Panel();
			this.pnlName = new System.Windows.Forms.Panel();
			this.labName = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.pnlDescription = new System.Windows.Forms.Panel();
			this.pnlIP = new System.Windows.Forms.Panel();
			this.ipadrboxIP = new MagicRemoteService.IPAddressBox();
			this.pnlPort = new System.Windows.Forms.Panel();
			this.pnlUser = new System.Windows.Forms.Panel();
			this.pnlPassword = new System.Windows.Forms.Panel();
			this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
			this.cbAdvanced = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numboxSendPort)).BeginInit();
			this.tlpTV.SuspendLayout();
			this.pnlPrivateKey.SuspendLayout();
			this.pnlPassphrase.SuspendLayout();
			this.pnlName.SuspendLayout();
			this.pnlDescription.SuspendLayout();
			this.pnlIP.SuspendLayout();
			this.pnlPort.SuspendLayout();
			this.pnlUser.SuspendLayout();
			this.pnlPassword.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnConfirm
			// 
			resources.ApplyResources(this.btnConfirm, "btnConfirm");
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.UseVisualStyleBackColor = true;
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// labIP
			// 
			resources.ApplyResources(this.labIP, "labIP");
			this.labIP.Name = "labIP";
			// 
			// labTVPort
			// 
			resources.ApplyResources(this.labTVPort, "labTVPort");
			this.labTVPort.Name = "labTVPort";
			// 
			// numboxSendPort
			// 
			resources.ApplyResources(this.numboxSendPort, "numboxSendPort");
			this.numboxSendPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.numboxSendPort.Name = "numboxSendPort";
			this.numboxSendPort.ValueChanged += new System.EventHandler(this.numboxSendPort_ValueChanged);
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
			this.tbDescription.TextChanged += new System.EventHandler(this.tbDescription_TextChanged);
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
			this.tbUser.TextChanged += new System.EventHandler(this.tbUser_TextChanged);
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
			this.tbPassword.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
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
			this.tbPassphrase.TextChanged += new System.EventHandler(this.tbPassphrase_TextChanged);
			// 
			// labPrivateKey
			// 
			resources.ApplyResources(this.labPrivateKey, "labPrivateKey");
			this.labPrivateKey.Name = "labPrivateKey";
			// 
			// tbPrivateKey
			// 
			resources.ApplyResources(this.tbPrivateKey, "tbPrivateKey");
			this.tbPrivateKey.Name = "tbPrivateKey";
			this.tbPrivateKey.TextChanged += new System.EventHandler(this.tbPrivateKey_TextChanged);
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
			this.pnlPrivateKey.Controls.Add(this.tbPrivateKey);
			this.pnlPrivateKey.Controls.Add(this.labPrivateKey);
			resources.ApplyResources(this.pnlPrivateKey, "pnlPrivateKey");
			this.pnlPrivateKey.Name = "pnlPrivateKey";
			// 
			// pnlPassphrase
			// 
			this.pnlPassphrase.Controls.Add(this.labPassphrase);
			this.pnlPassphrase.Controls.Add(this.tbPassphrase);
			resources.ApplyResources(this.pnlPassphrase, "pnlPassphrase");
			this.pnlPassphrase.Name = "pnlPassphrase";
			// 
			// pnlName
			// 
			this.pnlName.Controls.Add(this.labName);
			this.pnlName.Controls.Add(this.tbName);
			resources.ApplyResources(this.pnlName, "pnlName");
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
			this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
			// 
			// pnlDescription
			// 
			this.pnlDescription.Controls.Add(this.labDescription);
			this.pnlDescription.Controls.Add(this.tbDescription);
			resources.ApplyResources(this.pnlDescription, "pnlDescription");
			this.pnlDescription.Name = "pnlDescription";
			// 
			// pnlIP
			// 
			this.pnlIP.Controls.Add(this.labIP);
			this.pnlIP.Controls.Add(this.ipadrboxIP);
			resources.ApplyResources(this.pnlIP, "pnlIP");
			this.pnlIP.Name = "pnlIP";
			// 
			// ipadrboxIP
			// 
			this.ipadrboxIP.BackColor = System.Drawing.SystemColors.Window;
			this.ipadrboxIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ipadrboxIP.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.ipadrboxIP, "ipadrboxIP");
			this.ipadrboxIP.Name = "ipadrboxIP";
			this.ipadrboxIP.Value = null;
			this.ipadrboxIP.ValueChanged += new System.EventHandler(this.ipadrboxIP_ValueChanged);
			// 
			// pnlPort
			// 
			this.pnlPort.Controls.Add(this.labTVPort);
			this.pnlPort.Controls.Add(this.numboxSendPort);
			resources.ApplyResources(this.pnlPort, "pnlPort");
			this.pnlPort.Name = "pnlPort";
			// 
			// pnlUser
			// 
			this.pnlUser.Controls.Add(this.labUser);
			this.pnlUser.Controls.Add(this.tbUser);
			resources.ApplyResources(this.pnlUser, "pnlUser");
			this.pnlUser.Name = "pnlUser";
			// 
			// pnlPassword
			// 
			this.pnlPassword.Controls.Add(this.labPassword);
			this.pnlPassword.Controls.Add(this.tbPassword);
			resources.ApplyResources(this.pnlPassword, "pnlPassword");
			this.pnlPassword.Name = "pnlPassword";
			// 
			// directorySearcher1
			// 
			this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
			this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
			this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
			// 
			// cbAdvanced
			// 
			resources.ApplyResources(this.cbAdvanced, "cbAdvanced");
			this.cbAdvanced.Name = "cbAdvanced";
			this.cbAdvanced.UseVisualStyleBackColor = true;
			this.cbAdvanced.CheckedChanged += new System.EventHandler(this.cbAdvanced_CheckedChanged);
			// 
			// TV
			// 
			this.AcceptButton = this.btnConfirm;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.Controls.Add(this.cbAdvanced);
			this.Controls.Add(this.btnConfirm);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.tlpTV);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TV";
			((System.ComponentModel.ISupportInitialize)(this.numboxSendPort)).EndInit();
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
			this.pnlUser.ResumeLayout(false);
			this.pnlUser.PerformLayout();
			this.pnlPassword.ResumeLayout(false);
			this.pnlPassword.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label labIP;
		private System.Windows.Forms.Label labTVPort;
		private System.Windows.Forms.NumericUpDown numboxSendPort;
		private IPAddressBox ipadrboxIP;
		private System.Windows.Forms.Label labDescription;
		private System.Windows.Forms.TextBox tbDescription;
		private System.Windows.Forms.Label labUser;
		private System.Windows.Forms.TextBox tbUser;
		private System.Windows.Forms.Label labPassword;
		private System.Windows.Forms.TextBox tbPassword;
		private System.Windows.Forms.Label labPassphrase;
		private System.Windows.Forms.TextBox tbPassphrase;
		private System.Windows.Forms.Label labPrivateKey;
		private System.Windows.Forms.TextBox tbPrivateKey;
		private System.Windows.Forms.TableLayoutPanel tlpTV;
		private System.Windows.Forms.Panel pnlDescription;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.Label labName;
		private System.Windows.Forms.Panel pnlName;
		private System.DirectoryServices.DirectorySearcher directorySearcher1;
		private System.Windows.Forms.Panel pnlPassphrase;
		private System.Windows.Forms.Panel pnlIP;
		private System.Windows.Forms.Panel pnlPort;
		private System.Windows.Forms.Panel pnlUser;
		private System.Windows.Forms.Panel pnlPassword;
		private System.Windows.Forms.Panel pnlPrivateKey;
		private System.Windows.Forms.CheckBox cbAdvanced;
		private System.Windows.Forms.Label labKeyServer;
	}
}