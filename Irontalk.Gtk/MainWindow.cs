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
using Gtk;
using Irontalk;
using System.IO;
using System.Text;

public partial class MainWindow : Gtk.Window {
	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		compiler = new Compiler();
		context = new LocalContext();
		
		GlobalContext.Instance.SetVariable("Transcript", new Transcript(new Writer(this)));
	}

	private class Writer : TextWriter {
		public Writer (MainWindow window)
		{
			this.window = window;	
		}
		
		MainWindow window;
		
		public override Encoding Encoding {
			get { return Encoding.Default; }
		}
		
		public override void Write(string str)
		{
			window.Write (str);
		}	
	}
	
	Compiler compiler;
	Context context;
	bool showParseTrees = false;
	bool printInput = true;
	
	public void Write(string val)
	{
		var iter = output.Buffer.EndIter;
		output.Buffer.Insert(ref iter, val);
		output.Buffer.PlaceCursor(output.Buffer.EndIter);
		output.ScrollToIter(output.Buffer.EndIter, 0, false, 0, 0);
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
	
	public void Activate(string source)
	{
		STObject obj;
		string val = "null";
		
		STDebug.ShowParseTrees = showParseTrees;
		
		try {
			obj = compiler.Evaluate(source, context);
			if (obj != null)
				val = " => " + obj.Send(STSymbol.Get("asString")).Native.ToString();
		} catch (Exception ex) {
			val = " x> " + ex;
		}
		
		if (printInput)
			Write (source + "\n");
		Write (val + "\n");
	}
	
	protected virtual void EvalActivated (object sender, System.EventArgs e)
	{
		Activate (input.Text);
		input.Text = "";
	}
	
	protected virtual void ToggleWordWrap (object sender, System.EventArgs e)
	{
		output.WrapMode = WordWrapAction.Active ? WrapMode.Word : WrapMode.None;
	}
	
	protected virtual void InputEnter (object sender, System.EventArgs e)
	{
		EvalActivated(sender, e);
	}
	
	protected virtual void MultiInputActivated (object sender, System.EventArgs e)
	{
		Console.WriteLine("wtf");
		Activate(multiInput.Buffer.Text);
		multiInput.Buffer.Text = "";
	}
	
	protected virtual void ToggleShowParseTree (object sender, System.EventArgs e)
	{
		showParseTrees = !showParseTrees;
	}
	
	protected virtual void TogglePrintInput (object sender, System.EventArgs e)
	{
		printInput = !printInput;
	}
	
	
	
	
	
	
	
	
}

