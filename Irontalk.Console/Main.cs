using System;
using System.IO;
using Irontalk;
using System.Reflection;
using System.Runtime.InteropServices;
namespace Test
{
	public static class ReadLine {
		[DllImport("libreadline.so")]
		public static extern string readline(string prompt);
	}
	
	class MainClass
	{
		public static void TestInput (Compiler interp, string input, Context ctx)
		{
			input = input.Trim(' ', '\n', '\t');
			if (input == "") return;
			
			STClass stringClass = STClass.GetForCLR(typeof(string), "String");
			STObject obj = interp.Evaluate (input, ctx);
			       
			if (obj == null) {
				Console.WriteLine(" x> null");
				return;
			}
			
			STObject display = obj;
			
			if (display.Class != stringClass) {
				try {
					display = display.Send(STSymbol.Get("toString"));
				} catch (Exception e) {
					Console.Error.WriteLine("*** Caught {0} while sending #toString to result", e.GetType().FullName);
					Console.Error.WriteLine(e);
				}
			}
			
			Console.WriteLine(" => " + display.Native.ToString());
		}
		
		public static void Main (string[] args)
		{
			var interp = new Compiler(Assembly.GetCallingAssembly());
			var ctx = new LocalContext(GlobalContext.Instance);
			/*/
			TestInput (interp, "2");
			/*/
			while (true) {
				string input = ReadLine.readline("ist: ");
				
				if (input == "exit")
					break;
				
				try {
					TestInput (interp, input, ctx);
				} catch (Exception e) {
					Console.WriteLine ("Unhandled exception: " + e);
				}
			}
			//*/
		}
	}
}
