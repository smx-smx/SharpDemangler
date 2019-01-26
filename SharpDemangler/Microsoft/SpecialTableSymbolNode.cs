using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class SpecialTableSymbolNode : SymbolNode
	{
		public Qualifiers Quals;
		public QualifiedNameNode TargetName = null;

		public SpecialTableSymbolNode() : base(NodeKind.SpecialTableSymbol) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			OutputQualifiers(os, Quals, false, true);
			Name.Output(os, flags);
			if(TargetName != null) {
				os.Append("{for `");
				TargetName.Output(os, flags);
				os.Append("'}");
			}
		}
	}
}
