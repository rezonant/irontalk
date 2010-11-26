
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
public abstract class Context {
		public Context(Context pc)
		{
			ParentContext = pc;
		}
		
		public Context ParentContext { get; set; }
		
		public virtual STObject GetVariable(string name)
		{
			if (ParentContext != null)
				return ParentContext.GetVariable(name);
			return STUndefinedObject.Instance;
		}
		
		public virtual void Declare(string name)
		{
		}
		
		public abstract bool SetVariable(string name, STObject value);
	}
}
