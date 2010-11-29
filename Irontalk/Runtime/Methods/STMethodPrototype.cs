
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
using System.Text;
namespace Irontalk
{
	public class STMethodPrototype: STRuntimeObject {
		public STMethodPrototype(STClass @class, STSymbol selector, STSymbol[] parameterNames)
		{
			BuildingClass = @class;
			Selector = selector;
			ParameterNames = parameterNames;
		}
		
		string sendSample = null;
		string[] keywords = null;
		
		public STSymbol Selector { get; private set; }
		public STSymbol[] ParameterNames { get; private set; }
		public STClass BuildingClass { get; private set; }
		
		public string[] Keywords {
			get {
				if (keywords == null)
					keywords = Selector.Name.Trim(':').Split(':');
				
				return keywords;
			}
		}
		
		public string SendSample {
			get {
				if (sendSample == null) {
					if (ParameterNames.Length == 0) {
						sendSample = Selector.Name;	
					} else {
						StringBuilder sb = new StringBuilder();
						string[] keywords = Keywords;
						
						for (int i = 0, max = keywords.Length; i < max; ++i) {
							sb.AppendFormat("{0}: {1}", keywords[i], ParameterNames[i].Name);
							if (i + 1 < max) sb.Append(' ');
						}
						
						sendSample = sb.ToString();
					}
				}
				
				return sendSample;
			}
		}
		
		[STRuntimeMethod("asString")]
		public override string ToString ()
		{
			return string.Format("MethodPrototype({0} {1})", BuildingClass.GenericInstanceSymbol, SendSample);
		}
		
		[STRuntimeMethod("with:")]
		public STCompiledMethod With(STBlock block)
		{
			return BuildingClass.CompleteMethod(this, block);
		}
	}
}
