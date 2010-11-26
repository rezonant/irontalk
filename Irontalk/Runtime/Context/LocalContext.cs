
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
public class LocalContext : Context {
		public LocalContext(Context parent, bool explicitVariables):
			base (parent)
		{
			ExplicitVariables = explicitVariables;
		}
		
		public LocalContext(Context parent):
			this (parent, false)
		{}
		
		public LocalContext():
			this (GlobalContext.Instance, false)
		{}
		
		public bool ExplicitVariables { get; private set; }
		
		Dictionary<string,STObject> variables =
			new Dictionary<string, STObject>();
		
		public override STObject GetVariable(string name)
		{
			if (variables.ContainsKey(name))
				return variables[name];
			return base.GetVariable(name);
		}
		
		public override void Declare (string name)
		{
			if (!variables.ContainsKey(name))
				variables[name] = STUndefinedObject.Instance;
		}
		
		public override bool SetVariable(string name, STObject value)
		{
			if (ExplicitVariables) {
				if (!variables.ContainsKey(name)) {
					if (ParentContext.SetVariable(name, value)) return true;
					throw new Exception("No such variable " + name);
				}
			}
			
			variables[name] = value;
			return true;
		}
	}
}
