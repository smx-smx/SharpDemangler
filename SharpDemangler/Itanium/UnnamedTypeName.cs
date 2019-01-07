using System.Text;

namespace SharpDemangler.Itanium
{
	internal class UnnamedTypeName : Node
	{
		readonly string count;

		public UnnamedTypeName(string count) : base(ItaniumDemangleNodeType.UnnamedTypeName) {
			this.count = count;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append("'unnamed");
			sb.Append(count);
			sb.Append("\'");
		}
	}
}