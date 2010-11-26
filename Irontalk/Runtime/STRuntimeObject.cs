
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
public class STRuntimeObject : STObject {
		protected virtual string GenericName {
			get { return GetType().Name; }
		}
		
		public override STClassDescription Class {
			get {
				return STClass.GetForCLR(GetType(), GenericName);
			}
		}
	}
}
