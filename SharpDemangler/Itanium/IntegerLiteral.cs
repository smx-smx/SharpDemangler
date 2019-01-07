namespace SharpDemangler.Itanium
{
	internal class IntegerLiteral : Node
	{
		readonly string type;
		readonly string value;

		public IntegerLiteral(string type, string value) : base(ItaniumDemangleNodeType.IntegerLiteral) {
			this.type = type;
			this.value = value;
		}
	}
}