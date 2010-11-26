using System;
using NUnit.Framework;
using System.Reflection;
namespace Irontalk.Tests
{
	[TestFixture] public class EvaluationTest
	{
		public EvaluationTest()
		{
			compiler = new Compiler(Assembly.GetExecutingAssembly());
		}
		
		Compiler compiler;
		
		[Test]
		public void EmptyLine()
		{
			var result = compiler.Evaluate("");
			
			Assert.IsNotNull(result);
			Assert.That(result == STUndefinedObject.Instance);
		}
		
		[Test]
		public void NumLiteral()
		{
			var result = compiler.Evaluate("3");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)3));
		}
		
		[Test]
		public void CharLiteral()
		{
			var result = compiler.Evaluate("$a");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(char), "Character"));
			Assert.That(result.Native.Equals('a'));
		}
		
		[Test]
		public void StringLiteral()
		{
			var result = compiler.Evaluate("'hello'");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(string), "String"));
			Assert.That(result.Native.Equals("hello"));
		}
		
		[Test]
		public void WordArrayLiteralEmpty()
		{
			var result = compiler.Evaluate("#()");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array"));
			var array = result as Irontalk.Array;
			Assert.IsNotNull(array);
			Assert.AreEqual(array.Size(), 0);
		}
		
		[Test]
		public void WordArrayLiteralWithInts()
		{
			var result = compiler.Evaluate("#(2 3 4)");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array"));
			var array = result as Irontalk.Array;
			Assert.IsNotNull(array);
			Assert.AreEqual(array.Size(), 3);
			for (int i = 1; i <= array.Size(); ++i)
				Assert.IsNotNull(array.At(i));
			
			Assert.That(array.At(1) is STInstance);
			Assert.That(array.At(2) is STInstance);
			Assert.That(array.At(3) is STInstance);
			
			Assert.AreEqual((long)2, (array.At(1) as STInstance).Target);
			Assert.AreEqual((long)3, (array.At(2) as STInstance).Target);
			Assert.AreEqual((long)4, (array.At(3) as STInstance).Target);
		}
		
		[Test]
		public void WordArrayLiteralWithSymbols()
		{
			var result = compiler.Evaluate("#(alpha b foo)");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array"));
			var array = result as Irontalk.Array;
			Assert.IsNotNull(array);
			Assert.AreEqual(3, array.Size());
			for (int i = 1; i <= array.Size(); ++i)
				Assert.IsNotNull(array.At(i));
			
			Assert.That(array.At(1) is STSymbol);
			Assert.That(array.At(2) is STSymbol);
			Assert.That(array.At(3) is STSymbol);
			
			Assert.AreEqual("alpha", (array.At(1) as STSymbol).Name);
			Assert.AreEqual("b", (array.At(2) as STSymbol).Name);
			Assert.AreEqual("foo", (array.At(3) as STSymbol).Name);
		}
		
		[Test]
		public void BlockLiteralSimple()
		{
			var result = compiler.Evaluate("[ 1 + 2 ]");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STBlock), result);
			var block = result as STBlock;
			
			Assert.AreEqual(0, block.BlockArgumentNames.Length);
			Assert.AreEqual(0, block.LocalVariableNames.Length);
		}
		
		[Test]
		public void BlockLiteralOneUnusedArgument()
		{
			var result = compiler.Evaluate("[:foo| 1 + 2 ]");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STBlock), result);
			var block = result as STBlock;
			
			Assert.AreEqual(1, block.BlockArgumentNames.Length);
			Assert.AreEqual("foo", block.BlockArgumentNames[0]);
			Assert.AreEqual(0, block.LocalVariableNames.Length);
		}
		
		[Test]
		public void BlockLiteralOneUnusedArgumentAndLocals()
		{
			var result = compiler.Evaluate("[:foo| |x| 1 + 2 ]");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STBlock), result);
			var block = result as STBlock;
			
			Assert.AreEqual(1, block.BlockArgumentNames.Length);
			Assert.AreEqual("foo", block.BlockArgumentNames[0]);
			Assert.AreEqual(1, block.LocalVariableNames.Length);
			Assert.AreEqual("x", block.LocalVariableNames[0]);
		}
		
		[Test]
		public void BlockLiteralTwoUnusedArguments()
		{
			var result = compiler.Evaluate("[:foo :bar| 1 + 2 ]");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STBlock), result);
			var block = result as STBlock;
			
			Assert.AreEqual(2, block.BlockArgumentNames.Length);
			Assert.AreEqual("foo", block.BlockArgumentNames[0]);
			Assert.AreEqual("bar", block.BlockArgumentNames[1]);
			Assert.AreEqual(0, block.LocalVariableNames.Length);
		}
		
		[Test]
		public void BlockLiteralTwoUnusedArgumentsAndLocals()
		{
			var result = compiler.Evaluate("[:foo :bar| |x| 1 + 2 ]");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STBlock), result);
			var block = result as STBlock;
			
			Assert.AreEqual(2, block.BlockArgumentNames.Length);
			Assert.AreEqual("foo", block.BlockArgumentNames[0]);
			Assert.AreEqual("bar", block.BlockArgumentNames[1]);
			Assert.AreEqual(1, block.LocalVariableNames.Length);
			Assert.AreEqual("x", block.LocalVariableNames[0]);
		}
		
		[Test]
		public void BlockLiteralSimpleEvaluated()
		{
			var result = compiler.Evaluate("[ 1 + 2 ] value");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STInstance), result);
			Assert.AreEqual(3, (result as STInstance).Target);
		}
		
		[Test]
		public void BlockLiteralOneUnusedArgumentEvaluated()
		{
			var result = compiler.Evaluate("[:x| 1 + 2 ] value: 10");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STInstance), result);
			Assert.AreEqual(3, (result as STInstance).Target);
		}
		
		[Test]
		public void BlockLiteralTwoUnusedArgumentEvaluated()
		{
			var result = compiler.Evaluate("[:x :y| 1 + 2 ] value: 10 value: 20");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STInstance), result);
			Assert.AreEqual(3, (result as STInstance).Target);
		}
		
		[Test]
		public void BlockLiteralOneArgumentEvaluated()
		{
			var result = compiler.Evaluate("[:x| x + 2 ] value: 10");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STInstance), result);
			Assert.AreEqual(12, (result as STInstance).Target);
		}
		
		[Test]
		public void BlockLiteralTwoArgumentsEvaluated()
		{
			var result = compiler.Evaluate("[:x :y| x + y + 1 ] value: 10 value: 10");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STInstance), result);
			Assert.AreEqual(21, (result as STInstance).Target);
		}
		
		[Test]
		public void WordArrayLiteralWithChars()
		{
			var result = compiler.Evaluate("#($a $b $c)");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array"));
			var array = result as Irontalk.Array;
			Assert.IsNotNull(array);
			Assert.AreEqual(array.Size(), 3);
			for (int i = 1; i <= array.Size(); ++i)
				Assert.IsNotNull(array.At(i));
			
			Assert.That(array.At(1) is STInstance);
			Assert.That(array.At(2) is STInstance);
			Assert.That(array.At(3) is STInstance);
			
			Assert.That((array.At(1) as STInstance).Target.Equals('a'));
			Assert.That((array.At(2) as STInstance).Target.Equals('b'));
			Assert.That((array.At(3) as STInstance).Target.Equals('c'));
		}
		
		[Test]
		public void WordArrayLiteralWithMixed()
		{
			var result = compiler.Evaluate("#(3 foo $b)");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array"));
			var array = result as Irontalk.Array;
			Assert.IsNotNull(array);
			Assert.AreEqual(array.Size(), 3);
			for (int i = 1; i <= array.Size(); ++i)
				Assert.IsNotNull(array.At(i));
			
			Assert.That(array.At(1) is STInstance);
			Assert.That(array.At(2) is STSymbol);
			Assert.That(array.At(3) is STInstance);
			
			Assert.That((array.At(1) as STInstance).Target.Equals((long)3));
			Assert.AreEqual((array.At(2) as STSymbol).Name, "foo");
			Assert.That((array.At(3) as STInstance).Target.Equals('b'));
		}
		
		[Test]
		public void ArrayLiteralEmpty()
		{
			var result = compiler.Evaluate("{}");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array"));
			var array = result as Irontalk.Array;
			Assert.IsNotNull(array);
			Assert.AreEqual(array.Size(), 0);
		}
		
		[Test]
		public void ArrayLiteralSingleItemSimple()
		{
			var result = compiler.Evaluate("{42}");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array"));
			var array = result as Irontalk.Array;
			Assert.IsNotNull(array);
			Assert.AreEqual(array.Size(), 1);
			Assert.That(array.At(1) is STInstance);
			Assert.That((array.At(1) as STInstance).Target.Equals((long)42));
		}
		
		[Test]
		public void ArrayLiteralMultiItemSimple()
		{
			var result = compiler.Evaluate("{42 . 128 . 99}");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array"));
			var array = result as Irontalk.Array;
			Assert.IsNotNull(array);
			Assert.AreEqual(array.Size(), 3);
			
			Assert.That(array.At(1) is STInstance);
			Assert.That(array.At(2) is STInstance);
			Assert.That(array.At(3) is STInstance);
			
			Assert.That((array.At(1) as STInstance).Target.Equals((long)42));
			Assert.That((array.At(2) as STInstance).Target.Equals((long)128));
			Assert.That((array.At(3) as STInstance).Target.Equals((long)99));
		}
		
		[Test]
		public void ArrayLiteralSingleItemUnarySend()
		{
			var result = compiler.Evaluate("{42 class}");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array"));
			var array = result as Irontalk.Array;
			Assert.IsNotNull(array);
			Assert.AreEqual(array.Size(), 1);
			
			Assert.That(array.At(1) is STClass);
		}
		
		[Test]
		public void ArrayLiteralMultiItemUnarySend()
		{
			var result = compiler.Evaluate("{42 class . 42 class }");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array"));
			var array = result as Irontalk.Array;
			Assert.IsNotNull(array);
			Assert.AreEqual(array.Size(), 2);
			
			Assert.That(array.At(1) is STClass);
			Assert.That(array.At(2) is STClass);
			Assert.AreEqual(array.At(1), array.At(2));
		}
		
		[Test]
		public void ArrayLiteralMultiItemMixed()
		{
			var result = compiler.Evaluate("{42 . 42 class}");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array"));
			var array = result as Irontalk.Array;
			Assert.IsNotNull(array);
			Assert.AreEqual(array.Size(), 2);
			
			Assert.That((array.At(1) as STInstance).Target.Equals((long)42));
			Assert.That(array.At(2) is STClass);
		}
		
		[Test]
		public void SendMessageToArrayLiteral()
		{
			var result = compiler.Evaluate("{42 . 43} at: 2");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STInstance), result);
			Assert.That((result as STInstance).Target.Equals((long)43));
		}
		
		[Test]
		public void IntegerBinarySend()
		{
			var result = compiler.Evaluate("5 + 7");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STInstance), result);
			Assert.That((result as STInstance).Target.Equals((long)12));
		}
		
		[Test]
		public void KeywordSend()
		{
			var result = compiler.Evaluate("1 to: 10");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(Irontalk.Array), result);
			var array = result as Irontalk.Array;
			Assert.AreEqual(array.Size(), 10);
		}
		
		[Test]
		public void BinaryOrderOfOperations()
		{
			var result = compiler.Evaluate("5 + 7 * 2");
			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(typeof(STInstance), result);
			Assert.That((result as STInstance).Target.Equals((long)24));
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

