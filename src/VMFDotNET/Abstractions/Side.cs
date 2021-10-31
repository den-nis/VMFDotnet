using VMFDotNET.Attributes;
using VMFDotNET.Abstractions.DataTypes;
using VMFDotNET.Abstractions.Converters;
using VMFDotNET.Serialization;

namespace VMFDotNET.Abstractions
{
	public class Side : VmfContainer
	{
		[VmfName("id")]
		public int Id { get; set; }

		[VmfName("plane")]
		[VmfConverter(typeof(PlaneConverter))]
		public Plane Plane { get; set; }

		[VmfName("material")]
		public string Material { get; set; }

		[VmfName("uaxis")]
		[VmfConverter(typeof(UVConverter))]
		public UV UAxis { get; set; }

		[VmfName("vaxis")]
		[VmfConverter(typeof(UVConverter))]
		public UV VAxis { get; set; }

		[VmfName("rotation")]
		public float Rotation { get; set; }

		[VmfName("lightmapscale")]
		public int LightMapScale { get; set; }

		[VmfName("smoothing_groups")]
		public int SmoothingGroups { get; set; }
	}
}
