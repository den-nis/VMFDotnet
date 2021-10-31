using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using VMFDotNET.Abstractions;
using VMFDotNET.Attributes;
using VMFDotNET.Converter;
using VMFDotNET.Linq;

namespace VMFDotNET.Serialization
{
	public class VmfDeserializer
	{
		private readonly ConverterCache _cache = new(); 

		public Map DeserializeMap(VmfObject main)
		{
			return (Map)Deserialize(typeof(Map), main);
		}

		public T Deserialize<T>(VmfObject obj) where T : VmfContainer
		{
			return (T)Deserialize(typeof(T), obj);
		}

		private VmfContainer Deserialize(Type type, VmfObject main)
		{
			var children = SerializationHelpers.GetModelChildren(type);
			var properties = SerializationHelpers.GetModelProperties(type);
			VmfContainer instance = (VmfContainer)Activator.CreateInstance(type);

			foreach (var property in main.Properties)
			{
				if (properties.TryGetValue(property.Name, out var info))
				{
					SetPropertyValue(instance, info, property.Value);
				}
				else
				{
					instance.AdditionalProperties.Add(property);
				}
			}

			foreach (var child in main.Children)
			{
				if (children.TryGetValue(child.Name, out var property))
				{
					VmfContainer obj;
					if (SerializationHelpers.IsCollection(property.PropertyType))
					{
						obj = Deserialize(property.PropertyType.GetGenericArguments()[0], child);
					}
					else
					{
						obj = Deserialize(property.PropertyType, child);
					}
					SetChildValue(instance, property, obj);
				}
				else
				{
					instance.AdditionalChildren.Add(child);
				}
			}

			return instance;
		}

		private static void SetChildValue(object instance, PropertyInfo property, VmfContainer value)
		{
			if (SerializationHelpers.IsCollection(property.PropertyType))
			{
				if (property.GetValue(instance) is not IList collection)
				{
					collection = SerializationHelpers.CreateListFromEnumerableType(property.PropertyType);
					property.SetValue(instance, collection);
				}
				collection.Add(value);
			}
			else
			{
				property.SetValue(instance, value);
			}
		}

		private void SetPropertyValue(object instance, PropertyInfo property, string value)
		{
			var converter = property.GetCustomAttribute<VmfConverterAttribute>();

			if (converter != null)
			{
				property.SetValue(instance, _cache.GetConverter(converter.Converter).ConvertToObject(value));
			}
			else
			{
				property.SetValue(instance, Convert.ChangeType(value, property.PropertyType, CultureInfo.InvariantCulture));
			}
		}
	}
}
