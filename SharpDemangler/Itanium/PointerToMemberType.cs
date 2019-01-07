using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class PointerToMemberType : Node
	{
		public readonly Node ClassType;
		public readonly Node MemberType;

		public PointerToMemberType(Node classType, Node memberType) : base(ItaniumDemangleNodeType.PointerToMemberType, memberType.RHSComponentCache) {
			this.ClassType = classType;
			this.MemberType = memberType;
		}

		public override bool HasRHSComponent => MemberType.HasRHSComponent;

		public override void PrintLeft(OutputStream sb) {
			MemberType.PrintLeft(sb);
			if(MemberType.HasArray || MemberType.HasFunction) {
				sb.Append('(');
			} else {
				sb.Append(' ');
			}
			ClassType.Print(sb);
			sb.Append("::*");
		}

		public override void PrintRight(OutputStream sb) {
			if (MemberType.HasArray || MemberType.HasFunction)
				sb.Append(')');
			MemberType.PrintRight(sb);
		}
	}
}
