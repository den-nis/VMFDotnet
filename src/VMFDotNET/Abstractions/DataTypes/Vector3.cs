using System.Globalization;

namespace VMFDotNET.Abstractions.DataTypes
{
	public struct Vector3
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }

		public override string ToString()
		{
			string x = X.ToString(CultureInfo.InvariantCulture);
			string y = Y.ToString(CultureInfo.InvariantCulture);
			string z = Z.ToString(CultureInfo.InvariantCulture);

			return $"{x} {y} {z}";
		}

		public string ToStringWithParenthesis()
		{
			return $"({ToString()})";
		}
	}
}
