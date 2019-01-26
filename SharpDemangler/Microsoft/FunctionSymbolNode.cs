using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class FunctionSymbolNode : SymbolNode
	{
		public FunctionSignatureNode Signature = null;

		public FunctionSymbolNode() : base(NodeKind.FunctionSymbol) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			Signature.OutputPre(os, flags);
			OutputSpaceIfNecessary(os);
			Name.Output(os, flags);
			Signature.OutputPost(os, flags);
		}
	}
}
