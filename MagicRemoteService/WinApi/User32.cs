
#pragma warning disable IDE1006
namespace MagicRemoteService.WinApi {
	public enum OemCursorRessourceId : uint {
		OCR_NORMAL = 32512,
		OCR_IBEAM = 32513,
		OCR_WAIT = 32514,
		OCR_CROSS = 32515,
		OCR_UP = 32516,
		OCR_SIZENWSE = 32642,
		OCR_SIZENESW = 32643,
		OCR_SIZEWE = 32644,
		OCR_SIZENS = 32645,
		OCR_SIZEALL = 32646,
		OCR_HAND = 32649,
		OCR_NO = 32648,
		OCR_APPSTARTING = 32650
	}
	public enum SystemParametersInfoAction : uint {
		SPI_GETBEEP = 0x0001,
		SPI_SETBEEP = 0x0002,
		SPI_GETMOUSE = 0x0003,
		SPI_SETMOUSE = 0x0004,
		SPI_GETBORDER = 0x0005,
		SPI_SETBORDER = 0x0006,
		SPI_GETKEYBOARDSPEED = 0x000A,
		SPI_SETKEYBOARDSPEED = 0x000B,
		SPI_LANGDRIVER = 0x000C,
		SPI_ICONHORIZONTALSPACING = 0x000D,
		SPI_GETSCREENSAVETIMEOUT = 0x000E,
		SPI_SETSCREENSAVETIMEOUT = 0x000F,
		SPI_GETSCREENSAVEACTIVE = 0x0010,
		SPI_SETSCREENSAVEACTIVE = 0x0011,
		SPI_GETGRIDGRANULARITY = 0x0012,
		SPI_SETGRIDGRANULARITY = 0x0013,
		SPI_SETDESKWALLPAPER = 0x0014,
		SPI_SETDESKPATTERN = 0x0015,
		SPI_GETKEYBOARDDELAY = 0x0016,
		SPI_SETKEYBOARDDELAY = 0x0017,
		SPI_ICONVERTICALSPACING = 0x0018,
		SPI_GETICONTITLEWRAP = 0x0019,
		SPI_SETICONTITLEWRAP = 0x001A,
		SPI_GETMENUDROPALIGNMENT = 0x001B,
		SPI_SETMENUDROPALIGNMENT = 0x001C,
		SPI_SETDOUBLECLKWIDTH = 0x001D,
		SPI_SETDOUBLECLKHEIGHT = 0x001E,
		SPI_GETICONTITLELOGFONT = 0x001F,
		SPI_SETDOUBLECLICKTIME = 0x0020,
		SPI_SETMOUSEBUTTONSWAP = 0x0021,
		SPI_SETICONTITLELOGFONT = 0x0022,
		SPI_GETFASTTASKSWITCH = 0x0023,
		SPI_SETFASTTASKSWITCH = 0x0024,
		SPI_SETDRAGFULLWINDOWS = 0x0025,
		SPI_GETDRAGFULLWINDOWS = 0x0026,
		SPI_GETNONCLIENTMETRICS = 0x0029,
		SPI_SETNONCLIENTMETRICS = 0x002A,
		SPI_GETMINIMIZEDMETRICS = 0x002B,
		SPI_SETMINIMIZEDMETRICS = 0x002C,
		SPI_GETICONMETRICS = 0x002D,
		SPI_SETICONMETRICS = 0x002E,
		SPI_SETWORKAREA = 0x002F,
		SPI_GETWORKAREA = 0x0030,
		SPI_SETPENWINDOWS = 0x0031,
		SPI_GETHIGHCONTRAST = 0x0042,
		SPI_SETHIGHCONTRAST = 0x0043,
		SPI_GETKEYBOARDPREF = 0x0044,
		SPI_SETKEYBOARDPREF = 0x0045,
		SPI_GETSCREENREADER = 0x0046,
		SPI_SETSCREENREADER = 0x0047,
		SPI_GETANIMATION = 0x0048,
		SPI_SETANIMATION = 0x0049,
		SPI_GETFONTSMOOTHING = 0x004A,
		SPI_SETFONTSMOOTHING = 0x004B,
		SPI_SETDRAGWIDTH = 0x004C,
		SPI_SETDRAGHEIGHT = 0x004D,
		SPI_SETHANDHELD = 0x004E,
		SPI_GETLOWPOWERTIMEOUT = 0x004F,
		SPI_GETPOWEROFFTIMEOUT = 0x0050,
		SPI_SETLOWPOWERTIMEOUT = 0x0051,
		SPI_SETPOWEROFFTIMEOUT = 0x0052,
		SPI_GETLOWPOWERACTIVE = 0x0053,
		SPI_GETPOWEROFFACTIVE = 0x0054,
		SPI_SETLOWPOWERACTIVE = 0x0055,
		SPI_SETPOWEROFFACTIVE = 0x0056,
		SPI_SETCURSORS = 0x0057,
		SPI_SETICONS = 0x0058,
		SPI_GETDEFAULTINPUTLANG = 0x0059,
		SPI_SETDEFAULTINPUTLANG = 0x005A,
		SPI_SETLANGTOGGLE = 0x005B,
		SPI_GETWINDOWSEXTENSION = 0x005C,
		SPI_SETMOUSETRAILS = 0x005D,
		SPI_GETMOUSETRAILS = 0x005E,
		SPI_SETSCREENSAVERRUNNING = 0x0061,
		SPI_SCREENSAVERRUNNING = SPI_SETSCREENSAVERRUNNING,
		SPI_GETFILTERKEYS = 0x0032,
		SPI_SETFILTERKEYS = 0x0033,
		SPI_GETTOGGLEKEYS = 0x0034,
		SPI_SETTOGGLEKEYS = 0x0035,
		SPI_GETMOUSEKEYS = 0x0036,
		SPI_SETMOUSEKEYS = 0x0037,
		SPI_GETSHOWSOUNDS = 0x0038,
		SPI_SETSHOWSOUNDS = 0x0039,
		SPI_GETSTICKYKEYS = 0x003A,
		SPI_SETSTICKYKEYS = 0x003B,
		SPI_GETACCESSTIMEOUT = 0x003C,
		SPI_SETACCESSTIMEOUT = 0x003D,
		SPI_GETSERIALKEYS = 0x003E,
		SPI_SETSERIALKEYS = 0x003F,
		SPI_GETSOUNDSENTRY = 0x0040,
		SPI_SETSOUNDSENTRY = 0x0041,
		SPI_GETSNAPTODEFBUTTON = 0x005F,
		SPI_SETSNAPTODEFBUTTON = 0x0060,
		SPI_GETMOUSEHOVERWIDTH = 0x0062,
		SPI_SETMOUSEHOVERWIDTH = 0x0063,
		SPI_GETMOUSEHOVERHEIGHT = 0x0064,
		SPI_SETMOUSEHOVERHEIGHT = 0x0065,
		SPI_GETMOUSEHOVERTIME = 0x0066,
		SPI_SETMOUSEHOVERTIME = 0x0067,
		SPI_GETWHEELSCROLLLINES = 0x0068,
		SPI_SETWHEELSCROLLLINES = 0x0069,
		SPI_GETMENUSHOWDELAY = 0x006A,
		SPI_SETMENUSHOWDELAY = 0x006B,
		SPI_GETSHOWIMEUI = 0x006E,
		SPI_SETSHOWIMEUI = 0x006F,
		SPI_GETMOUSESPEED = 0x0070,
		SPI_SETMOUSESPEED = 0x0071,
		SPI_GETSCREENSAVERRUNNING = 0x0072,
		SPI_GETDESKWALLPAPER = 0x0073,
		SPI_GETACTIVEWINDOWTRACKING = 0x1000,
		SPI_SETACTIVEWINDOWTRACKING = 0x1001,
		SPI_GETMENUANIMATION = 0x1002,
		SPI_SETMENUANIMATION = 0x1003,
		SPI_GETCOMBOBOXANIMATION = 0x1004,
		SPI_SETCOMBOBOXANIMATION = 0x1005,
		SPI_GETLISTBOXSMOOTHSCROLLING = 0x1006,
		SPI_SETLISTBOXSMOOTHSCROLLING = 0x1007,
		SPI_GETGRADIENTCAPTIONS = 0x1008,
		SPI_SETGRADIENTCAPTIONS = 0x1009,
		SPI_GETKEYBOARDCUES = 0x100A,
		SPI_SETKEYBOARDCUES = 0x100B,
		SPI_GETMENUUNDERLINES = SPI_GETKEYBOARDCUES,
		SPI_SETMENUUNDERLINES = SPI_SETKEYBOARDCUES,
		SPI_GETACTIVEWNDTRKZORDER = 0x100C,
		SPI_SETACTIVEWNDTRKZORDER = 0x100D,
		SPI_GETHOTTRACKING = 0x100E,
		SPI_SETHOTTRACKING = 0x100F,
		SPI_GETMENUFADE = 0x1012,
		SPI_SETMENUFADE = 0x1013,
		SPI_GETSELECTIONFADE = 0x1014,
		SPI_SETSELECTIONFADE = 0x1015,
		SPI_GETTOOLTIPANIMATION = 0x1016,
		SPI_SETTOOLTIPANIMATION = 0x1017,
		SPI_GETTOOLTIPFADE = 0x1018,
		SPI_SETTOOLTIPFADE = 0x1019,
		SPI_GETCURSORSHADOW = 0x101A,
		SPI_SETCURSORSHADOW = 0x101B,
		SPI_GETMOUSESONAR = 0x101C,
		SPI_SETMOUSESONAR = 0x101D,
		SPI_GETMOUSECLICKLOCK = 0x101E,
		SPI_SETMOUSECLICKLOCK = 0x101F,
		SPI_GETMOUSEVANISH = 0x1020,
		SPI_SETMOUSEVANISH = 0x1021,
		SPI_GETFLATMENU = 0x1022,
		SPI_SETFLATMENU = 0x1023,
		SPI_GETDROPSHADOW = 0x1024,
		SPI_SETDROPSHADOW = 0x1025,
		SPI_GETBLOCKSENDINPUTRESETS = 0x1026,
		SPI_SETBLOCKSENDINPUTRESETS = 0x1027,
		SPI_GETUIEFFECTS = 0x103E,
		SPI_SETUIEFFECTS = 0x103F,
		SPI_GETFOREGROUNDLOCKTIMEOUT = 0x2000,
		SPI_SETFOREGROUNDLOCKTIMEOUT = 0x2001,
		SPI_GETACTIVEWNDTRKTIMEOUT = 0x2002,
		SPI_SETACTIVEWNDTRKTIMEOUT = 0x2003,
		SPI_GETFOREGROUNDFLASHCOUNT = 0x2004,
		SPI_SETFOREGROUNDFLASHCOUNT = 0x2005,
		SPI_GETCARETWIDTH = 0x2006,
		SPI_SETCARETWIDTH = 0x2007,
		SPI_GETMOUSECLICKLOCKTIME = 0x2008,
		SPI_SETMOUSECLICKLOCKTIME = 0x2009,
		SPI_GETFONTSMOOTHINGTYPE = 0x200A,
		SPI_SETFONTSMOOTHINGTYPE = 0x200B,
		SPI_GETFONTSMOOTHINGCONTRAST = 0x200C,
		SPI_SETFONTSMOOTHINGCONTRAST = 0x200D,
		SPI_GETFOCUSBORDERWIDTH = 0x200E,
		SPI_SETFOCUSBORDERWIDTH = 0x200F,
		SPI_GETFOCUSBORDERHEIGHT = 0x2010,
		SPI_SETFOCUSBORDERHEIGHT = 0x2011,
		SPI_GETFONTSMOOTHINGORIENTATION = 0x2012,
		SPI_SETFONTSMOOTHINGORIENTATION = 0x2013
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

		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern bool SystemParametersInfo(SystemParametersInfoAction uiAction, uint uiParam, System.IntPtr pvParam, uint fWinIni);
		[System.Runtime.InteropServices.DllImport("user32.dll")]
		public static extern bool SystemParametersInfo(SystemParametersInfoAction uiAction, uint uiParam, int pvParam, uint fWinIni);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetSystemCursor(System.IntPtr hcur, OemCursorRessourceId id);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern System.IntPtr LoadCursorFromFile(string lpFileName);
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern System.IntPtr CopyIcon(System.IntPtr pcur);
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
