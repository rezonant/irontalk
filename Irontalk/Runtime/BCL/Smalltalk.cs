
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
using System.Text;
namespace Irontalk
{
	public class Smalltalk : STObject {
		static bool initialized = false;
		
		public static void Initialize()
		{
			if (!initialized) {
				initialized = true;
				
				Import(GlobalContext.Instance.GetVariable("Irontalk"));
				GlobalContext.Instance.AtPut("Integer", STClass.GetForCLR(typeof(long), "Integer"));
				GlobalContext.Instance.AtPut("String", STClass.GetForCLR(typeof(string), "String"));
			}
		}
		
		[STRuntimeMethod("version")]
		public static string Version()
		{
			return "Irontalk 0.1";
		}
		
		[STRuntimeMethod("import:")]
		public static void Import(STObject nsObj)
		{
			var ns = nsObj as STNamespace;
			
			if (ns == null)
				throw new Exception ("Argument must be a Namespace");
			
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
				foreach (var type in assembly.GetTypes()) {
					if (type.Namespace == ns.FullName) {
						var @class = STClass.GetForCLR(type, type.Name);
						GlobalContext.Instance.AtPut(@class.Name, @class);
					}
				}
			}
		}
	}
}
