using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class SpecialSubstitution : Node
	{
		public readonly SpecialSubKind SSK;

		public SpecialSubstitution(SpecialSubKind ssk) : base(ItaniumDemangleNodeType.SpecialSubstitution) {
			this.SSK = ssk;
		}

		public override void PrintLeft(OutputStream sb) {
			switch (SSK) {
				case SpecialSubKind.allocator:
					sb.Append("std::allocator");
					break;
				case SpecialSubKind.basic_string:
					sb.Append("std::basic_string");
					break;
				case SpecialSubKind.@string:
					sb.Append("std::string");
					break;
				case SpecialSubKind.istream:
					sb.Append("std::istream");
					break;
				case SpecialSubKind.ostream:
					sb.Append("std::ostream");
					break;
				case SpecialSubKind.iostream:
					sb.Append("std::iostream");
					break;
			}
		}
	}
}
