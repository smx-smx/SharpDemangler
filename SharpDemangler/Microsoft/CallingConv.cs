using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Microsoft
{
	public enum CallingConv
	{
		None,
		Cdecl,
		Pascal,
		Thiscall,
		Stdcall,
		Fastcall,
		Clrcall,
		Eabi,
		Vectorcall,
		Regcall
	}
}
