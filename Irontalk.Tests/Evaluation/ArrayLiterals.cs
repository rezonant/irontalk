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
	public class ArrayLiterals: CompilerTestFixture {
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
		
	}
}

