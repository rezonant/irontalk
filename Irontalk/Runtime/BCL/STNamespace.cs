
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
public class STNamespace : STRuntimeObject {
		public STNamespace(string name, string fullName)
		{
			Name = name;
			FullName = fullName;
			Map = new Dictionary<STSymbol, STObject>();
		}
		
		public string FullName { get; private set; }
		public string Name { get; private set; }
		protected override string GenericName { get { return "Namespace"; } }
		public Dictionary<STSymbol,STObject> Map { get; private set; }

		public void Install(STSymbol key, STObject value)
		{
			Map[key] = value;
		}
		
		[STRuntimeMethod("name")]
		protected string GetName() { return Name; }
		
		[STRuntimeMethod("didNotUnderstand:")]
		protected object DidNotUnderstand(STMessage msg)
		{
			if (Map.ContainsKey(msg.Selector))
				return Map[msg.Selector];
			
			var t = Type.GetType(string.Format("{0}.{1}", FullName, msg.Selector.Name));
			
			if (t != null) {
				var @class = STClass.GetForCLR(t, t.Name);
				Map[msg.Selector] = @class;
				return @class;
			}
			
			try {
				return Class.Superclass.RouteMessage(new STMessage(this, STSymbol.Get("didNotUnderstand:"), msg));
			} catch (MessageNotUnderstood) {
				Console.Error.WriteLine ("No one implements didNotUnderstand:! Bailing out.");
				return STUndefinedObject.Instance;
			}
		}
		
		[STRuntimeMethod("toString")]
		public override string ToString ()
		{
			return string.Format("Namespace({0})", FullName);
		}

		public static STNamespace Build(string name, NSPrototype prototype)
		{
			var ns = new STNamespace(name, name);
			ns.Build (prototype.Entries);
			return ns;
		}
		
		public void Build(Dictionary<string,NSPrototype> prototypes)
		{
			foreach (KeyValuePair<string,NSPrototype> kvp in prototypes) {
				var ns = new STNamespace(kvp.Key, FullName + "." + kvp.Key);
				ns.Build (kvp.Value.Entries);
				Map[STSymbol.Get(kvp.Key)] = ns;
			}
		}
	}
}
