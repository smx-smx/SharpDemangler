using System.Text;

namespace SharpDemangler.Itanium
{
	internal class GlobalQualifiedName : Node
	{
		readonly Node child;

		public GlobalQualifiedName(Node child) : base(ItaniumDemangleNodeType.GlobalQualifiedName){
			this.child = child;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append("::");
			child.Print(sb);
		}
	}
}