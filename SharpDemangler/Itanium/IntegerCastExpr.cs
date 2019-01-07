namespace SharpDemangler.Itanium
{
	internal class IntegerCastExpr : Node
	{
		readonly Node ty;
		readonly string integer;

		public IntegerCastExpr(Node ty, string integer) : base(ItaniumDemangleNodeType.IntegerCastExpr){
			this.ty = ty;
			this.integer = integer;
		}
	}
}