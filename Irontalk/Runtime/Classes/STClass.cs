
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	public class STClass : STClassDescription {
		private STClass (Type type, string name):
			this(name)
		{
			Type = type;
			
			if (STDebug.ClassInitialization) {
				Console.WriteLine("Initializing Smalltalk class '{0}' for .NET class '{1}'", name, type.FullName);
				Console.WriteLine("  Instance Methods");
			}
			
			foreach (MethodInfo method in type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)) {
				if (STDebug.ClassInitialization) Console.WriteLine("   - " + method);
				var attr = STRuntimeMethodAttribute.Get(method);
				
				if (attr == null) continue;
				
				if (STDebug.ClassInitialization) 
					Console.WriteLine("  ** included as " + attr.Selector);
				
				MethodDictionary[STSymbol.Get(attr.Selector)] = new STRuntimeMethod(method);
			}
			
			if (STDebug.ClassInitialization) Console.WriteLine ("  Static methods:");
			foreach (MethodInfo method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)) {
				if (STDebug.ClassInitialization) Console.WriteLine("   - " + method);
				var attr = STRuntimeMethodAttribute.Get(method);
				
				if (attr == null) continue;
				
				if (STDebug.ClassInitialization) 
					Console.WriteLine("  ** included as (" + Name + " " + attr.Selector + ")");
				
				Metaclass.MethodDictionary[STSymbol.Get(attr.Selector)] = new STRuntimeMethod(method);
			}
		}
		
		public STClass (string name):
			base (name)
		{
			Type = null;
			Metaclass = new STMetaclass(this);
		}
		
		public STClass (STClass superclass, string name):
			this (name)
		{
			this.superclass = superclass;
			
			Metaclass.MethodDictionary[STSymbol.Get("new")] = new STRuntimeMethod(delegate (STMessage msg)
				{
					return new STObject(this);
				});
		}
		
		static STClass()
		{
			foreach (var type in Assembly.GetExecutingAssembly().GetTypes()) {
				var classAttr = Attribute.GetCustomAttribute(type, typeof(STRuntimeClassDelegateAttribute), true) 
					as STRuntimeClassDelegateAttribute;	
				var metaclassAttr = Attribute.GetCustomAttribute(type, typeof(STRuntimeMetaclassDelegateAttribute), true)
					as STRuntimeMetaclassDelegateAttribute;
				
				if (classAttr != null)
					GetForCLR(classAttr.Type, classAttr.Branding).InstallDelegate(type);
				else if (metaclassAttr != null)
					GetForCLR(metaclassAttr.Type, metaclassAttr.Branding).Class.InstallDelegate(type);
				else
					continue;
			}
		}
		
		STClass superclass = null;
		
		public STCompiledMethod CompleteMethod (STMethodPrototype prototype, STBlock block)
		{
			return MethodDictionary[prototype.Selector] = new STNativeMethod(prototype, block);
		}

		public override void Initialize()
		{
			base.Initialize();	
			Metaclass.Initialize();
		}
		
		[STRuntimeMethod("superclass")]
		public STClassDescription GetSuperclass() { return Superclass; }
		
		[STRuntimeMethod("subclass:")]
		public STClass Subclass(STSymbol name)
		{
			Console.WriteLine ("Creating subclass of {0} named '{1}'", Type.FullName, name.Name);
			
			var @class = new STClass(this, name.Name);
			@class.Initialize();
			return @class; 
		}
		
		[STRuntimeMethod("subclass:namespace:")]
		public STClass SubclassNamespace(STSymbol name, STObject nsObj)
		{
			var ns = nsObj as STNamespace;
			
			if (ns == null)
				throw new Exception ("namespace parameter must hold a valid Namespace object, not " + nsObj.GetType().FullName);
			
			var @class = Subclass(name);
			ns.Install(name, @class);
			
			return @class;
		}
		
		[STRuntimeMethod("subclass:namespace:with:")]
		public STClass SubclassNamespaceWith(STSymbol name, STObject nsObj, STBlock blockObj)
		{
			var @class = SubclassNamespace(name, nsObj);
			@class.With(blockObj as STBlock);
			return @class;
		}
		
		[STRuntimeMethod("define")]
		public STMethodBuilder Define()
		{
			return new STMethodBuilder(this);
		}
		
		[STRuntimeMethod("define:")]
		public STMethodPrototype Define(STObject symObj)
		{
			var sym = symObj as STSymbol;
			
			if (sym == null)
				throw new Exception("Argument must be the symbol for the message to define");
			
			return new STMethodPrototype(this, sym, new STSymbol[0]);
		}
		
		[STRuntimeMethod("define:with:")]
		public STCompiledMethod DefineWith(STObject symObj, STObject blockObj)
		{
			var sym = symObj as STSymbol;
			var block = blockObj as STBlock;
			
			if (sym == null)
				throw new Exception("First parameter must be the symbol for the message to define");
			
			if (block == null)
				throw new Exception("Second parameter must be a block");
			
			return CompleteMethod(Define(symObj), block);
		}
		
		[STRuntimeMethod("with:")]
		public void With(STBlock block)
		{
			block.Context = new LocalContext(block.Context);
			block.Context.SetVariable("self", this);
			block.Evaluate();
		}
		
		[STRuntimeMethod("asString")]
		public override string ToString ()
		{
			return Name;
		}

		public override STClassDescription Superclass {
			get {
				if (superclass != null)
					return superclass;
				
				if (Type != null) {
					if (Type.BaseType == null)
						return null;
					return STClass.GetForCLR(Type.BaseType, Type.BaseType.Name);
				}
				
				return STClass.GetForCLR(typeof(object), "Object");
			}
		}
		
		[STRuntimeMethod("doesNotUnderstand:")]
		public override STObject HandleDoesNotUnderstand (STMessage msg)
		{
			if (Type == null) 
				return base.HandleDoesNotUnderstand(msg);
			
			// System Console writeLine: { 'thing1' . 'thing2' }
			// --> System.Console.WriteLine("thing1", "thing2");
			
			Console.WriteLine ("Hrm odd");
			if (msg.Parameters.Length > 1)
				return base.HandleDoesNotUnderstand(msg);
			
			string name = msg.Selector.Name.Trim(':');
			name = char.ToUpper(name[0]) + name.Substring(1);
			STObject[] parms = msg.Parameters;
			
			if (name == "New")
				name = ".ctor";
			
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
			
			if (stobj.Length <= 1 && name != ".ctor") {
				var props = Type.GetProperties(BindingFlags.Public | BindingFlags.Static);
				foreach (var prop in props) {
					if (prop.Name == name) {
						if (stobj.Length == 0)
							method = prop.GetGetMethod();
						else
							method = prop.GetSetMethod();
						break;
					}
				}
			}
			
			if (name == ".ctor") {
				var ctor = Type.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance, 
				                               null, types, null);
				
				if (ctor == null) {
					Console.WriteLine ("failed to find matching constructor!");
					return base.HandleDoesNotUnderstand(msg);
				}
				
				return STInstance.For(ctor.Invoke(native));
			}
			
			if (method == null) {
				method = Type.GetMethod(name, BindingFlags.Static | BindingFlags.Public, null, types, null);
			}
			
			if (method != null) {
				object result = method.Invoke(null, native);
				
				if (method.ReturnType == typeof(void))
					return msg.Receiver;
				
				return STInstance.For(result);
			}
			
			return base.HandleDoesNotUnderstand(msg);
			
#if false
			if (Type == null) 
				return base.HandleDidNotUnderstand(msg);
			
			Console.WriteLine ("Attempting to route message via .NET");
			// System Console writeLine: { 'thing1' . 'thing2' }
			// --> System.Console.WriteLine("thing1", "thing2");
			
			string name = msg.Selector.Name.Replace(":", "");
			name = char.ToUpper(name[0]) + name.Substring(1);
			
			STObject[] stobj = msg.Parameters;
			object[] native = new object[stobj.Length];
			Type[] types = new Type[stobj.Length];
			
			for (int i = 0, max = stobj.Length; i < max; ++i) {
				native[i] = stobj[i].Native;
				types[i] = native[i].GetType();
			}
			
			MethodInfo method = null;
			
			if (stobj.Length == 0) {
				var props = Type.GetProperties(BindingFlags.Public | BindingFlags.Static);
				foreach (var prop in props) {
					if (prop.Name == name) {
						method = prop.GetGetMethod();
						break;
					}
				}
			}
			
			if (method == null)
				method = Type.GetMethod(name, BindingFlags.Static, null, types, null);
			
			if (method != null) {
				object result = method.Invoke(null, native);
				
				if (result is STObject)
					return result as STObject;
				
				return new STInstance(result);
			}
			
			return base.HandleDidNotUnderstand(msg);
#endif			
		}

		static STClass metaclassInstance = null;
		
		public static STClass MetaclassInstance {
			get {
				if (metaclassInstance == null) {
					metaclassInstance = new STClass("Metaclass");
					metaclassInstance.Initialize();
					
					metaclassInstance.MethodDictionary[STSymbol.Get("asString")] =
						new STRuntimeMethod(STMetaclass.ToString);
					metaclassInstance.Class.MethodDictionary[STSymbol.Get("asString")] =
						new STRuntimeMethod(delegate(STMessage msg) {
							return (msg.Receiver as STClass).Name;
						});
					if (metaclassInstance.Class.Class != metaclassInstance)
						throw new Exception ("Metaclass STClass instance is broken!");
					
					metaclassInstance.Class.MethodDictionary[STSymbol.Get("inspect")] =
						new STRuntimeMethod(delegate(STMessage msg) {
							Console.WriteLine("Harm.");
							return STUndefinedObject.Instance;
						});
				}
				
				return metaclassInstance;
			}
		}
		
		static Dictionary<Type,STClass> clrClasses =
			new Dictionary<Type, STClass>();
		
		public static STClass GetForCLR(Type type, string branding)
		{
			STClass @class;
			if (!clrClasses.TryGetValue(type, out @class)) {
				@class = new STClass(type, branding);
				@class.Initialize();
				clrClasses[type] = @class;
			}
			
			return @class;
		}
		
		public Type Type { get; private set; }
		public STMetaclass Metaclass { get; private set; }
		public override STClassDescription Class {
			get {
				return Metaclass;
			}
		}
	}
}
