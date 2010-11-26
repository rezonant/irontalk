/**
 * © 2010 William Lahti. 
 */

using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;

namespace Irontalk
{
	public delegate STObject Builder<T> (T value);
	
	public class SingletonFactory<T> {
		public SingletonFactory(Builder<T> builder)
		{
			Builder = builder;
		}
		
		Dictionary<T,WeakReference> singletons =
			new Dictionary<T, WeakReference>();
		
		
		public Builder<T> Builder { get; private set; }
		
		public STObject this[T key] {
			get {
				WeakReference obj;
				if (!singletons.TryGetValue(key, out obj) || !obj.IsAlive) {
					obj = new WeakReference(Builder(key));
					singletons[key] = obj;
				}
				return obj.Target as STObject;
			}
		}
	}
	

}
