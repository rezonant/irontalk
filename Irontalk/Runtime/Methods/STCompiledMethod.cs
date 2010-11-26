
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	public abstract class STCompiledMethod : STObject {
		public abstract STObject Invoke(STMessage message);
	}
}
