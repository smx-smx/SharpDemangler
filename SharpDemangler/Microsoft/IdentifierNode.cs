using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class IdentifierNode : Node
	{
		public NodeArrayNode TemplateParams;
		public IdentifierNode(NodeKind kind) : base(kind) {
		}

		public void OutputTemplateParameters(OutputStream os, OutputFlags flags) {
			if (TemplateParams == null)
				return;
			os.Append('<');
			TemplateParams.Output(os, flags);
			os.Append('>');
		}
	}
}