using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpDemangler.Itanium
{
	internal class ReferenceType : Node
	{
		public readonly Node Pointee;
		readonly ReferenceKind rk;

		public ReferenceType(Node pointee, ReferenceKind rk) : base(ItaniumDemangleNodeType.ReferenceType, pointee.RHSComponentCache){
			this.Pointee = pointee;
			this.rk = rk;
		}

		private bool printing;

		private KeyValuePair<ReferenceKind, Node> Collapse(OutputStream sb) {
			var soFar = new KeyValuePair<ReferenceKind, Node>(rk, Pointee);
			for(; ; ) {
				Node sn = soFar.Value.SyntaxNode;
				if (sn.Kind != ItaniumDemangleNodeType.ReferenceType)
					break;
				ReferenceType rt = (ReferenceType)sn;

				soFar = new KeyValuePair<ReferenceKind, Node>(
					(ReferenceKind)Math.Min((int)soFar.Key, (int)rt.rk),
					rt.Pointee
				);
			}
			return soFar;
		}

		public override void PrintLeft(OutputStream sb) {
			if (printing)
				return;

			SwapAndRestore.Execute(ref printing, true, () => {
				KeyValuePair<ReferenceKind, Node> collapsed = Collapse(sb);
				collapsed.Value.PrintLeft(sb);
				if (collapsed.Value.HasArray)
					sb.Append(' ');
				if (collapsed.Value.HasArray || collapsed.Value.HasFunction)
					sb.Append('(');

				sb.Append((collapsed.Key == ReferenceKind.LValue) ? "&" : "&&");
			});
		}

		public override void PrintRight(OutputStream sb) {
			if (printing)
				return;

			SwapAndRestore.Execute(ref printing, true, () => {
				KeyValuePair<ReferenceKind, Node> collapsed = Collapse(sb);
				if (collapsed.Value.HasArray || collapsed.Value.HasFunction)
					sb.Append(')');
				collapsed.Value.PrintRight(sb);
			});
		}
	}
}