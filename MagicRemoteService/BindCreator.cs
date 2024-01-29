
namespace MagicRemoteService {

	public partial class BindCreator : System.Windows.Forms.Form {
		private System.Collections.Generic.List<MagicRemoteService.Bind> liBind = new System.Collections.Generic.List<MagicRemoteService.Bind>();
		private MagicRemoteService.Bind[] arrBind;
		public MagicRemoteService.Bind[] Bind {
			get {
				return this.arrBind;
			}
		}

		public BindCreator(MagicRemoteService.Bind[] arrBind) {
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.InitializeComponent();
			this.arrBind = arrBind;
			switch(arrBind?[0]) {
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
					this.libKeyboard.Text = string.Join<Bind>(" + ", arrBind);
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
				case MagicRemoteService.BindCommand bc:
					this.selBindCommand.Checked = true;
					this.tbCommand.Text = string.Join<Bind>(" + ", arrBind);
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
		private void BindCommand_CheckedChanged(object sender, System.EventArgs e) {
			this.pnlCommand.Visible = ((System.Windows.Forms.RadioButton)sender).Checked;
		}
		private void MouseLeft_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this.arrBind = new Bind[] { new MagicRemoteService.BindMouse(BindMouseValue.Left) };
			}
		}
		private void MouseMiddle_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this.arrBind = new Bind[] { new MagicRemoteService.BindMouse(BindMouseValue.Middle) };
			}
		}
		private void MouseRight_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this.arrBind = new Bind[] { new MagicRemoteService.BindMouse(BindMouseValue.Right) };
			}
		}
		protected override bool ProcessCmdKey(ref System.Windows.Forms.Message m, System.Windows.Forms.Keys k) {
			if(this.selBindKeyboard.Checked) {
				switch(m.Msg) {
					case 0x0100:
					case 0x0104:
						this.liBind.Add(new MagicRemoteService.BindKeyboard(System.BitConverter.GetBytes(m.WParam.ToInt32())[0], System.BitConverter.GetBytes(m.LParam.ToInt32())[2], (System.BitConverter.GetBytes(m.LParam.ToInt32())[3] & 0x01) == 0x01));
						this.arrBind = this.liBind.ToArray();
						this.libKeyboard.Text = string.Join<Bind>(" + ", this.arrBind);
						return true;
					default:
						return base.ProcessCmdKey(ref m, k);
				}
			} else {
				return base.ProcessCmdKey(ref m, k);
			}
		}
		protected override bool ProcessKeyEventArgs(ref System.Windows.Forms.Message m) {
			if(this.selBindKeyboard.Checked) {
				switch(m.Msg) {
					case 0x0101:
					case 0x0105:
						BindKeyboard bkUp = new MagicRemoteService.BindKeyboard(System.BitConverter.GetBytes(m.WParam.ToInt32())[0], System.BitConverter.GetBytes(m.LParam.ToInt32())[2], (System.BitConverter.GetBytes(m.LParam.ToInt32())[3] & 0x01) == 0x01);

						this.liBind.RemoveAll(delegate(Bind b) {
							switch(b) {
								case MagicRemoteService.BindKeyboard bk:
									return bk.ucVirtualKey == bkUp.ucVirtualKey && bk.ucScanCode == bkUp.ucScanCode && bk.bExtended == bkUp.bExtended;
								default:
									return false;
							}
						});
						return true;
					default:
						return base.ProcessKeyEventArgs(ref m);
				}
			} else {
				return base.ProcessKeyEventArgs(ref m);
			}
		}
		private void ActionShutdown_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this.arrBind = new Bind[] { new MagicRemoteService.BindAction(BindActionValue.Shutdown) };
			}
		}
		private void ActionKeyboard_CheckedChanged(object sender, System.EventArgs e) {
			if(((System.Windows.Forms.RadioButton)sender).Checked) {
				this.arrBind = new Bind[] { new MagicRemoteService.BindAction(BindActionValue.Keyboard) };
			}
		}
		private void Command_TextChanged(object sender, System.EventArgs e) {
			this.arrBind = new Bind[] { new MagicRemoteService.BindCommand(this.tbCommand.Text) };
		}
	}
}
