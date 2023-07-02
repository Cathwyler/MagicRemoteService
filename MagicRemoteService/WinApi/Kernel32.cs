
namespace MagicRemoteService.WinApi {
	public enum ObjectSate : uint {
		WAIT_ABANDONED = 0x00000080,
		WAIT_OBJECT_0 = 0x00000000,
		WAIT_TIMEOUT = 0x00000102,
		WAIT_FAILED = 0xFFFFFFFF,
	}
	static class Kernel32 {
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint WTSGetActiveConsoleSessionId();
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint GetCurrentThreadId();
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern System.IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool CloseHandle(System.IntPtr hObject);
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		public static extern uint WaitForSingleObject(System.IntPtr hHandle, uint dwMilliseconds);
	}
}
