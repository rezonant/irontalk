using System;
using NUnit.Framework;
namespace Irontalk.Tests
{
	[TestFixture]
	public class Arrays: CompilerTestFixture
	{
		[Test]
		public void NewEmpty()
		{
			var arrayObj = compiler.Evaluate("Array new");
			
			Assert.IsNotNull(arrayObj);
			Assert.IsInstanceOfType(typeof(Irontalk.Array), arrayObj);
			
			var array = arrayObj as Irontalk.Array;
			
			Assert.AreEqual(array.Size(), 0);
		}
		
		[Test]
		public void NewSized()
		{
			var arrayObj = compiler.Evaluate("Array new: 3");
			
			Assert.IsNotNull(arrayObj);
			Assert.IsInstanceOfType(typeof(Irontalk.Array), arrayObj);
			
			var array = arrayObj as Irontalk.Array;
			
			Assert.AreEqual(array.Size(), 3);
			
			for (int i = 1; i <= array.Size(); ++i)
				Assert.AreSame(array.At(i), STUndefinedObject.Instance);
		}
		
		[Test]
		public void NewSizedAssignment()
		{
			var arrayObj = compiler.Evaluate("x := Array new: 3. x at: 1 put: 3. x at: 2 put: 4. x at: 3 put: 5.");
			
			Assert.IsNotNull(arrayObj);
			Assert.IsInstanceOfType(typeof(Irontalk.Array), arrayObj);
			
			var array = arrayObj as Irontalk.Array;
			
			Assert.AreEqual(array.Size(), 3);
			
			Assert.IsInstanceOfType(typeof(long), array.At(1));
			Assert.IsInstanceOfType(typeof(long), array.At(2));
			Assert.IsInstanceOfType(typeof(long), array.At(3));
			
			Assert.AreEqual(array.At(1), (long)3);
			Assert.AreEqual(array.At(2), (long)4);
			Assert.AreEqual(array.At(3), (long)5);
		}
	}
}

