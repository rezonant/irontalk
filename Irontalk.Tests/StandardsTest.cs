using System;
using NUnit.Framework;
namespace Irontalk.Tests
{
	[TestFixture]
	public class StandardsTest
	{
		public StandardsTest ()
		{
			compiler = new Compiler();
		}
		
		Compiler compiler;
		
		[Test]
		public void ClassStructure()
		{
			var @int = compiler.Evaluate			("3 class");
			var intClass = compiler.Evaluate		("3 class class");
			var metaclass = compiler.Evaluate		("3 class class class");
			var metaclassClass = compiler.Evaluate	("3 class class class class");
			var alsoMetaclass = compiler.Evaluate	("3 class class class class class");
			
			Assert.IsNotNull (@int);
			Assert.IsNotNull (intClass);
			Assert.IsNotNull (metaclass);
			Assert.IsNotNull (metaclassClass);
			Assert.IsNotNull (alsoMetaclass);
			
			Assert.IsInstanceOfType(typeof(STClass), @int);
			Assert.IsInstanceOfType(typeof(STMetaclass), intClass);
			Assert.IsInstanceOfType(typeof(STClass), metaclass);
			Assert.IsInstanceOfType(typeof(STMetaclass), metaclassClass);
			Assert.IsInstanceOfType(typeof(STClass), alsoMetaclass);
			
			Assert.AreEqual ((@int as STClass).Name, "Integer");
			Assert.AreEqual ((intClass as STMetaclass).Name, "Integer class");
			Assert.AreEqual ((metaclass as STClass).Name, "Metaclass");
			Assert.AreEqual ((metaclassClass as STMetaclass).Name, "Metaclass class");
			Assert.AreEqual ((alsoMetaclass as STClass).Name, "Metaclass");
			
			Assert.AreSame (metaclass, alsoMetaclass);
		}
		
		[Test]
		public void HasStringClass()
		{
			var @class = compiler.Evaluate("String");
			var instance = compiler.Evaluate("'hello'");
			
			Assert.IsNotNull(@class);
			Assert.IsNotNull(instance);
			
			Assert.IsInstanceOfType(typeof(STClass), @class);
			Assert.IsInstanceOfType(typeof(STInstance), instance);
			
			Assert.AreEqual((instance as STInstance).Class, @class);
		}
		
		[Test]
		public void HasSmalltalkClass()
		{
			var @class = compiler.Evaluate("Smalltalk");
			Assert.IsNotNull(@class);
			Assert.IsInstanceOfType(typeof(STClass), @class);
		}
		
		[Test]
		public void SmalltalkClassHasVersion()
		{
			var version = compiler.Evaluate("Smalltalk version");
			Assert.IsNotNull(version);
			Assert.IsInstanceOfType(typeof(STInstance), version);
			Assert.IsInstanceOfType(typeof(string), (version as STInstance).Target);
		}
		
		[Test]
		public void HasTranscriptClass()
		{
			var @class = compiler.Evaluate("Transcript");
			Assert.IsNotNull(@class);
			Assert.IsInstanceOfType(typeof(STClass), @class);
		}
		
		[Test]
		public void HasIntegerClass()
		{
			var @class = compiler.Evaluate("Integer");
			Assert.IsNotNull(@class);
			Assert.IsInstanceOfType(typeof(STClass), @class);
		}
		
	}
}

