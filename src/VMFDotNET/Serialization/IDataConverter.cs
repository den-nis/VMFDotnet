using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMFDotNET.Abstractions.Converters
{
	internal interface IDataConverter
	{
		string ConvertToString(object obj);
		object ConvertToObject(string data);
	}
}
