using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class LocalName : Node
	{
		public readonly Node Encoding;
		public readonly Node Entity;

		public LocalName(Node encoding, Node entity) : base(ItaniumDemangleNodeType.LocalName) {
			this.Encoding = encoding;
			this.Entity = entity;
		}

		public override void PrintLeft(OutputStream sb) {
			Encoding.Print(sb);
			sb.Append("::");
			Entity.Print(sb);
		}
	}
}
