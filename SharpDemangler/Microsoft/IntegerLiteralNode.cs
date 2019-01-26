using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class IntegerLiteralNode : Node
	{
		public ulong Value;
		bool IsNegative = false;

		public IntegerLiteralNode(ulong value, bool isNegative) : base(NodeKind.IntegerLiteral) {
			this.Value = value;
			this.IsNegative = IsNegative;
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			if (IsNegative)
				os.Append('-');
			os.Append(Value);
		}
	}
}
