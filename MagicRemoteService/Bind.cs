namespace MagicRemoteService {
	public enum BindMouseValue : ushort {
		Left = 0x0001,
		Right = 0x0002,
		Middle = 0x0003
	}
	public enum BindActionValue : byte {
		Shutdown = 0x01,
		Keyboard = 0x02,
		DisplayDefault = 0x03,
		DisplayNext = 0x04,
		DisplayPrevious = 0x05
	}
	public abstract class Bind {
	}
	public class BindMouse : Bind {
		public readonly BindMouseValue bmvValue;
		public BindMouse(BindMouseValue _bmvValue) {
			this.bmvValue = _bmvValue;
		}
		public override string ToString() {
			switch(this.bmvValue) {
				case BindMouseValue.Left:
					return MagicRemoteService.Properties.Resources.BindMouseValueLeft;
				case BindMouseValue.Right:
					return MagicRemoteService.Properties.Resources.BindMouseValueRight;
				case BindMouseValue.Middle:
					return MagicRemoteService.Properties.Resources.BindMouseValueMiddle;
				default:
					return base.ToString();
			}
		}
	}
	public class BindKeyboard : Bind {
		public readonly byte ucVirtualKey;
		public readonly byte ucScanCode;
		public readonly bool bExtended;
		public BindKeyboard(byte _ucVirtualKey, byte _ucScanCode, bool _bExtended) {
			this.ucVirtualKey = _ucVirtualKey;
			this.ucScanCode = _ucScanCode;
			this.bExtended = _bExtended;
		}
		public override string ToString() {
			if(this.ucScanCode == 0x00) {
				return new System.Windows.Forms.KeysConverter().ConvertToString((System.Windows.Forms.Keys)this.ucVirtualKey);
			} else {
				System.Text.StringBuilder sbString = new System.Text.StringBuilder(32);
				if(WinApi.User32.GetKeyNameText(System.BitConverter.ToInt32(new byte[] { 0x00, 0x00, this.ucScanCode, this.bExtended ? (byte)0x01 : (byte)0x00 }, 0), sbString, sbString.Capacity) == 0) {
					return new System.Windows.Forms.KeysConverter().ConvertToString((System.Windows.Forms.Keys)this.ucVirtualKey);
				} else {
					return sbString.ToString();
				}
			}
		}
	}
	public class BindAction : Bind {
		public readonly BindActionValue bavValue;
		public BindAction(BindActionValue _bavValue) {
			this.bavValue = _bavValue;
		}
		public override string ToString() {
			switch(this.bavValue) {
				case BindActionValue.Shutdown:
					return MagicRemoteService.Properties.Resources.BindActionValueShutdown;
				case BindActionValue.Keyboard:
					return MagicRemoteService.Properties.Resources.BindActionValueKeyboard;
				case BindActionValue.DisplayDefault:
					return MagicRemoteService.Properties.Resources.BindActionValueDisplayDefault;
				case BindActionValue.DisplayNext:
					return MagicRemoteService.Properties.Resources.BindActionValueDisplayNext;
				case BindActionValue.DisplayPrevious:
					return MagicRemoteService.Properties.Resources.BindActionValueDisplayPrevious;
				default:
					return base.ToString();
			}
		}
	}
	public class BindCommand : Bind {
		public readonly string strCommand;
		public BindCommand(string _strCommand) {
			this.strCommand = _strCommand;
		}
		public override string ToString() {
			return this.strCommand;
		}
	}
}