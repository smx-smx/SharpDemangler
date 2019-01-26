using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Microsoft
{
	public class DynamicStructorIdentifierNode : IdentifierNode
	{
		public VariableSymbolNode Variable;
		public QualifiedNameNode Name;
		public bool IsDestructor;

		public DynamicStructorIdentifierNode() : base(NodeKind.DynamicStructorIdentifier) {
		}
	}
}
