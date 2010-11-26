
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
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
		
		[STRuntimeMethod("toString")]
		public static string ToString(STObject @bool)
		{	
			return ((bool)@bool.Native).ToString().ToLower();
		}
	}
}
