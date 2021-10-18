using System;
using System.Diagnostics;
using System.IO;
using VMFDotNET.ObjectNotation;

namespace VMFDotNET
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{

				SonReader reader = new(File.OpenRead(args[0]));
				while (true)
				{
					var type = reader.Read();
					Console.WriteLine($"{type} : {reader.Value}");
				}
			}
		}
	}
}
