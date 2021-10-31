using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using VMFDotNET.Abstractions;
using VMFDotNET.Attributes;
using VMFDotNET.Converter;
using VMFDotNET.Linq;

namespace VMFDotNET.Serialization
{
	public class VmfSerializer
	{
		private readonly ConverterCache _cache = new();

		public VmfObject SerializeMap(Map map)
		{
			return Serialize(map);
		}

		public VmfObject Serialize(VmfContainer container)
		{
			return SerializeOne(container, string.Empty);
		}

		private IEnumerable<VmfObject> SerializeOneOrMore(object obj, string name)
		{
			if (SerializationHelpers.IsCollection(obj.GetType()))
			{
				if (obj is IEnumerable list)
				{
					foreach(var item in list)
					{
						yield return SerializeOne((VmfContainer)item, name);
					}
				}
			}
			else
			{
				yield return SerializeOne((VmfContainer)obj, name);
			}
		}

		private VmfObject SerializeOne(VmfContainer container, string name)
		{
			VmfObject vmfObject = new();
			vmfObject.Name = name;
			SetupProperties(vmfObject, container);
			SetupChildren(vmfObject, container);
			return vmfObject;
		}

		private void SetupChildren(VmfObject vmfObject, VmfContainer container)
		{
			var children = SerializationHelpers.GetModelChildren(container.GetType());

			foreach (var child in children)
			{
				object value = child.Value.GetValue(container);

				if (value == null)
				{
					if (child.Value.GetCustomAttribute<VmfIncludeIfNullAttribute>() != null)
					{
						vmfObject.Add(new VmfObject { Name = child.Key });
					}

					continue;
				}

				foreach(var item in SerializeOneOrMore(value, child.Key))
				{
					vmfObject.Add(item);
				}
			}

			foreach (var additional in container.AdditionalChildren)
			{
				vmfObject.Add(additional);
			}
		}

		private void SetupProperties(VmfObject vmfObject, VmfContainer container)
		{
			var properties = SerializationHelpers.GetModelProperties(container.GetType());

			foreach (var property in properties)
			{
				var value = property.Value.GetValue(container);

				if (value == null && property.Value.GetCustomAttribute<VmfIncludeIfNullAttribute>() == null)
					continue;

				var attribute = property.Value.GetCustomAttribute<VmfConverterAttribute>();
				if (attribute != null)
				{
					vmfObject.AddProperty(property.Key, _cache.GetConverter(attribute.Converter).ConvertToString(value));
				}
				else
				{
					vmfObject.AddProperty(property.Key, ToStringInvariant(value));
				}
			}

			foreach (var additional in container.AdditionalProperties)
			{
				vmfObject.AddProperty(additional.Name, additional.Value);
			}
		}

		private static string ToStringInvariant(object obj)
		{
			return (string)obj.GetType()
			.GetMethod("ToString", new[] { typeof(IFormatProvider) })
			.Invoke(obj, new object[] { CultureInfo.InvariantCulture });
		}
	}
}
