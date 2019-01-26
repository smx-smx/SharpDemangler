using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class SymbolNode : Node
	{
		public QualifiedNameNode Name;

		public SymbolNode(NodeKind kind) : base(kind) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			Name.Output(os, flags);
		}
	}
}