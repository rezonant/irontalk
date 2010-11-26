
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	[STRuntimeClassDelegate(typeof(Int64), "Integer")]
	public static class STInt64Delegate {
		[STRuntimeMethod("to:")]
		public static Irontalk.Array To(STObject self, long end)
		{
			long start = (long)self.Native;
			long diff = end - start + 1;
			long len = Math.Abs(diff);
			var array = new Irontalk.Array(new object[len]);
			long factor = (diff < 0 ? -1 : 1);
			for (int i = 0; i < len; ++i)
				array.AtPut(i + 1, start + i * factor);
			return array;
		}
		
		[STRuntimeMethod("+")] public static long Add(STObject self, long other) { return (long)self.Native + other; }
		[STRuntimeMethod("-")] public static long Sub(STObject self, long other) { return (long)self.Native - other; }
		[STRuntimeMethod("/")] public static long Div(STObject self, long other) { return (long)self.Native / other; }
		[STRuntimeMethod("*")] public static long Mul(STObject self, long other) { return (long)self.Native * other; }
	}
}
