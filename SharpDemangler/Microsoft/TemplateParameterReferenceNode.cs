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
	}
}