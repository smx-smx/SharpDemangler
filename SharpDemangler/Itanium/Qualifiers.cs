using System;

namespace SharpDemangler.Itanium
{
	[Flags]
	public enum Qualifiers
	{
		None,
		Const,
		Volatile,
		Restrict
	}
}