using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class TagTypeNode : TypeNode
	{
		public QualifiedNameNode QualifiedName = null;
		public readonly TagKind Tag;

		public TagTypeNode(TagKind kind) : base(NodeKind.TagType) {
			this.Tag = kind;
		}

		public override void OutputPre(OutputStream os, OutputFlags flags) {
			if (!flags.HasFlag(OutputFlags.NoTagSpecifier)) {
				switch (Tag) {
					case TagKind.Class:
						os.Append("class");
						break;
					case TagKind.Struct:
						os.Append("struct");
						break;
					case TagKind.Union:
						os.Append("union");
						break;
					case TagKind.Enum:
						os.Append("enum");
						break;
				}
				os.Append(' ');
			}
			QualifiedName.Output(os, flags);
			OutputQualifiers(os, Quals, true, false);
		}

		public override void OutputPost(OutputStream os, OutputFlags flags) {
		}
	}
}
