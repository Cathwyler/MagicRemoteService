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
		public readonly System.Windows.Forms.Keys kValue;
		public BindKeyboard(System.Windows.Forms.Keys _kValue) {
			this.kValue = _kValue;
		}
		public override string ToString() {
			return new System.Windows.Forms.KeysConverter().ConvertToString(this.kValue);
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