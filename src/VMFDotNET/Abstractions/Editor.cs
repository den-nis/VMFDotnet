using VMFDotNET.Attributes;
using VMFDotNET.Abstractions.DataTypes;
using VMFDotNET.Abstractions.Converters;
using VMFDotNET.Serialization;

namespace VMFDotNET.Abstractions
{
	public class Editor : VmfContainer
	{
		[VmfName("color")]
		[VmfConverter(typeof(ColorConverter))]
		public Color Color { get; set; }

		[VmfName("logicalpos")]
		[VmfConverter(typeof(LogicalPositionConverter))]
		public LogicalPosition? Position { get; set; }

		[VmfName("visgroupshown")]
		public int VisGroupsShown { get; set; }

		[VmfName("visgroupautoshown")]
		public int VisGroupsAutoShown { get; set; }
	}
}
