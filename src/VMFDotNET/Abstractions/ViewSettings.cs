using VMFDotNET.Abstractions.Converters;
using VMFDotNET.Attributes;
using VMFDotNET.Serialization;

namespace VMFDotNET.Abstractions
{
	public class ViewSettings : VmfContainer
	{
		[VmfName("bSnapToGrid")]
		[VmfConverter(typeof(IntBoolConverter))]
		public bool SnapToGrid { get; set; }

		[VmfName("bShowGrid")]
		[VmfConverter(typeof(IntBoolConverter))]
		public bool ShowGrid { get; set; }

		[VmfName("bShowLogicalGrid")]
		[VmfConverter(typeof(IntBoolConverter))]
		public bool ShowLogicalGrid { get; set; }

		[VmfName("nGridSpacing")]
		public int GridSpacing { get; set; }

		[VmfName("bShow3DGrid")]
		[VmfConverter(typeof(IntBoolConverter))]
		public bool Show3DGrid { get; set; }
	}
}
