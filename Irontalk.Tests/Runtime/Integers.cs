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
	public class Integers : CompilerTestFixture {
		[Test]
		public void TwoTermAddition ()
		{
			var result = compiler.Evaluate("1 + 2");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)3, result.Native);
		}
		
		[Test]
		public void ThreeTermAddition ()
		{
			var result = compiler.Evaluate("1 + 2 + 3");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)6, result.Native);
		}
		
		[Test]
		public void TwoTermMultiplication ()
		{
			var result = compiler.Evaluate("2 * 3");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)6, result.Native);
		}
		
		[Test]
		public void ThreeTermMultiplication ()
		{
			var result = compiler.Evaluate("2 * 3 * 4");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)24, result.Native);
		}
		
		[Test]
		public void MixedAdditionAndMultiplication ()
		{
			var result = compiler.Evaluate("2 + 3 * 4");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)20, result.Native);
		}
		
		[Test]
		public void TwoTermDivision ()
		{
			var result = compiler.Evaluate("10 / 5");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)2, result.Native);
		}
		
		[Test]
		public void ThreeTermDivision ()
		{
			var result = compiler.Evaluate("20 / 2 / 2");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)5, result.Native);
		}
		
		[Test]
		public void TwoTermSubtraction ()
		{
			var result = compiler.Evaluate("20 - 2");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)18, result.Native);
		}
		
		[Test]
		public void ThreeTermSubtraction ()
		{
			var result = compiler.Evaluate("20 - 2 - 3");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)15, result.Native);
		}
		
		[Test]
		public void ComplicatedExpression1 ()
		{
			var result = compiler.Evaluate("10 / 2 * 4 + 8 / 2 + 9 - 2 * 3");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)63, result.Native);
		}
		
		[Test]
		public void Xor ()
		{
			var result = compiler.Evaluate("1 bitXor: 2");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)3, result.Native);
		}
		
		[Test]
		public void Or ()
		{
			var result = compiler.Evaluate("9 bitOr: 10");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)11, result.Native);
		}
		
		[Test]
		public void And ()
		{
			var result = compiler.Evaluate("7 bitAnd: 9");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)1, result.Native);
		}
		
		[Test]
		public void BitshiftLeft ()
		{
			var result = compiler.Evaluate("7 bitshift: 2");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)28, result.Native);
		}
		
		[Test]
		public void BitshiftRight ()
		{
			var result = compiler.Evaluate("7 bitshift: -2");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)1, result.Native);
		}
		
	}
}

