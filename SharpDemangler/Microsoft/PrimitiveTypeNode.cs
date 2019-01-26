using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class PrimitiveTypeNode : TypeNode
	{
		public readonly PrimitiveKind PrimKind;

		public PrimitiveTypeNode(PrimitiveKind kind) : base(NodeKind.PrimitiveType) {
			this.PrimKind = kind;
		}

		public override void OutputPre(OutputStream os, OutputFlags flags) {
			switch (PrimKind) {
				case PrimitiveKind.Void: os.Append("void"); break;
				case PrimitiveKind.Bool: os.Append("bool"); break;
				case PrimitiveKind.Char: os.Append("char"); break;
				case PrimitiveKind.Schar: os.Append("signed char"); break;
				case PrimitiveKind.Uchar: os.Append("unsigned char"); break;
				case PrimitiveKind.Char16: os.Append("char16_t"); break;
				case PrimitiveKind.Char32: os.Append("char32_t"); break;
				case PrimitiveKind.Short: os.Append("short"); break;
				case PrimitiveKind.Ushort: os.Append("unsigned short"); break;
				case PrimitiveKind.Int: os.Append("int"); break;
				case PrimitiveKind.Uint: os.Append("unsigned int"); break;
				case PrimitiveKind.Long: os.Append("long"); break;
				case PrimitiveKind.Ulong: os.Append("unsigned long"); break;
				case PrimitiveKind.Int64: os.Append("__int64"); break;
				case PrimitiveKind.Uint64: os.Append("unsigned __int64"); break;
				case PrimitiveKind.Wchar: os.Append("wchar_t"); break;
				case PrimitiveKind.Float: os.Append("float"); break;
				case PrimitiveKind.Double: os.Append("double"); break;
				case PrimitiveKind.Ldouble: os.Append("long double"); break;
				case PrimitiveKind.Nullptr: os.Append("std::nullptr_t"); break;
			}
			OutputQualifiers(os, Quals, true, false);
		}
	}
}
