using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class ConversionOperatorIdentifierNode : IdentifierNode
	{
		public TypeNode TargetType = null;
		public ConversionOperatorIdentifierNode() : base(NodeKind.ConversionOperatorIdentifier) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			os.Append("operator");
			OutputTemplateParameters(os, flags);
			os.Append(' ');
			TargetType.Output(os, flags);
		}
	}
}