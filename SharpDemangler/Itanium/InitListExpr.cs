namespace SharpDemangler.Itanium
{
	internal class InitListExpr : Node
	{
		private Node ty;
		private NodeArray inits;

		public InitListExpr(Node ty, NodeArray inits) : base(ItaniumDemangleNodeType.InitListExpr){
			this.ty = ty;
			this.inits = inits;
		}
	}
}