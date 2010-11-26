
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	public class STRuntimeClassDelegateAttribute : Attribute {
		public STRuntimeClassDelegateAttribute(Type t, string branding)
		{
			Type = t;
			Branding = branding;
		}
		
		public Type Type;
		public string Branding;
	}
}
