
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
			this.selBindMouse = new System.Windows.Forms.RadioButton();
			this.selBindKeyboard = new System.Windows.Forms.RadioButton();
			this.selBindAction = new System.Windows.Forms.RadioButton();
			this.pnlBind = new System.Windows.Forms.Panel();
			this.pnlMouse = new System.Windows.Forms.Panel();
			this.selMouseRight = new System.Windows.Forms.RadioButton();
			this.selMouseMiddle = new System.Windows.Forms.RadioButton();
			this.selMouseLeft = new System.Windows.Forms.RadioButton();
			this.libKeyboard = new System.Windows.Forms.Label();
			this.pnlAction = new System.Windows.Forms.Panel();
			this.selActionKeyboard = new System.Windows.Forms.RadioButton();
			this.selActionShutdown = new System.Windows.Forms.RadioButton();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnConfirm = new System.Windows.Forms.Button();
			this.tlpBind = new System.Windows.Forms.TableLayoutPanel();
			this.pnlBind.SuspendLayout();
			this.pnlMouse.SuspendLayout();
			this.pnlAction.SuspendLayout();
			this.tlpBind.SuspendLayout();
			this.SuspendLayout();
			// 
			// selBindMouse
			// 
			resources.ApplyResources(this.selBindMouse, "selBindMouse");
			this.selBindMouse.Name = "selBindMouse";
			this.selBindMouse.UseVisualStyleBackColor = true;
			this.selBindMouse.CheckedChanged += new System.EventHandler(this.selBindMouse_CheckedChanged);
			// 
			// selBindKeyboard
			// 
			resources.ApplyResources(this.selBindKeyboard, "selBindKeyboard");
			this.selBindKeyboard.Name = "selBindKeyboard";
			this.selBindKeyboard.UseVisualStyleBackColor = true;
			this.selBindKeyboard.CheckedChanged += new System.EventHandler(this.selBindKeyboard_CheckedChanged);
			// 
			// selBindAction
			// 
			resources.ApplyResources(this.selBindAction, "selBindAction");
			this.selBindAction.Name = "selBindAction";
			this.selBindAction.UseVisualStyleBackColor = true;
			this.selBindAction.CheckedChanged += new System.EventHandler(this.selBindAction_CheckedChanged);
			// 
			// pnlBind
			// 
			this.pnlBind.Controls.Add(this.selBindMouse);
			this.pnlBind.Controls.Add(this.selBindAction);
			this.pnlBind.Controls.Add(this.selBindKeyboard);
			resources.ApplyResources(this.pnlBind, "pnlBind");
			this.pnlBind.Name = "pnlBind";
			// 
			// pnlMouse
			// 
			this.pnlMouse.Controls.Add(this.selMouseRight);
			this.pnlMouse.Controls.Add(this.selMouseMiddle);
			this.pnlMouse.Controls.Add(this.selMouseLeft);
			resources.ApplyResources(this.pnlMouse, "pnlMouse");
			this.pnlMouse.Name = "pnlMouse";
			// 
			// selMouseRight
			// 
			resources.ApplyResources(this.selMouseRight, "selMouseRight");
			this.selMouseRight.Name = "selMouseRight";
			this.selMouseRight.TabStop = true;
			this.selMouseRight.UseVisualStyleBackColor = true;
			this.selMouseRight.CheckedChanged += new System.EventHandler(this.selMouseRight_CheckedChanged);
			// 
			// selMouseMiddle
			// 
			resources.ApplyResources(this.selMouseMiddle, "selMouseMiddle");
			this.selMouseMiddle.Name = "selMouseMiddle";
			this.selMouseMiddle.TabStop = true;
			this.selMouseMiddle.UseVisualStyleBackColor = true;
			this.selMouseMiddle.CheckedChanged += new System.EventHandler(this.selMouseMiddle_CheckedChanged);
			// 
			// selMouseLeft
			// 
			resources.ApplyResources(this.selMouseLeft, "selMouseLeft");
			this.selMouseLeft.Name = "selMouseLeft";
			this.selMouseLeft.TabStop = true;
			this.selMouseLeft.UseVisualStyleBackColor = true;
			this.selMouseLeft.CheckedChanged += new System.EventHandler(this.selMouseLeft_CheckedChanged);
			// 
			// libKeyboard
			// 
			resources.ApplyResources(this.libKeyboard, "libKeyboard");
			this.libKeyboard.Name = "libKeyboard";
			// 
			// pnlAction
			// 
			this.pnlAction.Controls.Add(this.selActionKeyboard);
			this.pnlAction.Controls.Add(this.selActionShutdown);
			resources.ApplyResources(this.pnlAction, "pnlAction");
			this.pnlAction.Name = "pnlAction";
			// 
			// selActionKeyboard
			// 
			resources.ApplyResources(this.selActionKeyboard, "selActionKeyboard");
			this.selActionKeyboard.Name = "selActionKeyboard";
			this.selActionKeyboard.TabStop = true;
			this.selActionKeyboard.UseVisualStyleBackColor = true;
			this.selActionKeyboard.CheckedChanged += new System.EventHandler(this.selActionKeyboard_CheckedChanged);
			// 
			// selActionShutdown
			// 
			resources.ApplyResources(this.selActionShutdown, "selActionShutdown");
			this.selActionShutdown.Name = "selActionShutdown";
			this.selActionShutdown.TabStop = true;
			this.selActionShutdown.UseVisualStyleBackColor = true;
			this.selActionShutdown.CheckedChanged += new System.EventHandler(this.selActionShutdown_CheckedChanged);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnConfirm
			// 
			resources.ApplyResources(this.btnConfirm, "btnConfirm");
			this.btnConfirm.Name = "btnConfirm";
			this.btnConfirm.UseVisualStyleBackColor = true;
			this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
			// 
			// tlpBind
			// 
			resources.ApplyResources(this.tlpBind, "tlpBind");
			this.tlpBind.Controls.Add(this.libKeyboard, 0, 1);
			this.tlpBind.Controls.Add(this.pnlAction, 0, 2);
			this.tlpBind.Controls.Add(this.pnlMouse, 0, 0);
			this.tlpBind.Name = "tlpBind";
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
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BindCreator";
			this.pnlBind.ResumeLayout(false);
			this.pnlBind.PerformLayout();
			this.pnlMouse.ResumeLayout(false);
			this.pnlMouse.PerformLayout();
			this.pnlAction.ResumeLayout(false);
			this.pnlAction.PerformLayout();
			this.tlpBind.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RadioButton selBindMouse;
		private System.Windows.Forms.RadioButton selBindAction;
		private System.Windows.Forms.RadioButton selBindKeyboard;
		private System.Windows.Forms.Panel pnlBind;
		private System.Windows.Forms.Panel pnlMouse;
		private System.Windows.Forms.RadioButton selMouseRight;
		private System.Windows.Forms.RadioButton selMouseMiddle;
		private System.Windows.Forms.RadioButton selMouseLeft;
		private System.Windows.Forms.Label libKeyboard;
		private System.Windows.Forms.Panel pnlAction;
		private System.Windows.Forms.RadioButton selActionKeyboard;
		private System.Windows.Forms.RadioButton selActionShutdown;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnConfirm;
		private System.Windows.Forms.TableLayoutPanel tlpBind;
	}
}