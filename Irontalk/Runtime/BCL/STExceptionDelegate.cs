
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;

namespace Irontalk {
	[STRuntimeClassDelegate(typeof(System.Exception), "Exception")]
	public static class STExceptionDelegate {
		[STRuntimeMethod("throw")]
		public static void Throw(Exception self)
		{
			throw self;
		}
	}
}
