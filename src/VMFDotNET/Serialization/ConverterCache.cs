using System;
using System.Collections.Generic;
using VMFDotNET.Abstractions.Converters;

namespace VMFDotNET.Serialization
{
	internal class ConverterCache
	{
		readonly Dictionary<Type, IDataConverter> _converterMethods = new();

		public IDataConverter GetConverter(Type converterType)
		{
			if (_converterMethods.TryGetValue(converterType, out var converter))
			{
				return converter;
			}
			else
			{
				converter = (IDataConverter)Activator.CreateInstance(converterType);
				_converterMethods.Add(converterType, converter);
				return converter;
			}
		}
	}
}
