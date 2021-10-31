using System.IO;
using System.Text;
using VMFDotNET.Tokenizer;

namespace VMFDotNET.Linq
{
	public static class VmfParser
	{
		public static VmfObject Parse(string input)
		{
			VmfReader reader = new(new StringReader(input));
			VmfObject current = new();
			string propertyName = null;

			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case VmfNodeType.ObjectEnd:
						if (current.Parent == null)
							return current;

						current = current.Parent;
						break;

					case VmfNodeType.ObjectHeader:
						var child = new VmfObject
						{
							Parent = current,
							Name = reader.Value
						};

						current.Add(child);
						current = child;
						break;

					case VmfNodeType.PropertyName:
						propertyName = reader.Value;
						break;

					case VmfNodeType.PropertyValue:
						current.AddProperty(propertyName, reader.Value);
						propertyName = null;
						break;
				}
			}
			return current;
		}

		public static string ConvertToString(VmfObject obj)
		{
			StringBuilder builder = new();
			WriteBlock(builder, obj, 0);
			return builder.ToString();
		}

		private static void WriteBlockWithBrackets(StringBuilder sb, VmfObject obj, int indentLevel)
		{
			string indent = new('\t', indentLevel);
			sb.AppendLine($"{indent}{obj.Name}");
			sb.AppendLine($"{indent}{{");
			WriteBlock(sb, obj, indentLevel + 1);
			sb.AppendLine($"{indent}}}");
		}

		private static void WriteBlock(StringBuilder sb, VmfObject obj, int indentLevel)
		{
			WriteProperties(sb, obj, indentLevel);
			WriteChildren(sb, obj, indentLevel);
		}

		private static void WriteChildren(StringBuilder builder, VmfObject obj, int indentLevel)
		{
			foreach (var child in obj.Children)
			{
				WriteBlockWithBrackets(builder, child, indentLevel);
			}
		}

		private static void WriteProperties(StringBuilder builder, VmfObject obj, int indentLevel)
		{
			string indent = new('\t', indentLevel);
			foreach (var property in obj.Properties)
			{
				builder.AppendLine($"{indent}\"{property.Name}\" \"{property.Value}\"");
			}
		}
	}
}
