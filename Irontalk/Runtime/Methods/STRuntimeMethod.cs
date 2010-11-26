
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;

namespace Irontalk
{
	public delegate object STMessageDelegate(STMessage msg);
	
	public class STRuntimeMethod : STCompiledMethod {
		public STRuntimeMethod (STMessageDelegate method)
		{
			@delegate = method;
		}
		
		public STRuntimeMethod (MethodInfo method):
			this (method, false)
		{}
		
		public STRuntimeMethod (MethodInfo method, bool passReceiver)
		{
			this.method = method;
			this.PassReceiver = passReceiver;
		}
		
		STMessageDelegate @delegate = null;
		MethodInfo method = null;
		
		public bool PassReceiver { get; private set; }
		
		public override STObject Invoke(STMessage message)
		{
			object result;
			
			if (method != null) {
				STObject[] stobj = message.Parameters;
				var parmInfo = method.GetParameters();
				List<object> native = new List<object> ();
				for (int i = 0, max = stobj.Length; i < max; ++i) {
					var parmType = parmInfo[i + (PassReceiver ? 1 : 0)].ParameterType;
					if (parmType == typeof(STObject) || parmType.IsSubclassOf(typeof(STObject))) {
						native.Add (stobj[i]);
					} else {
						native.Add(stobj[i].Native);
					}
				}
				
				if (PassReceiver)
					native.Insert(0, message.Receiver);
				
				result = method.Invoke(message.Receiver.MethodReceiver, native.ToArray());
				
				if (method.ReturnType == typeof(void))
					result = message.Receiver;
				
			} else if (@delegate != null) {
				result = @delegate(message);
			} else {
				throw new Exception ("This runtime-delegated message has no implementation!");	
			}
			
			if (result is STObject)
				return result as STObject;
			
			return new STInstance(result);
		}
	}
}
