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
					display = display.Send(STSymbol.Get("asString"));
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
				
				if (input.StartsWith("#include ")) {
					using (var fr = new StreamReader(input.Substring("#include ".Length)))
						input = fr.ReadToEnd();
				}
				
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
