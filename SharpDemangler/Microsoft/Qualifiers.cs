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
		None,
		Const,
		Volatile,
		Far,
		Huge,
		Unaligned,
		Restrict,
		Pointer64
	}
}
