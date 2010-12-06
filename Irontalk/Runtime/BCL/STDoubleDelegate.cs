
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;

namespace Irontalk {
	[STRuntimeClassDelegate(typeof(double), "Float")]
	public static class STDoubleDelegate {
		public static double Coerce(STObject sto)
		{
			var o = sto.Native;
			
			if (typeof(double).IsAssignableFrom(o.GetType()))
				return (double)o;
			
			if (typeof(long).IsAssignableFrom(o.GetType()))
				return (double)(long)o;
			
			throw new InvalidOperationException("Input must be coercible to Float");
		}
		
		[STRuntimeMethod("+")]			public static double  Add(double self, STObject other) { return self + Coerce(other); }
		[STRuntimeMethod("-")]			public static double  Sub(double self, STObject other) { return self - Coerce(other); }
		[STRuntimeMethod("/")]			public static double  Div(double self, STObject other) { return self / Coerce(other); }
		[STRuntimeMethod("*")]			public static double  Mul(double self, STObject other) { return self * Coerce(other); }
		[STRuntimeMethod(">")]			public static bool     Gt(double self, STObject other) { return self > Coerce(other); }
		[STRuntimeMethod("<")]			public static bool     Lt(double self, STObject other) { return self < Coerce(other); }
		[STRuntimeMethod(">=")]			public static bool   GtEq(double self, STObject other) { return self >= Coerce(other); }
		[STRuntimeMethod("<=")]			public static bool   LtEq(double self, STObject other) { return self <= Coerce(other); }
	}
}
