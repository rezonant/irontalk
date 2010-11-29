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

namespace Irontalk
{
	public delegate object STMessageDelegate(STMessage msg);
	
	public class STRuntimeMethod : STCompiledMethod {
		public STRuntimeMethod (STMessageDelegate method)
		{
			@delegate = method;
		}
		
		public STRuntimeMethod (MethodInfo method):
			this (method, false)
		{}
		
		public STRuntimeMethod (MethodInfo method, bool passReceiver)
		{
			this.method = method;
			this.PassReceiver = passReceiver;
		}
		
		STMessageDelegate @delegate = null;
		MethodInfo method = null;
		
		public bool PassReceiver { get; private set; }
		
		public override string ToString ()
		{
			return "a RuntimeMethod";
		}
		
		public override STObject Invoke(STMessage message)
		{
			object result;
			
			if (method != null) {
				var stobj = new List<STObject>();
				var parmInfo = method.GetParameters();
				
				if (PassReceiver)
					stobj.Insert(0, message.Receiver);
				
				stobj.AddRange(message.Parameters);
				
				List<object> native = new List<object> ();
				for (int i = 0, max = stobj.Count; i < max; ++i) {
					var parmType = parmInfo[i].ParameterType;
					if (parmType == typeof(STObject) || parmType.IsSubclassOf(typeof(STObject))) {
						native.Add (stobj[i]);
					} else {
						native.Add(stobj[i].Native);
					}
				}
				
				result = method.Invoke(message.Receiver.MethodReceiver, native.ToArray());
				
				if (method.ReturnType == typeof(void))
					result = message.Receiver;
				
			} else if (@delegate != null) {
				result = @delegate(message);
			} else {
				throw new Exception ("This runtime-delegated message has no implementation!");	
			}
			
			if (result is STObject)
				return result as STObject;
			
			return new STInstance(result);
		}
	}
}
