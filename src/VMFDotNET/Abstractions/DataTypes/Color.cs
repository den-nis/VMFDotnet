using System.Globalization;

namespace VMFDotNET.Abstractions.DataTypes
{
	public struct Color
	{
		public byte Red { get; set; }
		public byte Green { get; set; }
		public byte Blue { get; set; }

		public override string ToString()
		{
			string red = Red.ToString(CultureInfo.InvariantCulture);
			string green = Green.ToString(CultureInfo.InvariantCulture);
			string blue = Blue.ToString(CultureInfo.InvariantCulture);

			return $"{red} {green} {blue}";
		}
	}
}
