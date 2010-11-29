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
	/// <summary>
	/// Base class for STClass and STMetaclass. Implements most of the message routing behavior
	/// present in its subclasses. 
	/// </summary>
	public class STClassDescription: STObject {
		public STClassDescription(string name)
		{
			Name = name;
		}
		
		public virtual void Initialize ()
		{
			foreach (MethodInfo method in GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) {
				var attr = STRuntimeMethodAttribute.Get(method);
				if (attr == null) continue;
				Class.MethodDictionary[STSymbol.Get(attr.Selector)] = 
					new STRuntimeMethod(method);
			}
		}
		
		public string Plural {
			get {
				if (Name.EndsWith("s"))
					return Name + "es";
				else
					return Name + "s";
			}
		}
		
		public string GenericInstanceSymbol {
			get {
				if (Name[0].IsVowel() || char.ToLower(Name[0]) == 'y')
					return "an" + Name;
				else
					return "a" + Name;
			}
		}
		
		public string GenericInstanceName {
			get {
				if (Name[0].IsVowel() || char.ToLower(Name[0]) == 'y')
					return "an " + Name;
				else
					return "a " + Name;
			}
		}
		
		protected Dictionary<STSymbol, STCompiledMethod> methodDictionary = 
			new Dictionary<STSymbol, STCompiledMethod>();
		protected string[] ivarNames = new string[0];
		
		public Dictionary<STSymbol, STCompiledMethod> MethodDictionary { get { return methodDictionary; } }
		public virtual STClassDescription Superclass { get { return null; } }
		public virtual string Name { get; protected set; }
		
		private void CollectInstanceVariables (List<string> vars)
		{
			if (Superclass != null)
				Superclass.CollectInstanceVariables(vars);
			vars.AddRange(ivarNames);
		}
		
		public virtual string[] InstanceVariableNames { 
			get {
				var list = new List<string>();
				CollectInstanceVariables(list);
				return list.ToArray();
			}
		}
		
		[STRuntimeMethod("inspect")]
		public virtual void Inspect()
		{
			Console.WriteLine ("Class known as '{0}'", Name);
			Console.WriteLine ("--------------------");
			Console.WriteLine ("Method dictionary:");
			foreach (var kvp in MethodDictionary) {
				Console.WriteLine ("{0}: {1}", kvp.Key, kvp.Value);	
			}
			Console.WriteLine();
		}
		
		public void InstallDelegate(Type type)
		{
			var flags = BindingFlags.Static | BindingFlags.Public;
			foreach (var method in type.GetMethods(flags)) {
				var attr = STRuntimeMethodAttribute.Get(method);
				if (attr == null) continue;
				MethodDictionary[STSymbol.Get(attr.Selector)] = new STRuntimeMethod(method, true);
			}
		}
		
		public STObject New()
		{
			var obj = new STObject();
			obj.Class = this;
			obj.Send(STSymbol.Get("initialize"));
			return obj;
		}
		
		public override string ToString ()
		{
			return Name;
		}
		
		public STObject RouteMessage(STMessage msg)
		{
			if (STDebug.SuperclassTraversal)
				Console.WriteLine("(routing #{0}) - visiting: {1}", msg.Selector.Name, Name);
			
			STCompiledMethod method;
			
			if (STDebug.VerboseRouting) {
				Console.WriteLine("Routing {0} on class {1}", msg.Selector.Name, this.Name);
				Console.WriteLine("Available messages: ");
				foreach (var kvp in MethodDictionary) {
					Console.WriteLine(" - {0}", kvp.Key);
				}
			}
			
			if (MethodDictionary.TryGetValue(msg.Selector, out method)) {
				if (STDebug.MessageRouting) Console.WriteLine("Message routed to class {0}", Name);
				return method.Invoke(msg);
			}
			
			if (Superclass != null) 
				return Superclass.RouteMessage(msg);
			
			if (STDebug.VerboseRouting) Console.Error.WriteLine ("throwing doesNotUnderstand: error");
			throw new MessageNotUnderstood(msg);
		}
	}
}
