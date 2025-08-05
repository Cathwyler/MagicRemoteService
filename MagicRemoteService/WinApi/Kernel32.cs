
#pragma warning disable IDE0079
#pragma warning disable IDE1006
namespace MagicRemoteService.WinApi {
	internal static class Kernel32 {
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint WTSGetActiveConsoleSessionId();
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool CloseHandle(System.IntPtr hObject);
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetNamedPipeServerProcessId(System.IntPtr Pipe, out uint ServerProcessId);
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool GetNamedPipeClientProcessId(System.IntPtr Pipe, out uint ClientProcessId);
	}
}
#pragma warning restore IDE1006
#pragma warning restore IDE0079
