using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Common
{
	public class OutputStream
	{
		#region OutputStream
		private readonly StringBuilder sb;

		public OutputStream() {
			sb = new StringBuilder();
		}
		public OutputStream(int capacity) {
			sb = new StringBuilder(capacity);
		}
		public OutputStream(string value) {
			sb = new StringBuilder(value);
		}

		public OutputStream(string value, int capacity) {
			sb = new StringBuilder(value, capacity);
		}

		public OutputStream(int capacity, int maxCapacity) {
			sb = new StringBuilder(capacity, maxCapacity);
		}

		public OutputStream(string value, int startIndex, int length, int capacity) {
			sb = new StringBuilder(value, startIndex, length, capacity);
		}

		public char this[int index] {
			get => sb[index];
			set => sb[index] = value;
		}

		public int MaxCapacity => sb.MaxCapacity;
		public int Length {
			get => sb.Length;
			set => sb.Length = value;
		}
		public int Capacity {
			get => sb.Capacity;
			set => sb.Capacity = value;
		}
		public StringBuilder Append(double value) => sb.Append(value);
		public StringBuilder Append(char[] value) => sb.Append(value);
		public StringBuilder Append(object value) => sb.Append(value);
		public StringBuilder Append(ulong value) => sb.Append(value);
		public StringBuilder Append(uint value) => sb.Append(value);
		public StringBuilder Append(ushort value) => sb.Append(value);
		public StringBuilder Append(decimal value) => sb.Append(value);
		public StringBuilder Append(float value) => sb.Append(value);
		public StringBuilder Append(int value) => sb.Append(value);
		public StringBuilder Append(short value) => sb.Append(value);
		public StringBuilder Append(char value) => sb.Append(value);
		public StringBuilder Append(long value) => sb.Append(value);
		public StringBuilder Append(sbyte value) => sb.Append(value);
		public StringBuilder Append(byte value) => sb.Append(value);
		public StringBuilder Append(char[] value, int startIndex, int charCount) => sb.Append(value, startIndex, charCount);
		public StringBuilder Append(string value) => sb.Append(value);
		public StringBuilder Append(string value, int startIndex, int count) => sb.Append(value, startIndex, count);
		public StringBuilder Append(char value, int repeatCount) => sb.Append(value, repeatCount);
#if UNSAFE
		public StringBuilder Append(char* value, int valueCount) => sb.Append(value, valueCount);
#endif
		public StringBuilder Append(bool value) => sb.Append(value);
		public StringBuilder AppendFormat(IFormatProvider provider, string format, params object[] args) => sb.AppendFormat(provider, format, args);
		public StringBuilder AppendFormat(string format, object arg0, object arg1, object arg2) => sb.AppendFormat(format, arg0, arg1, arg2);
		public StringBuilder AppendFormat(string format, params object[] args) => sb.AppendFormat(format, args);
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0) => sb.AppendFormat(provider, format, arg0);
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1) => sb.AppendFormat(provider, format, arg0, arg1);
		public StringBuilder AppendFormat(IFormatProvider provider, string format, object arg0, object arg1, object arg2) => sb.AppendFormat(provider, format, arg0, arg1, arg2);
		public StringBuilder AppendFormat(string format, object arg0) => sb.AppendFormat(format, arg0);
		public StringBuilder AppendFormat(string format, object arg0, object arg1) => sb.AppendFormat(format, arg0, arg1);
		public StringBuilder AppendLine() => sb.AppendLine();
		public StringBuilder AppendLine(string value) => sb.AppendLine(value);
		public StringBuilder Clear() => sb.Clear();
		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count) => sb.CopyTo(sourceIndex, destination, destinationIndex, count);
		public int EnsureCapacity(int capacity) => sb.EnsureCapacity(capacity);
		public bool Equals(StringBuilder sb) => sb.Equals(sb);
		public StringBuilder Insert(int index, object value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, byte value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, ulong value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, uint value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, string value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, decimal value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, string value, int count) => sb.Insert(index, value, count);
		public StringBuilder Insert(int index, bool value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, ushort value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, short value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, char value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, sbyte value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, char[] value, int startIndex, int charCount) => sb.Insert(index, value, startIndex, charCount);
		public StringBuilder Insert(int index, int value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, long value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, float value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, double value) => sb.Insert(index, value);
		public StringBuilder Insert(int index, char[] value) => sb.Insert(index, value);
		public StringBuilder Remove(int startIndex, int length) => sb.Insert(startIndex, length);
		public StringBuilder Replace(string oldValue, string newValue) => sb.Replace(oldValue, newValue);
		public StringBuilder Replace(string oldValue, string newValue, int startIndex, int count) => sb.Replace(oldValue, newValue, startIndex, count);
		public StringBuilder Replace(char oldChar, char newChar) => sb.Replace(oldChar, newChar);
		public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count) => sb.Replace(oldChar, newChar, startIndex, count);
		public string ToString(int startIndex, int length) => sb.ToString(startIndex, length);
		public override string ToString() => sb.ToString();
		#endregion

		public static implicit operator StringBuilder(OutputStream sbw) => sbw.sb;

		public ParameterPackTracker PackTracker { get; private set; } = null;

		public void InitializePackExpansion(NodeArray array) {
			if(PackTracker == null)
				PackTracker = new ParameterPackTracker(array);
		}

		public void UsingParameterPackTracker(ParameterPackTracker ppt, Action action) {
			ParameterPackTracker _ppt = this.PackTracker;
			this.PackTracker = ppt;
			action.Invoke();
			this.PackTracker = _ppt;
		}

		public void UsingParameterPack(int CurrentPackIndex, int CurrentPackMax, Action action) {
			int _curIndex = CurrentPackIndex;
			int _curMax = CurrentPackMax;

			this.CurrentPackIndex = CurrentPackIndex;
			this.CurrentPackMax = CurrentPackMax;
			action.Invoke();

			this.CurrentPackIndex = _curIndex;
			this.CurrentPackMax = _curMax;
		}

		public int CurrentPackMax {
			get => PackTracker.CurrentPackMax;
			set => PackTracker.CurrentPackMax = value;
		}
		public int CurrentPackIndex {
			get => PackTracker.CurrentPackIndex;
			set => PackTracker.CurrentPackIndex = value;
		}
	}
}
