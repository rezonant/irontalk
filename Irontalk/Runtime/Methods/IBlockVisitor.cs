
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	public interface IBlockVisitor {
		void VisitBlock (STBlock block);
		void VisitStatement (Node statement);
	}
}
