using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class NameType : Node
	{
		public readonly string Name;
		public string BaseName => Name;

		public NameType(string name) : base(ItaniumDemangleNodeType.NameType) {
			this.Name = name;
		}

		public override void PrintLeft(OutputStream sb) {
			sb.Append(Name);
		}
	}
}
