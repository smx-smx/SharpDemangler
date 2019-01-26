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
		None,
		Public,
		Protected,
		Private,
		Global,
		Static,
		Virtual,
		Far,
		ExternC,
		NoParameterList,
		VirtualThisAdjust,
		VirtualThisAdjustEx,
		StaticThisAdjust
	}
}
