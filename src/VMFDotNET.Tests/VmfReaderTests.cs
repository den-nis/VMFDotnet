using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using VMFDotNET.Tokenizer;

namespace VMFDotNET.Tests
{
	[TestClass]
	public class VmfReaderTests
	{
		[TestMethod]
		public void When_ReadObjectHeader_Expect_Value()
		{
			using VmfReader reader = new(new StringReader("header{"));
			ReadUntilType(reader, VmfNodeType.ObjectHeader);
			Assert.AreEqual("header", reader.Value);
		}

		[TestMethod]
		public void When_ReadPropertyName_Expect_Value()
		{
			using VmfReader reader = new(new StringReader(@"header{""name"" ""value""}"));
			ReadUntilType(reader, VmfNodeType.PropertyName);
			Assert.AreEqual("name", reader.Value);
		}

		[TestMethod]
		public void When_ReadPropertyValue_Expect_Value()
		{
			using VmfReader reader = new(new StringReader(@"header{""name"" ""value""}"));
			ReadUntilType(reader, VmfNodeType.PropertyValue);
			Assert.AreEqual("value", reader.Value);
		}

		[TestMethod]
		public void When_ReadSpacedObjectHeader_Expect_Exception()
		{
			using VmfReader reader = new(new StringReader("h eader{"));

			var exception = Assert.ThrowsException<VmfReaderException>(() =>
				ReadUntilType(reader, VmfNodeType.ObjectStart)
			);
		}

		[TestMethod]
		public void When_ReadNumberInObjectHeader_Expect_Exception()
		{
			using VmfReader reader = new(new StringReader("h3ader{"));

			var exception = Assert.ThrowsException<VmfReaderException>(() =>
				ReadUntilType(reader, VmfNodeType.ObjectStart)
			);
		}

		private const int READ_UNTIL_LIMIT = 1000;

		private static void ReadUntilType(VmfReader reader, VmfNodeType type)
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