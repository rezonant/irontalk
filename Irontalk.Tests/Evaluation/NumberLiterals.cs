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
	public class NumberLiterals: CompilerTestFixture {
		[Test]
		public void Simple()
		{
			var result = compiler.Evaluate("3");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)3));
		}
		
		[Test]
		public void Negative()
		{
			var result = compiler.Evaluate("-3");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)-3));
		}
		
		[Test]
		[ExpectedException(typeof(ParseException))]
		public void NegativesCantBreathe()
		{
			compiler.Evaluate("- 3");
		}
		
		[Test]
		public void DecimalRadixNegative()
		{
			var result = compiler.Evaluate("10r-5");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)-5));
		}
		
		[Test]
		public void NegativeDecimalRadix()
		{
			var result = compiler.Evaluate("-10r20");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)-20));
		}
		
		[Test]
		public void NegativeDecimalRadixNegative()
		{
			var result = compiler.Evaluate("-10r-10");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)-10));
		}
		
		[Test]
		public void DecimalRadixNumLiteral()
		{
			var result = compiler.Evaluate("10r3");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)3));
		}
		
		[Test]
		public void BinaryRadixNumLiteral()
		{
			var result = compiler.Evaluate("2r110");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)6));
		}
		
		[Test]
		public void Radix3NumLiteral()
		{
			var result = compiler.Evaluate("3r10");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)3));
		}
		
		[Test]
		public void Radix5NumLiteral()
		{
			var result = compiler.Evaluate("5r10");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)5));
		}
		
		[Test]
		public void OctalRadixNumLiteral()
		{
			var result = compiler.Evaluate("8r10");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)8));
		}
		
		[Test]
		public void HexRadixNumLiteralNumeric()
		{
			var result = compiler.Evaluate("16r10");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)16));
		}
		
		[Test]
		public void HexRadixNumLiteralAlphaUpperCase()
		{
			var result = compiler.Evaluate("16rFF");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)255));
		}
		
		[Test]
		public void HexRadixNumLiteralAlphaLowerCase()
		{
			var result = compiler.Evaluate("16rff");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)255));
		}
		
		[Test]
		public void Radix12NumLiteralAlphaLowerCase()
		{
			var result = compiler.Evaluate("12rbb");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(long), "Integer"));
			Assert.That(result.Native.Equals((long)143));
		}
		
		[Test]
		[ExpectedException(typeof(CompileException))]
		public void Radix12DoesNotAcceptC()
		{
			compiler.Evaluate("16rcc");
		}
	}
}

