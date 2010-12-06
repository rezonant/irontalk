using System;
using NUnit.Framework;

namespace Irontalk.Tests {
	[TestFixture]
	public class Classes: CompilerTestFixture {
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
		public void ClassesHaveMethodDict()
		{
			var value = compiler.Evaluate("4 class methodDict");
			
			Assert.IsInstanceOfType(typeof(Int64), value.Send("size").Native);
			//Assert.AreEqual(STClass.GetForCLR(typeof(MethodDictionary)), value.Class);
		}
	}
}

