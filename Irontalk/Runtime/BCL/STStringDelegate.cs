// 
//  Author:
//       William Lahti <wilahti@gmail.com>
// 
//  Copyright © 2010 William Lahti
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  As a special exception, the copyright holders of this library give
//  you permission to link this library with independent modules to
//  produce an executable, regardless of the license terms of these 
//  independent modules, and to copy and distribute the resulting 
//  executable under terms of your choice, provided that you also meet,
//  for each linked independent module, the terms and conditions of the
//  license of that module. An independent module is a module which is
//  not derived from or based on this library. If you modify this library, you
//  may extend this exception to your version of the library, but you are
//  not obligated to do so. If you do not wish to do so, delete this
//  exception statement from your version. 
// 
//  This program is distributed in the hope that it will be useful, 
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
using System.Text;
namespace Irontalk {
	public static class ArrayCasting {
		public static DestT[] Cast<DestT>(this System.Array array)	
		{
			DestT[] destArray = new DestT[array.Length];
			for (int i = 0, max = destArray.Length; i < max; ++i) {
				object value = array.GetValue(i);
				if (value is STObject) value = (value as STObject).Native;
				
				destArray[i] = (DestT)value;	
			}
			
			return destArray;
		}
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
		
		[STRuntimeMethod("asInteger")] public static long AsInteger(string self) 
		{ 
			return long.Parse (self);
		}
		
		[STRuntimeMethod("size")] public static long Size(string self) 
		{ 
			return self.Length;
		}
		
		[STRuntimeMethod("at:")] public static char At(string self, int index) 
		{ 
			return self[index];
		}
		
		[STRuntimeMethod("withCRs")] public static string WithCRs(string self) 
		{ 
			return self.Replace("\\", "\n");
		}
		
		[STRuntimeMethod("join:")] public static string Join(string self, STObject collection) 
		{
			long size = (long)collection.Send("size").Native;
			string[] values = new string[size];
			var stIndex = new STInstance((long)0);
			for (long i = 0, max = (long)size; i < max; ++i) {
				stIndex.Target = i + 1;
				values[i] = collection.Send("at:", 	stIndex).Send("asString").Native as string;
			}
			
			return string.Join(self, values);
		}
		
		[STRuntimeMethod("format:")] public static string Format(string self, STObject collection) 
		{
			long size = (long)collection.Send("size").Native;
			object[] values = new object[size + 1];
			var stIndex = new STInstance((long)0);
			values[0] = STUndefinedObject.Instance;
			for (long i = 1, max = (long)size; i <= max; ++i) {
				stIndex.Target = i;
				values[i] = collection.Send("at:", 	stIndex);
			}
			
			return string.Format(self, values);
		}
		
		[STRuntimeMethod("subStrings:")] public static Irontalk.Array SubStrings(string self, string sep)
		{
			return SubStringsIncludeEmpty(self, sep, true);
		}
		
		[STRuntimeMethod("subStrings:includeEmpty:")] 
		public static Irontalk.Array SubStringsIncludeEmpty(string self, string sep, bool includeEmpty)
		{
			List<string> chunks = new List<string>();
			
			while (true) {
				int index = self.IndexOf(sep);
				
				if (index == -1)
					break;
				
				string chunk = self.Substring (0, index);
				
				if (includeEmpty || chunk != "")
					chunks.Add (chunk);
				
				self = self.Substring (index + sep.Length);
			}
			
			chunks.Add(self);
			
			return new Irontalk.Array(chunks.ToArray().Cast<Object>());
		}
		
		[STRuntimeMethod("findString:")] public static long FindString(string self, string substr) 
		{ 
			return self.IndexOf(substr) + 1;
		}
		
		[STRuntimeMethod("findFirst:")] public static long FindFirst(string self, STBlock aBlock) 
		{ 
			long index = 0;
			var chInst = new STInstance('a');
			foreach (char ch in self) {
				chInst.Target = ch;
				if ((bool)aBlock.Evaluate(chInst).Native)
					return index + 1;
				++index;
			}
			
			return 0;
		}
		
		[STRuntimeMethod("findString:startingAt:")] public static long FindStringStartingAt(string self, string substr, long at) 
		{
			return self.IndexOf(substr, (int)at) + 1;
		}
		
		[STRuntimeMethod("replace:with:")] public static string ReplaceWith(string self, string find, string replace) 
		{ 
			return self.Replace(find, replace);
		}
		
		[STRuntimeMethod("startsWith:")] public static bool StartsWith(string self, string find) 
		{ 
			return self.StartsWith(find);
		}
		
		[STRuntimeMethod("endsWith:")] public static bool EndsWith(string self, string find) 
		{ 
			return self.EndsWith(find);
		}
		
		[STRuntimeMethod("trim:")] public static string Trim(string self, Irontalk.Array chars) 
		{
			return self.Trim(chars.NativeArray().Cast<char>());
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
}
