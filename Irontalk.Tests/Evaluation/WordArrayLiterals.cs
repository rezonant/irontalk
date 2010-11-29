// 
//  Author:
//       William Lahti <wilahti@gmail.com>
// 
//  Copyright © 2010 William Lahti
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  As a special exception, the copyright holders of this library give
//  you permission to link this library with independent modules to
//  produce an executable, regardless of the license terms of these 
//  independent modules, and to copy and distribute the resulting 
//  executable under terms of your choice, provided that you also meet,
//  for each linked independent module, the terms and conditions of the
//  license of that module. An independent module is a module which is
//  not derived from or based on this library. If you modify this library, you
//  may extend this exception to your version of the library, but you are
//  not obligated to do so. If you do not wish to do so, delete this
//  exception statement from your version. 
// 
//  This program is distributed in the hope that it will be useful, 
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using NUnit.Framework;
namespace Irontalk.Tests {
	[TestFixture]
	public class WordArrayLiterals {
		public WordArrayLiterals ()
		{
			compiler = new Compiler();
		}
		
		Compiler compiler;
		
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
	}
}

