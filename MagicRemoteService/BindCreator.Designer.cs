
namespace MagicRemoteService {
	partial class BindCreator {
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BindCreator));
			this.pnlBind = new System.Windows.Forms.Panel();
			this.selBindMouse = new System.Windows.Forms.RadioButton();
			this.selBindKeyboard = new System.Windows.Forms.RadioButton();
			this.selBindAction = new System.Windows.Forms.RadioButton();
			this.selBindCommand = new System.Windows.Forms.RadioButton();
			this.tlpBind = new System.Windows.Forms.TableLayoutPanel();
			this.pnlMouse = new System.Windows.Forms.Panel();
			this.selMouseLeft = new System.Windows.Forms.RadioButton();
			this.selMouseMiddle = new System.Windows.Forms.RadioButton();
			this.selMouseRight = new System.Windows.Forms.RadioButton();
			this.labKeyboard = new System.Windows.Forms.Label();
			this.pnlKeyboard = new System.Windows.Forms.Panel();
			this.pnlAction = new System.Windows.Forms.Panel();
			this.labAction = new System.Windows.Forms.Label();
			this.cbbAction = new System.Windows.Forms.ComboBox();
			this.tbCommand = new System.Windows.Forms.TextBox();
			this.pnlCommand = new System.Windows.Forms.Panel();
			this.btnConfirm = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlBind.SuspendLayout();
			this.tlpBind.SuspendLayout();
			this.pnlMouse.SuspendLayout();
			this.pnlKeyboard.SuspendLayout();
			this.pnlAction.SuspendLayout();
			this.pnlCommand.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnlBind
			// 
			this.pnlBind.Controls.Add(this.selBindCommand);
			this.pnlBind.Controls.Add(this.selBindMouse);
			this.pnlBind.Controls.Add(this.selBindAction);
			this.pnlBind.Controls.Add(this.selBindKeyboard);
			resources.ApplyResources(this.pnlBind, "pnlBind");
			this.pnlBind.Name = "pnlBind";
			// 
			// selBindMouse
			// 
			resources.ApplyResources(this.selBindMouse, "selBindMouse");
			this.selBindMouse.Name = "selBindMouse";
			this.selBindMouse.UseVisualStyleBackColor = true;
			this.selBindMouse.CheckedChanged += new System.EventHandler(this.BindMouse_CheckedChanged);
			// 
			// selBindKeyboard
			// 
			resources.ApplyResources(this.selBindKeyboard, "selBindKeyboard");
			this.selBindKeyboard.Name = "selBindKeyboard";
			this.selBindKeyboard.UseVisualStyleBackColor = true;
			this.selBindKeyboard.CheckedChanged += new System.EventHandler(this.BindKeyboard_CheckedChanged);
			// 
			// selBindAction
			// 
			resources.ApplyResources(this.selBindAction, "selBindAction");
			this.selBindAction.Name = "selBindAction";
			this.selBindAction.UseVisualStyleBackColor = true;
			this.selBindAction.CheckedChanged += new System.EventHandler(this.BindAction_CheckedChanged);
			// 
			// selBindCommand
			// 
			resources.ApplyResources(this.selBindCommand, "selBindCommand");
			this.selBindCommand.Name = "selBindCommand";
			this.selBindCommand.UseVisualStyleBackColor = true;
			this.selBindCommand.CheckedChanged += new System.EventHandler(this.BindCommand_CheckedChanged);
			// 
			// tlpBind
			// 
			resources.ApplyResources(this.tlpBind, "tlpBind");
			this.tlpBind.Controls.Add(this.pnlAction, 0, 2);
			this.tlpBind.Controls.Add(this.pnlMouse, 0, 0);
			this.tlpBind.Controls.Add(this.pnlCommand, 0, 3);
			this.tlpBind.Controls.Add(this.pnlKeyboard, 0, 1);
			this.tlpBind.Name = "tlpBind";
			// 
			// pnlMouse
			// 
			this.pnlMouse.Controls.Add(this.selMouseRight);
			this.pnlMouse.Controls.Add(this.selMouseMiddle);
			this.pnlMouse.Controls.Add(this.selMouseLeft);
			resources.ApplyResources(this.pnlMouse, "pnlMouse");
			this.pnlMouse.Name = "pnlMouse";
			// 
			// selMouseLeft
			// 
			resources.ApplyResources(this.selMouseLeft, "selMouseLeft");
			this.selMouseLeft.Name = "selMouseLeft";
			this.selMouseLeft.TabStop = true;
			this.selMouseLeft.UseVisualStyleBackColor = true;
			this.selMouseLeft.CheckedChanged += new System.EventHandler(this.MouseLeft_CheckedChanged);
			// 
			// selMouseMiddle
			// 
			resources.ApplyResources(this.selMouseMiddle, "selMouseMiddle");
			this.selMouseMiddle.Name = "selMouseMiddle";
			this.selMouseMiddle.TabStop = true;
			this.selMouseMiddle.UseVisualStyleBackColor = true;
			this.selMouseMiddle.CheckedChanged += new System.EventHandler(this.MouseMiddle_CheckedChanged);
			// 
			// selMouseRight
			// 
			resources.ApplyResources(this.selMouseRight, "selMouseRight");
			this.selMouseRight.Name = "selMouseRight";
			this.selMouseRight.TabStop = true;
			this.selMouseRight.UseVisualStyleBackColor = true;
			this.selMouseRight.CheckedChanged += new System.EventHandler(this.MouseRight_CheckedChanged);
			// 
			// labKeyboard
			// 
			resources.ApplyResources(this.labKeyboard, "labKeyboard");
			this.labKeyboard.Name = "labKeyboard";
			// 
			// pnlKeyboard
			// 
			this.pnlKeyboard.Controls.Add(this.labKeyboard);
			resources.ApplyResources(this.pnlKeyboard, "pnlKeyboard");
			this.pnlKeyboard.Name = "pnlKeyboard";
			// 
			// pnlAction
			// 
			this.pnlAction.Controls.Add(this.cbbAction);
			this.pnlAction.Controls.Add(this.labAction);
			resources.ApplyResources(this.pnlAction, "pnlAction");
			this.pnlAction.Name = "pnlAction";
			// 
			// labAction
			// 
			resources.ApplyResources(this.labAction, "labAction");
			this.labAction.Name = "labAction";
			// 
			// cbbAction
			// 
			this.cbbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbbAction.FormattingEnabled = true;
			resources.ApplyResources(this.cbbAction, "cbbAction");
			this.cbbAction.Name = "cbbAction";
			this.cbbAction.SelectedIndexChanged += new System.EventHandler(this.Action_SelectedIndexChanged);
			// 
			// tbCommand
			// 
			resources.ApplyResources(this.tbCommand, "tbCommand");
			this.tbCommand.Name = "tbCommand";
			this.tbCommand.TextChanged += new System.EventHandler(this.Command_TextChanged);
			// 
			// pnlCommand
			// 
			this.pnlCommand.Controls.Add(this.tbCommand);
			resources.ApplyResources(this.pnlCommand, "pnlCommand");
			this.pnlCommand.Name = "pnlCommand";
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
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// BindCreator
			// 
			this.AcceptButton = this.btnConfirm;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.Controls.Add(this.tlpBind);
			this.Controls.Add(this.btnConfirm);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.pnlBind);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = global::MagicRemoteService.Properties.Resources.MagicRemoteServiceIcon;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BindCreator";
			this.pnlBind.ResumeLayout(false);
			this.pnlBind.PerformLayout();
			this.tlpBind.ResumeLayout(false);
			this.pnlMouse.ResumeLayout(false);
			this.pnlMouse.PerformLayout();
			this.pnlKeyboard.ResumeLayout(false);
			this.pnlAction.ResumeLayout(false);
			this.pnlCommand.ResumeLayout(false);
			this.pnlCommand.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel pnlBind;
		private System.Windows.Forms.RadioButton selBindMouse;
		private System.Windows.Forms.RadioButton selBindKeyboard;
		private System.Windows.Forms.RadioButton selBindAction;
		private System.Windows.Forms.RadioButton selBindCommand;
		private System.Windows.Forms.TableLayoutPanel tlpBind;
		private System.Windows.Forms.Panel pnlMouse;
		private System.Windows.Forms.RadioButton selMouseLeft;
		private System.Windows.Forms.RadioButton selMouseMiddle;
		private System.Windows.Forms.RadioButton selMouseRight;
		private System.Windows.Forms.Panel pnlKeyboard;
		private System.Windows.Forms.Label labKeyboard;
		private System.Windows.Forms.Panel pnlAction;
		private System.Windows.Forms.Label labAction;
		private System.Windows.Forms.ComboBox cbbAction;
		private System.Windows.Forms.Panel pnlCommand;
		private System.Windows.Forms.TextBox tbCommand;
		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.Button btnCancel;
	}
}