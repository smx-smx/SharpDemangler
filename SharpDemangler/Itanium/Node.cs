using SharpDemangler.Itanium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler
{
	public abstract class Node
	{
		public ItaniumDemangleNodeType Kind;
		public Cache RHSComponentCache = Cache.No;
		public Cache ArrayCache = Cache.No;
		public Cache FunctionCache = Cache.No;

		public virtual bool HasRHSComponent => RHSComponentCache == Cache.Yes;
		public virtual bool HasArray => ArrayCache == Cache.Yes;
		public virtual bool HasFunction => FunctionCache == Cache.Yes;

		public virtual Node SyntaxNode => this;

		public virtual string BaseName { get; set; } = string.Empty;

		public Node(
			ItaniumDemangleNodeType kind,
			Cache RHSComponentCache = Cache.No,
			Cache ArrayCache = Cache.No,
			Cache FunctionCache = Cache.No
		) {
			this.Kind = kind;
			this.RHSComponentCache = RHSComponentCache;
			this.ArrayCache = ArrayCache;
			this.FunctionCache = FunctionCache;
		}

		public void Print(OutputStream sb) {
			PrintLeft(sb);
			if (RHSComponentCache != Cache.No)
				PrintRight(sb);
		}
		public virtual void PrintLeft(OutputStream sb) { }
		public virtual void PrintRight(OutputStream sb) { }

	}
}
