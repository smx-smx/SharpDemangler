using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Common
{
	public static class Utils
	{
		public static void Swap<T>(ref T lhs, ref T rhs) {
			T temp;
			temp = lhs;
			lhs = rhs;
			rhs = temp;
		}
	}
}
