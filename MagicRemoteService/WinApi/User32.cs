

namespace MagicRemoteService.WinApi {

	public enum CursorName {
		OCR_APPSTARTING = 32650,
		OCR_NORMAL = 32512,
		OCR_CROSS = 32515,
		OCR_HAND = 32649,
		OCR_HELP = 32651,
		OCR_IBEAM = 32513,
		OCR_NO = 32648,
		OCR_SIZEALL = 32646,
		OCR_SIZENESW = 32643,
		OCR_SIZENS = 32645,
		OCR_SIZENWSE = 32642,
		OCR_SIZEWE = 32644,
		OCR_UP = 32516,
		OCR_WAIT = 32514
	}
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
	public enum MapTypeFlags : uint {
		MAPVK_VK_TO_VSC = 0,
		MAPVK_VSC_TO_VK = 1,
		MAPVK_VK_TO_CHAR = 2,
		MAPVK_VSC_TO_VK_EX = 3,
		MAPVK_VK_TO_VSC_EX = 04
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
	public struct DummyUnionName {
		[System.Runtime.InteropServices.FieldOffset(0)] public MouseInput mi;
		[System.Runtime.InteropServices.FieldOffset(0)] public KeybdInput ki;
		[System.Runtime.InteropServices.FieldOffset(0)] public HardwareInput hi;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct Input {
		public InputType type;
		public DummyUnionName u;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct LastInputInfo {
		public uint cbSize;
		public uint dwTime;
	}
	static class User32 {
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern int GetKeyNameText(int lParam, System.Text.StringBuilder lpString, int cchSize);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetSystemCursor(System.IntPtr hcur, uint id);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern System.IntPtr LoadCursor(System.IntPtr hInstance, int lpCursorName);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern System.IntPtr CreateCursor(System.IntPtr hInst, int xHotSpot, int yHotSpot, int nWidth, int nHeight, byte[] pvANDPlane, byte[] pvXORPlane);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern System.IntPtr CopyIcon(System.IntPtr pcur);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern System.IntPtr GetProcessWindowStation();

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern System.IntPtr OpenWindowStation(string lpszWinSta, bool fInherit, uint dwDesiredAccess);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern bool CloseWindowStation(System.IntPtr hWinsta);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetProcessWindowStation(System.IntPtr hWinSta);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern System.IntPtr OpenInputDesktop(uint dwFlags, bool fInherit, uint dwDesiredAccess);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern bool CloseDesktop(System.IntPtr hDesktop);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern System.IntPtr GetThreadDesktop(uint dwThreadId);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetThreadDesktop(System.IntPtr hDesktop);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern uint MapVirtualKey(uint uCode, MapTypeFlags uMapType);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern System.IntPtr GetMessageExtraInfo();

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetLastInputInfo(ref LastInputInfo plii);
	}
}
