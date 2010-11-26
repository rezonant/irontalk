
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
public class STMessage : STObject {
		public STMessage(STObject receiver, STSymbol message, params STObject[] args)
		{
			Receiver = receiver;
			Selector = message;
			Parameters = args;
		}
		
		public STObject Receiver { get; private set; }
		public STSymbol Selector { get; private set; }
		public STObject[] Parameters { get; private set; }
		
		public override STClassDescription Class {
			get { return STClass.GetForCLR(GetType(), "Message"); }
		}
	}
}
