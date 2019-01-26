using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class RttiBaseClassDescriptorNode : IdentifierNode
	{
		public uint NVOffset = 0;
		public int VBPtrOffset = 0;
		public uint VBTableOffset = 0;
		public uint Flags = 0;

		public RttiBaseClassDescriptorNode() : base(NodeKind.RttiBaseClassDescriptor) {
		}


		public override void Output(OutputStream os, OutputFlags flags) {
			os.Append("`RTTI Base Class Descriptor at (");
			os.Append(NVOffset);
			os.Append(", ");
			os.Append(VBPtrOffset);
			os.Append(", ");
			os.Append(VBTableOffset);
			os.Append(", ");
			os.Append(Flags);
			os.Append(")'");
		}

	}
}
