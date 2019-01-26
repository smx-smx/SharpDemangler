using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class NoexceptSpec : Node
	{
		readonly Node e;

		public NoexceptSpec(Node e) : base(ItaniumDemangleNodeType.NoexceptSpec) {
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append("noexcept(");
			e.Print(sb);
			sb.Append(')');
		}
	}
}
