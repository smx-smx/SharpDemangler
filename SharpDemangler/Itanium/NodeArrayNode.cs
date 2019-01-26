using SharpDemangler.Common;
using SharpDemangler.Itanium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class NodeArrayNode : Node
	{
		readonly NodeArray array = new NodeArray();

		public NodeArrayNode(NodeArray array) : base(ItaniumDemangleNodeType.NodeArrayNode) {
			this.array = array;
		}

		public override void PrintLeft(OutputStream sb) => array.PrintWithComma(sb);
	}
}
