
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
using System.Text;
namespace Irontalk
{
	/// <summary>
	/// 
	/// </summary>
	public class Compiler {
		/// <summary>
		/// 
		/// </summary>
		/// <param name="assembly">The <see cref="Assembly"/> to install compiled classes into.</param>
		public Compiler (Assembly assembly)
		{
			Assembly = assembly;
			Smalltalk.Initialize();
		}
		
		public Compiler():
			this (Assembly.GetCallingAssembly())
		{
		}
		
		Assembly Assembly { get; set; }
		public STObject GetNumberLiteral (Token literal)
		{
			if (literal.Image.Contains("."))
				return STInstance.For(double.Parse(literal.Image));
			else
				return STInstance.For(long.Parse(literal.Image));
		}
		
		public STObject GetStringLiteral (Token literal)
		{
			return STInstance.For(literal.Image.Substring(1, literal.Image.Length - 2));
		}
		
		public STObject GetCharLiteral (Token literal)
		{
			return STInstance.For(literal.Image[1]);
		}
		
		public STObject GetSymbolLiteral (Node literal)
		{
			// symbol_literal = '#' ( BINARY | SELECTOR ) ;
			return STSymbol.Get((literal.GetChildAt(1) as Token).Image);
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
			// word_array_literal = '#' '(' 
			//	 ( IDENT | STRING | NUM_LITERAL | CHAR_LITERAL | '#' ( IDENT | word_array_literal ) ) 
			// ')' ;	
			
			List<object> values = new List<object>();
			for (int i = 2, max = literal.GetChildCount() - 1; i < max; ++i) {
				Node child = literal.GetChildAt(i);
				
				if (child.Name == "IDENT")
					values.Add (STSymbol.Get((child as Token).Image));
				else if (child.Name == "STRING")
					values.Add (GetStringLiteral(child as Token));
				else if (child.Name == "NUM_LITERAL")
					values.Add (GetNumberLiteral(child as Token));
				else if (child.Name == "CHAR_LITERAL")
					values.Add (GetCharLiteral(child as Token));
				else if (child.Name == "HASH") {
					child = literal.GetChildAt(++i);
					if (child.Name == "IDENT")
						values.Add (STSymbol.Get((child as Token).Image));
					else if (child.Name == "word_array_literal")
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
			} else if (child.Name == "NUM_LITERAL") {
				return GetNumberLiteral(child as Token);
			} else if (child.Name == "CHAR_LITERAL") {
				return GetCharLiteral(child as Token);
			} else if (child.Name == "symbol_literal") {
				return GetSymbolLiteral(child);
			} else if (child.Name == "STRING") {
				return GetStringLiteral(child as Token);
			} else if (child.Name == "word_array_literal") {
				return GetWordArrayLiteral(child);
			} else if (child.Name == "LEFT_PAREN") {
				return EvaluateExpression(receiver.GetChildAt(1), context);
			} else if (child.Name == "block_literal") {
				return new STBlock (child, context, this);
			} else if (child.Name == "array_literal") {
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
					parm = EvaluateReceiver(keywordSend.GetChildAt(++i), context);
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
		
		public STObject EvaluateStatement (Node statement, Context context)
		{
			if (statement.GetChildAt(0).Name != "expression")
				return STUndefinedObject.Instance;
			
			return EvaluateExpression(statement.GetChildAt(0), context);
		}
		
		
		public STObject Evaluate (string str, Context context)
		{
			str = str.Trim('\n', ' ', '\t');
			
			if (str == string.Empty)
				return STUndefinedObject.Instance;
			
			var tr = new StringReader (str);
			var parser = new SmalltalkParser(tr);
			var node = parser.Parse();
			
			if (STDebug.ParseTree)
				node.PrintTo(Console.Out);
			
			return Evaluate (node, context);
		}
	}
}
