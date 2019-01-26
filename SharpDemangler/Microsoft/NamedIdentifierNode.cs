using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class NamedIdentifierNode : IdentifierNode
	{
		public StringView Name;

		public NamedIdentifierNode() : base(NodeKind.NamedIdentifier) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			os.Append(Name);
			OutputTemplateParameters(os, flags);
		}
	}
}