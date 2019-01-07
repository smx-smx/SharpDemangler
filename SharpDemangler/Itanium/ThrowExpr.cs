namespace SharpDemangler.Itanium
{
	internal class ThrowExpr : Node
	{
		private Node op;

		public ThrowExpr(Node op) : base(ItaniumDemangleNodeType.ThrowExpr){
			this.op = op;
		}
	}
}