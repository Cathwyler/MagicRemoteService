
namespace MagicRemoteService {

	public partial class BindCreator : System.Windows.Forms.Form {
		private MagicRemoteService.Bind _bBind;
		public MagicRemoteService.Bind bBind {
			get { return this._bBind;  }
		}

		public BindCreator(MagicRemoteService.Bind bBind) {
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.InitializeComponent();
			this._bBind = bBind;
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
					libKeyboard.Text = bk.ToString();
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
		private void btnConfirm_Click(object sender, System.EventArgs e) {
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}
		private void btnCancel_Click(object sender, System.EventArgs e) {
			this.Close();
		}
		private void selBindMouse_CheckedChanged(object sender, System.EventArgs e) {
			this.pnlMouse.Visible = ((System.Windows.Forms.RadioButton)sender).Checked;
		}
		private void selBindKeyboard_CheckedChanged(object sender, System.EventArgs e) {
			this.libKeyboard.Visible = ((System.Windows.Forms.RadioButton)sender).Checked;
		}
		private void selBindAction_CheckedChanged(object sender, System.EventArgs e) {
			this.pnlAction.Visible = ((System.Windows.Forms.RadioButton)sender).Checked;
		}
		private void selMouseLeft_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this._bBind = new MagicRemoteService.BindMouse(BindMouseValue.Left);
			}
		}
		private void selMouseMiddle_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this._bBind = new MagicRemoteService.BindMouse(BindMouseValue.Right);
			}
		}
		private void selMouseRight_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this._bBind = new MagicRemoteService.BindMouse(BindMouseValue.Middle);
			}
		}
		protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys keyData) {
			if(this.selBindKeyboard.Checked) {
				switch(m.Msg) {
					case 0x100: {
						byte[] tabParam = System.BitConverter.GetBytes(m.LParam.ToInt32());
						if(tabParam[2] != 0x00) {
							this._bBind = new MagicRemoteService.BindKeyboard(System.BitConverter.ToUInt16(new byte[] { tabParam[2], ((tabParam[3] & 0x01) == 0x01) ? (byte)0xE0 : (byte)0x00 }, 0));
							libKeyboard.Text = this._bBind.ToString();
						}
						break;
					}
				}
			}
			return true;//base.ProcessCmdKey(ref m, keyData);
		}
		private void selActionShutdown_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this._bBind = new MagicRemoteService.BindAction(BindActionValue.Shutdown);
			}

		}
		private void selActionKeyboard_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this._bBind = new MagicRemoteService.BindAction(BindActionValue.Keyboard);
			}
		}
	}
}
