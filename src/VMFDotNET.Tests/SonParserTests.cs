using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMFDotNET.Linq;

namespace VMFDotNET.Tests
{
	[TestClass]
	public class SonParserTests
	{
		[TestMethod]
		public void When_Parse_Expect_ParsedObjects()
		{
			string input = "headerOne{\"name\"\"value\"}headerTwo{\"name\"\"value\"}";

			SonObject obj = SonParser.Parse(input);

			Assert.IsTrue(obj.Children.Any(c => c.Name == "headerOne"));
			Assert.IsTrue(obj.Children.Any(c => c.Name == "headerTwo"));
			Assert.AreEqual(2, obj.Children.Sum(p => p.Properties.Count()));
		}

		[TestMethod]
		public void When_ParseProperty_Expect_ParsedProperty()
		{
			string input = "headerOne{\"name\"\"value\"}";

			SonObject obj = SonParser.Parse(input).Children.First();

			Assert.AreEqual("value", obj.GetValue("name"));
		}
	}
}
