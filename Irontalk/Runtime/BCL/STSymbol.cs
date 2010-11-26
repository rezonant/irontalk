
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
public class STSymbol : STRuntimeObject {
		private STSymbol(string str)
		{
			Name = str;
		}
		public string Name { get; private set; }
		static Dictionary<string,STSymbol> symbols =
			new Dictionary<string, STSymbol>();
		
		public override STClassDescription Class {
			get { return STClass.GetForCLR(GetType(), "Symbol"); }	
		}
		
		[STRuntimeMethod("toString")]
		public override string ToString ()
		{
			return string.Format("#{0}", Name);
		}

		public static STSymbol Get(string name)
		{
			STSymbol value;
			if (!symbols.TryGetValue(name, out value)) {
				value = new STSymbol(name);
				symbols[name] = value;
			}
			
			return value;
		}
	}
}
