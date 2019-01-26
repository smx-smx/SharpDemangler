using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Common
{
	public static class Assert
	{
		public static void True(bool condition, string message = null) {
			if (!condition) {
				if(message == null) {
					message = "Assertion failed";
				}
				throw new DemanglerAssertionException(message);
			}
		}
	}
}
