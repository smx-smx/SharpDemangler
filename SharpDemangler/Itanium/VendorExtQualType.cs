using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class VendorExtQualType : Node
	{
		readonly Node ty;
		readonly string ext;

		public VendorExtQualType(Node ty, string ext) : base(ItaniumDemangleNodeType.VendorExtQualType) {
			this.ty = ty;
			this.ext = ext;
		}

		public override void PrintLeft(OutputStream sb) {
			ty.Print(sb);
			sb.Append(' ');
			sb.Append(ext);
		}
	}
}
