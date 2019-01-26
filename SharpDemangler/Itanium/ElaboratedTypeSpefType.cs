using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class ElaboratedTypeSpefType : Node
	{
		readonly string kind;
		readonly Node child;

		public ElaboratedTypeSpefType(string kind, Node child) : base(ItaniumDemangleNodeType.ElaboratedTypeSpefType) {
			this.kind = kind;
			this.child = child;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append(kind);
			sb.Append(' ');
			child.Print(sb);
		} 

	}
}
