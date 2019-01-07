namespace SharpDemangler.Itanium
{
	internal class DeleteExpr : Node
	{
		private Node op;
		private bool isGlobal;
		private bool isArray;

		public DeleteExpr(Node op, bool isGlobal, bool isArray) : base(ItaniumDemangleNodeType.DeleteExpr){
			this.op = op;
			this.isGlobal = isGlobal;
			this.isArray = isArray;
		}
	}
}