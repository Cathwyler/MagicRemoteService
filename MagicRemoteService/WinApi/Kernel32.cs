
namespace MagicRemoteService.WinApi {
	static class Kernel32 {
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
