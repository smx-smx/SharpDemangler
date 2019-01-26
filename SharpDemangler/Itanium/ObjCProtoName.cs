using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class ObjCProtoName : Node
	{
		readonly Node ty;
		public readonly string Protocol;

		public ObjCProtoName(Node ty, string protocol) : base(ItaniumDemangleNodeType.ObjCProtoName) {
			this.ty = ty;
			this.Protocol = protocol;
		}

		public bool IsObjCObject {
			get {
				return ty.Kind == ItaniumDemangleNodeType.NameType && ((NameType)ty).Name == "objc_object";
			}
		}

		public override void PrintLeft(OutputStream sb) {
			ty.Print(sb);
			sb.Append('<');
			sb.Append(Protocol);
			sb.Append('>');
		}
	}
}
