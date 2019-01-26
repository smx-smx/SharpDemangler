using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class QualType : Node
	{
		readonly Qualifiers quals;
		readonly Node child;

		public QualType(Node child, Qualifiers quals) : base(ItaniumDemangleNodeType.QualType, child.RHSComponentCache, child.ArrayCache, child.FunctionCache) {
			this.child = child;
			this.quals = quals;
		}

		public void PrintQuals(OutputStream sb) {
			if (quals.HasFlag(Qualifiers.Const))
				sb.Append(" const");

			if (quals.HasFlag(Qualifiers.Volatile))
				sb.Append(" volatile");

			if (quals.HasFlag(Qualifiers.Restrict))
				sb.Append(" restrict");
		}

		public override bool HasArray => child.HasArray;
		public override bool HasFunction => child.HasFunction;
		public override bool HasRHSComponent => child.HasRHSComponent;

		public override void PrintLeft(OutputStream sb) {
			child.PrintLeft(sb);
			PrintQuals(sb);
		}

		public override void PrintRight(OutputStream sb) {
			child.PrintRight(sb);
		}
	}
}
