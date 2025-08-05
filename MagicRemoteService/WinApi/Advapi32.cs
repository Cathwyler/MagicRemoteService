
#pragma warning disable IDE0079
#pragma warning disable IDE1006
namespace MagicRemoteService.WinApi {
	[System.Flags]
	public enum ServiceCurrentState : uint {
		SERVICE_STOPPED = 0x00000001,
		SERVICE_START_PENDING = 0x00000002,
		SERVICE_STOP_PENDING = 0x00000003,
		SERVICE_RUNNING = 0x00000004,
		SERVICE_CONTINUE_PENDING = 0x00000005,
		SERVICE_PAUSE_PENDING = 0x00000006,
		SERVICE_PAUSED = 0x00000007,
	}
	[System.Flags]
	public enum AccessMaskStandard : uint {
		DELETE = 0x00010000,
		READ_CONTROL = 0x00020000,
		SYNCHRONIZE = 0x00100000,
		WRITE_DAC = 0x00040000,
		WRITE_OWNER = 0x00080000
	}
	[System.Flags]
	public enum AccessMaskWinsta : uint {
		WINSTA_ACCESSCLIPBOARD = 0x0004,
		WINSTA_ACCESSGLOBALATOMS = 0x0020,
		WINSTA_CREATEDESKTOP = 0x0008,
		WINSTA_ENUMDESKTOPS = 0x0001,
		WINSTA_ENUMERATE = 0x0100,
		WINSTA_EXITWINDOWS = 0x0040,
		WINSTA_READATTRIBUTES = 0x0002,
		WINSTA_READSCREEN = 0x0200,
		WINSTA_WRITEATTRIBUTES = 0x0010
	}
	[System.Flags]
	public enum AccessMaskToken : uint {
		TOKEN_ASSIGN_PRIMARY = 0x0001,
		TOKEN_DUPLICATE = 0x0002,
		TOKEN_IMPERSONATE = 0x0004,
		TOKEN_QUERY = 0x0008,
		TOKEN_QUERY_SOURCE = 0x0010,
		TOKEN_ADJUST_PRIVILEGES = 0x0020,
		TOKEN_ADJUST_GROUPS = 0x0040,
		TOKEN_ADJUST_DEFAULT = 0x0080,
		TOKEN_ADJUST_SESSIONID = 0x0100
	}
	[System.Flags]
	public enum AccessMaskDesktop : uint {
		DESKTOP_CREATEMENU = 0x0004,
		DESKTOP_CREATEWINDOW = 0x0002,
		DESKTOP_ENUMERATE = 0x0040,
		DESKTOP_HOOKCONTROL = 0x0008,
		DESKTOP_JOURNALPLAYBACK = 0x0020,
		DESKTOP_JOURNALRECORD = 0x0010,
		DESKTOP_READOBJECTS = 0x0001,
		DESKTOP_SWITCHDESKTOP = 0x0100,
		DESKTOP_WRITEOBJECTS = 0x0080
	}
	public enum SecurityImpersonationLevel : int {
		SecurityAnonymous = 0,
		SecurityIdentification = 1,
		SecurityImpersonation = 2,
		SecurityDelegation = 3,
	}
	public enum TokenType : int {
		TokenPrimary = 1,
		TokenImpersonation = 2
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct ServiceStatus {
		public uint dwServiceType;
		public ServiceCurrentState dwCurrentState;
		public uint dwControlsAccepted;
		public uint dwWin32ExitCode;
		public uint dwServiceSpecificExitCode;
		public uint dwCheckPoint;
		public uint dwWaitHint;
	};
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct ProcessInformation {
		public System.IntPtr hProcess;
		public System.IntPtr hThread;
		public uint dwProcessId;
		public uint dwThreadId;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct SecurityAttributes {
		public int Length;
		public System.IntPtr lpSecurityDescriptor;
		public bool bInheritHandle;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct StartupInfo {
		public int cb;
		public string lpReserved;
		public string lpDesktop;
		public string lpTitle;
		public uint dwX;
		public uint dwY;
		public uint dwXSize;
		public uint dwYSize;
		public uint dwXCountChars;
		public uint dwYCountChars;
		public uint dwFillAttribute;
		public uint dwFlags;
		public ushort wShowWindow;
		public short cbReserved2;
		public System.IntPtr lpReserved2;
		public System.IntPtr hStdInput;
		public System.IntPtr hStdOutput;
		public System.IntPtr hStdError;
	}

	internal static class Advapi32 {
		public const uint STANDARD_RIGHTS_ALL = (uint)(AccessMaskStandard.DELETE | AccessMaskStandard.READ_CONTROL | AccessMaskStandard.WRITE_DAC | AccessMaskStandard.WRITE_OWNER | AccessMaskStandard.SYNCHRONIZE);
		public const uint STANDARD_RIGHTS_READ = (uint)(AccessMaskStandard.READ_CONTROL);
		public const uint STANDARD_RIGHTS_WRITE = (uint)(AccessMaskStandard.READ_CONTROL);
		public const uint STANDARD_RIGHTS_EXECUTE = (uint)(AccessMaskStandard.READ_CONTROL);
		public const uint STANDARD_RIGHTS_REQUIRED = (uint)(AccessMaskStandard.DELETE | AccessMaskStandard.READ_CONTROL | AccessMaskStandard.WRITE_DAC | AccessMaskStandard.WRITE_OWNER);

		public const uint WINSTA_GENERIC_READ = STANDARD_RIGHTS_READ | (uint)(AccessMaskWinsta.WINSTA_ENUMDESKTOPS | AccessMaskWinsta.WINSTA_ENUMERATE | AccessMaskWinsta.WINSTA_READATTRIBUTES | AccessMaskWinsta.WINSTA_READSCREEN);
		public const uint WINSTA_GENERIC_WRITE = STANDARD_RIGHTS_WRITE | (uint)(AccessMaskWinsta.WINSTA_ACCESSCLIPBOARD | AccessMaskWinsta.WINSTA_CREATEDESKTOP | AccessMaskWinsta.WINSTA_WRITEATTRIBUTES);
		public const uint WINSTA_GENERIC_EXECUTE = STANDARD_RIGHTS_EXECUTE | (uint)(AccessMaskWinsta.WINSTA_ACCESSGLOBALATOMS | AccessMaskWinsta.WINSTA_EXITWINDOWS);
		public const uint WINSTA_GENERIC_ALL = STANDARD_RIGHTS_REQUIRED | (uint)(AccessMaskWinsta.WINSTA_ACCESSCLIPBOARD | AccessMaskWinsta.WINSTA_ACCESSGLOBALATOMS | AccessMaskWinsta.WINSTA_CREATEDESKTOP | AccessMaskWinsta.WINSTA_ENUMDESKTOPS | AccessMaskWinsta.WINSTA_ENUMERATE | AccessMaskWinsta.WINSTA_EXITWINDOWS | AccessMaskWinsta.WINSTA_READATTRIBUTES | AccessMaskWinsta.WINSTA_READSCREEN | AccessMaskWinsta.WINSTA_WRITEATTRIBUTES);

		public const uint DESKTOP_GENERIC_READ = STANDARD_RIGHTS_READ | (uint)(AccessMaskDesktop.DESKTOP_ENUMERATE | AccessMaskDesktop.DESKTOP_READOBJECTS);
		public const uint DESKTOP_GENERIC_WRITE = STANDARD_RIGHTS_WRITE | (uint)(AccessMaskDesktop.DESKTOP_CREATEMENU | AccessMaskDesktop.DESKTOP_CREATEWINDOW | AccessMaskDesktop.DESKTOP_HOOKCONTROL | AccessMaskDesktop.DESKTOP_JOURNALPLAYBACK | AccessMaskDesktop.DESKTOP_JOURNALRECORD | AccessMaskDesktop.DESKTOP_WRITEOBJECTS);
		public const uint DESKTOP_GENERIC_EXECUTE = STANDARD_RIGHTS_EXECUTE | (uint)(AccessMaskDesktop.DESKTOP_SWITCHDESKTOP);
		public const uint DESKTOP_GENERIC_ALL = STANDARD_RIGHTS_REQUIRED | (uint)(AccessMaskDesktop.DESKTOP_CREATEMENU | AccessMaskDesktop.DESKTOP_CREATEWINDOW | AccessMaskDesktop.DESKTOP_ENUMERATE | AccessMaskDesktop.DESKTOP_HOOKCONTROL | AccessMaskDesktop.DESKTOP_JOURNALPLAYBACK | AccessMaskDesktop.DESKTOP_JOURNALRECORD | AccessMaskDesktop.DESKTOP_READOBJECTS | AccessMaskDesktop.DESKTOP_SWITCHDESKTOP | AccessMaskDesktop.DESKTOP_WRITEOBJECTS);

		public const uint TOKEN_READ = STANDARD_RIGHTS_READ | (uint)(AccessMaskToken.TOKEN_QUERY);
		public const uint TOKEN_WRITE = STANDARD_RIGHTS_WRITE | (uint)(AccessMaskToken.TOKEN_ADJUST_PRIVILEGES | AccessMaskToken.TOKEN_ADJUST_GROUPS | AccessMaskToken.TOKEN_ADJUST_DEFAULT);
		public const uint TOKEN_EXECUTE = STANDARD_RIGHTS_EXECUTE;
		public const uint TOKEN_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | (uint)(AccessMaskToken.TOKEN_ADJUST_DEFAULT | AccessMaskToken.TOKEN_ADJUST_GROUPS | AccessMaskToken.TOKEN_ADJUST_PRIVILEGES | AccessMaskToken.TOKEN_ADJUST_SESSIONID | AccessMaskToken.TOKEN_ASSIGN_PRIMARY | AccessMaskToken.TOKEN_DUPLICATE | AccessMaskToken.TOKEN_IMPERSONATE | AccessMaskToken.TOKEN_QUERY | AccessMaskToken.TOKEN_QUERY_SOURCE);

		public const uint MAXIMUM_ALLOWED = 0x02000000;

		public const uint GENERIC_READ = 0x80000000;
		public const uint GENERIC_WRITE = 0x20000000;
		public const uint GENERIC_EXECUTE = 0x40000000;
		public const uint GENERIC_ALL = 0x10000000;

		[System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);
		[System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool OpenProcessToken(System.IntPtr hProcessHandle, uint dwDesiredAccess, out System.IntPtr hTokenHandle);
		[System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool DuplicateTokenEx(System.IntPtr hExistingToken, uint dwDesiredAccess, ref SecurityAttributes lpTokenAttributes, SecurityImpersonationLevel ImpersonationLevel, TokenType TokenType, out System.IntPtr phNewToken);
		[System.Runtime.InteropServices.DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool CreateProcessAsUser(System.IntPtr hToken, string lpApplicationName, string lpCommandLine, ref SecurityAttributes lpProcessAttributes, ref SecurityAttributes lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, System.IntPtr lpEnvironment, string lpCurrentDirectory, ref StartupInfo lpStartupInfo, out ProcessInformation lpProcessInformation);
	}
}
#pragma warning restore IDE1006
#pragma warning restore IDE0079
