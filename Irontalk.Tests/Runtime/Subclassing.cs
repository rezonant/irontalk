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
			var result = compiler.Evaluate("Irontalk Tests TestSuperclass subclass: #SimpleSubclass");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STClass), result);
		}
		
		[Test]
		public void SimpleSubclassWithNamespace()
		{
			var result = compiler.Evaluate("Irontalk Tests TestSuperclass subclass: #SimpleSubclassWithNamespace namespace: Irontalk Tests");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STClass), result);
			var sameResult = compiler.Evaluate("Irontalk Tests SimpleSubclassWithNamespace");
			Assert.AreSame(result, sameResult);
		}
		
		[Test]
		public void SimpleSubclassWithNamespaceAndBody()
		{
			Irontalk.Tests.TestSuperclass.Indicator = 42;
			
			var result = compiler.Evaluate(@"
				Irontalk Tests TestSuperclass subclass: #SimpleSubclassWithNamespaceAndBody namespace: Irontalk Tests with: [
					Irontalk Tests TestSuperclass indicator: 42
				] 
			");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STClass), result);
			var sameResult = compiler.Evaluate("Irontalk Tests SimpleSubclassWithNamespaceAndBody");
			Assert.AreSame(result, sameResult);
		}
		
		[Test]
		public void SimpleSubclassWithAnInstanceMethodThatReturns()
		{
			Irontalk.Tests.TestSuperclass.Indicator = 42;
			
			var result = compiler.Evaluate(@"
				Irontalk Tests TestSuperclass 
					subclass: #SimpleSubclassWithAnInstanceMethodThatReturns namespace: Irontalk Tests 
					with: [
						(self define aMethod) with: [ ^1. 0 ].
					]. 
				value := Irontalk Tests SimpleSubclassWithAnInstanceMethodThatReturns new aMethod.
				value = 1 ifTrue: [ ^99 ].
				^0.
			");
			Assert.IsNotNull(result);
			Assert.AreEqual((long)99, result.Native);
		}
	}
}