using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class QualifiedName : Node
	{
		private readonly Node qualifier;
		private readonly Node name;

		public QualifiedName(Node qualifier, Node name) : base(ItaniumDemangleNodeType.QualifiedName) {
			this.qualifier = qualifier;
			this.name = name;
		}

		public override void PrintLeft(OutputStream sb) {
			qualifier.Print(sb);
			sb.Append("::");
			name.Print(sb);
		}
	}
}
