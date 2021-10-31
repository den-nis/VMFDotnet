using System.Collections.Generic;
using VMFDotNET.Attributes;
using VMFDotNET.Serialization;

namespace VMFDotNET.Abstractions
{
	public class Entity : VmfContainer
	{
		[VmfName("id")]
		public int Id { get; set; }

		[VmfName("classname")]
		public string ClassName { get; set; }

		[VmfName("editor")]
		public Editor Editor { get; set; }

		[VmfName("solid")]
		public IList<Solid> Solids { get; set; }
	}
}
