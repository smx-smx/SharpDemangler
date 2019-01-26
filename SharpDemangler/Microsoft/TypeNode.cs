using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Microsoft
{
	public class TypeNode : Node
	{
		public Qualifiers Quals = Qualifiers.None;

		public TypeNode(NodeKind kind) : base(kind) {
		}

		public virtual void OutputPre(OutputStream os, OutputFlags flags) { }
		public virtual void OutputPost(OutputStream os, OutputFlags flags) { }

		public virtual void OutputQuals(bool spaceBefore, bool spaceAfter) { }

		public override void Output(OutputStream os, OutputFlags flags) {
			OutputPre(os, flags);
			OutputPost(os, flags);
		}
	}
}
