using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class NestedName : Node
	{
		public readonly Node Qual;
		public readonly Node Name;

		public NestedName(Node qual, Node name) : base(ItaniumDemangleNodeType.NestedName) {
			this.Qual = qual;
			this.Name = name;
		}

		public override void PrintLeft(OutputStream sb) {
			Qual.Print(sb);
			sb.Append("::");
			Name.Print(sb);
		}
	}
}
