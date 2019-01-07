namespace SharpDemangler.Itanium
{
	internal class ConditionalExpr : Node
	{
		private Node cond;
		private Node then;
		private Node @else;

		public ConditionalExpr(Node cond, Node then, Node @else) : base(ItaniumDemangleNodeType.ConditionalExpr){
			this.cond = cond;
			this.then = then;
			this.@else = @else;
		}
	}
}