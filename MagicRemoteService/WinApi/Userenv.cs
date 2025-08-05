
#pragma warning disable IDE0079
#pragma warning disable IDE1006
namespace MagicRemoteService.WinApi {
	internal static class Userenv {
		[System.Runtime.InteropServices.DllImport("userenv.dll", SetLastError = true)]
		public static extern bool CreateEnvironmentBlock(out System.IntPtr lpEnvironment, System.IntPtr hToken, bool bInherit);
		[System.Runtime.InteropServices.DllImport("userenv.dll", SetLastError = true)]
		public static extern bool DestroyEnvironmentBlock(System.IntPtr lpEnvironment);
	}
}
#pragma warning restore IDE1006
#pragma warning restore IDE0079
