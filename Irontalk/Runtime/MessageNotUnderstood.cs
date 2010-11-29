
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
public class MessageNotUnderstood : Exception {
		public MessageNotUnderstood(STMessage msg):
			base ("Message #" + msg.Selector.Name + " not understood")
		{
			PendingMessage = msg;
		}
		
		public STMessage PendingMessage { get; private set; }
	}
}
