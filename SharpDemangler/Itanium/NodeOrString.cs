using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class NodeOrString
	{
		private string StringValue;
		private Node NodeValue;

		public bool IsString => StringValue != null;
		public bool IsNode => NodeValue != null;
		public bool IsEmpty => !IsString && !IsNode;

		public NodeOrString(string stringValue) {
			StringValue = stringValue;
		}

		public NodeOrString(Node nodeValue) {
			NodeValue = nodeValue;
		}

		public string AsString() {
			Debug.Assert(IsString);
			return StringValue;
		}

		public Node AsNode() {
			Debug.Assert(IsNode);
			return NodeValue;
		}
	}
}
