
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
			this.ttFormating.SetToolTip(this.labSendIP, resources.GetString("labSendIP.ToolTip"));
			// 
			// labSubnetMask
			// 
			resources.ApplyResources(this.labSubnetMask, "labSubnetMask");
			this.labSubnetMask.Name = "labSubnetMask";
			this.ttFormating.SetToolTip(this.labSubnetMask, resources.GetString("labSubnetMask.ToolTip"));
			// 
			// labPCMac
			// 
			resources.ApplyResources(this.labPCMac, "labPCMac");
			this.labPCMac.Name = "labPCMac";
			this.ttFormating.SetToolTip(this.labPCMac, resources.GetString("labPCMac.ToolTip"));
			// 
			// labPCPort
			// 
			resources.ApplyResources(this.labPCPort, "labPCPort");
			this.labPCPort.Name = "labPCPort";
			this.ttFormating.SetToolTip(this.labPCPort, resources.GetString("labPCPort.ToolTip"));
			// 
			// labInput
			// 
			resources.ApplyResources(this.labInput, "labInput");
			this.labInput.Name = "labInput";
			this.ttFormating.SetToolTip(this.labInput, resources.GetString("labInput.ToolTip"));
			// 
			// labTV
			// 
			resources.ApplyResources(this.labTV, "labTV");
			this.labTV.Name = "labTV";
			this.ttFormating.SetToolTip(this.labTV, resources.GetString("labTV.ToolTip"));
			// 
			// labTimeoutRightClick
			// 
			resources.ApplyResources(this.labTimeoutRightClick, "labTimeoutRightClick");
			this.labTimeoutRightClick.Name = "labTimeoutRightClick";
			this.ttFormating.SetToolTip(this.labTimeoutRightClick, resources.GetString("labTimeoutRightClick.ToolTip"));
			// 
			// labTimeoutScreensaver
			// 
			resources.ApplyResources(this.labTimeoutScreensaver, "labTimeoutScreensaver");
			this.labTimeoutScreensaver.Name = "labTimeoutScreensaver";
			this.ttFormating.SetToolTip(this.labTimeoutScreensaver, resources.GetString("labTimeoutScreensaver.ToolTip"));
			// 
			// btnPCSave
			// 
			resources.ApplyResources(this.btnPCSave, "btnPCSave");
			this.btnPCSave.Name = "btnPCSave";
			this.ttFormating.SetToolTip(this.btnPCSave, resources.GetString("btnPCSave.ToolTip"));
			this.btnPCSave.UseVisualStyleBackColor = true;
			this.btnPCSave.Click += new System.EventHandler(this.btnPCSave_Click);
			// 
			// btnClose
			// 
			resources.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.Name = "btnClose";
			this.ttFormating.SetToolTip(this.btnClose, resources.GetString("btnClose.ToolTip"));
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
			this.ttFormating.SetToolTip(this.numboxListenPort, resources.GetString("numboxListenPort.ToolTip"));
			// 
			// chkboxInactivity
			// 
			resources.ApplyResources(this.chkboxInactivity, "chkboxInactivity");
			this.chkboxInactivity.Name = "chkboxInactivity";
			this.ttFormating.SetToolTip(this.chkboxInactivity, resources.GetString("chkboxInactivity.ToolTip"));
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
			this.ttFormating.SetToolTip(this.numboxTimeoutInactivity, resources.GetString("numboxTimeoutInactivity.ToolTip"));
			// 
			// labTimeoutIncativity
			// 
			resources.ApplyResources(this.labTimeoutIncativity, "labTimeoutIncativity");
			this.labTimeoutIncativity.Name = "labTimeoutIncativity";
			this.ttFormating.SetToolTip(this.labTimeoutIncativity, resources.GetString("labTimeoutIncativity.ToolTip"));
			// 
			// cmbboxTV
			// 
			resources.ApplyResources(this.cmbboxTV, "cmbboxTV");
			this.cmbboxTV.DisplayMember = "Name";
			this.cmbboxTV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbboxTV.FormattingEnabled = true;
			this.cmbboxTV.Name = "cmbboxTV";
			this.ttFormating.SetToolTip(this.cmbboxTV, resources.GetString("cmbboxTV.ToolTip"));
			this.cmbboxTV.ValueMember = "Name";
			this.cmbboxTV.SelectedIndexChanged += new System.EventHandler(this.cmbboxTV_SelectedIndexChanged);
			// 
			// cmbboxInput
			// 
			resources.ApplyResources(this.cmbboxInput, "cmbboxInput");
			this.cmbboxInput.DisplayMember = "Name";
			this.cmbboxInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbboxInput.FormattingEnabled = true;
			this.cmbboxInput.Name = "cmbboxInput";
			this.ttFormating.SetToolTip(this.cmbboxInput, resources.GetString("cmbboxInput.ToolTip"));
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
			this.ttFormating.SetToolTip(this.numboxTimeoutRightClick, resources.GetString("numboxTimeoutRightClick.ToolTip"));
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
			this.ttFormating.SetToolTip(this.numboxTimeoutScreensaver, resources.GetString("numboxTimeoutScreensaver.ToolTip"));
			// 
			// labTimeoutIncativityUnit
			// 
			resources.ApplyResources(this.labTimeoutIncativityUnit, "labTimeoutIncativityUnit");
			this.labTimeoutIncativityUnit.Name = "labTimeoutIncativityUnit";
			this.ttFormating.SetToolTip(this.labTimeoutIncativityUnit, resources.GetString("labTimeoutIncativityUnit.ToolTip"));
			// 
			// labTimeoutRightClickUnit
			// 
			resources.ApplyResources(this.labTimeoutRightClickUnit, "labTimeoutRightClickUnit");
			this.labTimeoutRightClickUnit.Name = "labTimeoutRightClickUnit";
			this.ttFormating.SetToolTip(this.labTimeoutRightClickUnit, resources.GetString("labTimeoutRightClickUnit.ToolTip"));
			// 
			// labTimeoutScreensaverUnit
			// 
			resources.ApplyResources(this.labTimeoutScreensaverUnit, "labTimeoutScreensaverUnit");
			this.labTimeoutScreensaverUnit.Name = "labTimeoutScreensaverUnit";
			this.ttFormating.SetToolTip(this.labTimeoutScreensaverUnit, resources.GetString("labTimeoutScreensaverUnit.ToolTip"));
			// 
			// btnTVRefresh
			// 
			resources.ApplyResources(this.btnTVRefresh, "btnTVRefresh");
			this.btnTVRefresh.Name = "btnTVRefresh";
			this.ttFormating.SetToolTip(this.btnTVRefresh, resources.GetString("btnTVRefresh.ToolTip"));
			this.btnTVRefresh.UseVisualStyleBackColor = true;
			this.btnTVRefresh.Click += new System.EventHandler(this.btnTVRefresh_Click);
			// 
			// btnTVInstall
			// 
			resources.ApplyResources(this.btnTVInstall, "btnTVInstall");
			this.btnTVInstall.Name = "btnTVInstall";
			this.ttFormating.SetToolTip(this.btnTVInstall, resources.GetString("btnTVInstall.ToolTip"));
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
			this.ttFormating.SetToolTip(this.numboxSendPort, resources.GetString("numboxSendPort.ToolTip"));
			// 
			// labTVPort
			// 
			resources.ApplyResources(this.labTVPort, "labTVPort");
			this.labTVPort.Name = "labTVPort";
			this.ttFormating.SetToolTip(this.labTVPort, resources.GetString("labTVPort.ToolTip"));
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
			this.ttFormating.SetToolTip(this.grpboxPC, resources.GetString("grpboxPC.ToolTip"));
			// 
			// chkboxStartup
			// 
			resources.ApplyResources(this.chkboxStartup, "chkboxStartup");
			this.chkboxStartup.Name = "chkboxStartup";
			this.ttFormating.SetToolTip(this.chkboxStartup, resources.GetString("chkboxStartup.ToolTip"));
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
			this.ttFormating.SetToolTip(this.grpboxTV, resources.GetString("grpboxTV.ToolTip"));
			// 
			// ipadrboxSendIP
			// 
			resources.ApplyResources(this.ipadrboxSendIP, "ipadrboxSendIP");
			this.ipadrboxSendIP.BackColor = System.Drawing.SystemColors.Window;
			this.ipadrboxSendIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ipadrboxSendIP.ForeColor = System.Drawing.SystemColors.WindowText;
			this.ipadrboxSendIP.Name = "ipadrboxSendIP";
			this.ttFormating.SetToolTip(this.ipadrboxSendIP, resources.GetString("ipadrboxSendIP.ToolTip"));
			this.ipadrboxSendIP.Value = null;
			// 
			// phyadrboxPCMac
			// 
			resources.ApplyResources(this.phyadrboxPCMac, "phyadrboxPCMac");
			this.phyadrboxPCMac.BackColor = System.Drawing.SystemColors.Window;
			this.phyadrboxPCMac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.phyadrboxPCMac.ForeColor = System.Drawing.SystemColors.WindowText;
			this.phyadrboxPCMac.Name = "phyadrboxPCMac";
			this.ttFormating.SetToolTip(this.phyadrboxPCMac, resources.GetString("phyadrboxPCMac.ToolTip"));
			this.phyadrboxPCMac.Value = null;
			// 
			// ipadrboxSubnetMask
			// 
			resources.ApplyResources(this.ipadrboxSubnetMask, "ipadrboxSubnetMask");
			this.ipadrboxSubnetMask.BackColor = System.Drawing.SystemColors.Window;
			this.ipadrboxSubnetMask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ipadrboxSubnetMask.ForeColor = System.Drawing.SystemColors.WindowText;
			this.ipadrboxSubnetMask.Name = "ipadrboxSubnetMask";
			this.ttFormating.SetToolTip(this.ipadrboxSubnetMask, resources.GetString("ipadrboxSubnetMask.ToolTip"));
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
			this.ttFormating.SetToolTip(this, resources.GetString("$this.ToolTip"));
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