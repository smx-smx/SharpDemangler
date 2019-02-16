using SharpDemangler.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpDemangler.Microsoft
{
	public class NodeArrayNode : Node, IEnumerable<Node>
	{
		public Node[] Nodes = new Node[] { };

		public NodeArrayNode() : base(NodeKind.NodeArray) {
		}

		public Node this[int i] {
			get => Nodes[i];
			set => Nodes[i] = value;
		}

		IEnumerator IEnumerable.GetEnumerator() => Nodes.GetEnumerator();
		public IEnumerator<Node> GetEnumerator() => Nodes.AsEnumerable().GetEnumerator();

		public void Output(OutputStream os, OutputFlags flags, string separator) {
			if (Nodes.Length == 0)
				return;
			if (Nodes[0] != null)
				Nodes[0].Output(os, flags);

			for (int i=1; i<Nodes.Length; i++) {
				os.Append(separator);
				Nodes[i].Output(os, flags);
			}
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			Output(os, flags, ", ");
		}

	}
}