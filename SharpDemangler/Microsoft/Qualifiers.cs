using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Microsoft
{
	[Flags]
	public enum Qualifiers
	{
		None = 0,
		Const = 1 << 0,
		Volatile = 1 << 1,
		Far = 1 << 2,
		Huge = 1 << 3,
		Unaligned = 1 << 4,
		Restrict = 1 << 5,
		Pointer64 = 1 << 6
	}
}
