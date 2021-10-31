using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using VMFDotNET.Attributes;

namespace VMFDotNET.Converter
{
	internal static class SerializationHelpers
	{
		private const BindingFlags BINDING_FLAGS =
			BindingFlags.Public |
			BindingFlags.NonPublic |
			BindingFlags.Instance;

		public static Dictionary<string, PropertyInfo> GetModelProperties(Type type)
		{
			var properties = type.GetProperties(BINDING_FLAGS)
				.Where(p => !ShouldIgnore(p))
				.Where(p => IsModelProperty(p));
			return properties.ToDictionary(k => k.GetCustomAttribute<VmfNameAttribute>().Name, v => v);
		}

		public static Dictionary<string, PropertyInfo> GetModelChildren(Type type)
		{
			var properties = type.GetProperties(BINDING_FLAGS)
				.Where(p => !ShouldIgnore(p))
				.Where(p => !IsModelProperty(p));
			return properties.ToDictionary(k => GetChildName(k), v => v);
		}

		private static bool ShouldIgnore(PropertyInfo property)
		{
			return property.GetCustomAttribute<VmfIgnoreAttribute>() != null;
		}

		private static string GetChildName(PropertyInfo property)
		{
			return property.GetCustomAttribute<VmfNameAttribute>()?.Name ?? property.Name;
		}

		private static bool IsModelProperty(PropertyInfo property)
		{
			if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
				return true;

			if (property.GetCustomAttribute<VmfConverterAttribute>() != null)
				return true;

			return false;
		}

		public static bool IsCollection(Type type) => type.IsGenericType 
			&& (type.GetGenericTypeDefinition() == typeof(IList<>)
			|| type.GetGenericTypeDefinition() == typeof(List<>));
		
		public static IList CreateListFromEnumerableType(Type enumerableType)
		{
			var child = enumerableType.GetGenericArguments()[0];
			return (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(child));
		}
	}
}
