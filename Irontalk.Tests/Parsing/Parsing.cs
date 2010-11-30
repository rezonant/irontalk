using System;
using NUnit.Framework;
using System.Reflection;
namespace Irontalk.Tests
{
	[TestFixture] public class Parsing: CompilerTestFixture {
		[Test]
		public void EmptyLine()
		{
			var result = compiler.Evaluate("");
			
			Assert.IsNotNull(result);
			Assert.That(result == STUndefinedObject.Instance);
		}
		
		[Test]
		[ExpectedException(typeof(Irontalk.ParseException))]
		public void ThrowsParseException()
		{
			compiler.Evaluate("####");
		}
		
		[Test]
		public void ParenthesizeSimple()
		{
			var result = compiler.Evaluate("(3) + 2");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STInstance), result);
			Assert.AreEqual((long)5, result.Native);
		}
		
		[Test]
		public void ParenthesizeComplex()
		{
			var result = compiler.Evaluate("(3 + 2) * 2");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STInstance), result);
			Assert.AreEqual((long)10, result.Native);
		}
	}
}

