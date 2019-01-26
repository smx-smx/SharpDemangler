using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class ArrayType : Node
	{
		readonly Node baseNode;
		readonly NodeOrString dimension;

		public ArrayType(Node baseNode, NodeOrString dimension) : base(ItaniumDemangleNodeType.ArrayType, Cache.Yes, Cache.Yes) {
			this.baseNode = baseNode;
			this.dimension = dimension;
		}

		public override bool HasRHSComponent => true;
		public override bool HasArray => true;

		public override void PrintLeft(OutputStream sb) => baseNode.PrintLeft(sb);
	}
}
