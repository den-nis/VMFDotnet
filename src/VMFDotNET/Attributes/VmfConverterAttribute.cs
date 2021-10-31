using System;

namespace VMFDotNET.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	internal class VmfConverterAttribute : Attribute
	{
		public Type Converter { get; }

		public VmfConverterAttribute(Type converter)
		{
			Converter = converter;
		}
	}
}
