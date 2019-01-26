using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class NameIdentifierNode : IdentifierNode
	{
		public StringView Name;

		public NameIdentifierNode() : base(NodeKind.NamedIdentifier) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			os.Append(Name);
			OutputTemplateParameters(os, flags);
		}
	}
}