using SharpDemangler.Common;
using System.Diagnostics;

namespace SharpDemangler.Microsoft
{
	public class PointerTypeNode : TypeNode
	{
		public PointerTypeNode() : base(NodeKind.PointerType) {
		}

		public PointerAffinity Affinity = PointerAffinity.None;
		public QualifiedNameNode ClassParent = null;
		public TypeNode Pointee = null;

		public override void OutputPre(OutputStream os, OutputFlags flags) {
			if(Pointee.Kind == NodeKind.FunctionSignature) {
				FunctionSignatureNode sig = (FunctionSignatureNode)Pointee;
				sig.OutputPre(os, OutputFlags.NoCallingConvention);
			} else {
				Pointee.OutputPre(os, flags);
			}

			OutputSpaceIfNecessary(os);

			if (Quals.HasFlag(Qualifiers.Unaligned))
				os.Append("__unaligned ");

			if(Pointee.Kind == NodeKind.ArrayType) {
				os.Append('(');
			} else if(Pointee.Kind == NodeKind.FunctionSignature) {
				os.Append('(');
				FunctionSignatureNode sig = (FunctionSignatureNode)Pointee;
				OutputCallingConvention(os, sig.CallConvention);
				os.Append(' ');
			}

			if(ClassParent != null) {
				ClassParent.Output(os, flags);
				os.Append("::");
			}

			switch (Affinity) {
				case PointerAffinity.Pointer:
					os.Append('*');
					break;
				case PointerAffinity.Reference:
					os.Append('&');
					break;
				case PointerAffinity.RValueReference:
					os.Append("&&");
					break;
				default:
					Debug.Assert(false);
					break;
			}

			OutputQualifiers(os, Quals, false, false);
		}

		public override void OutputPost(OutputStream os, OutputFlags flags) {
			if (Pointee.Kind == NodeKind.ArrayType || Pointee.Kind == NodeKind.FunctionSignature)
				os.Append(')');

			Pointee.OutputPost(os, flags);
		}
	}
}