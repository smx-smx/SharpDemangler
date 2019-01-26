using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class EncodedStringLiteralNode : SymbolNode
	{
		public string DecodedString;
		public bool IsTruncated = false;
		public CharKind Char = CharKind.Char;

		public EncodedStringLiteralNode() : base(NodeKind.EncodedStringLiteral) {
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			switch (Char) {
				case CharKind.Wchar:
					os.Append("L\"");
					break;
				case CharKind.Char:
					os.Append("\"");
					break;
				case CharKind.Char16:
					os.Append("u\"");
					break;
				case CharKind.Char32:
					os.Append("U\"");
					break;
			}
			os.Append(DecodedString);
			os.Append("\"");
			if (IsTruncated)
				os.Append("...");
		}
	}
}
