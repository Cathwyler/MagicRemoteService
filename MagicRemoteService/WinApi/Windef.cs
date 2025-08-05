
#pragma warning disable IDE0079
#pragma warning disable IDE1006
namespace MagicRemoteService.WinApi {
[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct PointL {
		public int x;
		public int y;
	}
	[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
	public struct RectL {
		public int left;
		public int top;
		public int right;
		public int bottom;
	}
}
#pragma warning restore IDE1006
#pragma warning restore IDE0079
