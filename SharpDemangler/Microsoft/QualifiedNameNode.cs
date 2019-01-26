using SharpDemangler.Common;
using System.Linq;

namespace SharpDemangler.Microsoft
{
	public class QualifiedNameNode : Node
	{
		public NodeArrayNode Components;

		public QualifiedNameNode() : base(NodeKind.QualifiedName) {
		}

		public IdentifierNode UnqualifiedIdentifier => (IdentifierNode)Components[Components.Count() - 1];

		public override void Output(OutputStream os, OutputFlags flags) {
			Components.Output(os, flags, "::");
		}
	}
}