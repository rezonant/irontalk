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
namespace Irontalk {
	public class Dictionary : Dictionary<object,object> {
		[STRuntimeMethod("at:")] public object At(object key)
			{ return this[key]; }
		[STRuntimeMethod("at:put:")] public void AtPut(object key, object value)
			{ this[key] = value; }
		[STRuntimeMethod("size")] public long Size()
			{ return this.Count; }
		[STRuntimeMethod("add:")] public void AddM(DictionaryPair pair)
			{ this[pair.Key] = pair.Value; }
		
		[STRuntimeMethod("Pair")]
		public static STClass Pair()
		{
			return STClass.GetForCLR(typeof(DictionaryPair), "DictionaryPair");
		}
	}
}
