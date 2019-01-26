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
		Default,
		NoCallingConvention,
		NoTagSpecifier
	}
}
