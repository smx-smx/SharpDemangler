namespace SharpDemangler.Itanium
{
	internal class PostfixExpr : Node
	{
		private Node child;
		private string @operator;

		public PostfixExpr(Node child, string @operator) : base(ItaniumDemangleNodeType.PostfixExpr){
			this.child = child;
			this.@operator = @operator;
		}
	}
}