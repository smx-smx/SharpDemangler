using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class PixelVectorType : Node
	{
		readonly NodeOrString dimension;

		public PixelVectorType(NodeOrString dimension) : base(ItaniumDemangleNodeType.PixelVectorType) {
			this.dimension = dimension;
		}

		public override void PrintLeft(OutputStream sb) {
			// FIXME: This should demangle as "vector pixel".
			sb.Append("pixel vector[");
			sb.Append(dimension.AsString());
			sb.Append(']');
		}
	}
}
