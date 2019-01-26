using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class ConversionOperatorType : Node
	{
		readonly Node ty;

		public ConversionOperatorType(Node ty) : base(ItaniumDemangleNodeType.ConversionOperatorType) {
			this.ty = ty;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append("operator ");
			ty.Print(sb);
		}
	}
}
