
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
public class GlobalContext : LocalContext {
		private GlobalContext():
			base (null)
		{
			Dictionary<string,NSPrototype> ns =
				new Dictionary<string, NSPrototype>();
			
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				foreach (var type in assembly.GetTypes()) {
					var current = ns;
					if (type.Namespace == null) continue;
					foreach (string component in type.Namespace.Split('.')) {
						if (current.ContainsKey(component)) {
							current = current[component].Entries;
						} else {
							var ent = new NSPrototype();
							current[component] = ent;
							current = ent.Entries;
						}
					}
				}
			}
			
			// Load the top-level namespaces
			foreach (var kvp in ns)
				SetVariable(kvp.Key, STNamespace.Build(kvp.Key, kvp.Value));
		}
		
		[STRuntimeMethod("at:put:")]
		public void AtPut(string alias, STObject obj)
		{
			SetVariable(alias, obj);
		}
		
		static GlobalContext instance = null;
		public static GlobalContext Instance {
			get { 
				if (instance == null) instance = new GlobalContext();
				return instance; 
			}
		}
		
		public override STObject GetVariable (string name)
		{
			if (name == "nil")
				return STUndefinedObject.Instance;
			if (name == "true")
				return STBooleanDelegate.True;
			if (name == "false")
				return STBooleanDelegate.False;
			
			var result = base.GetVariable (name);
			
			if (result.IsNil()) {
				Type t = Type.GetType(name);
				
				if (t != null)
					result = STClass.GetForCLR(t, t.Name);
			}
			
			return result;
		}
	}
}
