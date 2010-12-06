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
	public class Doubles : CompilerTestFixture {
		[Test]
		public void TwoTermAddition ()
		{
			var result = compiler.Evaluate("1.0 + 2.0");
			Assert.IsNotNull (result);
			Assert.AreEqual(3.0, result.Native);
		}
		
		[Test]
		public void TwoTermAdditionIntegerCoerce ()
		{
			var result = compiler.Evaluate("1.0 + 2");
			Assert.IsNotNull (result);
			Assert.AreEqual(3.0, result.Native);
		}
		
		[Test]
		public void ThreeTermAddition ()
		{
			var result = compiler.Evaluate("1.0 + 2.0 + 3.0");
			Assert.IsNotNull (result);
			Assert.AreEqual(6.0, result.Native);
		}
		
		[Test]
		public void TwoTermMultiplication ()
		{
			var result = compiler.Evaluate("2.0 * 3.0");
			Assert.IsNotNull (result);
			Assert.AreEqual(6.0, result.Native);
		}
		
		[Test]
		public void TwoTermMultiplicationIntegerCoerce ()
		{
			var result = compiler.Evaluate("0.5 * 3");
			Assert.IsNotNull (result);
			Assert.AreEqual(1.5, result.Native);
		}
		
		[Test]
		public void ThreeTermMultiplication ()
		{
			var result = compiler.Evaluate("2.0 * 3.0 * 4.0");
			Assert.IsNotNull (result);
			Assert.AreEqual(24.0, result.Native);
		}
		
		[Test]
		public void MixedAdditionAndMultiplication ()
		{
			var result = compiler.Evaluate("2.0 + 3.0 * 4.0");
			Assert.IsNotNull (result);
			Assert.AreEqual(20.0, result.Native);
		}
		
		[Test]
		public void TwoTermDivision ()
		{
			var result = compiler.Evaluate("5.0 / 2.0");
			Assert.IsNotNull (result);
			Assert.AreEqual(2.5, result.Native);
		}
		
		[Test]
		public void TwoTermDivisionCoerce ()
		{
			var result = compiler.Evaluate("5.0 / 2");
			Assert.IsNotNull (result);
			Assert.AreEqual(2.5, result.Native);
		}
		
		[Test]
		public void ThreeTermDivision ()
		{
			var result = compiler.Evaluate("2.0 / 2.0 / 2.0");
			Assert.IsNotNull (result);
			Assert.AreEqual(0.5, result.Native);
		}
		
		[Test]
		public void TwoTermSubtraction ()
		{
			var result = compiler.Evaluate("20.0 - 1.5");
			Assert.IsNotNull (result);
			Assert.AreEqual(18.5, result.Native);
		}
		
		[Test]
		public void TwoTermSubtractionCoerce ()
		{
			var result = compiler.Evaluate("5.5 - 2");
			Assert.IsNotNull (result);
			Assert.AreEqual(3.5, result.Native);
		}
		
		[Test]
		public void ThreeTermSubtraction ()
		{
			var result = compiler.Evaluate("20.0 - 2.0 - 2.5");
			Assert.IsNotNull (result);
			Assert.AreEqual(15.5, result.Native);
		}
		
		[Test]
		public void ComplicatedExpression1 ()
		{
			var result = compiler.Evaluate("10.0 / 2.0 * 4.0 + 8.0 / 2.0 + 9.0 - 2.0 * 3.0 + 0.5");
			Assert.IsNotNull (result);
			Assert.AreEqual(63.5, result.Native);
		}
	}
}

