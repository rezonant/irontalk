
using System;
using System.IO;
using System.Collections.Generic;
using PerCederberg.Grammatica.Runtime;
using System.Reflection;
namespace Irontalk
{
	public class STBlock : STRuntimeObject, IBlockVisitor {
		public STBlock(Node blockLiteral, Context context, Compiler compiler)
		{
			Compiler = compiler;
			OuterContext = context;
			Context = new LocalContext(context, true);
			
			BlockLiteral = blockLiteral;
			BlockArgumentNames = new string[0];
			LocalVariableNames = new string[0];
			
			int i = 1;
			
			Node blockParams = null;
			
			if (blockLiteral[i].Name == "block_params")
				blockParams = blockLiteral[i++];
			
			if (blockLiteral[i].Name == "sequence") {
				Sequence = blockLiteral[i++];
				
				if (Sequence[0].Name == "var_def") {
					List<string> varNames = new List<string>();
					var vardef = Sequence[0];
					for (int j = 1, max = vardef.Count; j < max; ++j) {
						if (vardef[j].Name == "VAR_DELIM") break;
						var varName = (vardef[j] as Token).Image;
						varNames.Add (varName);
						Context.Declare(varName);
					}
					
					LocalVariableNames = varNames.ToArray();
				}
			}
			
			if (blockParams != null) {
				var parms = blockParams;
				List<string> parmNames = new List<string>();
				for (int j = 0, max = parms.Count; j < max; ++j) {
					var child = parms[j];
					if (child.Name == "VAR_DELIM")
						break;
					var parmName = (parms[++j] as Token).Image;
					parmNames.Add (parmName);
					Context.Declare(parmName);
				}
				
				BlockArgumentNames = parmNames.ToArray();	
			}
		}
		
		public Compiler Compiler { get; private set; }
		public Context OuterContext { get; private set; }
		public Context Context { get; set; }
		public Node BlockLiteral { get; private set; }
		public Node Sequence { get; private set; }
		public MethodInfo Compiled { get; set; }
		public string[] BlockArgumentNames { get; private set; }
		public string[] LocalVariableNames { get; private set; }
		
		[STRuntimeMethod("compile")]
		public void Compile()
		{
			
		}
		
		void IBlockVisitor.VisitBlock (STBlock b) {}
		void IBlockVisitor.VisitStatement (Node statement)
		{
			Compiler.EvaluateStatement(statement, Context);
		}
		
		[STRuntimeMethod("asString")]
		public override string ToString()
		{
			return "a Block";
		}
		
		public STObject EvaluateWith(Context context)
		{
			if (BlockArgumentNames.Length != 0)
				throw new Exception("Incorrect number of arguments for block (expected " + BlockArgumentNames.Length + ")");
			
			if (Compiled != null)
				throw new NotImplementedException();
			
			var eval = new BlockEvaluator (this, Compiler, context);
			Visit (eval);
			return eval.Result;
		}
		
		[STRuntimeMethod("whileTrue:")]
		public STObject WhileTrue(STBlock aBlock)
		{
			STObject lastValue = this;
			
			while ((bool)Evaluate().Native)
				lastValue = aBlock.Evaluate();
			
			return lastValue;
		}
		
		[STRuntimeMethod("value")]
		public STObject Evaluate()
		{
			if (BlockArgumentNames.Length != 0)
				throw new Exception("Incorrect number of arguments for block (expected " + BlockArgumentNames.Length + ")");
			
			if (Compiled != null)
				throw new NotImplementedException();
			
			var eval = new BlockEvaluator (this, Compiler, this.Context);
			Visit (eval);
			return eval.Result;
		}
		
		[STRuntimeMethod("value:")]
		public STObject Evaluate(STObject value)
		{
			Console.WriteLine("invoking block");
			if (BlockArgumentNames.Length != 1)
				throw new Exception("Incorrect number of arguments for block (expected " + BlockArgumentNames.Length + ")");
			
			Context.SetVariable(BlockArgumentNames[0], value);
			
			if (Compiled != null)
				throw new NotImplementedException();
			
			var eval = new BlockEvaluator (this, Compiler, this.Context);
			Visit (eval);
			return eval.Result;
		}
		
		[STRuntimeMethod("value:value:")]
		public STObject Evaluate(STObject value, STObject value2)
		{
			if (BlockArgumentNames.Length != 2)
				throw new Exception("Incorrect number of arguments for block (expected " + BlockArgumentNames.Length + ")");
			
			Context.SetVariable(BlockArgumentNames[0], value);
			Context.SetVariable(BlockArgumentNames[1], value2);
			
			if (Compiled != null)
				throw new NotImplementedException();
			
			var eval = new BlockEvaluator (this, Compiler, this.Context);
			Visit (eval);
			return eval.Result;
		}
		
		public void Visit (IBlockVisitor visitor)
		{
			visitor.VisitBlock (this);
			int i = 0;
			
			if (Sequence == null) return;
			
			if (Sequence[i].Name == "var_def") 
				i += 1;
			
			for (int max = Sequence.Count; i < max; ++i)
				visitor.VisitStatement(Sequence[i]);
		}
	}
}
