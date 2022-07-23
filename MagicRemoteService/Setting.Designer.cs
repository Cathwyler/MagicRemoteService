
namespace MagicRemoteService {
	partial class Setting {
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Setting));
			this.labSendIP = new System.Windows.Forms.Label();
			this.labSubnetMask = new System.Windows.Forms.Label();
			this.labPCMac = new System.Windows.Forms.Label();
			this.labPCPort = new System.Windows.Forms.Label();
			this.labInput = new System.Windows.Forms.Label();
			this.labTV = new System.Windows.Forms.Label();
			this.labTimeoutRightClick = new System.Windows.Forms.Label();
			this.labTimeoutScreensaver = new System.Windows.Forms.Label();
			this.btnPCSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.numboxListenPort = new System.Windows.Forms.NumericUpDown();
			this.chkboxInactivity = new System.Windows.Forms.CheckBox();
			this.numboxTimeoutInactivity = new System.Windows.Forms.NumericUpDown();
			this.labTimeoutIncativity = new System.Windows.Forms.Label();
			this.cmbboxTV = new System.Windows.Forms.ComboBox();
			this.cmbboxInput = new System.Windows.Forms.ComboBox();
			this.numboxTimeoutRightClick = new System.Windows.Forms.NumericUpDown();
			this.numboxTimeoutScreensaver = new System.Windows.Forms.NumericUpDown();
			this.labTimeoutIncativityUnit = new System.Windows.Forms.Label();
			this.labTimeoutRightClickUnit = new System.Windows.Forms.Label();
			this.labTimeoutScreensaverUnit = new System.Windows.Forms.Label();
			this.btnTVRefresh = new System.Windows.Forms.Button();
			this.btnTVInstall = new System.Windows.Forms.Button();
			this.ttFormating = new System.Windows.Forms.ToolTip(this.components);
			this.numboxSendPort = new System.Windows.Forms.NumericUpDown();
			this.labTVPort = new System.Windows.Forms.Label();
			this.grpboxPC = new System.Windows.Forms.GroupBox();
			this.chkboxStartup = new System.Windows.Forms.CheckBox();
			this.grpboxTV = new System.Windows.Forms.GroupBox();
			this.ipadrboxSendIP = new MagicRemoteService.IPAddressBox();
			this.phyadrboxPCMac = new MagicRemoteService.MacAddressBox();
			this.ipadrboxSubnetMask = new MagicRemoteService.IPAddressBox();
			((System.ComponentModel.ISupportInitialize)(this.numboxListenPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numboxTimeoutInactivity)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numboxTimeoutRightClick)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numboxTimeoutScreensaver)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numboxSendPort)).BeginInit();
			this.grpboxPC.SuspendLayout();
			this.grpboxTV.SuspendLayout();
			this.SuspendLayout();
			// 
			// labSendIP
			// 
			resources.ApplyResources(this.labSendIP, "labSendIP");
			this.labSendIP.Name = "labSendIP";
			// 
			// labSubnetMask
			// 
			resources.ApplyResources(this.labSubnetMask, "labSubnetMask");
			this.labSubnetMask.Name = "labSubnetMask";
			// 
			// labPCMac
			// 
			resources.ApplyResources(this.labPCMac, "labPCMac");
			this.labPCMac.Name = "labPCMac";
			// 
			// labPCPort
			// 
			resources.ApplyResources(this.labPCPort, "labPCPort");
			this.labPCPort.Name = "labPCPort";
			// 
			// labInput
			// 
			resources.ApplyResources(this.labInput, "labInput");
			this.labInput.Name = "labInput";
			// 
			// labTV
			// 
			resources.ApplyResources(this.labTV, "labTV");
			this.labTV.Name = "labTV";
			// 
			// labTimeoutRightClick
			// 
			resources.ApplyResources(this.labTimeoutRightClick, "labTimeoutRightClick");
			this.labTimeoutRightClick.Name = "labTimeoutRightClick";
			// 
			// labTimeoutScreensaver
			// 
			resources.ApplyResources(this.labTimeoutScreensaver, "labTimeoutScreensaver");
			this.labTimeoutScreensaver.Name = "labTimeoutScreensaver";
			// 
			// btnPCSave
			// 
			resources.ApplyResources(this.btnPCSave, "btnPCSave");
			this.btnPCSave.Name = "btnPCSave";
			this.btnPCSave.UseVisualStyleBackColor = true;
			this.btnPCSave.Click += new System.EventHandler(this.btnPCSave_Click);
			// 
			// btnClose
			// 
			resources.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.Name = "btnClose";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// numboxListenPort
			// 
			resources.ApplyResources(this.numboxListenPort, "numboxListenPort");
			this.numboxListenPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.numboxListenPort.Name = "numboxListenPort";
			// 
			// chkboxInactivity
			// 
			resources.ApplyResources(this.chkboxInactivity, "chkboxInactivity");
			this.chkboxInactivity.Name = "chkboxInactivity";
			this.chkboxInactivity.UseVisualStyleBackColor = true;
			// 
			// numboxTimeoutInactivity
			// 
			resources.ApplyResources(this.numboxTimeoutInactivity, "numboxTimeoutInactivity");
			this.numboxTimeoutInactivity.Maximum = new decimal(new int[] {
            21600000,
            0,
            0,
            0});
			this.numboxTimeoutInactivity.Name = "numboxTimeoutInactivity";
			// 
			// labTimeoutIncativity
			// 
			resources.ApplyResources(this.labTimeoutIncativity, "labTimeoutIncativity");
			this.labTimeoutIncativity.Name = "labTimeoutIncativity";
			// 
			// cmbboxTV
			// 
			this.cmbboxTV.DisplayMember = "Name";
			this.cmbboxTV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbboxTV.FormattingEnabled = true;
			resources.ApplyResources(this.cmbboxTV, "cmbboxTV");
			this.cmbboxTV.Name = "cmbboxTV";
			this.cmbboxTV.ValueMember = "Name";
			this.cmbboxTV.SelectedIndexChanged += new System.EventHandler(this.cmbboxTV_SelectedIndexChanged);
			// 
			// cmbboxInput
			// 
			this.cmbboxInput.DisplayMember = "Name";
			this.cmbboxInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbboxInput.FormattingEnabled = true;
			resources.ApplyResources(this.cmbboxInput, "cmbboxInput");
			this.cmbboxInput.Name = "cmbboxInput";
			this.cmbboxInput.ValueMember = "Id";
			// 
			// numboxTimeoutRightClick
			// 
			resources.ApplyResources(this.numboxTimeoutRightClick, "numboxTimeoutRightClick");
			this.numboxTimeoutRightClick.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
			this.numboxTimeoutRightClick.Name = "numboxTimeoutRightClick";
			// 
			// numboxTimeoutScreensaver
			// 
			resources.ApplyResources(this.numboxTimeoutScreensaver, "numboxTimeoutScreensaver");
			this.numboxTimeoutScreensaver.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
			this.numboxTimeoutScreensaver.Name = "numboxTimeoutScreensaver";
			// 
			// labTimeoutIncativityUnit
			// 
			resources.ApplyResources(this.labTimeoutIncativityUnit, "labTimeoutIncativityUnit");
			this.labTimeoutIncativityUnit.Name = "labTimeoutIncativityUnit";
			// 
			// labTimeoutRightClickUnit
			// 
			resources.ApplyResources(this.labTimeoutRightClickUnit, "labTimeoutRightClickUnit");
			this.labTimeoutRightClickUnit.Name = "labTimeoutRightClickUnit";
			// 
			// labTimeoutScreensaverUnit
			// 
			resources.ApplyResources(this.labTimeoutScreensaverUnit, "labTimeoutScreensaverUnit");
			this.labTimeoutScreensaverUnit.Name = "labTimeoutScreensaverUnit";
			// 
			// btnTVRefresh
			// 
			resources.ApplyResources(this.btnTVRefresh, "btnTVRefresh");
			this.btnTVRefresh.Name = "btnTVRefresh";
			this.btnTVRefresh.UseVisualStyleBackColor = true;
			this.btnTVRefresh.Click += new System.EventHandler(this.btnTVRefresh_Click);
			// 
			// btnTVInstall
			// 
			resources.ApplyResources(this.btnTVInstall, "btnTVInstall");
			this.btnTVInstall.Name = "btnTVInstall";
			this.btnTVInstall.UseVisualStyleBackColor = true;
			this.btnTVInstall.Click += new System.EventHandler(this.btnTVInstall_Click);
			// 
			// ttFormating
			// 
			this.ttFormating.IsBalloon = true;
			this.ttFormating.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
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
			// 
			// labTVPort
			// 
			resources.ApplyResources(this.labTVPort, "labTVPort");
			this.labTVPort.Name = "labTVPort";
			// 
			// grpboxPC
			// 
			resources.ApplyResources(this.grpboxPC, "grpboxPC");
			this.grpboxPC.Controls.Add(this.chkboxStartup);
			this.grpboxPC.Controls.Add(this.labPCPort);
			this.grpboxPC.Controls.Add(this.btnPCSave);
			this.grpboxPC.Controls.Add(this.numboxListenPort);
			this.grpboxPC.Controls.Add(this.chkboxInactivity);
			this.grpboxPC.Controls.Add(this.numboxTimeoutInactivity);
			this.grpboxPC.Controls.Add(this.labTimeoutIncativity);
			this.grpboxPC.Controls.Add(this.labTimeoutIncativityUnit);
			this.grpboxPC.Name = "grpboxPC";
			this.grpboxPC.TabStop = false;
			// 
			// chkboxStartup
			// 
			resources.ApplyResources(this.chkboxStartup, "chkboxStartup");
			this.chkboxStartup.Name = "chkboxStartup";
			this.chkboxStartup.UseVisualStyleBackColor = true;
			// 
			// grpboxTV
			// 
			resources.ApplyResources(this.grpboxTV, "grpboxTV");
			this.grpboxTV.Controls.Add(this.labTV);
			this.grpboxTV.Controls.Add(this.ipadrboxSendIP);
			this.grpboxTV.Controls.Add(this.numboxSendPort);
			this.grpboxTV.Controls.Add(this.phyadrboxPCMac);
			this.grpboxTV.Controls.Add(this.labTVPort);
			this.grpboxTV.Controls.Add(this.ipadrboxSubnetMask);
			this.grpboxTV.Controls.Add(this.btnTVInstall);
			this.grpboxTV.Controls.Add(this.labSendIP);
			this.grpboxTV.Controls.Add(this.btnTVRefresh);
			this.grpboxTV.Controls.Add(this.labSubnetMask);
			this.grpboxTV.Controls.Add(this.labTimeoutScreensaverUnit);
			this.grpboxTV.Controls.Add(this.labPCMac);
			this.grpboxTV.Controls.Add(this.labTimeoutRightClickUnit);
			this.grpboxTV.Controls.Add(this.labInput);
			this.grpboxTV.Controls.Add(this.numboxTimeoutScreensaver);
			this.grpboxTV.Controls.Add(this.labTimeoutRightClick);
			this.grpboxTV.Controls.Add(this.numboxTimeoutRightClick);
			this.grpboxTV.Controls.Add(this.labTimeoutScreensaver);
			this.grpboxTV.Controls.Add(this.cmbboxInput);
			this.grpboxTV.Controls.Add(this.cmbboxTV);
			this.grpboxTV.Name = "grpboxTV";
			this.grpboxTV.TabStop = false;
			// 
			// ipadrboxSendIP
			// 
			this.ipadrboxSendIP.BackColor = System.Drawing.SystemColors.Window;
			this.ipadrboxSendIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ipadrboxSendIP.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.ipadrboxSendIP, "ipadrboxSendIP");
			this.ipadrboxSendIP.Name = "ipadrboxSendIP";
			this.ipadrboxSendIP.Value = null;
			// 
			// phyadrboxPCMac
			// 
			this.phyadrboxPCMac.BackColor = System.Drawing.SystemColors.Window;
			resources.ApplyResources(this.phyadrboxPCMac, "phyadrboxPCMac");
			this.phyadrboxPCMac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.phyadrboxPCMac.ForeColor = System.Drawing.SystemColors.WindowText;
			this.phyadrboxPCMac.Name = "phyadrboxPCMac";
			this.phyadrboxPCMac.Value = null;
			// 
			// ipadrboxSubnetMask
			// 
			this.ipadrboxSubnetMask.BackColor = System.Drawing.SystemColors.Window;
			this.ipadrboxSubnetMask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ipadrboxSubnetMask.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.ipadrboxSubnetMask, "ipadrboxSubnetMask");
			this.ipadrboxSubnetMask.Name = "ipadrboxSubnetMask";
			this.ipadrboxSubnetMask.Value = null;
			// 
			// Setting
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpboxTV);
			this.Controls.Add(this.grpboxPC);
			this.Controls.Add(this.btnClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Setting";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Setting_FormClosing);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Setting_MouseDown);
			((System.ComponentModel.ISupportInitialize)(this.numboxListenPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numboxTimeoutInactivity)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numboxTimeoutRightClick)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numboxTimeoutScreensaver)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numboxSendPort)).EndInit();
			this.grpboxPC.ResumeLayout(false);
			this.grpboxPC.PerformLayout();
			this.grpboxTV.ResumeLayout(false);
			this.grpboxTV.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private IPAddressBox ipadrboxSendIP;
		private MacAddressBox phyadrboxPCMac;
		private IPAddressBox ipadrboxSubnetMask;
		private System.Windows.Forms.Label labSendIP;
		private System.Windows.Forms.Label labSubnetMask;
		private System.Windows.Forms.Label labPCMac;
		private System.Windows.Forms.Label labPCPort;
		private System.Windows.Forms.Label labInput;
		private System.Windows.Forms.Label labTV;
		private System.Windows.Forms.Label labTimeoutScreensaver;
		private System.Windows.Forms.Button btnPCSave;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.NumericUpDown numboxListenPort;
		private System.Windows.Forms.CheckBox chkboxInactivity;
		private System.Windows.Forms.NumericUpDown numboxTimeoutInactivity;
		private System.Windows.Forms.Label labTimeoutIncativity;
		private System.Windows.Forms.ComboBox cmbboxTV;
		private System.Windows.Forms.ComboBox cmbboxInput;
		private System.Windows.Forms.NumericUpDown numboxTimeoutRightClick;
		private System.Windows.Forms.NumericUpDown numboxTimeoutScreensaver;
		private System.Windows.Forms.Label labTimeoutIncativityUnit;
		private System.Windows.Forms.Label labTimeoutRightClickUnit;
		private System.Windows.Forms.Label labTimeoutScreensaverUnit;
		private System.Windows.Forms.Label labTimeoutRightClick;
		private System.Windows.Forms.Button btnTVRefresh;
		private System.Windows.Forms.Button btnTVInstall;
		private System.Windows.Forms.ToolTip ttFormating;
		private System.Windows.Forms.NumericUpDown numboxSendPort;
		private System.Windows.Forms.Label labTVPort;
		private System.Windows.Forms.GroupBox grpboxPC;
		private System.Windows.Forms.GroupBox grpboxTV;
		private System.Windows.Forms.CheckBox chkboxStartup;
	}
}