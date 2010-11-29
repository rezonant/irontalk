
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	public static class ExtensionMethods {
		public static bool IsVowel(this char ch)
		{
			switch (char.ToLower(ch)) { 
			case 'a': case 'e': case 'i': case 'o': case 'u': 
				return true; 
			default:
				return false;
			}
		}
	}
}
