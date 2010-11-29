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
	public class MessagePassing: CompilerTestFixture {
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
	}
}

