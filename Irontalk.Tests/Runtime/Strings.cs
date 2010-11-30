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
	public class Strings : CompilerTestFixture {
		[Test] public void Space ()				{ AssertOutput("String space",				" ");					}
		[Test] public void Cr()					{ AssertOutput("String cr",					"\n");					}
		[Test] public void ByteSize()			{ AssertOutput("'abc' byteSize",			(long)3);				}
		[Test] public void FindString()			{ AssertOutput("'abc' findString: 'b'",		(long)2);				}
		[Test] public void AsInteger()			{ AssertOutput("'32' asInteger",			(long)32);				}
		[Test] public void AsSignedInteger()	{ AssertOutput("'abc-32' asSignedInteger",	(long)-32); 			}
		[Test] public void AsUnsignedInteger()	{ AssertOutput("'-32' asUnsignedInteger",	(long)32); 				}
		[Test] public void WithCRs()			{ AssertOutput("'hello\\world' withCRs",	"hello\nworld"); 		}
		[Test] public void Concatenation()		{ AssertOutput("'hello ', 'world'",			"hello world"); 		}
		[Test] public void Join()				{ AssertOutput("', ' join: #(1 2 3)",		"1, 2, 3"); 			}
		
		[Test] public void ContractTo()			{ AssertOutput(ContractToSample,			"h...d"); 				}
		[Test] public void FindFirst()			{ AssertOutput(FindFirstSample,				(long)3); 				}
		[Test] public void Format()				{ AssertOutput(FormatSample,				"a or b or c"); 		}
		
		const string ContractToSample 			= "'hello world' contractTo: 5";
		const string FindFirstSample			= "'hello' findFirst: [:char| char = 'l']";
		const string FormatSample				= "'{1} or {2} or {3}' format: { 'a', 'b', 'c' }";
		
		[Test] 
		public void FindStringStartingAt() 
		{
			AssertOutput("'abaca' findString: 'a' startingAt: 2", (long)3); 
		}
		
		[Test] 
		public void FindStringStartingAtCaseSensitiveFalse() 
		{
			AssertOutput("'Abaca' findString: 'a' startingAt: 0 caseSensitive: false", (long)1); 
		}
		
		[Test]
		public void FindStringStartingAtCaseSensitiveTrue()
		{
			var result = compiler.Evaluate("'Abaca' findString: 'a' startingAt: 0 caseSensitive: true");
			Assert.IsNotNull (result);
			Assert.AreEqual((long)3, result.Native);
		}
		
		[Test] 
		public void FindTokens()
		{
			var result = compiler.Evaluate("'hello,world' findTokens: ','");
			Assert.IsNotNull (result);
		}
		
		[Test] 
		public void SubStrings()
		{
			var result = compiler.Evaluate("'hello,world' subStrings: ','");
			Assert.IsNotNull (result);
			Assert.IsInstanceOfType(typeof(Irontalk.Array), result);
			var array = result as Irontalk.Array;
			Assert.AreEqual(2, array.Size());
			Assert.AreEqual("hello", array.At(1));
			Assert.AreEqual("world", array.At(2));
		}
		
	}
}

