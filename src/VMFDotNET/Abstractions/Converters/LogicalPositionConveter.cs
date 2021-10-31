using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMFDotNET.Abstractions.DataTypes;

namespace VMFDotNET.Abstractions.Converters
{
	internal class LogicalPositionConverter : IDataConverter
	{
		public object ConvertToObject(string data)
		{
			var parts = data.Trim(new[] { '[', ']' }).Split(' ', StringSplitOptions.RemoveEmptyEntries);

			return new LogicalPosition
			{
				X = int.Parse(parts[0], CultureInfo.InvariantCulture),
				Y = int.Parse(parts[1], CultureInfo.InvariantCulture),
			};
		}

		public string ConvertToString(object obj) => obj.ToString();
	}
}
