
namespace MagicRemoteService {

	public partial class BindCreator : System.Windows.Forms.Form {
		private MagicRemoteService.Bind bBind;
		public MagicRemoteService.Bind Bind {
			get {
				return this.bBind;
			}
		}

		public BindCreator(MagicRemoteService.Bind bBind) {
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.InitializeComponent();
			this.bBind = bBind;
			switch(bBind) {
				case MagicRemoteService.BindMouse bm:
					this.selBindMouse.Checked = true;
					switch(bm.bmvValue) {
						case BindMouseValue.Left:
							this.selMouseLeft.Checked = true;
							break;
						case BindMouseValue.Right:
							this.selMouseRight.Checked = true;
							break;
						case BindMouseValue.Middle:
							this.selMouseMiddle.Checked = true;
							break;
					}
					break;
				case MagicRemoteService.BindKeyboard bk:
					this.selBindKeyboard.Checked = true;
					this.libKeyboard.Text = bk.ToString();
					break;
				case MagicRemoteService.BindAction ba:
					this.selBindAction.Checked = true;
					switch(ba.bavValue) {
						case BindActionValue.Shutdown:
							this.selActionShutdown.Checked = true;
							break;
						case BindActionValue.Keyboard:
							this.selActionKeyboard.Checked = true;
							break;
					}
					break;
			}
		}
		private void Confirm_Click(object sender, System.EventArgs e) {
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}
		private void Cancel_Click(object sender, System.EventArgs e) {
			this.Close();
		}
		private void BindMouse_CheckedChanged(object sender, System.EventArgs e) {
			this.pnlMouse.Visible = ((System.Windows.Forms.RadioButton)sender).Checked;
		}
		private void BindKeyboard_CheckedChanged(object sender, System.EventArgs e) {
			this.libKeyboard.Visible = ((System.Windows.Forms.RadioButton)sender).Checked;
		}
		private void BindAction_CheckedChanged(object sender, System.EventArgs e) {
			this.pnlAction.Visible = ((System.Windows.Forms.RadioButton)sender).Checked;
		}
		private void MouseLeft_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this.bBind = new MagicRemoteService.BindMouse(BindMouseValue.Left);
			}
		}
		private void MouseMiddle_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this.bBind = new MagicRemoteService.BindMouse(BindMouseValue.Right);
			}
		}
		private void MouseRight_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this.bBind = new MagicRemoteService.BindMouse(BindMouseValue.Middle);
			}
		}
		protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys kData) {
			if(this.selBindKeyboard.Checked) {
				switch(m.Msg) {
					case 0x100: {
						this.bBind = new MagicRemoteService.BindKeyboard(System.BitConverter.GetBytes(m.WParam.ToInt32())[0], System.BitConverter.GetBytes(m.LParam.ToInt32())[2], (System.BitConverter.GetBytes(m.LParam.ToInt32())[3] & 0x01) == 0x01);
						this.libKeyboard.Text = this.bBind.ToString();
						break;
					}
				}
			}
			return true;//base.ProcessCmdKey(ref m, keyData);
		}
		private void ActionShutdown_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this.bBind = new MagicRemoteService.BindAction(BindActionValue.Shutdown);
			}
		}
		private void ActionKeyboard_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this.bBind = new MagicRemoteService.BindAction(BindActionValue.Keyboard);
			}
		}
	}
}
