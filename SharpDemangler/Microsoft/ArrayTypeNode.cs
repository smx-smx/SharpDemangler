using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class ArrayTypeNode : TypeNode
	{
		public NodeArrayNode Dimensions = null;
		public TypeNode ElementType = null;

		public ArrayTypeNode() : base(NodeKind.ArrayType) {
		}

		void OutputOneDimension(OutputStream os, OutputFlags flags, Node n) {
			Assert.True(n.Kind == NodeKind.IntegerLiteral);
			IntegerLiteralNode iln = (IntegerLiteralNode)n;
			if (iln.Value != 0)
				iln.Output(os, flags);
		}

		void OutputDimensionsImpl(OutputStream os, OutputFlags flags) {
			if (Dimensions.Count() == 0)
				return;

			OutputOneDimension(os, flags, Dimensions[0]);
			for(int i=1; i<Dimensions.Count(); i++) {
				os.Append("][");
				OutputOneDimension(os, flags, Dimensions[i]);
			}
		}

		public override void OutputPre(OutputStream os, OutputFlags flags) {
			ElementType.OutputPre(os, flags);
			OutputQualifiers(os, Quals, true, false);
		}

		public override void OutputPost(OutputStream os, OutputFlags flags) {
			os.Append('[');
			OutputDimensionsImpl(os, flags);
			os.Append(']');

			ElementType.OutputPost(os, flags);
		}
	}
}
