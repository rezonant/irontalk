
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
using System.Text;
namespace Irontalk
{
	public class TranscriptWriter : TextWriter {
		public TranscriptWriter(Transcript transcript)
		{
			this.transcript = transcript;	
		}
		
		Transcript transcript;
		
		public override Encoding Encoding {
			get { return Encoding.Default; }
		}
		public override void Write (string value)
		{
			transcript.Show(value);
		}
	}
	
	public class Transcript : STRuntimeObject {
		public Transcript(TextWriter output)
		{
			Out = output;
		}
		
		public TextWriter Out { get; set; }
		
		[STRuntimeMethod("show:")]
		public void Show(object value)
		{
			Out.Write(value);
		}
		
		[STRuntimeMethod("showLine:")]
		public void ShowLine(object value)
		{
			Show (value);
			Show ("\n");
		}
		
		public static Transcript Instance {
			get {
				return GlobalContext.Instance.GetVariable("Transcript").Native as Transcript;
			}
		}
		
		public static void WriteLine(string format, params object[] args)
		{
			var instance = Instance;
			
			if (instance == null)
				Console.WriteLine (format, args);
			
			instance.Show(string.Format(format + "\n", args));
		}
	}
}
