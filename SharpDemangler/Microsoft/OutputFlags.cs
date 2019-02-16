using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Microsoft
{
	[Flags]
	public enum OutputFlags
	{
		Default = 0,
		NoCallingConvention = 1 << 0,
		NoTagSpecifier = 1 << 1
	}
}
