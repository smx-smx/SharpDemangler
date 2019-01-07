using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class FunctionType : Node
	{
		Node ret;
		NodeArray fparams;
		Qualifiers cvQuals;
		FunctionRefQual refQual;
		Node exceptionSpec;

		public FunctionType(
			Node ret, NodeArray fparams,
			Qualifiers cvQuals, FunctionRefQual refQual,
			Node exceptionSpec
		) : base(ItaniumDemangleNodeType.FunctionType, Cache.Yes, Cache.No, Cache.Yes) {
			this.ret = ret;
			this.fparams = fparams;
			this.cvQuals = cvQuals;
			this.refQual = refQual;
			this.exceptionSpec = exceptionSpec;
		}

		public override bool HasRHSComponent => true;
		public override bool HasFunction => true;

		public override void PrintLeft(OutputStream sb) {
			ret.PrintLeft(sb);
			sb.Append(' ');
		}

		public override void PrintRight(OutputStream sb) {
			sb.Append('(');
			fparams.PrintWithComma(sb);
			sb.Append(')');

			ret.PrintRight(sb);

			if (cvQuals.HasFlag(Qualifiers.Const))
				sb.Append(" const");

			if (cvQuals.HasFlag(Qualifiers.Volatile))
				sb.Append(" volatile");

			if (cvQuals.HasFlag(Qualifiers.Restrict))
				sb.Append(" restrict");

			switch (refQual) {
				case FunctionRefQual.LValue:
					sb.Append(" &");
					break;
				case FunctionRefQual.RValue:
					sb.Append(" &&");
					break;
			}

			if(exceptionSpec != null) {
				sb.Append(' ');
				exceptionSpec.Print(sb);
			}
		}
	}
}
