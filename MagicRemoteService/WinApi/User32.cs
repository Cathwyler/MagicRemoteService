
#pragma warning disable IDE1006
namespace MagicRemoteService.WinApi {

	public enum InputType : uint {
		INPUT_MOUSE = 0,
		INPUT_KEYBOARD = 1,
		INPUT_HARDWARE = 2
	}
	[System.Flags]
	public enum KeybdInputFlags : uint {
		KEYEVENTF_KEYDOWN = 0x0000,
		KEYEVENTF_EXTENDEDKEY = 0x0001,
		KEYEVENTF_KEYUP = 0x0002,
		KEYEVENTF_UNICODE = 0x0004,
		KEYEVENTF_SCANCODE = 0x0008
	}
	[System.Flags]
	public enum MouseInputFlags : uint {
		MOUSEEVENTF_MOVE = 0x0001,
		MOUSEEVENTF_LEFTDOWN = 0x0002,
		MOUSEEVENTF_LEFTUP = 0x0004,
		MOUSEEVENTF_RIGHTDOWN = 0x0008,
		MOUSEEVENTF_RIGHTUP = 0x0010,
		MOUSEEVENTF_MIDDLEDOWN = 0x0020,
		MOUSEEVENTF_MIDDLEUP = 0x0040,
		MOUSEEVENTF_XDOWN = 0x0080,
		MOUSEEVENTF_XUP = 0x0100,
		MOUSEEVENTF_WHEEL = 0x0800,
		MOUSEEVENTF_HWHEEL = 0x01000,
		MOUSEEVENTF_MOVE_NOCOALESCE = 0x2000,
		MOUSEEVENTF_VIRTUALDESK = 0x4000,
		MOUSEEVENTF_ABSOLUTE = 0x8000
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct MouseInput {
		public int dx;
		public int dy;
		public uint mouseData;
		public MouseInputFlags dwFlags;
		public uint time;
		public System.IntPtr dwExtraInfo;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct KeybdInput {
		public ushort wVk;
		public ushort wScan;
		public KeybdInputFlags dwFlags;
		public uint time;
		public System.IntPtr dwExtraInfo;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct HardwareInput {
		public uint uMsg;
		public ushort wParamL;
		public ushort wParamH;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
	public struct InputDummyUnionName {
		[System.Runtime.InteropServices.FieldOffset(0)]
		public MouseInput mi;
		[System.Runtime.InteropServices.FieldOffset(0)]
		public KeybdInput ki;
		[System.Runtime.InteropServices.FieldOffset(0)]
		public HardwareInput hi;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct Input {
		public InputType type;
		public InputDummyUnionName u;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct LastInputInfo {
		public uint cbSize;
		public uint dwTime;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct PowerBroadcastSetting {
		public System.Guid PowerSetting;
		public uint DataLength;
		public byte Data;
	}

	internal static class User32 {

		public static System.Guid GUID_ACDC_POWER_SOURCE = new System.Guid("5D3E9A59-E9D5-4B00-A6BD-FF34FF516548");
		public static System.Guid GUID_BATTERY_PERCENTAGE_REMAINING = new System.Guid("A7AD8041-B45A-4CAE-87A3-EECBB468A9E1");
		public static System.Guid GUID_CONSOLE_DISPLAY_STATE = new System.Guid("6FE69556-704A-47A0-8F24-C28D936FDA47");
		public static System.Guid GUID_GLOBAL_USER_PRESENCE = new System.Guid("786E8A1D-B427-4344-9207-09E70BDCBEA9");
		public static System.Guid GUID_IDLE_BACKGROUND_TASK = new System.Guid("515C31D8-F734-163D-A0FD-11A08C91E8F1");
		public static System.Guid GUID_LIDSWITCH_STATE_CHANGE = new System.Guid("BA3E0F4D-B817-4094-A2D1-D56379E6A0F3");
		public static System.Guid GUID_MONITOR_POWER_ON = new System.Guid("02731015-4510-4526-99E6-E5A17EBD1AEA");
		public static System.Guid GUID_POWER_SAVING_STATUS = new System.Guid("E00958C0-C213-4ACE-AC77-FECCED2EEEA5");
		public static System.Guid GUID_ENERGY_SAVER_STATUS = new System.Guid("550E8400-E29B-41D4-A716-446655440000");
		public static System.Guid GUID_POWERSCHEME_PERSONALITY = new System.Guid("245D8541-3943-4422-B025-13A784F679B7");
		public static System.Guid GUID_MIN_POWER_SAVINGS = new System.Guid("8C5E7FDA-E8BF-4A96-9A85-A6E23A8C635C");
		public static System.Guid GUID_MAX_POWER_SAVINGS = new System.Guid("A1841308-3541-4FAB-BC81-F71556F20B4A");
		public static System.Guid GUID_TYPICAL_POWER_SAVINGS = new System.Guid("381B4222-F694-41F0-9685-FF5BB260DF2E");
		public static System.Guid GUID_SESSION_DISPLAY_STATUS = new System.Guid("2B84C20E-AD23-4DDF-93DB-05FFBD7EFCA5");
		public static System.Guid GUID_SESSION_USER_PRESENCE = new System.Guid("3C0F4548-C03F-4C4D-B9F2-237EDE686376");
		public static System.Guid GUID_SYSTEM_AWAYMODE = new System.Guid("98A7F580-01F7-48AA-9C0F-44352C29E5C0");

		public const uint DEVICE_NOTIFY_WINDOW_HANDLE = 0;
		public const uint DEVICE_NOTIFY_SERVICE_HANDLE = 1;

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern int GetKeyNameText(int lParam, System.Text.StringBuilder lpString, int cchSize);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern System.IntPtr OpenInputDesktop(uint dwFlags, bool fInherit, uint dwDesiredAccess);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern bool CloseDesktop(System.IntPtr hDesktop);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetThreadDesktop(System.IntPtr hDesktop);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetLastInputInfo(ref LastInputInfo plii);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern System.IntPtr RegisterPowerSettingNotification(System.IntPtr hRecipient, ref System.Guid PowerSettingGuid, uint Flags);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern bool UnregisterPowerSettingNotification(System.IntPtr Handle);
	}
}
#pragma warning restore IDE1006
