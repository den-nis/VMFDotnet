using System.Linq;
using System.Text.RegularExpressions;
using VMFDotNET.Abstractions.DataTypes;

namespace VMFDotNET.Abstractions.Converters
{
	internal class PlaneConverter : IDataConverter
	{
		public object ConvertToObject(string data)
		{
			ParenthesisVector3Converter vector3Converter = new();
			var parts = Regex.Matches(data, "\\([^)]+\\)")
				.Select(m => vector3Converter.ConvertToObject(m.Value))
				.Cast<Vector3>()
				.ToArray();

			return new Plane(parts);
		}

		public string ConvertToString(object obj) => obj.ToString();
	}
}
