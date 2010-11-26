
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
using System.Text;
namespace Irontalk
{
	public class Transcript : STObject {
		[STRuntimeMethod("put:")]
		public static STObject Put(object value)
		{
			if (value is STObject)
				value = (value as STObject).Send(STSymbol.Get("toString")).Native;
			Console.Write(value);
			
			return STUndefinedObject.Instance;
		}
		
		[STRuntimeMethod("putLine:")]
		public static STObject PutLine(object value)
		{
			Put (value);
			Console.WriteLine();
			return STUndefinedObject.Instance;
		}
	}
}
