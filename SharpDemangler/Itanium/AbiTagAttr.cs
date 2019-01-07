using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class AbiTagAttr : Node
	{
		public readonly Node BaseNode;
		public readonly string Tag;

		public AbiTagAttr(Node baseNode, string tag) : base(ItaniumDemangleNodeType.AbiTagAttr, baseNode.RHSComponentCache, baseNode.ArrayCache, baseNode.FunctionCache) {
			this.BaseNode = baseNode;
			this.Tag = tag;
		}

		public override void PrintLeft(OutputStream sb) {
			BaseNode.PrintLeft(sb);
			sb.Append($"[abi:{Tag}]");
		}
	}
}
