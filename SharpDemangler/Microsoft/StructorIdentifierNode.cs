using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class StructorIdentifierNode : IdentifierNode
	{
		public bool IsDestructor;
		public IdentifierNode Class = null;

		public StructorIdentifierNode() : base(NodeKind.StructorIdentifier) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			if (IsDestructor)
				os.Append('~');
			Class.Output(os, flags);
			OutputTemplateParameters(os, flags);
		}
	}
}