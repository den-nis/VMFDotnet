using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VMFDotNET.Tokenizer
{
	public sealed class VmfReader : IDisposable
	{
		public int Depth { get; private set; }
		public string Value { get; private set; }
		public VmfNodeType NodeType { get; private set; }

		private readonly Dictionary<VmfNodeType, Func<VmfNodeType, bool>> _nodeOrdering = new()
		{
			[VmfNodeType.PropertyName] = (to) => to == VmfNodeType.PropertyValue,
			[VmfNodeType.ObjectHeader] = (to) => to == VmfNodeType.ObjectStart,

			[VmfNodeType.PropertyValue] = (to) => new[] {
				VmfNodeType.PropertyName,
				VmfNodeType.ObjectHeader,
				VmfNodeType.ObjectEnd
				}.Contains(to),

			[VmfNodeType.ObjectStart] = (to) => new[] { 
				VmfNodeType.PropertyName,
				VmfNodeType.ObjectHeader,
				VmfNodeType.ObjectEnd
				}.Contains(to),

			[VmfNodeType.ObjectEnd] = (to) => new[] {
				VmfNodeType.PropertyName,
				VmfNodeType.ObjectHeader,
				VmfNodeType.ObjectEnd
				}.Contains(to),
		};

		private VmfNodeType? _lastNodeType;
		private readonly TrackedTextReader _textReader;

		public VmfReader(Stream reader)
		{
			_textReader = new TrackedTextReader(new StreamReader(reader));
		}

		public VmfReader(TextReader reader)
		{
			_textReader = new TrackedTextReader(reader);
		}

		public bool Read()
		{
			NodeType = VmfNodeType.None;
			Value = null;

			SkipWhiteSpace();

			var next = GetNextExpectedNodeType();
			CheckNodeOrdering(next);

			switch (next)
			{
				case VmfNodeType.PropertyName: ReadPropertyPart(); break;
				case VmfNodeType.PropertyValue: ReadPropertyPart(); break;
				case VmfNodeType.ObjectHeader: ReadObjectHeader(); break;

				case VmfNodeType.ObjectStart:
					Depth++;
					ReadObjectStart(); 
					break;

				case VmfNodeType.ObjectEnd:
					if (--Depth < 0) throw UnexpectedCharacter('}');
					ReadObjectEnd(); 
					break;

				case VmfNodeType.None:
					return false;

				default: break;
			}

			_lastNodeType = next;
			NodeType = next;
			return true;
		}

		private void CheckNodeOrdering(VmfNodeType next)
		{
			if (next == VmfNodeType.None)
				return;

			if (_lastNodeType.HasValue && !_nodeOrdering[_lastNodeType.Value](next))
			{
				throw UnexpectedCharacter((char)_textReader.Read());
			}
		}

		private VmfNodeType GetNextExpectedNodeType()
		{
			if (_lastNodeType == null) return VmfNodeType.ObjectHeader;
			if (_lastNodeType == VmfNodeType.PropertyName) return VmfNodeType.PropertyValue;
			if (_lastNodeType == VmfNodeType.ObjectHeader) return VmfNodeType.ObjectStart;

			int peek = _textReader.Peek();

			if (peek == -1)   return VmfNodeType.None;
			if (peek == '\"') return VmfNodeType.PropertyName;
			if (peek == '{')  return VmfNodeType.ObjectStart;
			if (peek == '}')  return VmfNodeType.ObjectEnd;

			if (_lastNodeType != VmfNodeType.ObjectHeader) return VmfNodeType.ObjectHeader;

			throw UnexpectedCharacter((char)_textReader.Read());
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
					throw UnexpectedCharacter((char)_textReader.Read());

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

			while (true)
			{
				int peek = _textReader.Peek();

				if (peek == -1)
					throw UnexpectedEndOfFile();

				if (untilCondition((char)peek))
					break;

				builder.Append((char)peek);
				_textReader.Read();
			}

			var result = builder.ToString();

			if (result.Length == 0)
				throw UnexpectedCharacter((char)_textReader.Read());

			return result;
		}

		private char ReadAndExpect(char expect)
		{
			var read = _textReader.Read();

			if (read == -1) throw UnexpectedEndOfFile();
			if (read != expect) throw UnexpectedCharacter((char)read, expect);

			return (char)read;
		}

		public void SkipWhiteSpace()
		{
			while (true)
			{
				var peek = _textReader.Peek();
				if (peek == -1) break;
				if (!char.IsWhiteSpace((char)peek)) break;
				_textReader.Read();
			}
		}

		private Exception UnexpectedCharacter(char character) => ReaderException($"Unexpected character '{character}'");

		private Exception UnexpectedCharacter(char character, char expected) => ReaderException($"Unexpected character '{character}' expected '{expected}'");

		private Exception UnexpectedEndOfFile() => ReaderException("Unexpected end of file");

		private Exception ReaderException(string message)
		{
			return new VmfReaderException(_textReader, message);
		}
		
		public void Dispose()
		{
			_textReader.Dispose();
		}
	}
}
