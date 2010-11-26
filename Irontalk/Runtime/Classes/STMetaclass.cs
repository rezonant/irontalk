
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	public class STMetaclass : STClassDescription {
		public STMetaclass(STClass instance):
			base (instance.Name + " class")
		{
			Instance = instance;
		}
		
		public static string ToString(STMessage m)
		{
			return (m.Receiver as STMetaclass).Name;
		}
		
		public STClass Instance { get; private set; }
		
		public override STClassDescription Class {
			get { return STClass.MetaclassInstance; }
		}
	}
}
