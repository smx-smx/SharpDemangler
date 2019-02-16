using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public struct ThisAdjustor
	{
		public uint StaticOffset ;
		public int VBPtrOffset;
		public int VBOffsetOffset;
		public int VtordispOffset;
	}

	public class ThunkSignatureNode : FunctionSignatureNode
	{
		public ThisAdjustor ThisAdjust = new ThisAdjustor() {
			StaticOffset = 0,
			VBOffsetOffset = 0,
			VBPtrOffset = 0,
			VtordispOffset = 0
		};

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