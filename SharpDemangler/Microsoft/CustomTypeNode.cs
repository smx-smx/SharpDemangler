using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class CustomTypeNode : TypeNode
	{
		public IdentifierNode Identifier;

		public CustomTypeNode() : base(NodeKind.Custom) {
		}

		public override void OutputPre(OutputStream os, OutputFlags flags) {
			Identifier.Output(os, flags);
		}

		public override void OutputPost(OutputStream os, OutputFlags flags) {
		}
	}
}
