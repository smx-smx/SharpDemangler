using SharpDemangler.Common;
using SharpDemangler.Itanium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler
{
	public class NodeArray : IEnumerable<Node>
	{
		Node[] Elements = null;

		public Node this[int i] {
			get { return Elements[i]; }
			set { Elements[i] = value; }
		}

		public NodeArray() { }

		public NodeArray(Node[] elements) {
			Elements = elements;
		}

		public IEnumerator<Node> GetEnumerator() {
			return Elements.AsEnumerable().GetEnumerator();
		}

		public void PrintWithComma(OutputStream sb) {
			bool firstElement = true;
			for(int i=0; i<Elements.Length; i++) {
				int beforeComma = sb.Length;
				if (!firstElement)
					sb.Append(", ");
				int afterComma = sb.Length;
				Elements[i].Print(sb);

				if(afterComma == sb.Length) {
					sb.Length -= (afterComma - beforeComma);
					continue;
				}

				firstElement = false;
			}
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return Elements.GetEnumerator();
		}
	}
}
