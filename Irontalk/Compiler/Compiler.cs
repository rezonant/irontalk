// 
//  Author:
//       William Lahti <wilahti@gmail.com>
// 
//  Copyright © 2010 William Lahti
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  As a special exception, the copyright holders of this library give
//  you permission to link this library with independent modules to
//  produce an executable, regardless of the license terms of these 
//  independent modules, and to copy and distribute the resulting 
//  executable under terms of your choice, provided that you also meet,
//  for each linked independent module, the terms and conditions of the
//  license of that module. An independent module is a module which is
//  not derived from or based on this library. If you modify this library, you
//  may extend this exception to your version of the library, but you are
//  not obligated to do so. If you do not wish to do so, delete this
//  exception statement from your version. 
// 
//  This program is distributed in the hope that it will be useful, 
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
// 
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.


using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
using System.Text;

namespace Irontalk {
	public interface IParseTreeVisitor {
		void VisitSequence(Node sequence);
		void VisitVariableDefinition (Node vardef);
		void VisitStatement (Node statement);
		void VisitExpression (Node expression);
		void VisitReceiver (Node receiver);
		void VisitMessage (Node message);
		void VisitUnarySend (Node unarySend);
		void VisitBinarySend (Node binarySend);
		void VisitKeywordSend (Node keywordSend);
	}
	
	public abstract class ParseTreeVisitor {
		public virtual void VisitVariableDefinition (Node vardef) {}
		public virtual void VisitStatement (Node statement) {}
		public virtual void VisitExpression (Node expression) {}
		public virtual void VisitReceiver (Node receiver) {}
		public virtual void VisitMessage (Node message) {}
		public virtual void VisitUnarySend (Node unarySend) {}
		public virtual void VisitBinarySend (Node binarySend) {}
		public virtual void VisitKeywordSend (Node keywordSend) {}
	}
	
	public class DigitValueOutOfRangeException : Exception {
		public DigitValueOutOfRangeException(char digit, int @base):
				base(string.Format(ErrorMessage, digit, @base))
		{
			Digit = digit;
			Radix = @base;
		}
		
		const string ErrorMessage = "The digit {0} is invalid for radix {1}";
		
		public char Digit;
		public int Radix;
	}
	
	public static class ParseTreeVisiting {
		public static void Visit (this Node node, IParseTreeVisitor visitor)
		{
			int i = 0;
			switch (node.Name) {
				case "sequence": 
					visitor.VisitSequence(node); 
					if (node[i].Name == "var_def") {
						visitor.VisitSequence(node);
						++i;
					}
				
					for (int max = node.Count; i < max; ++i)
						node[i].Visit(visitor);
				break;
				case "var_def": 
					visitor.VisitVariableDefinition(node); 
				break;
			}
		}
	}
	
	/// <summary>
	/// Implements the interpreter (immediate) mode of Irontalk, which also acts as the compiler (since compiling is 
	/// just message passing done at compile time).
	/// </summary>
	public class Compiler {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="assembly">The <see cref="Assembly"/> to install compiled classes into.</param>
		public Compiler (Assembly assembly)
		{
			Assembly = assembly;
			Parser = new IrontalkParser(new StringReader(""));
			SmalltalkImage.Initialize();
		}
		
		public Compiler():
			this (Assembly.GetCallingAssembly())
		{
		}
		
		Assembly Assembly { get; set; }
		IrontalkParser Parser { get; set; }
		InputSource Source { get; set; }
		
		public int GetDigitValue (char literal)
		{
			if (char.IsNumber(literal))
				return int.Parse(literal.ToString());	
			if (char.IsLetter(literal)) {
				if (char.IsUpper(literal))
					return 10 + (int)(literal - 'A');
				else if (char.IsLower(literal))
					return 10 + (int)(literal - 'a');
			}
			
			throw new InvalidOperationException("The digit character to convert must be in range 0-9 or A-F (case insensitive)");
		}
		
		public int GetDigitValue (char literal, int maxValue)
		{
			int value = GetDigitValue (literal);
			if (value >= maxValue) throw new DigitValueOutOfRangeException(literal, maxValue);
			return value;
		}
		
		public STObject GetNumberLiteral (Token literal)
		{
			try {
				string input = literal.Image;
				int rloc = input.IndexOf("r");
				
				if (rloc > 0) {
					// Radix numbers
					int sign = 1;
					
					if (input.StartsWith("-")) {
						sign *= -1;
						input = input.Substring(1);
						--rloc;
					}
						
					int @base = int.Parse(input.Substring(0, rloc));
					input = input.Substring(rloc + 1);
					
					if (input.StartsWith("-")) {
						sign *= -1;	
					}
					
					string[] parts = input.Split('.');
					long wholePart = 0;
					string wholePartStr = parts[0];
					
					for (int i = wholePartStr.Length - 1, power = 0; i >= 0; --i, ++power)
						wholePart += GetDigitValue(wholePartStr[i], @base) * (int)Math.Pow(@base, power);
					
					if (parts.Length > 1) {
						// has decimal point
						double dec = wholePart;
						string decStr = parts[1];
						
						for (int i = 0, max = decStr.Length; i < max; ++i)
							dec += GetDigitValue(decStr[i], @base) * Math.Pow(@base, -(i + 1));
						
						return new STInstance(dec * sign);
					}
					
					return new STInstance(wholePart * sign);
				}
				
				if (input.Contains("."))
					return STInstance.For(double.Parse(literal.Image));
				else
					return STInstance.For(long.Parse(literal.Image));		
			} catch (Exception e) {
				throw new CompileException(Source, literal.StartLine, "Error parsing numeric literal: " + e.Message, e);	
			}
		}
		
		public STObject GetStringLiteral (Token literal)
		{
			return STInstance.For(literal.Image.Substring(1, literal.Image.Length - 2).Replace("''", "'"));
		}
		
		public STObject GetCharLiteral (Token literal)
		{
			return STInstance.For(literal.Image[1]);
		}
		
		public STObject GetSymbolLiteral (Node literal)
		{
			// symbol = '#' ( IDENT | BINARY | KEYWORD+ ) ;
			var sb = new StringBuilder();
			for (int i = 1, max = literal.Count; i < max; ++i)
				sb.Append((literal[i] as Token).Image);
			return STSymbol.Get(sb.ToString());
		}
		
		public STObject GetArrayLiteral(Node literal, Context context)
		{
			List<object> items = new List<object>();
			for (int i = 1, max = literal.Count; i < max; i += 2) {
				var expr = literal[i];
				if (expr.Name != "expression") break;
				items.Add(EvaluateExpression(expr, context));
			}
			
			return new Irontalk.Array(items.ToArray());
		}
		 
		public STObject GetWordArrayLiteral(Node literal)
		{
			// word_array = '#' '(' 
			//	 ( IDENT | STRING | NUM | CHAR | '#' ( IDENT | word_array ) ) 
			// ')' ;	
			
			List<object> values = new List<object>();
			for (int i = 2, max = literal.GetChildCount() - 1; i < max; ++i) {
				Node child = literal.GetChildAt(i);
				
				if (child.Name == "IDENT")
					values.Add (STSymbol.Get((child as Token).Image));
				else if (child.Name == "STRING")
					values.Add (GetStringLiteral(child as Token));
				else if (child.Name == "NUM")
					values.Add (GetNumberLiteral(child as Token));
				else if (child.Name == "CHAR")
					values.Add (GetCharLiteral(child as Token));
				else if (child.Name == "HASH") {
					child = literal.GetChildAt(++i);
					if (child.Name == "IDENT")
						values.Add (STSymbol.Get((child as Token).Image));
					else if (child.Name == "word_array")
						values.Add (GetWordArrayLiteral(child));
					else
						throw new Exception ("Unhandled path in grammar");
				} else
					throw new Exception ("Unhandled path in grammar");
			}
			
			return new Irontalk.Array(values.ToArray());
		}
		
		public STObject EvaluateReceiver (Node receiver, Context context)
		{
			Node child = receiver.GetChildAt(0);
			if (child.Name == "IDENT") {
				return new STVariable((receiver[0] as Token).Image, context);
				//return context.GetVariable((receiver.GetChildAt(0) as Token).Image);
			} else if (child.Name == "NUM") {
				return GetNumberLiteral(child as Token);
			} else if (child.Name == "CHAR") {
				return GetCharLiteral(child as Token);
			} else if (child.Name == "symbol") {
				return GetSymbolLiteral(child);
			} else if (child.Name == "STRING") {
				return GetStringLiteral(child as Token);
			} else if (child.Name == "word_array") {
				return GetWordArrayLiteral(child);
			} else if (child.Name == "LEFT_PAREN") {
				return EvaluateExpression(receiver.GetChildAt(1), context);
			} else if (child.Name == "block") {
				return new STBlock (child, context, this);
			} else if (child.Name == "array") {
				return GetArrayLiteral(child, context);
			}
			
			return STUndefinedObject.Instance;
		}
		
		public STObject EvaluateUnarySend (STObject receiver, Node unarySend, Context context)
		{
			receiver = receiver.Dereference();
			return receiver.Send(STSymbol.Get((unarySend.GetChildAt(0) as Token).Image));
		}
		
		public STObject EvaluateAssignSend (STObject receiver, Node assignSend, Context context)
		{
			STObject other = EvaluateExpression(assignSend.GetChildAt(1), context).Dereference();
			
			var variable = receiver as STVariable;
			
			if (variable == null) 
				throw new Exception ("Error: can only assign to a valid variable lvalue");
			
			variable.Set(other);
			return other;
		}
		
		public STObject EvaluateBinarySend (STObject receiver, Node binarySend, Context context)
		{
			STObject other = EvaluateReceiver(binarySend.GetChildAt(1), context).Dereference();
			receiver = receiver.Dereference();
			
			for (int i = 2, max = binarySend.GetChildCount(); i < max; ++i)
				other = EvaluateUnarySend(other, binarySend.GetChildAt(i), context);
			return receiver.Send(STSymbol.Get((binarySend.GetChildAt(0) as Token).Image), other);
		}
		
		public STObject EvaluateSimpleSend(STObject receiver, Node simpleSend, Context context)
		{	
			var child = simpleSend.GetChildAt(0);
			if (child.Name == "unary_send")
				return EvaluateUnarySend(receiver, child, context);
			else if (child.Name == "binary_send")
				return EvaluateBinarySend(receiver, child, context);
			else
				throw new NotImplementedException("Unhandled grammar production within 'simple_send'");
		}
		
		public STObject EvaluateKeywordSend (STObject receiver, Node keywordSend, Context context)
		{
			receiver = receiver.Dereference();
			// keyword_send		= ( KEYWORD receiver simple_send* )+;
			string keyword = null;
			STObject parm = null;
			StringBuilder selectorBuffer = new StringBuilder();
			List<STObject> parms = new List<STObject>();
			
			for (int i = 0, max = keywordSend.GetChildCount(); i < max; ++i) {
				var item = keywordSend.GetChildAt(i);
				if (item.Name == "KEYWORD") {
					if (keyword != null) {
						selectorBuffer.Append(keyword);
						parms.Add (parm);
					}
					
					keyword = (item as Token).Image;
					parm = EvaluateReceiver(keywordSend.GetChildAt(++i), context).Dereference();
				} else if (item.Name == "simple_send") {
					parm = EvaluateSimpleSend(parm, item, context);
				}
			}
			
			selectorBuffer.Append(keyword);
			parms.Add (parm);
			STSymbol selector = STSymbol.Get(selectorBuffer.ToString());
			
			return receiver.Send(selector, parms.ToArray());
		}
		
		public STObject EvaluateSend (STObject receiver, Node message, Context context)
		{
			Node child = message.GetChildAt(0);
			
			if (child.Name == "simple_send")
				return EvaluateSimpleSend(receiver, child, context);
			else if (child.Name == "keyword_send")
				return EvaluateKeywordSend(receiver, child, context);
			else if (child.Name == "assign_send")
				return EvaluateAssignSend(receiver, child, context);
			throw new Exception("Should not reach");
		}
		
		public STObject EvaluateExpression (Node expression, Context context)
		{
			STObject receiver = EvaluateReceiver (expression.GetChildAt(0), context);
			
			for (int i = 1, max = expression.GetChildCount(); i < max; ++i) {
				receiver = EvaluateSend(receiver, expression.GetChildAt(i), context);
			}
			
			receiver = receiver.Dereference();
			
			return receiver;
		}
		
		public STObject Evaluate (string text)
		{
			return Evaluate (text, new LocalContext());
		}
		
		public STObject EvaluateStatement (Node statement, Context context)
		{
			if (statement.GetChildAt(0).Name != "expression")
				return STUndefinedObject.Instance;
			
			return EvaluateExpression(statement.GetChildAt(0), context);
		}
		
		public STObject Evaluate (Node sequence, Context context)
		{
			int start = 0;
			
			if (sequence.GetChildAt(start).Name == "var_def")
				++start;
			
			STObject last = STUndefinedObject.Instance;
			
			for (int i = start, max = sequence.GetChildCount(); i < max; ++i)
				last = EvaluateStatement (sequence.GetChildAt(i), context);
			
			return last;
		}
		
		public STObject Evaluate (InputSource source, Context context)
		{
			Source = source;
			try {
				Node root = source.Parse(Parser);
				
				if (root == null)
					return STUndefinedObject.Instance; // parser found empty string so aborted
				
				if (STDebug.ShowParseTrees) {
					var tr = Transcript.Instance;
					if (tr != null)
						root.PrintTo(tr.Out);
					else
						root.PrintTo(Console.Out);
				}
				
				return Evaluate (root, context);
			} finally {
				Source = null;	
			}
		}
		
		public STObject Evaluate (string str, Context context)
		{
			return Evaluate (new EvalSource (str), context);
		}
	}
}
