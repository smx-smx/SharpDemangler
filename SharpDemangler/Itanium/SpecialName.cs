using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class SpecialName : Node
	{
		readonly string special;
		readonly Node child;

		public SpecialName(string special, Node child) : base(ItaniumDemangleNodeType.SpecialName) {
			this.special = special;
			this.child = child;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append(special);
			child.Print(sb);
		}
	}
}
