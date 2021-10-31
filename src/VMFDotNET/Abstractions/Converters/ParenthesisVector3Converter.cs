using VMFDotNET.Abstractions.DataTypes;

namespace VMFDotNET.Abstractions.Converters
{
	internal class ParenthesisVector3Converter : IDataConverter
	{
		public object ConvertToObject(string data)
		{
			Vector3Converter converter = new();
			return converter.ConvertToObject(data.TrimStart('(').TrimEnd(')'));
		}

		public string ConvertToString(object obj) => ((Vector3)obj).ToStringWithParenthesis();
	}
}
