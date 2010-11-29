using System;
using NUnit.Framework;

namespace Irontalk.Tests
{
	public class TestSuperclass {
		public string Foo() { return "hello"; }
		static object indicator = null;
		public static object Indicator { get { return indicator; } set { indicator = value; } }
	}
	
	[TestFixture]
	public class Subclassing
	{
		public Subclassing ()
		{
			compiler = new Compiler();
		}
		
		Compiler compiler;
		
		[Test]
		public void SimpleSubclass()
		{
			var result = compiler.Evaluate("Irontalk Tests TestSuperclass subclass: #SimpleSubclassA");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STClass), result);
		}
		
		[Test]
		public void SimpleSubclassWithNamespace()
		{
			var result = compiler.Evaluate("Irontalk Tests TestSuperclass subclass: #SimpleSubclassB namespace: Irontalk Tests");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STClass), result);
			var sameResult = compiler.Evaluate("Irontalk Tests SimpleSubclassB");
			Assert.AreSame(result, sameResult);
		}
		
		[Test]
		public void SimpleSubclassWithNamespaceAndBody()
		{
			Irontalk.Tests.TestSuperclass.Indicator = 42;
			
			var result = compiler.Evaluate(
				"Irontalk Tests TestSuperclass subclass: #SimpleSubclassC namespace: Irontalk Tests with: [ " +
					"Irontalk Tests TestSuperclass indicator: 42" +
				" ] ");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STClass), result);
			var sameResult = compiler.Evaluate("Irontalk Tests SimpleSubclassC");
			Assert.AreSame(result, sameResult);
		}
	}
}