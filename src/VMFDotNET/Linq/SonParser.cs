using System.IO;
using System.Text;
using VMFDotNET.Tokenizer;

namespace VMFDotNET.Linq
{
	public static class SonParser
	{
		public static SonObject Parse(string input)
		{
			SonReader reader = new(new StringReader(input));
			SonObject current = new();
			string propertyName = null;

			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case SonNodeType.ObjectEnd:
						if (current.Parent == null)
							return current;

						current = current.Parent;
						break;

					case SonNodeType.ObjectHeader:
						var child = new SonObject
						{
							Parent = current,
							Name = reader.Value
						};

						current.Add(child);
						current = child;
						break;

					case SonNodeType.PropertyName:
						propertyName = reader.Value;
						break;

					case SonNodeType.PropertyValue:
						current.AddProperty(propertyName, reader.Value);
						propertyName = null;
						break;
				}
			}
			return current;
		}

		public static string ConvertToString(SonObject obj)
		{
			StringBuilder builder = new();
			WriteBlock(builder, obj, 0);
			return builder.ToString();
		}

		private static void WriteBlockWithBrackets(StringBuilder sb, SonObject obj, int indentLevel)
		{
			string indent = new('\t', indentLevel);
			sb.AppendLine($"{indent}{obj.Name}");
			sb.AppendLine($"{indent}{{");
			WriteBlock(sb, obj, indentLevel + 1);
			sb.AppendLine($"{indent}}}");
		}

		private static void WriteBlock(StringBuilder sb, SonObject obj, int indentLevel)
		{
			WriteProperties(sb, obj, indentLevel);
			WriteChildren(sb, obj, indentLevel);
		}

		private static void WriteChildren(StringBuilder builder, SonObject obj, int indentLevel)
		{
			foreach (var child in obj.Children)
			{
				WriteBlockWithBrackets(builder, child, indentLevel);
			}
		}

		private static void WriteProperties(StringBuilder builder, SonObject obj, int indentLevel)
		{
			string indent = new('\t', indentLevel);
			foreach (var property in obj.Properties)
			{
				builder.AppendLine($"{indent}\"{property.Name}\" \"{property.Value}\"");
			}
		}
	}
}
