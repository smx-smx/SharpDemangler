using SharpDemangler.Common;
using System.Text;

namespace SharpDemangler.Itanium
{
	internal class DtorName : Node
	{
		readonly Node baseNode;

		public DtorName(Node baseNode) : base(ItaniumDemangleNodeType.DtorName) {
			this.baseNode = baseNode;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append('~');
			base.PrintLeft(sb);
		}
	}
}