using System.Globalization;

namespace VMFDotNET.Abstractions.DataTypes
{
	public struct LogicalPosition
	{
		public int X { get; set; }
		public int Y { get; set; }

		public override string ToString()
		{
			string x = X.ToString(CultureInfo.InvariantCulture);
			string y = Y.ToString(CultureInfo.InvariantCulture);

			return $"[{x} {y}]";
		}
	}
}
