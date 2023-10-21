

namespace MagicRemoteService.WinApi {

	public enum CursorName {
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
	[System.Flags]
	public enum QueryDisplayConfigFlags : uint {
		QDC_ALL_PATHS = 0x00000001,
		QDC_ONLY_ACTIVE_PATHS = 0x00000002,
		QDC_DATABASE_CURRENT = 0x00000004,
		QDC_VIRTUAL_MODE_AWARE = 0x00000010,
		QDC_INCLUDE_HMD = 0x00000020,
		QDC_VIRTUAL_REFRESH_RATE_AWARE = 0x00000040
	}
	public enum DisplayConfigTopology : uint {
		DISPLAYCONFIG_TOPOLOGY_INTERNAL = 0x00000001,
		DISPLAYCONFIG_TOPOLOGY_CLONE = 0x00000002,
		DISPLAYCONFIG_TOPOLOGY_EXTEND = 0x00000004,
		DISPLAYCONFIG_TOPOLOGY_EXTERNAL = 0x00000008,
		DISPLAYCONFIG_TOPOLOGY_FORCE_UINT32 = 0xFFFFFFFF
	};
	public enum DisplayConfigOutputTechnology : uint {
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_OTHER = 0xFFFFFFFF,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15 = 0,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SVIDEO = 1,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPOSITE_VIDEO = 2,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPONENT_VIDEO = 3,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DVI = 4,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HDMI = 5,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_LVDS = 6,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_D_JPN = 8,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDI = 9,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EXTERNAL = 10,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EMBEDDED = 11,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EXTERNAL = 12,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EMBEDDED = 13,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDTVDONGLE = 14,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_MIRACAST = 15,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_INTERNAL = 0x80000000,
		DISPLAYCONFIG_OUTPUT_TECHNOLOGY_FORCE_UINT32 = 0xFFFFFFFF
	}

	public enum DisplayConfigScanlineOrdering : uint {
		DISPLAYCONFIG_SCANLINE_ORDERING_UNSPECIFIED = 0,
		DISPLAYCONFIG_SCANLINE_ORDERING_PROGRESSIVE = 1,
		DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED = 2,
		DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED_UPPERFIELDFIRST,
		DISPLAYCONFIG_SCANLINE_ORDERING_INTERLACED_LOWERFIELDFIRST = 3,
		DISPLAYCONFIG_SCANLINE_ORDERING_FORCE_UINT32 = 0xFFFFFFFF
	}

	public enum DisplayConfigRotation : uint {
		DISPLAYCONFIG_ROTATION_IDENTITY = 1,
		DISPLAYCONFIG_ROTATION_ROTATE90 = 2,
		DISPLAYCONFIG_ROTATION_ROTATE180 = 3,
		DISPLAYCONFIG_ROTATION_ROTATE270 = 4,
		DISPLAYCONFIG_ROTATION_FORCE_UINT32 = 0xFFFFFFFF
	}

	public enum DisplayConfigScaling : uint {
		DISPLAYCONFIG_SCALING_IDENTITY = 1,
		DISPLAYCONFIG_SCALING_CENTERED = 2,
		DISPLAYCONFIG_SCALING_STRETCHED = 3,
		DISPLAYCONFIG_SCALING_ASPECTRATIOCENTEREDMAX = 4,
		DISPLAYCONFIG_SCALING_CUSTOM = 5,
		DISPLAYCONFIG_SCALING_PREFERRED = 128,
		DISPLAYCONFIG_SCALING_FORCE_UINT32 = 0xFFFFFFFF
	}

	public enum DisplayConfigPixelFormat : uint {
		DISPLAYCONFIG_PIXELFORMAT_8BPP = 1,
		DISPLAYCONFIG_PIXELFORMAT_16BPP = 2,
		DISPLAYCONFIG_PIXELFORMAT_24BPP = 3,
		DISPLAYCONFIG_PIXELFORMAT_32BPP = 4,
		DISPLAYCONFIG_PIXELFORMAT_NONGDI = 5,
		DISPLAYCONFIG_PIXELFORMAT_FORCE_UINT32 = 0xFFFFFFFF
	}

	public enum DisplayConfigModeInfoType : uint {
		DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE = 1,
		DISPLAYCONFIG_MODE_INFO_TYPE_TARGET = 2,
		DISPLAYCONFIG_MODE_INFO_TYPE_DESKTOP_IMAGE = 3,
		DISPLAYCONFIG_MODE_INFO_TYPE_FORCE_UINT32 = 0xFFFFFFFF
	}

	public enum DisplayConfigDeviceInfoType : uint {
		DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME = 1,
		DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME = 2,
		DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE = 3,
		DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME = 4,
		DISPLAYCONFIG_DEVICE_INFO_SET_TARGET_PERSISTENCE = 5,
		DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_BASE_TYPE = 6,
		DISPLAYCONFIG_DEVICE_INFO_GET_SUPPORT_VIRTUAL_RESOLUTION = 7,
		DISPLAYCONFIG_DEVICE_INFO_SET_SUPPORT_VIRTUAL_RESOLUTION = 8,
		DISPLAYCONFIG_DEVICE_INFO_GET_ADVANCED_COLOR_INFO = 9,
		DISPLAYCONFIG_DEVICE_INFO_SET_ADVANCED_COLOR_STATE = 10,
		DISPLAYCONFIG_DEVICE_INFO_GET_SDR_WHITE_LEVEL = 11,
		DISPLAYCONFIG_DEVICE_INFO_GET_MONITOR_SPECIALIZATION,
		DISPLAYCONFIG_DEVICE_INFO_SET_MONITOR_SPECIALIZATION,
		DISPLAYCONFIG_DEVICE_INFO_FORCE_UINT32 = 0xFFFFFFFF
	}
	[System.Flags]
	public enum DisplayConfigPathInfoFlags : uint {
		DISPLAYCONFIG_PATH_ACTIVE = 0x00000001,
		DISPLAYCONFIG_PATH_BOOST_REFRESH_RATE = 0x00000008,
		DISPLAYCONFIG_PATH_SUPPORT_VIRTUAL_MODE = 0x00000010
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
	public struct LUID {
		public uint LowPart;
		public int HighPart;
	}


	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigPathSourceInfoDummyStructName {
		private readonly uint ui;
		public uint cloneGroupId { get { return (uint)((ui & 0x0000FFFF) >> 0); } }
		public uint sourceModeInfoIdx { get { return (uint)((ui & 0xFFFF0000) >> 16); } }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
	public struct DisplayConfigPathSourceInfoDummyUnionName {
		[System.Runtime.InteropServices.FieldOffset(0)]
		public uint modeInfoIdx;
		[System.Runtime.InteropServices.FieldOffset(0)]
		public DisplayConfigPathSourceInfoDummyStructName s;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigPathSourceInfo {
		public LUID adapterId;
		public uint id;
		public DisplayConfigPathSourceInfoDummyUnionName u;
		public uint statusFlags;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigPathTargetInfoDummyStructName {
		private readonly uint ui;
		public uint desktopModeInfoIdx { get { return (uint)((ui & 0x0000FFFF) >> 0); } }
		public uint targetModeInfoIdx { get { return (uint)((ui & 0xFFFF0000) >> 16); } }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
	public struct DisplayConfigPathTargetInfoDummyUnionName {
		[System.Runtime.InteropServices.FieldOffset(0)]
		public uint modeInfoIdx;
		[System.Runtime.InteropServices.FieldOffset(0)]
		public DisplayConfigPathTargetInfoDummyStructName s;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigPathTargetInfo {
		public LUID adapterId;
		public uint id;
		public uint modeInfoIdx;
		private DisplayConfigOutputTechnology outputTechnology;
		private DisplayConfigRotation rotation;
		private DisplayConfigScaling scaling;
		private DisplayConfigRational refreshRate;
		private DisplayConfigScanlineOrdering scanLineOrdering;
		public bool targetAvailable;
		public uint statusFlags;
	}


	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigPathInfo {
		public DisplayConfigPathSourceInfo sourceInfo;
		public DisplayConfigPathTargetInfo targetInfo;
		public DisplayConfigPathInfoFlags flags;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigRational {
		public uint Numerator;
		public uint Denominator;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfig2DRegion {
		public uint cx;
		public uint cy;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigVideoSignalInfoStructName {
		private readonly uint ui;
		public uint videoStandard { get { return (uint)((ui & 0x0000FFFF) >> 0); } }
		public uint vSyncFreqDivider { get { return (uint)((ui & 0x0003F0000) >> 16); } }
		public uint reserved { get { return (uint)((ui & 0xFFFC0000) >> 22); } }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
	public struct DisplayConfigVideoSignalInfoDummyUnionName {
		[System.Runtime.InteropServices.FieldOffset(0)]
		public DisplayConfigVideoSignalInfoStructName s;
		[System.Runtime.InteropServices.FieldOffset(0)]
		public uint videoStandard;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigVideoSignalInfo {
		public ulong pixelRate;
		public DisplayConfigRational hSyncFreq;
		public DisplayConfigRational vSyncFreq;
		public DisplayConfig2DRegion activeSize;
		public DisplayConfig2DRegion totalSize;
		public DisplayConfigVideoSignalInfoDummyUnionName u;
		public DisplayConfigScanlineOrdering scanLineOrdering;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigTargetMode {
		public DisplayConfigVideoSignalInfo targetVideoSignalInfo;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigSourceMode {
		public uint width;
		public uint height;
		public DisplayConfigPixelFormat pixelFormat;
		public System.Drawing.Point position;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigDesktopImageInfo {
		public System.Drawing.Point PathSourceSize;
		public System.Drawing.Rectangle DesktopImageRegion;
		public System.Drawing.Rectangle DesktopImageClip;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
	public struct DisplayConfigModeInfoDummyUnionName {
		[System.Runtime.InteropServices.FieldOffset(0)]
		public DisplayConfigTargetMode targetMode;
		[System.Runtime.InteropServices.FieldOffset(0)]
		public DisplayConfigSourceMode sourceMode;
		[System.Runtime.InteropServices.FieldOffset(0)]
		public DisplayConfigDesktopImageInfo desktopImageInfo;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigModeInfo {
		public DisplayConfigModeInfoType infoType;
		public uint id;
		public LUID adapterId;
		public DisplayConfigModeInfoDummyUnionName u;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigTargetDeviceNameFlagsStructName {
		private readonly uint ui;
		public uint friendlyNameFromEdid { get { return (uint)((ui & 0x00000001) >> 0); } }
		public uint friendlyNameForced { get { return (uint)((ui & 0x000000002) >> 1); } }
		public uint edidIdsValid { get { return (uint)((ui & 0x00000004) >> 2); } }
		public uint reserved { get { return (uint)((ui & 0xFFFFFFF8) >> 3); } }
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
	public struct DisplayConfigTargetDeviceNameFlagsDummyUnionName {
		[System.Runtime.InteropServices.FieldOffset(0)]
		public DisplayConfigTargetDeviceNameFlagsStructName s;
		[System.Runtime.InteropServices.FieldOffset(0)]
		public uint value;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigTargetDeviceNameFlags {
		public DisplayConfigTargetDeviceNameFlagsDummyUnionName u;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct DisplayConfigDeviceInfoHeader {
		public DisplayConfigDeviceInfoType type;
		public uint size;
		public LUID adapterId;
		public uint id;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
	public struct DisplayConfigSourceDeviceName {
		public DisplayConfigDeviceInfoHeader header;
		[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 32)]
		public string viewGdiDeviceName;
	}

	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
	public struct DisplayConfigTargetDeviceName {
		public DisplayConfigDeviceInfoHeader header;
		public DisplayConfigTargetDeviceNameFlags flags;
		public DisplayConfigOutputTechnology outputTechnology;
		public ushort edidManufactureId;
		public ushort edidProductCodeId;
		public uint connectorInstance;
		[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 64)]
		public string monitorFriendlyDeviceName;
		[System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 128)]
		public string monitorDevicePath;
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

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern int GetDisplayConfigBufferSizes(QueryDisplayConfigFlags flags, out uint numPathArrayElements, out uint numModeInfoArrayElements);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern int QueryDisplayConfig(QueryDisplayConfigFlags flags, ref uint numPathArrayElements, [System.Runtime.InteropServices.Out] DisplayConfigPathInfo[] pathArray, ref uint numModeInfoArrayElements, [System.Runtime.InteropServices.Out] DisplayConfigModeInfo[] modeInfoArray, out DisplayConfigTopology currentTopologyId);
		
		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern int QueryDisplayConfig(QueryDisplayConfigFlags flags, ref uint numPathArrayElements, [System.Runtime.InteropServices.Out] DisplayConfigPathInfo[] pathArray, ref uint numModeInfoArrayElements, [System.Runtime.InteropServices.Out] DisplayConfigModeInfo[] modeInfoArray, System.IntPtr currentTopologyId);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern int DisplayConfigGetDeviceInfo(ref DisplayConfigSourceDeviceName requestPacket);

		[System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
		public static extern int DisplayConfigGetDeviceInfo(ref DisplayConfigTargetDeviceName requestPacket);
	}
}
