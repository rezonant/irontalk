
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
using System.Text;
namespace Irontalk
{
	public class SmalltalkImage : STRuntimeObject {
		static bool initialized = false;
		
		public static void Initialize()
		{
			if (!initialized) {
				initialized = true;
				
				var gctx = GlobalContext.Instance;
				var img = new SmalltalkImage();
				
				img.Import(gctx.GetVariable("Irontalk"));
				gctx.SetVariable("Smalltalk", img);
				gctx.SetVariable("Transcript", new Transcript(Console.Out));
				gctx.SetVariable("Integer", STClass.GetForCLR(typeof(long), "Integer"));
				gctx.SetVariable("String", STClass.GetForCLR(typeof(string), "String"));
				gctx.SetVariable("Symbol", STClass.GetForCLR(typeof(STSymbol), "Symbol"));
			}
		}
		
		[STRuntimeMethod("version")]
		public string Version()
		{
			return "Irontalk 0.1";
		}
		
		[STRuntimeMethod("import:")]
		public void Import(STObject nsObj)
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
