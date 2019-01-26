using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class ExpandedSpecialSubstitution : Node
	{
		readonly SpecialSubKind SSK;

		public ExpandedSpecialSubstitution(SpecialSubKind ssk) : base(ItaniumDemangleNodeType.ExpandedSpecialSubstitution) {
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
					sb.Append("std::basic_string<char, std::char_traits<char>, std::allocator<char> >");
					break;
				case SpecialSubKind.istream:
					sb.Append("std::basic_istream<char, std::char_traits<char> >");
					break;
				case SpecialSubKind.ostream:
					sb.Append("std::basic_ostream<char, std::char_traits<char> >");
					break;
				case SpecialSubKind.iostream:
					sb.Append("std::basic_iostream<char, std::char_traits<char> >");
					break;
			}
		}
	}
}
