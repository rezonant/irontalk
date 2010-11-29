
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
public class STUndefinedObject : STRuntimeObject {
		private STUndefinedObject()
		{
		}
		
		protected override string GenericName { get { return "UndefinedObject"; } }
	
		[STRuntimeMethod("isNil")]
		public override bool IsNil() { return true; }
		
		[STRuntimeMethod("asString")]
		public override string ToString ()
		{
			return "nil";
		}

		static STUndefinedObject instance;
		
		public static STUndefinedObject Instance {
			get {
				if (instance == null)
					instance = new STUndefinedObject();
				
				return instance;
			}
		}
	}
}
