using System;

namespace SharpDemangler.Microsoft
{
	[Flags]
	public enum NameBackrefBehaviour
	{
		None = 0,
		Template = 1 << 0,
		Simple = 1 << 1
	}
}