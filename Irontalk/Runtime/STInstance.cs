
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	/// <summary>
	/// STInstance is an STObject subclass which makes direct usage of .NET-native objects possible within Irontalk.
	/// Merely create an instance by passing in the target reference and voila. STInstance will create any needed
	/// Smalltalk classes on the fly.
	/// </summary>
	public class STInstance : STObject {
		public STInstance (object target)
		{
			Target = target;
		}
		
		public object Target;
		public override STClassDescription Class {
			get {
				var type = Target.GetType();
				return STClass.GetForCLR(type, type.Name);
			}
		}
		
		public static STObject For(object obj)
		{
			if (obj is STObject)
				return obj as STObject;	// no need to wrap a Smalltalk object.
			
			if (obj is bool) {
				return (bool)obj ? STBooleanDelegate.True : STBooleanDelegate.False;
			}
			
			return new STInstance (obj);
		}
		
		public override object Native {
			get { return Target; }
		}
		
		public override object MethodReceiver {
			get { return Target; }
		}

		[STRuntimeMethod("didNotUnderstand:")]
		public override STObject HandleDidNotUnderstand(STMessage msg)
		{
			// System Console writeLine: { 'thing1' . 'thing2' }
			// --> System.Console.WriteLine("thing1", "thing2");
			
			if (msg.Parameters.Length > 1)
				return base.HandleDidNotUnderstand(msg);
			
			string name = msg.Selector.Name.Trim(':');
			name = char.ToUpper(name[0]) + name.Substring(1);
			STObject[] parms = msg.Parameters;
			
			if (parms.Length > 0) {
				if (parms[0].Class == STClass.GetForCLR(typeof(Irontalk.Array), "Array")) {
					// Expand the array into the arguments we want...
					var array = parms[0] as Irontalk.Array;
					parms = new STObject[array.Size()];
					for (long i = 1, max = array.Size(); i <= max; ++i)
						parms[i - 1] = STInstance.For(array.At(i));
				}
			}
			
			STObject[] stobj = parms;
			object[] native = new object[stobj.Length];
			Type[] types = new Type[stobj.Length];
			
			for (int i = 0, max = stobj.Length; i < max; ++i) {
				native[i] = stobj[i].Native;
				types[i] = native[i].GetType();
			}
			
			MethodInfo method = null;
			
			if (stobj.Length == 0) {
				var props = Target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
				foreach (var prop in props) {
					if (prop.Name == name) {
						method = prop.GetGetMethod();
						break;
					}
				}
			}
			
			if (method == null)
				method = Target.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public, null, types, null);
			
			if (method != null) {
				object result = method.Invoke(Target, native);
				if (result == Target)
					return this;
			
				if (method.ReturnType == typeof(void))
					return msg.Receiver;
				
				return STInstance.For(result);
			}
			
			return base.HandleDidNotUnderstand(msg);
		}
	}
}
