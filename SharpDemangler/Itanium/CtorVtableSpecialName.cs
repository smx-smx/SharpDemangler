using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class CtorVtableSpecialName : Node
	{
		readonly Node firstType;
		readonly Node secondType;

		public CtorVtableSpecialName(Node firstType, Node secondType) : base(ItaniumDemangleNodeType.CtorVtableSpecialName) {
			this.firstType = firstType;
			this.secondType = secondType;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append("construction vtable for ");
			firstType.Print(sb);
			sb.Append("-in-");
			secondType.Print(sb);
		}
	}
}
