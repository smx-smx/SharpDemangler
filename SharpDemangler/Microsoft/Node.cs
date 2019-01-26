using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Microsoft
{
	public class Node
	{
		public NodeKind Kind;

		public Node(NodeKind kind) {
			this.Kind = kind;
		}

		public virtual void Output(OutputStream os, OutputFlags flags) {

		}

		public void OutputSpaceIfNecessary(OutputStream os) {
			if (os.Length == 0)
				return;

			char c = os[os.Length - 1];
			if (char.IsLetterOrDigit(c) || c == '>')
				os.Append(' ');
		}

		public void OutputCallingConvention(OutputStream os, CallingConv cc) {
			OutputSpaceIfNecessary(os);

			switch (cc) {
				case CallingConv.Cdecl:
					os.Append("__cdecl");
					break;
				case CallingConv.Fastcall:
					os.Append("__fastcall");
					break;
				case CallingConv.Pascal:
					os.Append("__pascal");
					break;
				case CallingConv.Regcall:
					os.Append("__regcall");
					break;
				case CallingConv.Stdcall:
					os.Append("__stdcall");
					break;
				case CallingConv.Thiscall:
					os.Append("__thiscall");
					break;
				case CallingConv.Eabi:
					os.Append("__eabi");
					break;
				case CallingConv.Vectorcall:
					os.Append("__vectorcall");
					break;
				case CallingConv.Clrcall:
					os.Append("__clrcall");
					break;
				default:
					break;
			}
		}

		public bool OutputSingleQualifier(OutputStream os, Qualifiers Q) {
			switch (Q) {
				case Qualifiers.Const:
					os.Append("const");
					return true;
				case Qualifiers.Volatile:
					os.Append("volatile");
					return true;
				case Qualifiers.Restrict:
					os.Append("__restrict");
					return true;
				default:
					break;
			}
			return false;
		}

		public bool OutputQualifierIfPresent(OutputStream os, Qualifiers Q, Qualifiers Mask, bool needSpace) {
			if (!(Q.HasFlag(Mask)))
				return needSpace;

			if (needSpace)
				os.Append(' ');

			OutputSingleQualifier(os, Mask);
			return true;
		}

		public void OutputQualifiers(OutputStream os, Qualifiers Q, bool spaceBefore, bool spaceAfter) {
			if (Q == Qualifiers.None)
				return;

			int pos1 = os.Length;
			spaceBefore = OutputQualifierIfPresent(os, Q, Qualifiers.Const, spaceBefore);
			spaceBefore = OutputQualifierIfPresent(os, Q, Qualifiers.Volatile, spaceBefore);
			spaceBefore = OutputQualifierIfPresent(os, Q, Qualifiers.Restrict, spaceBefore);
			int pos2 = os.Length;
			if (spaceAfter && pos2 > pos1)
				os.Append(' ');
		}
	}
}
