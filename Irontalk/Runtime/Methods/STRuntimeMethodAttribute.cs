
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
public class STRuntimeMethodAttribute : Attribute {
		public STRuntimeMethodAttribute(string selector)
		{
			Selector = selector;
		}
		
		public string Selector;
		
		public static STRuntimeMethodAttribute Get(MemberInfo info)
		{
			return GetCustomAttribute(info, typeof (STRuntimeMethodAttribute), true) as STRuntimeMethodAttribute;
		}
	}
}
