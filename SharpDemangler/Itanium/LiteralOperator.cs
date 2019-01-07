using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class LiteralOperator : Node
	{
		readonly Node opName;

		public LiteralOperator(Node opName) : base(ItaniumDemangleNodeType.LiteralOperator) {
			this.opName = opName;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append("operator\"\" ");
			opName.Print(sb);
		}

	}
}
