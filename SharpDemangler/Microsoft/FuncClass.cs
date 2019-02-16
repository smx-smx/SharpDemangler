using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Microsoft
{
	[Flags]
	public enum FuncClass
	{
		None = 0,
		Public = 1 << 0,
		Protected = 1 << 1,
		Private = 1 << 2,
		Global = 1 << 3,
		Static = 1 << 4,
		Virtual = 1 << 5,
		Far = 1 << 6,
		ExternC = 1 << 7,
		NoParameterList = 1 << 8,
		VirtualThisAdjust = 1 << 9,
		VirtualThisAdjustEx = 1 << 10,
		StaticThisAdjust = 1 << 11
	}
}
