using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VMFDotNET.ObjectNotation
{
	public sealed class SonReader : IDisposable
	{
		public string Value { get; private set; }
		private SonNodeType? _lastNodeType;
		private readonly TextReader _reader;

		public SonReader(TextReader reader)
		{
			_reader = reader;
		}

		public SonReader(Stream stream)
		{
			_reader = new StreamReader(stream);
		}

		public SonNodeType Read()
		{
			SkipWhiteSpace();

			var next = GetNextExpectedNodeType();

			switch (next)
			{
				case SonNodeType.PropertyName: ReadPropertyPart(); break;
				case SonNodeType.PropertyValue: ReadPropertyPart(); break;
				case SonNodeType.ObjectHeader: ReadObjectHeader(); break;
				case SonNodeType.ObjectStart: ReadObjectStart(); break;
				case SonNodeType.ObjectEnd: ReadObjectEnd(); break;

				default: break;
			}

			_lastNodeType = next;
			return next;
		}

		private SonNodeType GetNextExpectedNodeType()
		{
			if (_lastNodeType == null) return SonNodeType.DocumentStart;
			if (_lastNodeType == SonNodeType.DocumentStart) return SonNodeType.ObjectHeader;
			if (_lastNodeType == SonNodeType.PropertyName) return SonNodeType.PropertyValue;
			if (_lastNodeType == SonNodeType.ObjectHeader) return SonNodeType.ObjectStart;

			int peek = _reader.Peek();

			if (peek == -1)   return SonNodeType.DocumentEnd;
			if (peek == '\"') return SonNodeType.PropertyName;
			if (peek == '{')  return SonNodeType.ObjectStart;
			if (peek == '}')  return SonNodeType.ObjectEnd;

			if (_lastNodeType != SonNodeType.ObjectHeader) return SonNodeType.ObjectHeader;

			throw UnexpectedCharacter((char)peek);
		}

		private void ReadObjectEnd()
		{
			Value = ReadAndExpect('}').ToString();
		}

		private void ReadObjectStart()
		{
			Value = ReadAndExpect('{').ToString();
		}

		private void ReadObjectHeader()
		{
			Value = ReadUntil((peek) =>
			{
				if (char.IsNumber(peek))
					throw UnexpectedCharacter(peek);

				if (peek == '{' || char.IsControl(peek) || char.IsWhiteSpace(peek))
					return true;

				return false;
			});
		}

		private void ReadPropertyPart()
		{
			ReadAndExpect('\"');
			Value = ReadUntil((peek) =>
			{
				if (peek == '\"')
					return true;

				return false;
			});
			ReadAndExpect('\"');
		}

		private string ReadUntil(Func<char, bool> untilCondition)
		{
			StringBuilder builder = new();

			int peek;
			while (true)
			{
				peek = _reader.Peek();

				if (peek == -1)
					throw UnexpectedEndOfFile();

				if (untilCondition((char)peek))
					break;

				builder.Append((char)peek);
				_reader.Read();
			}

			var result = builder.ToString();

			if (result.Length == 0)
				throw UnexpectedCharacter((char)peek);

			return result;
		}

		private char ReadAndExpect(char expect)
		{
			var read = _reader.Read();

			if (read == -1) throw UnexpectedEndOfFile();
			if (read != expect) throw UnexpectedCharacter((char)read, expect);

			return (char)read;
		}

		public void SkipWhiteSpace()
		{
			while (true)
			{
				var peek = _reader.Peek();
				if (peek == -1) break;
				if (!char.IsWhiteSpace((char)peek)) break;
				_reader.Read();
			}
		}

		private static Exception UnexpectedCharacter(char character) => ReaderException($"Unexpected character '{character}'");

		private static Exception UnexpectedCharacter(char character, char expected) => ReaderException($"Unexpected character '{character}' expected '{expected}'");

		private static Exception UnexpectedEndOfFile() => ReaderException("Unexpected end of file");

		private static Exception ReaderException(string message) => new InvalidOperationException(message);
		
		public void Dispose()
		{
			_reader.Dispose();
		}
	}
}
