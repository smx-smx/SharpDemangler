using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Microsoft
{
	public enum StorageClass
	{
		None,
		PrivateStatic,
		ProtectedStatic,
		PublicStatic,
		Global,
		FunctionLocalStatic
	}
}
