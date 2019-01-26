using SharpDemangler.Common;
using System.Text;

namespace SharpDemangler.Itanium
{
	internal class ClosureTypeName : Node
	{
		readonly NodeArray @params;
		readonly string count;

		public ClosureTypeName(NodeArray @params, string count) : base(ItaniumDemangleNodeType.ClosureTypeName) {
			this.@params = @params;
			this.count = count;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append("\'lambda");
			sb.Append(count);
			sb.Append("\'(");
			@params.PrintWithComma(sb);
			sb.Append(')');
		}
	}
}