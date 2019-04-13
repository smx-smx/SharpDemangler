using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class DynamicStructorIdentifierNode : IdentifierNode
	{
		public VariableSymbolNode Variable;
		public QualifiedNameNode Name;
		public bool IsDestructor;

		public DynamicStructorIdentifierNode() : base(NodeKind.DynamicStructorIdentifier) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			if (IsDestructor) {
				os.Append("`dynamic atexit destructor for ");
			} else {
				os.Append("`dynamic initializer for ");
			}

			if (Variable != null) {
				os.Append('`');
				Variable.Output(os, flags);
				os.Append("''");
			} else {
				os.Append("'");
				Name.Output(os, flags);
				os.Append("''");
			}
		}
	}
}
