
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	[STRuntimeClassDelegate(typeof(Object), "Object")]
	public static class STObjectDelegate {
		[STRuntimeMethod("class")]
		public static STClassDescription GetClass(STObject self)
		{
			return self.Class;
		}
		
		[STRuntimeMethod("==")]
		public static bool Identity(STObject self, STObject other)
		{
			return self.Native == other.Native;
		}
		
		[STRuntimeMethod("=")]
		public static bool Equality(STObject self, STObject other)
		{
			return self.Native.Equals(other.Native);
		}
	}
}
