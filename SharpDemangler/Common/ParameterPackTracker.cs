using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Common
{
	public class ParameterPackTracker
	{
		public int CurrentPackIndex { get; set; }
		public int CurrentPackMax { get; set; }

		public ParameterPackTracker(NodeArray array) {
			CurrentPackIndex = 0;
			CurrentPackMax = array.Count();
		}
	}
}
