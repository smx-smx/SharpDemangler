namespace SharpDemangler.Itanium
{
	internal class CallExpr : Node
	{
		private Node callee;
		private NodeArray args;

		public CallExpr(Node callee, NodeArray args) : base(ItaniumDemangleNodeType.CallExpr){
			this.callee = callee;
			this.args = args;
		}
	}
}