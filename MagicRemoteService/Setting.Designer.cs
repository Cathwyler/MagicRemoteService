
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
			this.cbInputDirect = new System.Windows.Forms.CheckBox();
			this.btnTVInspect = new System.Windows.Forms.Button();
			this.labDisplay = new System.Windows.Forms.Label();
			this.cbbDisplay = new System.Windows.Forms.ComboBox();
			this.btnTVRemove = new System.Windows.Forms.Button();
			this.btnTVModify = new System.Windows.Forms.Button();
			this.btnTVAdd = new System.Windows.Forms.Button();
			this.cbExtend = new System.Windows.Forms.CheckBox();
			this.labTV = new System.Windows.Forms.Label();
			this.iabSendIP = new MagicRemoteService.IPAddressBox();
			this.nbSendPort = new System.Windows.Forms.NumericUpDown();
			this.pabPCMac = new MagicRemoteService.PhysicalAddressBox();
			this.labTVPort = new System.Windows.Forms.Label();
			this.iabSubnetMask = new MagicRemoteService.IPAddressBox();
			this.btnTVInstall = new System.Windows.Forms.Button();
			this.labSendIP = new System.Windows.Forms.Label();
			this.btnTVRefresh = new System.Windows.Forms.Button();
			this.labSubnetMask = new System.Windows.Forms.Label();
			this.labPCMac = new System.Windows.Forms.Label();
			this.labTimeoutRightClickUnit = new System.Windows.Forms.Label();
			this.labInput = new System.Windows.Forms.Label();
			this.labLongClick = new System.Windows.Forms.Label();
			this.nbLongClick = new System.Windows.Forms.NumericUpDown();
			this.cbbInput = new System.Windows.Forms.ComboBox();
			this.cbbTV = new System.Windows.Forms.ComboBox();
			this.tabPC = new System.Windows.Forms.TabPage();
			this.cbStartup = new System.Windows.Forms.CheckBox();
			this.labPCPort = new System.Windows.Forms.Label();
			this.btnPCSave = new System.Windows.Forms.Button();
			this.nbListenPort = new System.Windows.Forms.NumericUpDown();
			this.cbInactivity = new System.Windows.Forms.CheckBox();
			this.nbTimeoutInactivity = new System.Windows.Forms.NumericUpDown();
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
			((System.ComponentModel.ISupportInitialize)(this.nbSendPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nbLongClick)).BeginInit();
			this.tabPC.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nbListenPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nbTimeoutInactivity)).BeginInit();
			this.tabRemote.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			resources.ApplyResources(this.btnClose, "btnClose");
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Name = "btnClose";
			this.btnClose.UseVisualStyleBackColor = true;
			this.btnClose.Click += new System.EventHandler(this.Close_Click);
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
			// 
			// tabTV
			// 
			this.tabTV.Controls.Add(this.cbInputDirect);
			this.tabTV.Controls.Add(this.btnTVInspect);
			this.tabTV.Controls.Add(this.labDisplay);
			this.tabTV.Controls.Add(this.cbbDisplay);
			this.tabTV.Controls.Add(this.btnTVRemove);
			this.tabTV.Controls.Add(this.btnTVModify);
			this.tabTV.Controls.Add(this.btnTVAdd);
			this.tabTV.Controls.Add(this.cbExtend);
			this.tabTV.Controls.Add(this.labTV);
			this.tabTV.Controls.Add(this.iabSendIP);
			this.tabTV.Controls.Add(this.nbSendPort);
			this.tabTV.Controls.Add(this.pabPCMac);
			this.tabTV.Controls.Add(this.labTVPort);
			this.tabTV.Controls.Add(this.iabSubnetMask);
			this.tabTV.Controls.Add(this.btnTVInstall);
			this.tabTV.Controls.Add(this.labSendIP);
			this.tabTV.Controls.Add(this.btnTVRefresh);
			this.tabTV.Controls.Add(this.labSubnetMask);
			this.tabTV.Controls.Add(this.labPCMac);
			this.tabTV.Controls.Add(this.labTimeoutRightClickUnit);
			this.tabTV.Controls.Add(this.labInput);
			this.tabTV.Controls.Add(this.labLongClick);
			this.tabTV.Controls.Add(this.nbLongClick);
			this.tabTV.Controls.Add(this.cbbInput);
			this.tabTV.Controls.Add(this.cbbTV);
			resources.ApplyResources(this.tabTV, "tabTV");
			this.tabTV.Name = "tabTV";
			this.tabTV.UseVisualStyleBackColor = true;
			// 
			// cbInputDirect
			// 
			resources.ApplyResources(this.cbInputDirect, "cbInputDirect");
			this.cbInputDirect.Name = "cbInputDirect";
			this.cbInputDirect.UseVisualStyleBackColor = true;
			// 
			// btnTVInspect
			// 
			resources.ApplyResources(this.btnTVInspect, "btnTVInspect");
			this.btnTVInspect.Name = "btnTVInspect";
			this.btnTVInspect.UseVisualStyleBackColor = true;
			this.btnTVInspect.Click += new System.EventHandler(this.Inspect_Click);
			// 
			// labDisplay
			// 
			resources.ApplyResources(this.labDisplay, "labDisplay");
			this.labDisplay.Name = "labDisplay";
			// 
			// cbbDisplay
			// 
			this.cbbDisplay.DisplayMember = "NumberUserFriendlyName";
			this.cbbDisplay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbDisplay.FormattingEnabled = true;
			resources.ApplyResources(this.cbbDisplay, "cbbDisplay");
			this.cbbDisplay.Name = "cbbDisplay";
			this.cbbDisplay.ValueMember = "Id";
			// 
			// btnTVRemove
			// 
			resources.ApplyResources(this.btnTVRemove, "btnTVRemove");
			this.btnTVRemove.Name = "btnTVRemove";
			this.btnTVRemove.UseVisualStyleBackColor = true;
			this.btnTVRemove.Click += new System.EventHandler(this.TVRemove_Click);
			// 
			// btnTVModify
			// 
			resources.ApplyResources(this.btnTVModify, "btnTVModify");
			this.btnTVModify.Name = "btnTVModify";
			this.btnTVModify.UseVisualStyleBackColor = true;
			this.btnTVModify.Click += new System.EventHandler(this.TVModify_Click);
			// 
			// btnTVAdd
			// 
			resources.ApplyResources(this.btnTVAdd, "btnTVAdd");
			this.btnTVAdd.Name = "btnTVAdd";
			this.btnTVAdd.UseVisualStyleBackColor = true;
			this.btnTVAdd.Click += new System.EventHandler(this.TVAdd_Click);
			// 
			// cbExtend
			// 
			resources.ApplyResources(this.cbExtend, "cbExtend");
			this.cbExtend.Name = "cbExtend";
			this.cbExtend.UseVisualStyleBackColor = true;
			// 
			// labTV
			// 
			resources.ApplyResources(this.labTV, "labTV");
			this.labTV.Name = "labTV";
			// 
			// iabSendIP
			// 
			this.iabSendIP.BackColor = System.Drawing.SystemColors.Window;
			this.iabSendIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iabSendIP.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.iabSendIP, "iabSendIP");
			this.iabSendIP.Name = "iabSendIP";
			this.iabSendIP.Value = null;
			// 
			// nbSendPort
			// 
			resources.ApplyResources(this.nbSendPort, "nbSendPort");
			this.nbSendPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nbSendPort.Name = "nbSendPort";
			// 
			// pabPCMac
			// 
			this.pabPCMac.BackColor = System.Drawing.SystemColors.Window;
			resources.ApplyResources(this.pabPCMac, "pabPCMac");
			this.pabPCMac.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pabPCMac.ForeColor = System.Drawing.SystemColors.WindowText;
			this.pabPCMac.Name = "pabPCMac";
			this.pabPCMac.Value = null;
			// 
			// labTVPort
			// 
			resources.ApplyResources(this.labTVPort, "labTVPort");
			this.labTVPort.Name = "labTVPort";
			// 
			// iabSubnetMask
			// 
			this.iabSubnetMask.BackColor = System.Drawing.SystemColors.Window;
			this.iabSubnetMask.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.iabSubnetMask.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.iabSubnetMask, "iabSubnetMask");
			this.iabSubnetMask.Name = "iabSubnetMask";
			this.iabSubnetMask.Value = null;
			// 
			// btnTVInstall
			// 
			resources.ApplyResources(this.btnTVInstall, "btnTVInstall");
			this.btnTVInstall.Name = "btnTVInstall";
			this.btnTVInstall.UseVisualStyleBackColor = true;
			this.btnTVInstall.Click += new System.EventHandler(this.TVInstall_Click);
			// 
			// labSendIP
			// 
			resources.ApplyResources(this.labSendIP, "labSendIP");
			this.labSendIP.Name = "labSendIP";
			// 
			// btnTVRefresh
			// 
			resources.ApplyResources(this.btnTVRefresh, "btnTVRefresh");
			this.btnTVRefresh.Name = "btnTVRefresh";
			this.btnTVRefresh.UseVisualStyleBackColor = true;
			this.btnTVRefresh.Click += new System.EventHandler(this.TVRefresh_Click);
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
			// labTimeoutRightClickUnit
			// 
			resources.ApplyResources(this.labTimeoutRightClickUnit, "labTimeoutRightClickUnit");
			this.labTimeoutRightClickUnit.Name = "labTimeoutRightClickUnit";
			// 
			// labInput
			// 
			resources.ApplyResources(this.labInput, "labInput");
			this.labInput.Name = "labInput";
			// 
			// labLongClick
			// 
			resources.ApplyResources(this.labLongClick, "labLongClick");
			this.labLongClick.Name = "labLongClick";
			// 
			// nbLongClick
			// 
			resources.ApplyResources(this.nbLongClick, "nbLongClick");
			this.nbLongClick.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
			this.nbLongClick.Name = "nbLongClick";
			// 
			// cbbInput
			// 
			this.cbbInput.DisplayMember = "Name";
			this.cbbInput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbInput.FormattingEnabled = true;
			resources.ApplyResources(this.cbbInput, "cbbInput");
			this.cbbInput.Name = "cbbInput";
			this.cbbInput.ValueMember = "Id";
			// 
			// cbbTV
			// 
			this.cbbTV.DisplayMember = "Name";
			this.cbbTV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbTV.FormattingEnabled = true;
			resources.ApplyResources(this.cbbTV, "cbbTV");
			this.cbbTV.Name = "cbbTV";
			this.cbbTV.ValueMember = "Name";
			this.cbbTV.SelectedIndexChanged += new System.EventHandler(this.TV_SelectedIndexChanged);
			// 
			// tabPC
			// 
			this.tabPC.Controls.Add(this.cbStartup);
			this.tabPC.Controls.Add(this.labPCPort);
			this.tabPC.Controls.Add(this.btnPCSave);
			this.tabPC.Controls.Add(this.nbListenPort);
			this.tabPC.Controls.Add(this.cbInactivity);
			this.tabPC.Controls.Add(this.nbTimeoutInactivity);
			this.tabPC.Controls.Add(this.labTimeoutIncativity);
			this.tabPC.Controls.Add(this.labTimeoutIncativityUnit);
			resources.ApplyResources(this.tabPC, "tabPC");
			this.tabPC.Name = "tabPC";
			this.tabPC.UseVisualStyleBackColor = true;
			// 
			// cbStartup
			// 
			resources.ApplyResources(this.cbStartup, "cbStartup");
			this.cbStartup.Name = "cbStartup";
			this.cbStartup.UseVisualStyleBackColor = true;
			// 
			// labPCPort
			// 
			resources.ApplyResources(this.labPCPort, "labPCPort");
			this.labPCPort.Name = "labPCPort";
			// 
			// btnPCSave
			// 
			resources.ApplyResources(this.btnPCSave, "btnPCSave");
			this.btnPCSave.Name = "btnPCSave";
			this.btnPCSave.UseVisualStyleBackColor = true;
			this.btnPCSave.Click += new System.EventHandler(this.PCSave_Click);
			// 
			// nbListenPort
			// 
			resources.ApplyResources(this.nbListenPort, "nbListenPort");
			this.nbListenPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nbListenPort.Name = "nbListenPort";
			// 
			// cbInactivity
			// 
			resources.ApplyResources(this.cbInactivity, "cbInactivity");
			this.cbInactivity.Name = "cbInactivity";
			this.cbInactivity.UseVisualStyleBackColor = true;
			// 
			// nbTimeoutInactivity
			// 
			resources.ApplyResources(this.nbTimeoutInactivity, "nbTimeoutInactivity");
			this.nbTimeoutInactivity.Maximum = new decimal(new int[] {
            21600000,
            0,
            0,
            0});
			this.nbTimeoutInactivity.Minimum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
			this.nbTimeoutInactivity.Name = "nbTimeoutInactivity";
			this.nbTimeoutInactivity.Value = new decimal(new int[] {
            300000,
            0,
            0,
            0});
			// 
			// labTimeoutIncativity
			// 
			resources.ApplyResources(this.labTimeoutIncativity, "labTimeoutIncativity");
			this.labTimeoutIncativity.Name = "labTimeoutIncativity";
			// 
			// labTimeoutIncativityUnit
			// 
			resources.ApplyResources(this.labTimeoutIncativityUnit, "labTimeoutIncativityUnit");
			this.labTimeoutIncativityUnit.Name = "labTimeoutIncativityUnit";
			// 
			// tabRemote
			// 
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
			resources.ApplyResources(this.tabRemote, "tabRemote");
			this.tabRemote.Name = "tabRemote";
			this.tabRemote.UseVisualStyleBackColor = true;
			// 
			// bcRemoteRewind
			// 
			this.bcRemoteRewind.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteRewind.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteRewind, "bcRemoteRewind");
			this.bcRemoteRewind.Name = "bcRemoteRewind";
			this.bcRemoteRewind.Value = null;
			// 
			// bcRemoteFastForward
			// 
			this.bcRemoteFastForward.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteFastForward.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteFastForward, "bcRemoteFastForward");
			this.bcRemoteFastForward.Name = "bcRemoteFastForward";
			this.bcRemoteFastForward.Value = null;
			// 
			// bcRemotePause
			// 
			this.bcRemotePause.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemotePause.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemotePause, "bcRemotePause");
			this.bcRemotePause.Name = "bcRemotePause";
			this.bcRemotePause.Value = null;
			// 
			// bcRemotePlay
			// 
			this.bcRemotePlay.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemotePlay.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemotePlay, "bcRemotePlay");
			this.bcRemotePlay.Name = "bcRemotePlay";
			this.bcRemotePlay.Value = null;
			// 
			// bcRemoteBackspace
			// 
			this.bcRemoteBackspace.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteBackspace.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteBackspace, "bcRemoteBackspace");
			this.bcRemoteBackspace.Name = "bcRemoteBackspace";
			this.bcRemoteBackspace.Value = null;
			// 
			// bcRemoteLongClick
			// 
			this.bcRemoteLongClick.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteLongClick.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteLongClick, "bcRemoteLongClick");
			this.bcRemoteLongClick.Name = "bcRemoteLongClick";
			this.bcRemoteLongClick.Value = null;
			// 
			// bcRemoteClick
			// 
			this.bcRemoteClick.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteClick.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteClick, "bcRemoteClick");
			this.bcRemoteClick.Name = "bcRemoteClick";
			this.bcRemoteClick.Value = null;
			// 
			// bcRemoteStop
			// 
			this.bcRemoteStop.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteStop.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteStop, "bcRemoteStop");
			this.bcRemoteStop.Name = "bcRemoteStop";
			this.bcRemoteStop.Value = null;
			// 
			// bcRemoteBlue
			// 
			this.bcRemoteBlue.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteBlue.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteBlue, "bcRemoteBlue");
			this.bcRemoteBlue.Name = "bcRemoteBlue";
			this.bcRemoteBlue.Value = null;
			// 
			// bcRemoteYellow
			// 
			this.bcRemoteYellow.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteYellow.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteYellow, "bcRemoteYellow");
			this.bcRemoteYellow.Name = "bcRemoteYellow";
			this.bcRemoteYellow.Value = null;
			// 
			// bcRemoteGreen
			// 
			this.bcRemoteGreen.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteGreen.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteGreen, "bcRemoteGreen");
			this.bcRemoteGreen.Name = "bcRemoteGreen";
			this.bcRemoteGreen.Value = null;
			// 
			// bcRemoteRed
			// 
			this.bcRemoteRed.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteRed.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteRed, "bcRemoteRed");
			this.bcRemoteRed.Name = "bcRemoteRed";
			this.bcRemoteRed.Value = null;
			// 
			// bcRemoteBack
			// 
			this.bcRemoteBack.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteBack.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteBack, "bcRemoteBack");
			this.bcRemoteBack.Name = "bcRemoteBack";
			this.bcRemoteBack.Value = null;
			// 
			// bcRemoteOk
			// 
			this.bcRemoteOk.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteOk.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteOk, "bcRemoteOk");
			this.bcRemoteOk.Name = "bcRemoteOk";
			this.bcRemoteOk.Value = null;
			// 
			// bcRemoteDown
			// 
			this.bcRemoteDown.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteDown.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteDown, "bcRemoteDown");
			this.bcRemoteDown.Name = "bcRemoteDown";
			this.bcRemoteDown.Value = null;
			// 
			// bcRemoteRight
			// 
			this.bcRemoteRight.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteRight.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteRight, "bcRemoteRight");
			this.bcRemoteRight.Name = "bcRemoteRight";
			this.bcRemoteRight.Value = null;
			// 
			// bcRemoteLeft
			// 
			this.bcRemoteLeft.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteLeft.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteLeft, "bcRemoteLeft");
			this.bcRemoteLeft.Name = "bcRemoteLeft";
			this.bcRemoteLeft.Value = null;
			// 
			// bcRemoteUp
			// 
			this.bcRemoteUp.BackColor = System.Drawing.SystemColors.Window;
			this.bcRemoteUp.ForeColor = System.Drawing.SystemColors.WindowText;
			resources.ApplyResources(this.bcRemoteUp, "bcRemoteUp");
			this.bcRemoteUp.Name = "bcRemoteUp";
			this.bcRemoteUp.Value = null;
			// 
			// libRemoteBackspace
			// 
			resources.ApplyResources(this.libRemoteBackspace, "libRemoteBackspace");
			this.libRemoteBackspace.Name = "libRemoteBackspace";
			// 
			// libRemoteStop
			// 
			resources.ApplyResources(this.libRemoteStop, "libRemoteStop");
			this.libRemoteStop.Name = "libRemoteStop";
			// 
			// libRemoteRewind
			// 
			resources.ApplyResources(this.libRemoteRewind, "libRemoteRewind");
			this.libRemoteRewind.Name = "libRemoteRewind";
			// 
			// libRemoteFastForward
			// 
			resources.ApplyResources(this.libRemoteFastForward, "libRemoteFastForward");
			this.libRemoteFastForward.Name = "libRemoteFastForward";
			// 
			// libRemotePause
			// 
			resources.ApplyResources(this.libRemotePause, "libRemotePause");
			this.libRemotePause.Name = "libRemotePause";
			// 
			// libRemotePlay
			// 
			resources.ApplyResources(this.libRemotePlay, "libRemotePlay");
			this.libRemotePlay.Name = "libRemotePlay";
			// 
			// libRemoteBlue
			// 
			resources.ApplyResources(this.libRemoteBlue, "libRemoteBlue");
			this.libRemoteBlue.Name = "libRemoteBlue";
			// 
			// libRemoteYellow
			// 
			resources.ApplyResources(this.libRemoteYellow, "libRemoteYellow");
			this.libRemoteYellow.Name = "libRemoteYellow";
			// 
			// libRemoteGreen
			// 
			resources.ApplyResources(this.libRemoteGreen, "libRemoteGreen");
			this.libRemoteGreen.Name = "libRemoteGreen";
			// 
			// libRemoteRed
			// 
			resources.ApplyResources(this.libRemoteRed, "libRemoteRed");
			this.libRemoteRed.Name = "libRemoteRed";
			// 
			// libRemoteBack
			// 
			resources.ApplyResources(this.libRemoteBack, "libRemoteBack");
			this.libRemoteBack.Name = "libRemoteBack";
			// 
			// libRemoteOk
			// 
			resources.ApplyResources(this.libRemoteOk, "libRemoteOk");
			this.libRemoteOk.Name = "libRemoteOk";
			// 
			// libRemoteDown
			// 
			resources.ApplyResources(this.libRemoteDown, "libRemoteDown");
			this.libRemoteDown.Name = "libRemoteDown";
			// 
			// libRemoteRight
			// 
			resources.ApplyResources(this.libRemoteRight, "libRemoteRight");
			this.libRemoteRight.Name = "libRemoteRight";
			// 
			// libRemoteUp
			// 
			resources.ApplyResources(this.libRemoteUp, "libRemoteUp");
			this.libRemoteUp.Name = "libRemoteUp";
			// 
			// libRemoteLeft
			// 
			resources.ApplyResources(this.libRemoteLeft, "libRemoteLeft");
			this.libRemoteLeft.Name = "libRemoteLeft";
			// 
			// libRemoteLongClick
			// 
			resources.ApplyResources(this.libRemoteLongClick, "libRemoteLongClick");
			this.libRemoteLongClick.Name = "libRemoteLongClick";
			// 
			// libRemoteClick
			// 
			resources.ApplyResources(this.libRemoteClick, "libRemoteClick");
			this.libRemoteClick.Name = "libRemoteClick";
			// 
			// btnRemoteSave
			// 
			resources.ApplyResources(this.btnRemoteSave, "btnRemoteSave");
			this.btnRemoteSave.Name = "btnRemoteSave";
			this.btnRemoteSave.UseVisualStyleBackColor = true;
			this.btnRemoteSave.Click += new System.EventHandler(this.RemoteSave_Click);
			// 
			// Setting
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnClose;
			this.Controls.Add(this.tabSetting);
			this.Controls.Add(this.btnClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Setting";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Setting_FormClosing);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Setting_MouseDown);
			this.tabSetting.ResumeLayout(false);
			this.tabTV.ResumeLayout(false);
			this.tabTV.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nbSendPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nbLongClick)).EndInit();
			this.tabPC.ResumeLayout(false);
			this.tabPC.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nbListenPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nbTimeoutInactivity)).EndInit();
			this.tabRemote.ResumeLayout(false);
			this.tabRemote.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ToolTip ttFormating;
		private System.Windows.Forms.TabControl tabSetting;
		private System.Windows.Forms.TabPage tabTV;
		private System.Windows.Forms.CheckBox cbExtend;
		private System.Windows.Forms.Label labTV;
		private IPAddressBox iabSendIP;
		private System.Windows.Forms.NumericUpDown nbSendPort;
		private PhysicalAddressBox pabPCMac;
		private System.Windows.Forms.Label labTVPort;
		private IPAddressBox iabSubnetMask;
		private System.Windows.Forms.Button btnTVInstall;
		private System.Windows.Forms.Label labSendIP;
		private System.Windows.Forms.Button btnTVRefresh;
		private System.Windows.Forms.Label labSubnetMask;
		private System.Windows.Forms.Label labPCMac;
		private System.Windows.Forms.Label labTimeoutRightClickUnit;
		private System.Windows.Forms.Label labInput;
		private System.Windows.Forms.Label labLongClick;
		private System.Windows.Forms.NumericUpDown nbLongClick;
		private System.Windows.Forms.ComboBox cbbInput;
		private System.Windows.Forms.ComboBox cbbTV;
		private System.Windows.Forms.TabPage tabPC;
		private System.Windows.Forms.CheckBox cbStartup;
		private System.Windows.Forms.Label labPCPort;
		private System.Windows.Forms.Button btnPCSave;
		private System.Windows.Forms.NumericUpDown nbListenPort;
		private System.Windows.Forms.CheckBox cbInactivity;
		private System.Windows.Forms.NumericUpDown nbTimeoutInactivity;
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
		private System.Windows.Forms.Button btnTVAdd;
		private System.Windows.Forms.Button btnTVRemove;
		private System.Windows.Forms.Button btnTVModify;
		private System.Windows.Forms.Label labDisplay;
		private System.Windows.Forms.ComboBox cbbDisplay;
		private System.Windows.Forms.Button btnTVInspect;
		private System.Windows.Forms.CheckBox cbInputDirect;
	}
}