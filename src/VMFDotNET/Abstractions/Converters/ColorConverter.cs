using System;
using System.Globalization;
using VMFDotNET.Abstractions.DataTypes;

namespace VMFDotNET.Abstractions.Converters
{
	internal class ColorConverter : IDataConverter
	{
		public object ConvertToObject(string data)
		{
			var parts = data.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			return new Color
			{
				Red = byte.Parse(parts[0], CultureInfo.InvariantCulture),
				Green = byte.Parse(parts[1], CultureInfo.InvariantCulture),
				Blue = byte.Parse(parts[2], CultureInfo.InvariantCulture)
			};
		}

		public string ConvertToString(object obj) => obj.ToString();
	}
}
