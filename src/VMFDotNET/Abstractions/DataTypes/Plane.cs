using System;

namespace VMFDotNET.Abstractions.DataTypes
{
	public class Plane
	{
		public Plane(Vector3 a, Vector3 b, Vector3 c)
		{
			Vectors = new Vector3[] { a, b, c };
		}

		public Plane(Vector3[] vectors)
		{
			if (vectors.Length != 3)
				throw new ArgumentException("Vector array must be of size 3");

			Vectors = vectors;
		}

		public Vector3[] Vectors { get; } = new Vector3[3];

		public override string ToString()
		{
			var a = Vectors[0].ToStringWithParenthesis();
			var b = Vectors[1].ToStringWithParenthesis();
			var c = Vectors[2].ToStringWithParenthesis();

			return $"{a} {b} {c}";
		}
	}
}
