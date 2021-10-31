using VMFDotNET.Attributes;
using VMFDotNET.Serialization;

namespace VMFDotNET.Abstractions
{
	public class Cameras : VmfContainer
	{
		[VmfName("activecamera")]
		public int ActiveCamera { get; set; } = -1;
	}
}
