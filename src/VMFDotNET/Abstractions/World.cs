using System.Collections.Generic;
using VMFDotNET.Attributes;
using VMFDotNET.Serialization;

namespace VMFDotNET.Abstractions
{
	public class World : VmfContainer
	{
		[VmfName("id")]
		public int Id { get; set; }

		[VmfName("mapversion")]
		public int MapVersion { get; set; }

		[VmfName("classname")]
		public string ClassName { get; set; }

		[VmfName("detailvbsp")]
		public string DetailVBSP { get; set; }

		[VmfName("detailmaterial")]
		public string DetailMaterial { get; set; }

		[VmfName("maxpropscreenwidth")]
		public string MaxPropScreenWidth { get; set; }

		[VmfName("skyname")]
		public string SkyName { get; set; }

		[VmfName("solid")]
		public IList<Solid> Solids { get; set; }
	}
}
