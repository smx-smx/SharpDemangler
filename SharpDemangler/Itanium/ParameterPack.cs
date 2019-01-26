using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class ParameterPack : Node
	{
		private readonly NodeArray data;

		public ParameterPack(NodeArray data) : base(ItaniumDemangleNodeType.ParameterPack) {
			this.data = data;

			this.ArrayCache = this.FunctionCache = this.RHSComponentCache = Cache.Unknown;

			if (data.All(p => p.ArrayCache == Cache.No)) {
				ArrayCache = Cache.No;
			}
			if (data.All(p => p.FunctionCache == Cache.No)) {
				FunctionCache = Cache.No;
			}
			if (data.All(p => p.RHSComponentCache == Cache.No)) {
				RHSComponentCache = Cache.No;
			}
		}

		public override void PrintLeft(OutputStream sb) {
			sb.InitializePackExpansion(data);
			int idx = sb.CurrentPackMax;
			if (idx < data.Count())
				data[idx].PrintLeft(sb);
		}

		public override void PrintRight(OutputStream sb) {
			sb.InitializePackExpansion(data);
			int idx = sb.CurrentPackIndex;

		}
	}
}
