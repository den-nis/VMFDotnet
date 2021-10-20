using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMFDotNET.Tokenizer
{
	public enum SonNodeType
	{
		DocumentStart,
		DocumentEnd,
		ObjectHeader,
		ObjectStart,
		ObjectEnd,
		PropertyName,
		PropertyValue,
	}
}
