
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	public class STVariable: STObject {
		public STVariable(string name, Context context)
		{
			Name = name;
			Context = context;
		}
		
		public string Name;
		public Context Context;
		
		public override STObject Dereference ()
		{
			return Context.GetVariable(Name);
		}
		
		public void Set(STObject value)
		{
			Context.SetVariable(Name, value);	
		}
		
		public override string ToString ()
		{
			return "variable #" + Name;
		}
	}
	
	public class STObject {
		public virtual STClassDescription Class { get; set; }	
		
		/// <summary>
		/// This should be overridden by subclasses when a better native representation
		/// is available. For instance, STInstance wraps a .NET object, so that object would
		/// be returned by that subclass.
		/// </summary>
		public virtual object Native {
			get { return this; }
		}
		
		public virtual object MethodReceiver {
			get { return this; }
		}
		
		public virtual STObject Dereference()
		{
			return this;	
		}
		
		[STRuntimeMethod("isNil")]
		public virtual bool IsNil() { return false; }
		[STRuntimeMethod("class")]
		public virtual STObject GetClass() { return Class; }
			
		public virtual STObject Send (STSymbol message, params STObject[] args)
		{
			STMessage msg = new STMessage (this, message, args);
			try {
				return Class.RouteMessage(msg);
			} catch (MessageNotUnderstood e) {
				if (message.Name == "didNotUnderstand:")
					throw e;	// avoid infinite recursion
				
				try {
					return Send (STSymbol.Get("didNotUnderstand:"), msg);
				} catch (MessageNotUnderstood) {
						return HandleDidNotUnderstand(msg);
				}
			}
		}
		
		public virtual STObject HandleDidNotUnderstand(STMessage msg)
		{
			Console.Error.WriteLine("{0} didNotUnderstand: #{1}", msg.Receiver, msg.Selector.Name);
			return STUndefinedObject.Instance;
		}
	}
}
