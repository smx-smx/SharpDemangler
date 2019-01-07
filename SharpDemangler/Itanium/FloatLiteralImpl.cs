namespace SharpDemangler.Itanium
{
	internal class FloatLiteralImpl<T> : Node
	{
		readonly string contents;

		public FloatLiteralImpl(string contents) : base(ItaniumDemangleNodeType.FloatLiteral) {
			this.contents = contents;
		}
	}
}