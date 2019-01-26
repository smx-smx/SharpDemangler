using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class LiteralOperatorIdentifierNode : IdentifierNode
	{
		public StringView Name;

		public LiteralOperatorIdentifierNode() : base(NodeKind.LiteralOperatorIdentifier) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			os.Append(Name);
			OutputTemplateParameters(os, flags);
		}
	}
}