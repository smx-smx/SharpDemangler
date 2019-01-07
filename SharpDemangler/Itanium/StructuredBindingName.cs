using System.Text;

namespace SharpDemangler.Itanium
{
	public class StructuredBindingName : Node
	{
		readonly NodeArray bindings;

		public StructuredBindingName(NodeArray bindings) : base(ItaniumDemangleNodeType.StructuredBindingName){
			this.bindings = bindings;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append('[');
			bindings.PrintWithComma(sb);
			sb.Append(']');
		}
	}
}