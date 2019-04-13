using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class TemplateParameterReferenceNode : Node
	{
		public SymbolNode Symbol;
		public int ThunkOffsetCount = 0;

		public long[] ThunkOffsets = new long[3];
		public bool IsMemberPointer = false;
		public PointerAffinity Affinity = PointerAffinity.None;

		public TemplateParameterReferenceNode() : base(NodeKind.TemplateParameterReference) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			if(ThunkOffsetCount > 0) {
				os.Append('{');
			} else if(Affinity == PointerAffinity.Pointer) {
				os.Append('&');
			}

			if(Symbol != null) {
				Symbol.Output(os, flags);
				if(ThunkOffsetCount > 0) {
					os.Append(", ");
				}
			}

			if(ThunkOffsetCount > 0) {
				os.Append(ThunkOffsets[0]);
			}

			for(int i=1; i<ThunkOffsetCount; i++) {
				os.Append(", ");
				os.Append(ThunkOffsets[i]);
			}

			if(ThunkOffsetCount > 0) {
				os.Append('}');
			}
		}
	}
}