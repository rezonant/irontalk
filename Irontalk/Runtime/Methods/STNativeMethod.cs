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
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;

namespace Irontalk {
	public class STNativeMethod : STCompiledMethod {
		public STNativeMethod(STMethodPrototype prototype, STBlock block)
		{
			Prototype = prototype;
			Block = block;
			Block.Context = new LocalContext(Block.Context, true);
			
			foreach (var name in prototype.ParameterNames)
				Block.Context.Declare(name.Name);
		}
		
		public STMethodPrototype Prototype { get; private set; }
		public STBlock Block { get; private set; }
		
		[STRuntimeMethod("asString")]
		public override string ToString ()
		{
			return string.Format("CompiledMethod({0})", Prototype);
		}
		
		public override STObject Invoke(STMessage message)
		{
			try {
				var instanceCtx = message.Receiver.InstanceContext;
				var invocationCtx = new LocalContext (instanceCtx);
				for (int i = 0, max = Prototype.ParameterNames.Length; i < max; ++i)
					invocationCtx.SetVariable(Prototype.ParameterNames[i].Name, message.Parameters[i]);
				
				return Block.EvaluateWith(invocationCtx);
			} catch (ReturnException e) {
				return e.Value;	
			}
		}
	}
}
