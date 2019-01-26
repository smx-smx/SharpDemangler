using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class DotSuffix : Node
	{
		readonly Node prefix;
		readonly string suffix;

		public DotSuffix(Node prefix, string suffix) : base(ItaniumDemangleNodeType.DotSuffix) {
			this.prefix = prefix;
			this.suffix = suffix;
		}

		public override void PrintLeft(OutputStream sb) {
			prefix.Print(sb);
			sb.Append(" (");
			sb.Append(suffix);
			sb.Append(')');
		}
	}
}
