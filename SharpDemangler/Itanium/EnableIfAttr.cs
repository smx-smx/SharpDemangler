using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class EnableIfAttr : Node
	{
		readonly NodeArray conditions;

		public EnableIfAttr(NodeArray conditions) : base(ItaniumDemangleNodeType.EnableIfAttr) {
			this.conditions = conditions;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append(" [enable_if:");
			conditions.PrintWithComma(sb);
			sb.Append(']');
		}
	}
}
