using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Common
{
	public class StringView : IEnumerable<char>
	{
		private StringBuilder str;

		public StringView() { }
		public StringView(string str) {
			this.str = new StringBuilder(str);
		}

		public bool IsEmpty => str.Length < 1;

		public StringView(char[] values) {
			this.str = new StringBuilder(new string(values));
		}
		public StringView(char c, int count) {
			this.str = new StringBuilder(new string(c, count));
		}
		public StringView(char[] value, int startIndex, int length) {
			this.str = new StringBuilder(new string(value, startIndex, length));
		}
#if UNSAFE
		public StringView(sbyte* value) {
			this.str = new StringBuilder(new string(value));
		}
		public StringView(char* value) {
			this.str = new StringBuilder(new string(value));
		}
		public StringView(sbyte* value, int startIndex, int length){
			this.str = new StringBuilder(new string(value, startIndex, length));
		}
		public StringView(char* value, int startIndex, int length){
			this.str = new StringBuilder(new string(value, startIndex, length));
		}
		public StringView(sbyte* value, int startIndex, int length, Encoding encoding){
			this.str = new StringBuilder(new string(value, startIndex, length, encoding));
		}
#endif

		public static string Empty => string.Empty;

		public static implicit operator string(StringView view) => view.ToString();
		public static implicit operator StringBuilder(StringView view) => view;
		public static implicit operator StringView(string str) => new StringView(str);

		public char this[int index] {
			get => str[index];
			set => str[index] = value;
		}

		public int Length => str.Length;

		public bool ConsumeFront(char ch) {
			if (str[0] == ch) {
				PopFront();
				return true;
			}
			return false;
		}

		public bool ConsumeFront(string str) {
			if (StartsWith(str)) {
				this.str = this.str.Remove(0, str.Length);
				return true;
			}
			return false;
		}

		public char PopFront() {
			char ch = str[0];
			str = str.Remove(0, 1);
			return ch;
		}

		public bool StartsWith(char value) => this.First() == value;
		public bool StartsWith(string value) => str.ToString().StartsWith(value);

		public IEnumerator<char> GetEnumerator() => str.ToString().GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => str.ToString().GetEnumerator();

		public int IndexOf(char value) => str.ToString().IndexOf(value);

		public StringView Substring(int start) => new StringView(str.ToString().Substring(start));
		public StringView Substring(int start, int end) => new StringView(str.ToString().Substring(start, end - start));

		public StringView DropFront(int skip = 1) {
			if (skip >= Length)
				skip = Length;
			return new StringView(new string(
				this.Skip(skip).ToArray()
			));
		}

		public StringView DropBack(int skip = 1) {
			if (skip >= Length)
				skip = Length;
			return new StringView(new string(
				this.Take(this.Count() - skip).ToArray()
			));
		}

		public override string ToString() => str.ToString();
	}
}
