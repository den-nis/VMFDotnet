using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using VMFDotNET.Tokenizer;

namespace VMFDotNET.Tests
{
	[TestClass]
	public class SonReaderTests
	{
		[TestMethod]
		public void When_ReadObjectHeader_Expect_Value()
		{
			using SonReader reader = new(new StringReader("header{"));
			ReadUntilType(reader, SonNodeType.ObjectHeader);
			Assert.AreEqual("header", reader.Value);
		}

		[TestMethod]
		public void When_ReadPropertyName_Expect_Value()
		{
			using SonReader reader = new(new StringReader(@"header{""name"" ""value""}"));
			ReadUntilType(reader, SonNodeType.PropertyName);
			Assert.AreEqual("name", reader.Value);
		}

		[TestMethod]
		public void When_ReadPropertyValue_Expect_Value()
		{
			using SonReader reader = new(new StringReader(@"header{""name"" ""value""}"));
			ReadUntilType(reader, SonNodeType.PropertyValue);
			Assert.AreEqual("value", reader.Value);
		}

		[TestMethod]
		public void When_ReadSpacedObjectHeader_Expect_Exception()
		{
			using SonReader reader = new(new StringReader("h eader{"));

			var exception = Assert.ThrowsException<SonReaderException>(() =>
				ReadUntilType(reader, SonNodeType.ObjectStart)
			);
		}

		[TestMethod]
		public void When_ReadNumberInObjectHeader_Expect_Exception()
		{
			using SonReader reader = new(new StringReader("h3ader{"));

			var exception = Assert.ThrowsException<SonReaderException>(() =>
				ReadUntilType(reader, SonNodeType.ObjectStart)
			);
		}

		private const int READ_UNTIL_LIMIT = 1000;

		private static void ReadUntilType(SonReader reader, SonNodeType type)
		{
			for (int i = 0; i < READ_UNTIL_LIMIT; i++)
			{
				if (reader.Read() && reader.NodeType == type)
				{
					return;
				}
			}
		}
	}
}