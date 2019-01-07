using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public static class SwapAndRestore
	{
		public static void Execute<Tvar>(ref Tvar variable, Tvar value, Action action) {
			Tvar old = variable;
			variable = value;
			action.Invoke();
			variable = old;
		}

		public static Tret Execute<Tret, Tvar>(ref Tvar variable, Tvar value, Func<Tret> action) {
			Tvar old = variable;
			variable = value;
			Tret ret = action.Invoke();
			variable = old;
			return ret;
		}
	}
}
