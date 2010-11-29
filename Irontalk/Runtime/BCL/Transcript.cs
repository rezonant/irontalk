
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
			Out = Console.Out;
		}
		
		public TextWriter Out { get; set; }
		
		[STRuntimeMethod("show:")]
		public STObject Show(object value)
		{
			Out.Write(value);
			return STClass.GetForCLR(typeof(Transcript), "Transcript");
		}
		
		public static Transcript Instance {
			get {
				return GlobalContext.Instance.GetVariable("Transcript").Native as Transcript;
			}
		}
		
		public static void WriteLine(string format, params object[] args)
		{
			var tr = GlobalContext.Instance.GetVariable("Transcript").Native as Transcript;
			
			if (tr == null)
				Console.WriteLine (format, args);
			
			tr.Show(string.Format(format + "\n", args));
		}
	}
}
