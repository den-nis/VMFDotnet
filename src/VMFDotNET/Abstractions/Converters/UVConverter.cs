using System.Globalization;
using System.Linq;
using VMFDotNET.Abstractions.DataTypes;

namespace VMFDotNET.Abstractions.Converters
{
	internal class UVConverter : IDataConverter
	{
		public object ConvertToObject(string data)
		{
			var parts = data.Split(' ');

			return new UV
			{
				X = float.Parse(parts[0].TrimStart('['), CultureInfo.InvariantCulture),
				Y = float.Parse(parts[1], CultureInfo.InvariantCulture),
				Z = float.Parse(parts[2], CultureInfo.InvariantCulture),

				Offset = float.Parse(parts[3].TrimEnd(']'), CultureInfo.InvariantCulture),
				Scale = float.Parse(parts.Last(), CultureInfo.InvariantCulture),
			};
		}

		public string ConvertToString(object obj) => obj.ToString();
	}
}
