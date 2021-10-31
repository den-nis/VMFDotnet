using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMFDotNET.Attributes;
using VMFDotNET.Linq;

namespace VMFDotNET.Serialization
{
	public abstract class VmfContainer
	{
		[VmfIgnore]
		public List<VmfProperty> AdditionalProperties { get; } = new();

		[VmfIgnore]
		public List<VmfObject> AdditionalChildren { get; } = new(); 
	}
}