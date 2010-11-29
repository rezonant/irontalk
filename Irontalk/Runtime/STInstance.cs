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
	/// <summary>
	/// STInstance is an STObject subclass which makes direct usage of .NET-native objects possible within Irontalk.
	/// Merely create an instance by passing in the target reference and voila. STInstance will create any needed
	/// Smalltalk classes on the fly.
	/// </summary>
	public class STInstance : STObject {
		public STInstance (object target)
		{
			Target = target;
		}
		
		public object Target;
		public override STClassDescription Class {
			get {
				var type = Target.GetType();
				return STClass.GetForCLR(type, type.Name);
			}
		}
		
		public override string ToString ()
		{
			if (Target == null)
				return "null";
			
			return Target.ToString();
		}
		
		public static STObject For(object obj)
		{
			if (obj is STObject)
				return obj as STObject;	// no need to wrap a Smalltalk object.
			
			if (obj is bool) {
				return (bool)obj ? STBooleanDelegate.True : STBooleanDelegate.False;
			}
			
			return new STInstance (obj);
		}
		
		public override object Native {
			get { return Target; }
		}
		
		public override object MethodReceiver {
			get { return Target; }
		}

		[STRuntimeMethod("doesNotUnderstand:")]
		public override STObject HandleDoesNotUnderstand(STMessage msg)
		{
			Console.WriteLine ("doing " + msg.Selector);
			if (msg.Parameters.Length > 1)
				return base.HandleDoesNotUnderstand(msg);
			
			string name = msg.Selector.Name.Trim(':');
			name = char.ToUpper(name[0]) + name.Substring(1);
			STObject[] parms = msg.Parameters;
			
			if (parms.Length > 0) {
				if (parms[0].Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array")) {
					// Expand the array into the arguments we want...
					var array = parms[0] as Irontalk.Array;
					parms = new STObject[array.Size()];
					for (long i = 1, max = array.Size(); i <= max; ++i)
						parms[i - 1] = STInstance.For(array.At(i));
				}
			}
			
			STObject[] stobj = parms;
			object[] native = new object[stobj.Length];
			Type[] types = new Type[stobj.Length];
			
			for (int i = 0, max = stobj.Length; i < max; ++i) {
				native[i] = stobj[i].Native;
				types[i] = native[i].GetType();
			}
			
			MethodInfo method = null;
			
			if (stobj.Length <= 1) {
				var props = Target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
				foreach (var prop in props) {
					if (prop.Name == name) {
						if (stobj.Length == 0)
							method = prop.GetGetMethod();
						else
							method = prop.GetSetMethod();
						break;
					}
				}
			}
			
			if (method == null)
				method = Target.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public, null, types, null);
			
			if (method != null) {
				object result = method.Invoke(Target, native);
				if (result == Target)
					return this;
			
				if (method.ReturnType == typeof(void))
					return msg.Receiver;
				
				return STInstance.For(result);
			}
			
			return base.HandleDoesNotUnderstand(msg);
		}
	}
}
