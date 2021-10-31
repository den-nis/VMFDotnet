using System;
using System.Globalization;
using VMFDotNET.Abstractions.DataTypes;

namespace VMFDotNET.Abstractions.Converters
{
	internal class Vector3Converter : IDataConverter
	{
		public object ConvertToObject(string data)
		{
			var parts = data.Split(' ', StringSplitOptions.RemoveEmptyEntries);

			return new Vector3
			{
				X = float.Parse(parts[0], CultureInfo.InvariantCulture),
				Y = float.Parse(parts[1], CultureInfo.InvariantCulture),
				Z = float.Parse(parts[2], CultureInfo.InvariantCulture),
			};
		}

		public string ConvertToString(object obj) => obj.ToString();
	}
}
