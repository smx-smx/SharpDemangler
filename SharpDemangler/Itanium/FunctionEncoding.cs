using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class FunctionEncoding : Node
	{
		public readonly Node ReturnType;
		public readonly Node Name;
		public readonly NodeArray Params;
		readonly Node attrs;
		public readonly Qualifiers CVQuals;
		public readonly FunctionRefQual RefQual;

		public FunctionEncoding(
			Node ret, Node name,
			NodeArray fparams, Node attrs,
			Qualifiers cvQuals, FunctionRefQual refQual
		) : base(ItaniumDemangleNodeType.FunctionEncoding, Cache.Yes, Cache.No, Cache.Yes) {
			this.ReturnType = ret;
			this.Name = name;
			this.Params = fparams;
			this.attrs = attrs;
			this.CVQuals = cvQuals;
			this.RefQual = refQual;
		}

		public override bool HasRHSComponent => true;
		public override bool HasFunction => true;

		public override void PrintLeft(OutputStream sb) {
			if (ReturnType != null) {
				ReturnType.PrintLeft(sb);
				if (!ReturnType.HasRHSComponent) {
					sb.Append(' ');
				}
			}
			Name.Print(sb);
		}

		public override void PrintRight(OutputStream sb) {
			sb.Append('(');
			Params.PrintWithComma(sb);
			sb.Append(')');
			if (ReturnType != null) {
				ReturnType.PrintRight(sb);
			}

			if (CVQuals.HasFlag(Qualifiers.Const)) {
				sb.Append(" const");
			}
			if (CVQuals.HasFlag(Qualifiers.Volatile)) {
				sb.Append(" volatile");
			}
			if (CVQuals.HasFlag(Qualifiers.Restrict)) {
				sb.Append(" restrict");
			}

			if (RefQual.HasFlag(FunctionRefQual.LValue)) {
				sb.Append(" &");
			} else if (RefQual.HasFlag(FunctionRefQual.RValue)){
				sb.Append(" &&");
			}

			if (attrs != null)
				attrs.Print(sb);
		}
	}
}
