using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class NameWithTemplateArgs : Node
	{
		public readonly Node Name;
		readonly Node templateArgs;

		public NameWithTemplateArgs(Node name, Node templateArgs) : base(ItaniumDemangleNodeType.NameWithTemplateArgs) {
			this.Name = name;
			this.templateArgs = templateArgs;
		}

		public override void PrintLeft(OutputStream sb) {
			Name.Print(sb);
			templateArgs.Print(sb);
		}
	}
}
