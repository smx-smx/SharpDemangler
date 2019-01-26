using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class LocalStaticGuardVariableNode : SymbolNode
	{
		public bool IsVisible = false;

		public LocalStaticGuardVariableNode() : base(NodeKind.LocalStaticGuardVariable) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			Name.Output(os, flags);
		}
	}
}
