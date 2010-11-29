
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	public class STVariable: STObject {
		public STVariable(string name, Context context)
		{
			Name = name;
			Context = context;
		}
		
		public string Name;
		public Context Context;
		
		public override STObject Dereference ()
		{
			return Context.GetVariable(Name);
		}
		
		public void Set(STObject value)
		{
			Context.SetVariable(Name, value);	
		}
		
		public override string ToString ()
		{
			return "variable #" + Name;
		}
	}
}
