using SharpDemangler.Common;

namespace SharpDemangler.Itanium
{
	internal class ParameterPackExpansion : Node
	{
		readonly Node child;

		public ParameterPackExpansion(Node child) : base(ItaniumDemangleNodeType.ParameterPackExpansion) {
			this.child = child;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.UsingParameterPack(int.MaxValue, int.MaxValue, () => {
				int oldLength = sb.Length;
				child.Print(sb);

				if(sb.CurrentPackMax == int.MaxValue) {
					sb.Append("...");
					return;
				}

				if(sb.CurrentPackMax == 0) {
					sb.Length = oldLength;
				}

				for(int i=1, e=sb.CurrentPackMax; i<e; i++) {
					sb.Append(", ");
					sb.CurrentPackIndex = i;
					child.Print(sb);
				}
			});
		}
	}
}