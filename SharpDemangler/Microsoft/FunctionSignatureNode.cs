using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class FunctionSignatureNode : TypeNode
	{
		public PointerAffinity Affinity = PointerAffinity.None;
		public CallingConv CallConvention = CallingConv.None;
		public FuncClass FunctionClass = FuncClass.Global;
		public FunctionRefQualifier RefQualifier = FunctionRefQualifier.None;
		public TypeNode ReturnType = null;
		public bool IsVariadic = false;
		public NodeArrayNode Params = null;

		public FunctionSignatureNode(NodeKind kind) : base(kind) {
		}

		public FunctionSignatureNode() : base(NodeKind.FunctionSignature) {
		}

		public override void OutputPre(OutputStream os, OutputFlags flags) {
			if (FunctionClass.HasFlag(FuncClass.Public))
				os.Append("public: ");
			if (FunctionClass.HasFlag(FuncClass.Protected))
				os.Append("protected: ");
			if (FunctionClass.HasFlag(FuncClass.Private))
				os.Append("private: ");

			if (!(FunctionClass.HasFlag(FuncClass.Global))) {
				if (FunctionClass.HasFlag(FuncClass.Static))
					os.Append("static ");
			}
			if (FunctionClass.HasFlag(FuncClass.Virtual))
				os.Append("virtual ");

			if (FunctionClass.HasFlag(FuncClass.ExternC))
				os.Append("extern \"C\" ");

			if(ReturnType != null) {
				ReturnType.Output(os, flags);
				os.Append(' ');
			}

			if (!(flags.HasFlag(OutputFlags.NoCallingConvention)))
				OutputCallingConvention(os, CallConvention);
		}

		public override void OutputPost(OutputStream os, OutputFlags flags) {
			if (!(FunctionClass.HasFlag(FuncClass.NoParameterList))) {
				os.Append('(');
				if (Params != null) {
					Params.Output(os, flags);
				} else {
					os.Append("void");
				}
				os.Append(')');
			}

			if (Quals.HasFlag(Qualifiers.Const))
				os.Append(" const");
			if (Quals.HasFlag(Qualifiers.Volatile))
				os.Append(" volatile");
			if (Quals.HasFlag(Qualifiers.Restrict))
				os.Append(" __restrict");
			if (Quals.HasFlag(Qualifiers.Unaligned))
				os.Append(" __unaligned");

			if (RefQualifier == FunctionRefQualifier.Reference)
				os.Append(" &");
			else if (RefQualifier == FunctionRefQualifier.RValueReference)
				os.Append(" &&");

			if(ReturnType != null) {
				ReturnType.OutputPost(os, flags);
			}
		}
	}
}
