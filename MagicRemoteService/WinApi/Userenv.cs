
namespace MagicRemoteService.WinApi {
	static class Userenv {
		[System.Runtime.InteropServices.DllImport("userenv.dll", SetLastError = true)]
		public static extern bool CreateEnvironmentBlock(out System.IntPtr lpEnvironment, System.IntPtr hToken, bool bInherit);
		[System.Runtime.InteropServices.DllImport("userenv.dll", SetLastError = true)]
		public static extern bool DestroyEnvironmentBlock(System.IntPtr lpEnvironment);
	}
}
