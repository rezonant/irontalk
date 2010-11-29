
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	public class STMethodBuilder : STRuntimeObject {
		public STMethodBuilder(STClass buildingClass): base()
		{
			BuildingClass = buildingClass;
			Class.MethodDictionary.Clear(); // Blank slate
		}
		
		public STClass BuildingClass { get; private set; }
		
		[STRuntimeMethod("asString")]
		public override string ToString ()
		{
			return string.Format("MethodBuilder({0})", BuildingClass);
		}
		public override STObject HandleDoesNotUnderstand(STMessage msg)
		{
			List<STSymbol> parms = new List<STSymbol>();
			var symbolClass = STClass.GetForCLR(typeof(STSymbol), "Symbol");
			foreach (var obj in msg.Parameters) {
				if (obj.Class != symbolClass)
					throw new Exception("Pass the names to bind to keyword parameters as symbols, not " + obj.Class.Plural);
				parms.Add(obj as STSymbol);
			}
			
			return new STMethodPrototype(BuildingClass, msg.Selector, parms.ToArray());
		}
	}
}
