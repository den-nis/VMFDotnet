using System;

namespace VMFDotNET.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	internal class VmfNameAttribute : Attribute
	{
		public string Name { get; }

		public VmfNameAttribute(string name)
		{
			Name = name;
		}
	}
}
