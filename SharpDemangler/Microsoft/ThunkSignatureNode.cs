using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class ThisAdjustor
	{
		public uint StaticOffset = 0;
		public int VBPtrOffset = 0;
		public int VBOffsetOffset = 0;
		public int VtordispOffset = 0;
	}

	public class ThunkSignatureNode : FunctionSignatureNode
	{
		public ThisAdjustor ThisAdjust;

		public ThunkSignatureNode() : base(NodeKind.ThunkSignature) {
		}

		public override void OutputPre(OutputStream os, OutputFlags flags) {
			os.Append("[thunk]: ");
			base.OutputPre(os, flags);
		}

		public override void OutputPost(OutputStream os, OutputFlags flags) {
			if (FunctionClass.HasFlag(FuncClass.StaticThisAdjust)) {
				os.Append("`adjustor{");
				os.Append(ThisAdjust.StaticOffset);
				os.Append("}'");
			} else if (FunctionClass.HasFlag(FuncClass.VirtualThisAdjust)) {
				if (FunctionClass.HasFlag(FuncClass.VirtualThisAdjustEx)) {
					os.Append("`vtordispex{");
					os.Append(ThisAdjust.VBPtrOffset);
					os.Append(", ");

					os.Append(ThisAdjust.VBOffsetOffset);
					os.Append(", ");
					os.Append(ThisAdjust.VtordispOffset);

					os.Append(", ");
					os.Append(ThisAdjust.StaticOffset);
					os.Append("}'");
				} else {
					os.Append("`vtordisp{");
					os.Append(ThisAdjust.VtordispOffset);
					os.Append(", ");
					os.Append(ThisAdjust.StaticOffset);
					os.Append("}'");
				}
			}

			base.OutputPost(os, flags);
		}
	}
}