
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
using System.Text;
namespace Irontalk
{
	public class Array : STRuntimeObject {
		public Array (object[] array)
		{
			this.array = array;	
		}
		
		[STRuntimeMethod("new")]
		public static Array New()
		{
			return new Array(new object[0]);
		}
		
		[STRuntimeMethod("native")]
		public object[] NativeArray()
		{
			return array;	
		}
		
		[STRuntimeMethod("new:")]
		public static Array New(long size)
		{
			var arr = new object[size];
			for (int i = 0; i < size; ++i)
				arr[i] = STUndefinedObject.Instance;
			return new Array(arr);
		}
		
		object[] array;
		
		[STRuntimeMethod("asString")]
		public override string ToString ()
		{
			var sb = new StringBuilder();
			sb.Append("#(");
			for (int i = 0, max = array.Length; i < max; ++i) {
				object o = array[i];
				if (o is STObject)
					sb.Append((o as STObject).Send(STSymbol.Get("asString")).Native);
				else 
					sb.Append(o.ToString());
				
				if (i + 1 < max) sb.Append(' ');
			}
			sb.Append(")");
			
			return sb.ToString();
		}
		
		[STRuntimeMethod("size")]
		public long Size()
		{
			return array.Length;
		}

		[STRuntimeMethod("at:")]
		public object At(long index)
		{
			return array[index - 1]; // 1-based arrays
		}
		
		[STRuntimeMethod("at:put:")]
		public void AtPut(long index, object value)
		{
			array[index - 1] = value;
		}
		
		[STRuntimeMethod("add:")]
		public void STAdd(object value)
		{
			throw new Exception("Message not appropriate for this collection.");
		}
	}
}
