using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class PointerType : Node
	{
		public readonly Node Pointee;

		public PointerType(Node pointee) : base(ItaniumDemangleNodeType.PointerType, pointee.RHSComponentCache) {
			this.Pointee = pointee;
		}

		public override bool HasRHSComponent => Pointee.HasRHSComponent;

		public override void PrintLeft(OutputStream sb) {
			if(Pointee.Kind == ItaniumDemangleNodeType.ObjCProtoName && ((ObjCProtoName)Pointee).IsObjCObject) {
				ObjCProtoName objcProto = (ObjCProtoName)Pointee;
				sb.Append("id<");
				sb.Append(objcProto.Protocol);
				sb.Append('>');
			} else {
				Pointee.PrintLeft(sb);

				if (Pointee.HasArray) {
					sb.Append(' ');
				}
				if(Pointee.HasArray || Pointee.HasFunction) {
					sb.Append('(');
				}
				sb.Append('*');
			}
		}

		public override void PrintRight(OutputStream sb) {
			if(Pointee.Kind != ItaniumDemangleNodeType.ObjCProtoName || !((ObjCProtoName)Pointee).IsObjCObject) {
				if (Pointee.HasArray || Pointee.HasFunction) {
					sb.Append(')');
					Pointee.PrintRight(sb);
				}
			}
		}
	}
}
