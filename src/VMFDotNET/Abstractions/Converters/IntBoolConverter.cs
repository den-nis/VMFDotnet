using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMFDotNET.Abstractions.Converters
{
	/// <summary>
	/// 0 == false
	/// other numbers are true
	/// </summary>
	internal class IntBoolConverter : IDataConverter
	{
		public object ConvertToObject(string data)
		{
			return int.Parse(data, CultureInfo.InvariantCulture) != 0;
		}

		public string ConvertToString(object obj)
		{
			return (bool)obj ? "1" : "0";
		}
	}
}
