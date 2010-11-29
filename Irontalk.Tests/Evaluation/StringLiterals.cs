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
	[TestFixture()]
	public class StringLiterals: CompilerTestFixture {
		[Test]
		public void Simple()
		{
			var result = compiler.Evaluate("'hello'");
			Assert.IsNotNull(result);
			Assert.That(result.Class == STClass.GetForCLR(typeof(string), "String"));
			Assert.That(result.Native.Equals("hello"));
		}
		
		/// <summary>
		/// This test ensures that the string token is properly formed. The hash portion
		/// of the evaluated string should not be part of a string literal (ie, the following
		/// should parse as 'hello', # {syntax error}, 'world', not as a single string token
		/// containing "'hello' #### 'world'").
		/// </summary>
		[Test]
		[ExpectedException(typeof(Irontalk.ParseException))]
		public void StringTokensAreMinimal()
		{
			compiler.Evaluate("'hello' #### 'world'");
		}
		
		[Test]
		public void EscapeQuotes()
		{
			var result = compiler.Evaluate("'Aren''t you glad?'");
			Assert.IsNotNull(result);
			Assert.AreSame(STClass.GetForCLR(typeof(string), "String"), result.Class);
			Assert.AreEqual("Aren't you glad?", result.Native);
		}
		
	}
}

