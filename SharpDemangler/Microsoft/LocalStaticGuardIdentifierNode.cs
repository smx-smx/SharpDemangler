using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class LocalStaticGuardIdentifierNode : IdentifierNode
	{
		public uint ScopeIndex = 0;

		public LocalStaticGuardIdentifierNode() : base(NodeKind.LocalStaticGuardIdentifier) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			os.Append("`local static guard'");
			if(ScopeIndex > 0) {
				os.Append('{');
				os.Append(ScopeIndex);
				os.Append('}');
			}
		}
	}
}