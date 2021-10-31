using System.Collections.Generic;
using VMFDotNET.Attributes;
using VMFDotNET.Serialization;

namespace VMFDotNET.Abstractions 
{
	public class Solid : VmfContainer
	{
		[VmfName("id")]
		public int Id { get; set; }

		[VmfName("side")]
		public IList<Side> Sides { get; set; }

		[VmfName("editor")]
		public Editor Editor { get; set; }
	}
}
