using SharpDemangler.Common;
using System.Text;

namespace SharpDemangler.Itanium
{
	internal class StdQualifiedName : Node
	{
		public readonly Node Child;

		public StdQualifiedName(Node child) : base(ItaniumDemangleNodeType.StdQualifiedName){
			this.Child = child;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append("std::");
			Child.Print(sb);
		}
	}
}