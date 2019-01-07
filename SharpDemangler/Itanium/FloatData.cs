using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public enum FloatDataArch
	{
		Default,
		Mips,
		MipsN64,
		AArch64,
		Wasm,
		Arm,
		Hexagon
	}

	public abstract class FloatingData
	{
		public readonly int MangledSize;
		public readonly int MaxDemangledSize;
		public readonly string Spec;

		public FloatingData(int mangledSize, int maxDemangledSize, string spec) {
			this.MangledSize = mangledSize;
			this.MaxDemangledSize = maxDemangledSize;
			this.Spec = spec;
		}
	}

	public class FloatData : FloatingData
	{
		public FloatData() : base(8, 24, "%af") { }
	}

	public class DoubleData : FloatingData
	{
		public DoubleData() : base(16, 32, "%a") { }
	}

	public class LongDoubleData : FloatingData
	{
		public LongDoubleData(FloatDataArch arch) : base(
			(arch == FloatDataArch.Mips || arch == FloatDataArch.MipsN64 || arch == FloatDataArch.AArch64 || arch == FloatDataArch.Wasm) ? 32 : 
			(arch == FloatDataArch.Arm || arch == FloatDataArch.Mips || arch == FloatDataArch.Hexagon) ? 16 : 20,
			40, "%LaL"
		) { }
	}
}
