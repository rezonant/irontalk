
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	
	[STRuntimeMetaclassDelegate(typeof(string), "String")]
	public static class STStringClassDelegate {
		
	}
	
	[STRuntimeClassDelegate(typeof(string), "String")]
	public static class STStringDelegate {
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
		
		[STRuntimeMethod(",")] public static string Concatenate(string self, string other) 
		{ 
			return self + other; 
		}
		
		[STRuntimeMethod(">")] public static bool Gt(string self, string other) { 
			throw new NotImplementedException();
		}
		
		[STRuntimeMethod("<")] public static bool Lt(string self, string other) { 
			throw new NotImplementedException();
		}
		
		[STRuntimeMethod(">=")] public static bool GtEq(string self, string other) 
		{ 
			return self == other || Gt(self, other); 
		}
		
		[STRuntimeMethod("<=")] public static bool LtEq(string self, string other) 
		{
			return self == other || Lt(self, other);
		}
	}
	
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
		
		[STRuntimeMethod("+")] public static long Add(long self, long other) { return self + other; }
		[STRuntimeMethod("-")] public static long Sub(long self, long other) { return self - other; }
		[STRuntimeMethod("/")] public static long Div(long self, long other) { return self / other; }
		[STRuntimeMethod("*")] public static long Mul(long self, long other) { return self * other; }
		[STRuntimeMethod(">")] public static bool Gt(long self, long other) { return self > other; }
		[STRuntimeMethod("<")] public static bool Lt(long self, long other) { return self < other; }
		[STRuntimeMethod(">=")] public static bool GtEq(long self, long other) { return self >= other; }
		[STRuntimeMethod("<=")] public static bool LtEq(long self, long other) { return self <= other; }
	}
}
