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
			var result = compiler.Evaluate("(3) class");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STClass), result);
			Assert.IsNotNull((result as STClass).Type);
			Assert.AreEqual((result as STClass).Type, typeof(long));
		}
		
		[Test]
		public void ParenthesizeComplex()
		{
			var result = compiler.Evaluate("(3 + 2) class");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STClass), result);
			Assert.IsNotNull((result as STClass).Type);
			Assert.AreEqual((result as STClass).Type, typeof(long));
		}
	}
}

