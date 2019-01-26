using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class PostfixQualifiedType : Node
	{
		readonly Node ty;
		readonly string postfix;

		public PostfixQualifiedType(Node ty, string postfix) : base(ItaniumDemangleNodeType.PostfixQualifiedType) {
			this.ty = ty;
			this.postfix = postfix;
		}

		public override void PrintLeft(OutputStream sb) {
			ty.PrintLeft(sb);
			sb.Append(postfix);
		}
	}
}
