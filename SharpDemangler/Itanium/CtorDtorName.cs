using SharpDemangler.Common;
using System.Text;

namespace SharpDemangler.Itanium
{
	internal class CtorDtorName : Node
	{
		readonly Node baseName;
		readonly bool isDtor;
		readonly int variant;

		public CtorDtorName(Node soFar, bool v, int variant) : base(ItaniumDemangleNodeType.CtorDtorName){
			this.baseName = soFar;
			this.isDtor = v;
			this.variant = variant;
		}

		public override void PrintLeft(OutputStream sb) {
			if (isDtor)
				sb.Append('~');
			sb.Append(baseName.BaseName);
		}
	}
}