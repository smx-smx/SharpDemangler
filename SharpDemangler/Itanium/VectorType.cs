using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class VectorType : Node
	{
		readonly Node baseType;
		readonly NodeOrString dimension;

		public VectorType(Node baseType, NodeOrString dimension) : base(ItaniumDemangleNodeType.VectorType) {
			this.baseType = baseType;
			this.dimension = dimension;
		}

		public override void PrintLeft(OutputStream sb) {
			baseType.Print(sb);
			sb.Append(" vector[");
			if (dimension.IsNode) {
				dimension.AsNode().Print(sb);
			} else if (dimension.IsString) {
				sb.Append(dimension.AsString());
			}
			sb.Append(']');
		}
	}
}
