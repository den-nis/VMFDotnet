using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMFDotNET.Tokenizer
{
	public class SonReaderException : Exception
	{
		public int LineNumber { get; private set; } = 1;
		public int CharacterNumber { get; private set; } = 1;
		public override string Message => _message;

		private readonly string _message;
		
		internal SonReaderException(TrackedTextReader reader, string message)
		{
			LineNumber = reader.LineNumber;
			CharacterNumber = reader.CharacterNumber;
			_message = $"{message} at line: ({LineNumber}) position: ({CharacterNumber})";
		}
	}
}
