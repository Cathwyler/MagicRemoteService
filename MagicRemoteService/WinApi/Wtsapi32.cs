
namespace MagicRemoteService.WinApi {
	internal static class Wtsapi32 {
		[System.Runtime.InteropServices.DllImport("wtsapi32.dll", SetLastError = true)]
		public static extern bool WTSQueryUserToken(uint sessionId, out System.IntPtr hToken);
	}
}
