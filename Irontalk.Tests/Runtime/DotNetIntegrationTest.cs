using System;
using NUnit.Framework;
namespace Irontalk.Tests
{
	public class PropertyTestClass {
		public object InstanceProperty { get; set; }
		static object staticProperty = null;
		public static object StaticProperty { get { return staticProperty; } set { staticProperty = value; } }
	}
	
	[TestFixture]
	public class DotNetIntegrationTest: CompilerTestFixture
	{
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
		public void NativeInstancePropertyGetMessage()
		{
			var ctx = new LocalContext();
			var inst = new PropertyTestClass();
			
			inst.InstanceProperty = 3;
			
			ctx.SetVariable("x", new STInstance(inst));
			var three = compiler.Evaluate("x instanceProperty", ctx);
			Assert.IsNotNull(three);
			Assert.IsInstanceOfType(typeof(STInstance), three);
			Assert.AreEqual((three as STInstance).Target, (long)3);
		}
		
		[Test]
		public void NativeInstancePropertySetMessage()
		{
			var ctx = new LocalContext();
			var inst = new PropertyTestClass();
			
			ctx.SetVariable("x", new STInstance(inst));
			compiler.Evaluate("x instanceProperty: 3", ctx);
			Assert.AreEqual((long)3, inst.InstanceProperty);
		}
		
		[Test]
		public void NativeStaticPropertyGetMessage()
		{	
			PropertyTestClass.StaticProperty = 3;
			
			var three = compiler.Evaluate("Irontalk Tests PropertyTestClass staticProperty");
			Assert.IsNotNull(three);
			Assert.IsInstanceOfType(typeof(STInstance), three);
			Assert.AreEqual((three as STInstance).Target, (long)3);
		}
		
		[Test]
		public void NativeStaticPropertySetMessage()
		{
			compiler.Evaluate("Irontalk Tests PropertyTestClass staticProperty: 3");
			Assert.AreEqual((long)3, PropertyTestClass.StaticProperty);
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

