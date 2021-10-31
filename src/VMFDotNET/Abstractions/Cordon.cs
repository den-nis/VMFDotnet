using VMFDotNET.Attributes;
using VMFDotNET.Abstractions.DataTypes;
using VMFDotNET.Abstractions.Converters;
using VMFDotNET.Serialization;

namespace VMFDotNET.Abstractions
{
	public class Cordon : VmfContainer
	{
		[VmfName("mins")]
		[VmfConverter(typeof(ParenthesisVector3Converter))]
		public Vector3 Mins { get; set; }

		[VmfName("maxs")]
		[VmfConverter(typeof(ParenthesisVector3Converter))]
		public Vector3 Maxs { get; set; }

		[VmfName("active")]
		[VmfConverter(typeof(IntBoolConverter))]
		public bool Active { get; set; }
	}
}
