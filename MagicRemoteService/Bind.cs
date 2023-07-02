namespace MagicRemoteService {
	public enum BindMouseValue : ushort {
		Left = 0x0001,
		Right = 0x0002,
		Middle = 0x0003
	}
	public enum BindActionValue : byte {
		Shutdown = 0x01,
		Keyboard = 0x02,
	}
	public abstract class Bind {
	}
	public class BindMouse : Bind {
		public readonly BindMouseValue bmvValue;
		public BindMouse(BindMouseValue _bmvValue) {
			this.bmvValue = _bmvValue;
		}
		public override string ToString() {
			switch(bmvValue) {
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
		public readonly ushort usScanCode;
		public BindKeyboard(ushort _usScanCode) {
			this.usScanCode = _usScanCode;
		}
		public override string ToString() {
			if(this.usScanCode == 0x0000) {
				return MagicRemoteService.Properties.Resources.BindKeyboardErreur;
			} else {
				System.Text.StringBuilder sbString = new System.Text.StringBuilder(32);
				byte[] tabScanCode = System.BitConverter.GetBytes(this.usScanCode);
				if(WinApi.User32.GetKeyNameText(System.BitConverter.ToInt32(new byte[] { 0x00, 0x00, tabScanCode[0], (tabScanCode[1] == 0xE0) ? (byte)0x01 : (byte)0x00 }, 0), sbString, sbString.Capacity) == 0) {
					return "";
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
				default:
					return base.ToString();
			}
		}
	}
}