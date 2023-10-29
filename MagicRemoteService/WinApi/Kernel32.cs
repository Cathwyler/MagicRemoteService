
namespace MagicRemoteService.WinApi {
	static class Kernel32 {
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint WTSGetActiveConsoleSessionId();
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetCurrentThreadId();
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern System.IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool CloseHandle(System.IntPtr hObject);
	}
}
