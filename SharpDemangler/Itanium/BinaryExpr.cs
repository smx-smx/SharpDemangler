namespace SharpDemangler.Itanium
{
	internal class BinaryExpr : Node
	{
		readonly Node lhs;
		readonly string infixOperator;
		readonly Node rhs;

		public BinaryExpr(Node lhs, string infixOperator, Node rhs) : base(ItaniumDemangleNodeType.BinaryExpr) {
			this.lhs = lhs;
			this.infixOperator = infixOperator;
			this.rhs = rhs;
		}
	}
}