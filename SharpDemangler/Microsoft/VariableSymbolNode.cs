using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class VariableSymbolNode : SymbolNode
	{
		public StorageClass sc = StorageClass.None;
		public TypeNode Type = null;

		public VariableSymbolNode() : base(NodeKind.VariableSymbol) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			switch (sc) {
				case StorageClass.PrivateStatic:
					os.Append("private: static ");
					break;
				case StorageClass.PublicStatic:
					os.Append("public: static ");
					break;
				case StorageClass.ProtectedStatic:
					os.Append("protected: static ");
					break;
				default:
					break;
			}

			if(Type != null) {
				Type.OutputPre(os, flags);
				OutputSpaceIfNecessary(os);
			}

			Name.Output(os, flags);

			if (Type != null)
				Type.OutputPost(os, flags);
		}
	}
}