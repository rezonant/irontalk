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
namespace Irontalk
{
	/// <summary>
	/// Defines messages for the (System Boolean) class.
	/// </summary>
	[STRuntimeClassDelegate(typeof(bool), "Boolean")]
	public class STBooleanDelegate {
		static STInstance @true, @false;
		
		public static STInstance True {
			get {
				if (@true == null)
					@true = new STInstance(true);
				return @true;
			}
		}
		
		public static STInstance False {
			get {
				if (@false == null)
					@false = new STInstance(false);
				return @false;
			}
		}
		
		[STRuntimeMethod("ifTrue:")]
		public static STObject IfTrue(STObject self, STObject aBlockObj)
		{
			var aBlock = aBlockObj as STBlock;
			if (aBlock == null)
				throw new Exception ("Argument to ifTrue: must be a block");
			if (aBlock.BlockArgumentNames.Length > 0)
				throw new Exception("Argument to ifTrue: has too many arguments");
			if (self.Class != STClass.GetForCLR(typeof(bool), "Boolean"))
				throw new Exception("ifTrue: Non-boolean receiver");
			
			if ((bool)self.Native)
				return aBlock.Evaluate();
			
			return self;
		}
		
		[STRuntimeMethod("ifFalse:")]
		public static STObject IfFalse(STObject self, STObject aBlockObj)
		{
			var aBlock = aBlockObj as STBlock;
			if (aBlock == null)
				throw new Exception ("Argument to ifTrue: must be a block");
			if (aBlock.BlockArgumentNames.Length > 0)
				throw new Exception("Argument to ifTrue: has too many arguments");
			if ((bool)self.Native)
				return aBlock.Evaluate();
			return self;
		}
		
		[STRuntimeMethod("not")] public static bool Not (bool self)				{ return !self; }
		[STRuntimeMethod("&")]	 public static bool And (bool self, bool other) { return self && other; }
		[STRuntimeMethod("|")]	 public static bool Or  (bool self, bool other) { return self || other; }
		
		[STRuntimeMethod("asString")]
		public static string ToString(STObject @bool)
		{	
			return ((bool)@bool.Native).ToString().ToLower();
		}
	}
}
