using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class DynamicExceptionSpec : Node
	{
		readonly NodeArray types;

		public DynamicExceptionSpec(NodeArray types) : base(ItaniumDemangleNodeType.DynamicExceptionSpec) {
			this.types = types;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append("throw(");
			types.PrintWithComma(sb);
			sb.Append(')');
		}
	}
}
