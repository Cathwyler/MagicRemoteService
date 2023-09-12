
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
			this.btnClose = new System.Windows.Forms.Button();
			this.ttFormating = new System.Windows.Forms.ToolTip(this.components);
			this.tabSetting = new System.Windows.Forms.TabControl();
			this.tabTV = new System.Windows.Forms.TabPage();
			this.chkboxExtend = new System.Windows.Forms.CheckBox();
			this.labTV = new System.Windows.Forms.Label();
			this.ipadrboxSendIP = new MagicRemoteService.IPAddressBox();
			this.numboxSendPort = new System.Windows.Forms.NumericUpDown();
			this.phyadrboxPCMac = new MagicRemoteService.PhysicalAddressBox();
			this.labTVPort = new System.Windows.Forms.Label();
			this.ipadrboxSubnetMask = new MagicRemoteService.IPAddressBox();
			this.btnTVInstall = new System.Windows.Forms.Button();
			this.labSendIP = new System.Windows.Forms.Label();
			this.btnTVRefresh = new System.Windows.Forms.Button();
			this.labSubnetMask = new System.Windows.Forms.Label();
			this.labPCMac = new System.Windows.Forms.Label();
			this.labTimeoutRightClickUnit = new System.Windows.Forms.Label();
			this.labInput = new System.Windows.Forms.Label();
			this.labTimeoutRightClick = new System.Windows.Forms.Label();
			this.numboxTimeoutRightClick = new System.Windows.Forms.NumericUpDown();
			this.cmbboxInput = new System.Windows.Forms.ComboBox();
			this.cmbboxTV = new System.Windows.Forms.ComboBox();
			this.tabPC = new System.Windows.Forms.TabPage();
			this.chkboxStartup = new System.Windows.Forms.CheckBox();
			this.labPCPort = new System.Windows.Forms.Label();
			this.btnPCSave = new System.Windows.Forms.Button();
			this.numboxListenPort = new System.Windows.Forms.NumericUpDown();
			this.chkboxInactivity = new System.Windows.Forms.CheckBox();
			this.numboxTimeoutInactivity = new System.Windows.Forms.NumericUpDown();
			this.labTimeoutIncativity = new System.Windows.Forms.Label();
			this.labTimeoutIncativityUnit = new System.Windows.Forms.Label();
			this.tabRemote = new System.Windows.Forms.TabPage();
			this.bcRemoteRewind = new MagicRemoteService.BindControl();
			this.bcRemoteFastForward = new MagicRemoteService.BindControl();
			this.bcRemotePause = new MagicRemoteService.BindControl();
			this.bcRemotePlay = new MagicRemoteService.BindControl();
			this.bcRemoteBackspace = new MagicRemoteService.BindControl();
			this.bcRemoteLongClick = new MagicRemoteService.BindControl();
			this.bcRemoteClick = new MagicRemoteService.BindControl();
			this.bcRemoteStop = new MagicRemoteService.BindControl();
			this.bcRemoteBlue = new MagicRemoteService.BindControl();
			this.bcRemoteYellow = new MagicRemoteService.BindControl();
			this.bcRemoteGreen = new MagicRemoteService.BindControl();
			this.bcRemoteRed = new MagicRemoteService.BindControl();
			this.bcRemoteBack = new MagicRemoteService.BindControl();
			this.bcRemoteOk = new MagicRemoteService.BindControl();
			this.bcRemoteDown = new MagicRemoteService.BindControl();
			this.bcRemoteRight = new MagicRemoteService.BindControl();
			this.bcRemoteLeft = new MagicRemoteService.BindControl();
			this.bcRemoteUp = new MagicRemoteService.BindControl();
			this.libRemoteBackspace = new System.Windows.Forms.Label();
			this.libRemoteStop = new System.Windows.Forms.Label();
			this.libRemoteRewind = new System.Windows.Forms.Label();
			this.libRemoteFastForward = new System.Windows.Forms.Label();
			this.libRemotePause = new System.Windows.Forms.Label();
			this.libRemotePlay = new System.Windows.Forms.Label();
			this.libRemoteBlue = new System.Windows.Forms.Label();
			this.libRemoteYellow = new System.Windows.Forms.Label();
			this.libRemoteGreen = new System.Windows.Forms.Label();
			this.libRemoteRed = new System.Windows.Forms.Label();
			this.libRemoteBack = new System.Windows.Forms.Label();
			this.libRemoteOk = new System.Windows.Forms.Label();
			this.libRemoteDown = new System.Windows.Forms.Label();
			this.libRemoteRight = new System.Windows.Forms.Label();
			this.libRemoteUp = new System.Windows.Forms.Label();
			this.libRemoteLeft = new System.Windows.Forms.Label();
			this.libRemoteLongClick = new System.Windows.Forms.Label();
			this.libRemoteClick = new System.Windows.Forms.Label();
			this.btnRemoteSave = new System.Windows.Forms.Button();
			this.tabSetting.SuspendLayout();
			this.tabTV.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numboxSendPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numboxTimeoutRightClick)).BeginInit();
			this.tabPC.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numboxListenPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numboxTimeoutInactivity)).BeginInit();
			this.tabRemote.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			resources.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.Name = "btnClose";
			this.ttFormating.SetToolTip(this.btnClose, resources.GetString("btnClose.ToolTip"));
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// ttFormating
			// 
			this.ttFormating.IsBalloon = true;
			this.ttFormating.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
			// 
			// tabSetting
			// 
			resources.ApplyResources(this.tabSetting, "tabSetting");
			this.tabSetting.Controls.Add(this.tabTV);
			this.tabSetting.Controls.Add(this.tabPC);
			this.tabSetting.Controls.Add(this.tabRemote);
			this.tabSetting.Name = "tabSetting";
			this.tabSetting.SelectedIndex = 0;
			this.ttFormating.SetToolTip(this.tabSetting, resources.GetString("tabSetting.ToolTip"));
			// 
			// tabTV
			// 
			resources.ApplyResources(this.tabTV, "tabTV");
			this.tabTV.Controls.Add(this.chkboxExtend);
			this.tabTV.Controls.Add(this.labTV);
			this.tabTV.Controls.Add(this.ipadrboxSendIP);
			this.tabTV.Controls.Add(this.numboxSendPort);
			this.tabTV.Controls.Add(this.phyadrboxPCMac);
			this.tabTV.Controls.Add(this.labTVPort);
			this.tabTV.Controls.Add(this.ipadrboxSubnetMask);
			this.tabTV.Controls.Add(this.btnTVInstall);
			this.tabTV.Controls.Add(this.labSendIP);
			this.tabTV.Controls.Add(this.btnTVRefresh);
			this.tabTV.Controls.Add(this.labSubnetMask);
			this.tabTV.Controls.Add(this.labPCMac);
			this.tabTV.Controls.Add(this.labTimeoutRightClickUnit);
			this.tabTV.Controls.Add(this.labInput);
			this.tabTV.Controls.Add(this.labTimeoutRightClick);
			this.tabTV.Controls.Add(this.numboxTimeoutRightClick);
			this.tabTV.Controls.Add(this.cmbboxInput);
			this.tabTV.Controls.Add(this.cmbboxTV);
			this.tabTV.Name = "tabTV";
			this.ttFormating.SetToolTip(this.tabTV, resources.GetString("tabTV.ToolTip"));
			this.tabTV.UseVisualStyleBackColor = true;
			// 
			// chkboxExtend
			// 
			resources.ApplyResources(this.chkboxExtend, "chkboxExtend");
			this.chkboxExtend.Name = "chkboxExtend";
			this.ttFormating.SetToolTip(this.chkboxExtend, resources.GetString("chkboxExtend.ToolTip"));
			this.chkboxExtend.UseVisualStyleBackColor = true;
			// 
			// labTV
			// 
			resources.ApplyResources(this.labTV, "labTV");
			this.labTV.Name = "labTV";
			this.ttFormating.SetToolTip(this.labTV, resources.GetString("labTV.ToolTip"));
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
			// labTVPort
			// 
			resources.ApplyResources(this.labTVPort, "labTVPort");
			this.labTVPort.Name = "labTVPort";
			this.ttFormating.SetToolTip(this.labTVPort, resources.GetString("labTVPort.ToolTip"));
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
			// btnTVInstall
			// 
			resources.ApplyResources(this.btnTVInstall, "btnTVInstall");
			this.btnTVInstall.Name = "btnTVInstall";
			this.ttFormating.SetToolTip(this.btnTVInstall, resources.GetString("btnTVInstall.ToolTip"));
			this.btnTVInstall.UseVisualStyleBackColor = true;
			this.btnTVInstall.Click += new System.EventHandler(this.btnTVInstall_Click);
			// 
			// labSendIP
			// 
			resources.ApplyResources(this.labSendIP, "labSendIP");
			this.labSendIP.Name = "labSendIP";
			this.ttFormating.SetToolTip(this.labSendIP, resources.GetString("labSendIP.ToolTip"));
			// 
			// btnTVRefresh
			// 
			resources.ApplyResources(this.btnTVRefresh, "btnTVRefresh");
			this.btnTVRefresh.Name = "btnTVRefresh";
			this.ttFormating.SetToolTip(this.btnTVRefresh, resources.GetString("btnTVRefresh.ToolTip"));
			this.btnTVRefresh.UseVisualStyleBackColor = true;
			this.btnTVRefresh.Click += new System.EventHandler(this.btnTVRefresh_Click);
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
			// labTimeoutRightClickUnit
			// 
			resources.ApplyResources(this.labTimeoutRightClickUnit, "labTimeoutRightClickUnit");
			this.labTimeoutRightClickUnit.Name = "labTimeoutRightClickUnit";
			this.ttFormating.SetToolTip(this.labTimeoutRightClickUnit, resources.GetString("labTimeoutRightClickUnit.ToolTip"));
			// 
			// labInput
			// 
			resources.ApplyResources(this.labInput, "labInput");
			this.labInput.Name = "labInput";
			this.ttFormating.SetToolTip(this.labInput, resources.GetString("labInput.ToolTip"));
			// 
			// labTimeoutRightClick
			// 
			resources.ApplyResources(this.labTimeoutRightClick, "labTimeoutRightClick");
			this.labTimeoutRightClick.Name = "labTimeoutRightClick";
			this.ttFormating.SetToolTip(this.labTimeoutRightClick, resources.GetString("labTimeoutRightClick.ToolTip"));
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
			// tabPC
			// 
			resources.ApplyResources(this.tabPC, "tabPC");
			this.tabPC.Controls.Add(this.chkboxStartup);
			this.tabPC.Controls.Add(this.labPCPort);
			this.tabPC.Controls.Add(this.btnPCSave);
			this.tabPC.Controls.Add(this.numboxListenPort);
			this.tabPC.Controls.Add(this.chkboxInactivity);
			this.tabPC.Controls.Add(this.numboxTimeoutInactivity);
			this.tabPC.Controls.Add(this.labTimeoutIncativity);
			this.tabPC.Controls.Add(this.labTimeoutIncativityUnit);
			this.tabPC.Name = "tabPC";
			this.ttFormating.SetToolTip(this.tabPC, resources.GetString("tabPC.ToolTip"));
			this.tabPC.UseVisualStyleBackColor = true;
			// 
			// chkboxStartup
			// 
			resources.ApplyResources(this.chkboxStartup, "chkboxStartup");
			this.chkboxStartup.Name = "chkboxStartup";
			this.ttFormating.SetToolTip(this.chkboxStartup, resources.GetString("chkboxStartup.ToolTip"));
			this.chkboxStartup.UseVisualStyleBackColor = true;
			// 
			// labPCPort
			// 
			resources.ApplyResources(this.labPCPort, "labPCPort");
			this.labPCPort.Name = "labPCPort";
			this.ttFormating.SetToolTip(this.labPCPort, resources.GetString("labPCPort.ToolTip"));
			// 
			// btnPCSave
			// 
			resources.ApplyResources(this.btnPCSave, "btnPCSave");
			this.btnPCSave.Name = "btnPCSave";
			this.ttFormating.SetToolTip(this.btnPCSave, resources.GetString("btnPCSave.ToolTip"));
			this.btnPCSave.UseVisualStyleBackColor = true;
			this.btnPCSave.Click += new System.EventHandler(this.btnPCSave_Click);
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
			this.numboxTimeoutInactivity.Minimum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
			this.numboxTimeoutInactivity.Name = "numboxTimeoutInactivity";
			this.ttFormating.SetToolTip(this.numboxTimeoutInactivity, resources.GetString("numboxTimeoutInactivity.ToolTip"));
			this.numboxTimeoutInactivity.Value = new decimal(new int[] {
            300000,
            0,
            0,
            0});
			// 
			// labTimeoutIncativity
			// 
			resources.ApplyResources(this.labTimeoutIncativity, "labTimeoutIncativity");
			this.labTimeoutIncativity.Name = "labTimeoutIncativity";
			this.ttFormating.SetToolTip(this.labTimeoutIncativity, resources.GetString("labTimeoutIncativity.ToolTip"));
			// 
			// labTimeoutIncativityUnit
			// 
			resources.ApplyResources(this.labTimeoutIncativityUnit, "labTimeoutIncativityUnit");
			this.labTimeoutIncativityUnit.Name = "labTimeoutIncativityUnit";
			this.ttFormating.SetToolTip(this.labTimeoutIncativityUnit, resources.GetString("labTimeoutIncativityUnit.ToolTip"));
			// 
			// tabRemote
			// 
			resources.ApplyResources(this.tabRemote, "tabRemote");
			this.tabRemote.Controls.Add(this.bcRemoteRewind);
			this.tabRemote.Controls.Add(this.bcRemoteFastForward);
			this.tabRemote.Controls.Add(this.bcRemotePause);
			this.tabRemote.Controls.Add(this.bcRemotePlay);
			this.tabRemote.Controls.Add(this.bcRemoteBackspace);
			this.tabRemote.Controls.Add(this.bcRemoteLongClick);
			this.tabRemote.Controls.Add(this.bcRemoteClick);
			this.tabRemote.Controls.Add(this.bcRemoteStop);
			this.tabRemote.Controls.Add(this.bcRemoteBlue);
			this.tabRemote.Controls.Add(this.bcRemoteYellow);
			this.tabRemote.Controls.Add(this.bcRemoteGreen);
			this.tabRemote.Controls.Add(this.bcRemoteRed);
			this.tabRemote.Controls.Add(this.bcRemoteBack);
			this.tabRemote.Controls.Add(this.bcRemoteOk);
			this.tabRemote.Controls.Add(this.bcRemoteDown);
			this.tabRemote.Controls.Add(this.bcRemoteRight);
			this.tabRemote.Controls.Add(this.bcRemoteLeft);
			this.tabRemote.Controls.Add(this.bcRemoteUp);
			this.tabRemote.Controls.Add(this.libRemoteBackspace);
			this.tabRemote.Controls.Add(this.libRemoteStop);
			this.tabRemote.Controls.Add(this.libRemoteRewind);
			this.tabRemote.Controls.Add(this.libRemoteFastForward);
			this.tabRemote.Controls.Add(this.libRemotePause);
			this.tabRemote.Controls.Add(this.libRemotePlay);
			this.tabRemote.Controls.Add(this.libRemoteBlue);
			this.tabRemote.Controls.Add(this.libRemoteYellow);
			this.tabRemote.Controls.Add(this.libRemoteGreen);
			this.tabRemote.Controls.Add(this.libRemoteRed);
			this.tabRemote.Controls.Add(this.libRemoteBack);
			this.tabRemote.Controls.Add(this.libRemoteOk);
			this.tabRemote.Controls.Add(this.libRemoteDown);
			this.tabRemote.Controls.Add(this.libRemoteRight);
			this.tabRemote.Controls.Add(this.libRemoteUp);
			this.tabRemote.Controls.Add(this.libRemoteLeft);
			this.tabRemote.Controls.Add(this.libRemoteLongClick);
			this.tabRemote.Controls.Add(this.libRemoteClick);
			this.tabRemote.Controls.Add(this.btnRemoteSave);
			this.tabRemote.Name = "tabRemote";
			this.ttFormating.SetToolTip(this.tabRemote, resources.GetString("tabRemote.ToolTip"));
			this.tabRemote.UseVisualStyleBackColor = true;
			// 
			// bcRemoteRewind
			// 
			resources.ApplyResources(this.bcRemoteRewind, "bcRemoteRewind");
			this.bcRemoteRewind.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteRewind.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteRewind.Name = "bcRemoteRewind";
			this.ttFormating.SetToolTip(this.bcRemoteRewind, resources.GetString("bcRemoteRewind.ToolTip"));
			this.bcRemoteRewind.Value = null;
			// 
			// bcRemoteFastForward
			// 
			resources.ApplyResources(this.bcRemoteFastForward, "bcRemoteFastForward");
			this.bcRemoteFastForward.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteFastForward.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteFastForward.Name = "bcRemoteFastForward";
			this.ttFormating.SetToolTip(this.bcRemoteFastForward, resources.GetString("bcRemoteFastForward.ToolTip"));
			this.bcRemoteFastForward.Value = null;
			// 
			// bcRemotePause
			// 
			resources.ApplyResources(this.bcRemotePause, "bcRemotePause");
			this.bcRemotePause.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemotePause.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemotePause.Name = "bcRemotePause";
			this.ttFormating.SetToolTip(this.bcRemotePause, resources.GetString("bcRemotePause.ToolTip"));
			this.bcRemotePause.Value = null;
			// 
			// bcRemotePlay
			// 
			resources.ApplyResources(this.bcRemotePlay, "bcRemotePlay");
			this.bcRemotePlay.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemotePlay.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemotePlay.Name = "bcRemotePlay";
			this.ttFormating.SetToolTip(this.bcRemotePlay, resources.GetString("bcRemotePlay.ToolTip"));
			this.bcRemotePlay.Value = null;
			// 
			// bcRemoteBackspace
			// 
			resources.ApplyResources(this.bcRemoteBackspace, "bcRemoteBackspace");
			this.bcRemoteBackspace.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteBackspace.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteBackspace.Name = "bcRemoteBackspace";
			this.ttFormating.SetToolTip(this.bcRemoteBackspace, resources.GetString("bcRemoteBackspace.ToolTip"));
			this.bcRemoteBackspace.Value = null;
			// 
			// bcRemoteLongClick
			// 
			resources.ApplyResources(this.bcRemoteLongClick, "bcRemoteLongClick");
			this.bcRemoteLongClick.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteLongClick.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteLongClick.Name = "bcRemoteLongClick";
			this.ttFormating.SetToolTip(this.bcRemoteLongClick, resources.GetString("bcRemoteLongClick.ToolTip"));
			this.bcRemoteLongClick.Value = null;
			// 
			// bcRemoteClick
			// 
			resources.ApplyResources(this.bcRemoteClick, "bcRemoteClick");
			this.bcRemoteClick.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteClick.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteClick.Name = "bcRemoteClick";
			this.ttFormating.SetToolTip(this.bcRemoteClick, resources.GetString("bcRemoteClick.ToolTip"));
			this.bcRemoteClick.Value = null;
			// 
			// bcRemoteStop
			// 
			resources.ApplyResources(this.bcRemoteStop, "bcRemoteStop");
			this.bcRemoteStop.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteStop.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteStop.Name = "bcRemoteStop";
			this.ttFormating.SetToolTip(this.bcRemoteStop, resources.GetString("bcRemoteStop.ToolTip"));
			this.bcRemoteStop.Value = null;
			// 
			// bcRemoteBlue
			// 
			resources.ApplyResources(this.bcRemoteBlue, "bcRemoteBlue");
			this.bcRemoteBlue.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteBlue.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteBlue.Name = "bcRemoteBlue";
			this.ttFormating.SetToolTip(this.bcRemoteBlue, resources.GetString("bcRemoteBlue.ToolTip"));
			this.bcRemoteBlue.Value = null;
			// 
			// bcRemoteYellow
			// 
			resources.ApplyResources(this.bcRemoteYellow, "bcRemoteYellow");
			this.bcRemoteYellow.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteYellow.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteYellow.Name = "bcRemoteYellow";
			this.ttFormating.SetToolTip(this.bcRemoteYellow, resources.GetString("bcRemoteYellow.ToolTip"));
			this.bcRemoteYellow.Value = null;
			// 
			// bcRemoteGreen
			// 
			resources.ApplyResources(this.bcRemoteGreen, "bcRemoteGreen");
			this.bcRemoteGreen.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteGreen.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteGreen.Name = "bcRemoteGreen";
			this.ttFormating.SetToolTip(this.bcRemoteGreen, resources.GetString("bcRemoteGreen.ToolTip"));
			this.bcRemoteGreen.Value = null;
			// 
			// bcRemoteRed
			// 
			resources.ApplyResources(this.bcRemoteRed, "bcRemoteRed");
			this.bcRemoteRed.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteRed.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteRed.Name = "bcRemoteRed";
			this.ttFormating.SetToolTip(this.bcRemoteRed, resources.GetString("bcRemoteRed.ToolTip"));
			this.bcRemoteRed.Value = null;
			// 
			// bcRemoteBack
			// 
			resources.ApplyResources(this.bcRemoteBack, "bcRemoteBack");
			this.bcRemoteBack.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteBack.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteBack.Name = "bcRemoteBack";
			this.ttFormating.SetToolTip(this.bcRemoteBack, resources.GetString("bcRemoteBack.ToolTip"));
			this.bcRemoteBack.Value = null;
			// 
			// bcRemoteOk
			// 
			resources.ApplyResources(this.bcRemoteOk, "bcRemoteOk");
			this.bcRemoteOk.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteOk.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteOk.Name = "bcRemoteOk";
			this.ttFormating.SetToolTip(this.bcRemoteOk, resources.GetString("bcRemoteOk.ToolTip"));
			this.bcRemoteOk.Value = null;
			// 
			// bcRemoteDown
			// 
			resources.ApplyResources(this.bcRemoteDown, "bcRemoteDown");
			this.bcRemoteDown.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteDown.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteDown.Name = "bcRemoteDown";
			this.ttFormating.SetToolTip(this.bcRemoteDown, resources.GetString("bcRemoteDown.ToolTip"));
			this.bcRemoteDown.Value = null;
			// 
			// bcRemoteRight
			// 
			resources.ApplyResources(this.bcRemoteRight, "bcRemoteRight");
			this.bcRemoteRight.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteRight.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteRight.Name = "bcRemoteRight";
			this.ttFormating.SetToolTip(this.bcRemoteRight, resources.GetString("bcRemoteRight.ToolTip"));
			this.bcRemoteRight.Value = null;
			// 
			// bcRemoteLeft
			// 
			resources.ApplyResources(this.bcRemoteLeft, "bcRemoteLeft");
			this.bcRemoteLeft.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteLeft.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteLeft.Name = "bcRemoteLeft";
			this.ttFormating.SetToolTip(this.bcRemoteLeft, resources.GetString("bcRemoteLeft.ToolTip"));
			this.bcRemoteLeft.Value = null;
			// 
			// bcRemoteUp
			// 
			resources.ApplyResources(this.bcRemoteUp, "bcRemoteUp");
			this.bcRemoteUp.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteUp.ForeColor = System.Drawing.SystemColors.WindowText;
			this.bcRemoteUp.Name = "bcRemoteUp";
			this.ttFormating.SetToolTip(this.bcRemoteUp, resources.GetString("bcRemoteUp.ToolTip"));
			this.bcRemoteUp.Value = null;
			// 
			// libRemoteBackspace
			// 
			resources.ApplyResources(this.libRemoteBackspace, "libRemoteBackspace");
			this.libRemoteBackspace.Name = "libRemoteBackspace";
			this.ttFormating.SetToolTip(this.libRemoteBackspace, resources.GetString("libRemoteBackspace.ToolTip"));
			// 
			// libRemoteStop
			// 
			resources.ApplyResources(this.libRemoteStop, "libRemoteStop");
			this.libRemoteStop.Name = "libRemoteStop";
			this.ttFormating.SetToolTip(this.libRemoteStop, resources.GetString("libRemoteStop.ToolTip"));
			// 
			// libRemoteRewind
			// 
			resources.ApplyResources(this.libRemoteRewind, "libRemoteRewind");
			this.libRemoteRewind.Name = "libRemoteRewind";
			this.ttFormating.SetToolTip(this.libRemoteRewind, resources.GetString("libRemoteRewind.ToolTip"));
			// 
			// libRemoteFastForward
			// 
			resources.ApplyResources(this.libRemoteFastForward, "libRemoteFastForward");
			this.libRemoteFastForward.Name = "libRemoteFastForward";
			this.ttFormating.SetToolTip(this.libRemoteFastForward, resources.GetString("libRemoteFastForward.ToolTip"));
			// 
			// libRemotePause
			// 
			resources.ApplyResources(this.libRemotePause, "libRemotePause");
			this.libRemotePause.Name = "libRemotePause";
			this.ttFormating.SetToolTip(this.libRemotePause, resources.GetString("libRemotePause.ToolTip"));
			// 
			// libRemotePlay
			// 
			resources.ApplyResources(this.libRemotePlay, "libRemotePlay");
			this.libRemotePlay.Name = "libRemotePlay";
			this.ttFormating.SetToolTip(this.libRemotePlay, resources.GetString("libRemotePlay.ToolTip"));
			// 
			// libRemoteBlue
			// 
			resources.ApplyResources(this.libRemoteBlue, "libRemoteBlue");
			this.libRemoteBlue.Name = "libRemoteBlue";
			this.ttFormating.SetToolTip(this.libRemoteBlue, resources.GetString("libRemoteBlue.ToolTip"));
			// 
			// libRemoteYellow
			// 
			resources.ApplyResources(this.libRemoteYellow, "libRemoteYellow");
			this.libRemoteYellow.Name = "libRemoteYellow";
			this.ttFormating.SetToolTip(this.libRemoteYellow, resources.GetString("libRemoteYellow.ToolTip"));
			// 
			// libRemoteGreen
			// 
			resources.ApplyResources(this.libRemoteGreen, "libRemoteGreen");
			this.libRemoteGreen.Name = "libRemoteGreen";
			this.ttFormating.SetToolTip(this.libRemoteGreen, resources.GetString("libRemoteGreen.ToolTip"));
			// 
			// libRemoteRed
			// 
			resources.ApplyResources(this.libRemoteRed, "libRemoteRed");
			this.libRemoteRed.Name = "libRemoteRed";
			this.ttFormating.SetToolTip(this.libRemoteRed, resources.GetString("libRemoteRed.ToolTip"));
			// 
			// libRemoteBack
			// 
			resources.ApplyResources(this.libRemoteBack, "libRemoteBack");
			this.libRemoteBack.Name = "libRemoteBack";
			this.ttFormating.SetToolTip(this.libRemoteBack, resources.GetString("libRemoteBack.ToolTip"));
			// 
			// libRemoteOk
			// 
			resources.ApplyResources(this.libRemoteOk, "libRemoteOk");
			this.libRemoteOk.Name = "libRemoteOk";
			this.ttFormating.SetToolTip(this.libRemoteOk, resources.GetString("libRemoteOk.ToolTip"));
			// 
			// libRemoteDown
			// 
			resources.ApplyResources(this.libRemoteDown, "libRemoteDown");
			this.libRemoteDown.Name = "libRemoteDown";
			this.ttFormating.SetToolTip(this.libRemoteDown, resources.GetString("libRemoteDown.ToolTip"));
			// 
			// libRemoteRight
			// 
			resources.ApplyResources(this.libRemoteRight, "libRemoteRight");
			this.libRemoteRight.Name = "libRemoteRight";
			this.ttFormating.SetToolTip(this.libRemoteRight, resources.GetString("libRemoteRight.ToolTip"));
			// 
			// libRemoteUp
			// 
			resources.ApplyResources(this.libRemoteUp, "libRemoteUp");
			this.libRemoteUp.Name = "libRemoteUp";
			this.ttFormating.SetToolTip(this.libRemoteUp, resources.GetString("libRemoteUp.ToolTip"));
			// 
			// libRemoteLeft
			// 
			resources.ApplyResources(this.libRemoteLeft, "libRemoteLeft");
			this.libRemoteLeft.Name = "libRemoteLeft";
			this.ttFormating.SetToolTip(this.libRemoteLeft, resources.GetString("libRemoteLeft.ToolTip"));
			// 
			// libRemoteLongClick
			// 
			resources.ApplyResources(this.libRemoteLongClick, "libRemoteLongClick");
			this.libRemoteLongClick.Name = "libRemoteLongClick";
			this.ttFormating.SetToolTip(this.libRemoteLongClick, resources.GetString("libRemoteLongClick.ToolTip"));
			// 
			// libRemoteClick
			// 
			resources.ApplyResources(this.libRemoteClick, "libRemoteClick");
			this.libRemoteClick.Name = "libRemoteClick";
			this.ttFormating.SetToolTip(this.libRemoteClick, resources.GetString("libRemoteClick.ToolTip"));
			// 
			// btnRemoteSave
			// 
			resources.ApplyResources(this.btnRemoteSave, "btnRemoteSave");
			this.btnRemoteSave.Name = "btnRemoteSave";
			this.ttFormating.SetToolTip(this.btnRemoteSave, resources.GetString("btnRemoteSave.ToolTip"));
			this.btnRemoteSave.UseVisualStyleBackColor = true;
			this.btnRemoteSave.Click += new System.EventHandler(this.btnRemoteSave_Click);
			// 
			// Setting
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabSetting);
			this.Controls.Add(this.btnClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Setting";
			this.ttFormating.SetToolTip(this, resources.GetString("$this.ToolTip"));
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Setting_FormClosing);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Setting_MouseDown);
			this.tabSetting.ResumeLayout(false);
			this.tabTV.ResumeLayout(false);
			this.tabTV.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numboxSendPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numboxTimeoutRightClick)).EndInit();
			this.tabPC.ResumeLayout(false);
			this.tabPC.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numboxListenPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numboxTimeoutInactivity)).EndInit();
			this.tabRemote.ResumeLayout(false);
			this.tabRemote.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ToolTip ttFormating;
		private System.Windows.Forms.TabControl tabSetting;
		private System.Windows.Forms.TabPage tabTV;
		private System.Windows.Forms.CheckBox chkboxExtend;
		private System.Windows.Forms.Label labTV;
		private IPAddressBox ipadrboxSendIP;
		private System.Windows.Forms.NumericUpDown numboxSendPort;
		private PhysicalAddressBox phyadrboxPCMac;
		private System.Windows.Forms.Label labTVPort;
		private IPAddressBox ipadrboxSubnetMask;
		private System.Windows.Forms.Button btnTVInstall;
		private System.Windows.Forms.Label labSendIP;
		private System.Windows.Forms.Button btnTVRefresh;
		private System.Windows.Forms.Label labSubnetMask;
		private System.Windows.Forms.Label labPCMac;
		private System.Windows.Forms.Label labTimeoutRightClickUnit;
		private System.Windows.Forms.Label labInput;
		private System.Windows.Forms.Label labTimeoutRightClick;
		private System.Windows.Forms.NumericUpDown numboxTimeoutRightClick;
		private System.Windows.Forms.ComboBox cmbboxInput;
		private System.Windows.Forms.ComboBox cmbboxTV;
		private System.Windows.Forms.TabPage tabPC;
		private System.Windows.Forms.CheckBox chkboxStartup;
		private System.Windows.Forms.Label labPCPort;
		private System.Windows.Forms.Button btnPCSave;
		private System.Windows.Forms.NumericUpDown numboxListenPort;
		private System.Windows.Forms.CheckBox chkboxInactivity;
		private System.Windows.Forms.NumericUpDown numboxTimeoutInactivity;
		private System.Windows.Forms.Label labTimeoutIncativity;
		private System.Windows.Forms.Label labTimeoutIncativityUnit;
		private System.Windows.Forms.TabPage tabRemote;
		private System.Windows.Forms.Button btnRemoteSave;
		private System.Windows.Forms.Label libRemoteGreen;
		private System.Windows.Forms.Label libRemoteRed;
		private System.Windows.Forms.Label libRemoteBack;
		private System.Windows.Forms.Label libRemoteOk;
		private System.Windows.Forms.Label libRemoteDown;
		private System.Windows.Forms.Label libRemoteRight;
		private System.Windows.Forms.Label libRemoteUp;
		private System.Windows.Forms.Label libRemoteLeft;
		private System.Windows.Forms.Label libRemoteLongClick;
		private System.Windows.Forms.Label libRemoteClick;
		private System.Windows.Forms.Label libRemoteStop;
		private System.Windows.Forms.Label libRemoteRewind;
		private System.Windows.Forms.Label libRemotePause;
		private System.Windows.Forms.Label libRemotePlay;
		private System.Windows.Forms.Label libRemoteBlue;
		private System.Windows.Forms.Label libRemoteYellow;
		private System.Windows.Forms.Label libRemoteFastForward;
		private System.Windows.Forms.Label libRemoteBackspace;
		private BindControl bcRemoteRewind;
		private BindControl bcRemoteFastForward;
		private BindControl bcRemotePause;
		private BindControl bcRemotePlay;
		private BindControl bcRemoteBackspace;
		private BindControl bcRemoteLongClick;
		private BindControl bcRemoteClick;
		private BindControl bcRemoteStop;
		private BindControl bcRemoteBlue;
		private BindControl bcRemoteYellow;
		private BindControl bcRemoteGreen;
		private BindControl bcRemoteRed;
		private BindControl bcRemoteBack;
		private BindControl bcRemoteOk;
		private BindControl bcRemoteDown;
		private BindControl bcRemoteRight;
		private BindControl bcRemoteLeft;
		private BindControl bcRemoteUp;
	}
}