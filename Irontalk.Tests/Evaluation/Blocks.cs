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
	public class Blocks: CompilerTestFixture {
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
		public void BlockReturn()
		{
			var result = compiler.Evaluate("[ ^0. 1 + 2 ] value");
			Assert.IsNotNull(result);
			Assert.AreEqual((long)0, result.Native);
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
	}
}

