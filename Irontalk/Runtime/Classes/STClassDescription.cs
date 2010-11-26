
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
public class STClassDescription: STObject {
		public STClassDescription(string name)
		{
			Name = name;
		}
		
		public virtual void Initialize ()
		{
			foreach (MethodInfo method in GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) {
				var attr = STRuntimeMethodAttribute.Get(method);
				if (attr == null) continue;
				Class.MethodDictionary[STSymbol.Get(attr.Selector)] = 
					new STRuntimeMethod(method);
			}
		}
		
		Dictionary<STSymbol, STCompiledMethod> methodDictionary = 
			new Dictionary<STSymbol, STCompiledMethod>();
		
		public Dictionary<STSymbol, STCompiledMethod> MethodDictionary { get { return methodDictionary; } }
		public virtual STClassDescription Superclass { get { return null; } }
		public virtual string Name { get; protected set; }
		
		[STRuntimeMethod("inspect")]
		public virtual void Inspect()
		{
			Console.WriteLine ("Class known as '{0}'", Name);
			Console.WriteLine ("--------------------");
			Console.WriteLine ("Method dictionary:");
			foreach (var kvp in MethodDictionary) {
				Console.WriteLine ("{0}: {1}", kvp.Key, kvp.Value);	
			}
			Console.WriteLine();
		}
		
		public void InstallDelegate(Type type)
		{
			var flags = BindingFlags.Static | BindingFlags.Public;
			foreach (var method in type.GetMethods(flags)) {
				var attr = STRuntimeMethodAttribute.Get(method);
				if (attr == null) continue;
				MethodDictionary[STSymbol.Get(attr.Selector)] = new STRuntimeMethod(method, true);
			}
		}
		
		public STObject New()
		{
			var obj = new STObject();
			obj.Class = this;
			obj.Send(STSymbol.Get("initialize"));
			return obj;
		}
		
		public STObject RouteMessage(STMessage msg)
		{
			if (STDebug.SuperclassTraversal)
				Console.WriteLine("(routing #{0}) - visiting: {1}", msg.Selector.Name, Name);
			
			STCompiledMethod method;
			
			if (STDebug.VerboseRouting) {
				Console.WriteLine("Routing {0} on class {1}", msg.Selector.Name, this.Name);
				Console.WriteLine("Available messages: ");
				foreach (var kvp in MethodDictionary) {
					Console.WriteLine(" - {0}", kvp.Key);
				}
			}
			
			if (MethodDictionary.TryGetValue(msg.Selector, out method)) {
				if (STDebug.MessageRouting) Console.WriteLine("Message routed to class {0}", Name);
				return method.Invoke(msg);
			}
			
			if (Superclass != null) 
				return Superclass.RouteMessage(msg);
			
			if (STDebug.VerboseRouting) Console.Error.WriteLine ("throwing didNotUnderstand: error");
			throw new MessageNotUnderstood(msg);
		}
	}
}
