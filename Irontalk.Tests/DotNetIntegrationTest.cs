using System;
using NUnit.Framework;
namespace Irontalk.Tests
{
	[TestFixture]
	public class DotNetIntegrationTest
	{
		public DotNetIntegrationTest ()
		{
			compiler = new Compiler();
		}
		
		Compiler compiler;
		
		[Test]
		public void TopLevelNamespace()
		{
			var nsObj = compiler.Evaluate("System");
			Assert.IsNotNull(nsObj);
			Assert.IsInstanceOfType(typeof(STNamespace), nsObj);
			var ns = nsObj as STNamespace;
			Assert.AreEqual(ns.FullName, "System");
		}
		
		[Test]
		public void NestedNamespace()
		{
			var nsObj = compiler.Evaluate("System IO");
			Assert.IsNotNull(nsObj);
			Assert.IsInstanceOfType(typeof(STNamespace), nsObj);
			var ns = nsObj as STNamespace;
			Assert.AreEqual(ns.FullName, "System.IO");
		}
		
		[Test]
		public void NativeClass()
		{
			var classObj = compiler.Evaluate("System Console");
			Assert.IsNotNull(classObj);
			Assert.IsInstanceOfType(typeof(STClass), classObj);
			var @class = classObj as STClass;
			Assert.AreEqual(@class.Name, "Console");
			Assert.AreEqual(@class.Type.FullName, "System.Console");
		}
		
		[Test]
		public void NativeMessage()
		{
			var trueObj = compiler.Evaluate("System Char isLetter: $a");
			var falseObj = compiler.Evaluate("System Char isLetter: $1");
			
			Assert.IsNotNull(trueObj);
			Assert.IsNotNull(falseObj);
			Assert.IsInstanceOfType(typeof(STInstance), trueObj);
			Assert.IsInstanceOfType(typeof(STInstance), falseObj);
			Assert.AreEqual(STBooleanDelegate.True, trueObj);
			Assert.AreEqual(STBooleanDelegate.False, falseObj);
		}
		
		[Test]
		public void NativeCtor()
		{
			var obj = compiler.Evaluate("System Text StringBuilder new");
			
			Assert.IsNotNull(obj);
			Assert.IsInstanceOfType(typeof(STInstance), obj);
			Assert.IsNotNull((obj as STInstance).Target);
			Assert.IsInstanceOfType(typeof(System.Text.StringBuilder), (obj as STInstance).Target);
		}
	}
}

