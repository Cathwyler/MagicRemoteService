
#pragma warning disable IDE0079
#pragma warning disable IDE1006
namespace MagicRemoteService.WinApi {
	internal static class Wtsapi32 {
		[System.Runtime.InteropServices.DllImport("wtsapi32.dll", SetLastError = true)]
		public static extern bool WTSQueryUserToken(uint sessionId, out System.IntPtr hToken);
	}
}
#pragma warning restore IDE1006
#pragma warning restore IDE0079
