using System.Globalization;

namespace VMFDotNET.Abstractions.DataTypes
{
	public struct UV
	{
		public float X { get; set; }
		public float Y { get; set; }
		public float Z { get; set; }
	
		public float Offset { get; set; }
		public float Scale { get; set; }

		public override string ToString()
		{
			var x = X.ToString(CultureInfo.InvariantCulture);
			var y = Y.ToString(CultureInfo.InvariantCulture);
			var z = Z.ToString(CultureInfo.InvariantCulture);

			var offset = Offset.ToString(CultureInfo.InvariantCulture);
			var scale = Scale.ToString(CultureInfo.InvariantCulture);

			return $"[{x} {y} {z} {offset}] {scale}";
		}
	}
}
