using System;
using System.IO;

namespace VMFDotNET.ObjectNotation
{
	internal sealed class TrackedTextReader : IDisposable
	{
		private readonly TextReader _reader;

		public int LineNumber { get; private set; } = 1;
		public int CharacterNumber { get; private set; } = 1;

		public TrackedTextReader(TextReader reader)
		{
			_reader = reader;
		}

		public int Read()
		{
			var read = _reader.Read();

			if (read == '\n')
			{
				LineNumber++;
				CharacterNumber = 1;
			}
			else if (!char.IsControl((char)read))
			{
				CharacterNumber++;
			}

			return read;
		}

		public int Peek() => _reader.Peek();

		public void Dispose()
		{
			_reader.Dispose();
		}
	}
}
