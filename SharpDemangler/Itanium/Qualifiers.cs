using System;

namespace SharpDemangler.Itanium
{
	[Flags]
	public enum Qualifiers
	{
		None = 0,
		Const = 1 << 0,
		Volatile = 1 << 1,
		Restrict = 1 << 2
	}
}