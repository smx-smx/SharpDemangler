using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Common
{
	public class DemanglerAssertionException : Exception
	{
		public DemanglerAssertionException() {
		}

		public DemanglerAssertionException(string message) : base(message) {
		}

		public DemanglerAssertionException(string message, Exception innerException) : base(message, innerException) {
		}

		protected DemanglerAssertionException(SerializationInfo info, StreamingContext context) : base(info, context) {
		}
	}
}
