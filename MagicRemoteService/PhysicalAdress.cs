
namespace MagicRemoteService {
	public class PhysicalAddress : System.Net.NetworkInformation.PhysicalAddress {
		public PhysicalAddress(byte[] tab) : base(tab) { }
		public PhysicalAddress(System.Net.NetworkInformation.PhysicalAddress pa) : base(pa.GetAddressBytes()) { }
		public static new MagicRemoteService.PhysicalAddress Parse(string str) {
			return new MagicRemoteService.PhysicalAddress(System.Net.NetworkInformation.PhysicalAddress.Parse(str));
		}
		public static bool TryParse(string str, out MagicRemoteService.PhysicalAddress pa) {
			try {
				pa = MagicRemoteService.PhysicalAddress.Parse(str.ToUpper().Replace(':', '-'));
				return true;
			} catch(System.FormatException) {
				pa = null;
				return false;
			}
		}
		public override string ToString() {
			return string.Join(":", System.Linq.Enumerable.Select<byte, string>(this.GetAddressBytes(), delegate (byte b) {
				return b.ToString("X2");
			}));
		}
	}
}
