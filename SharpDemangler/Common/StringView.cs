﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpDemangler.Common
{
	public struct StringView : IEnumerable<char>
	{
		private string str;

		public StringView(string str) {
			this.str = str;
		}

		public bool IsEmpty => str.Length < 1;

		public StringView(char[] values) {
			this.str = new string(values);
		}
		public StringView(char c, int count) {
			this.str = new string(c, count);
		}
		public StringView(char[] value, int startIndex, int length) {
			this.str = new string(value, startIndex, length);
		}

		public static string Empty => string.Empty;

		public static implicit operator string(StringView view) => view.ToString();
		public static implicit operator StringView(string str) => new StringView(str);

		public char this[int index] {
			get => str[index];
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
