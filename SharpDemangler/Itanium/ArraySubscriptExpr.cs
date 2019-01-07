namespace SharpDemangler.Itanium
{
	internal class ArraySubscriptExpr : Node
	{
		private Node op1;
		private Node op2;

		public ArraySubscriptExpr(Node op1, Node op2) : base(ItaniumDemangleNodeType.ArraySubscriptExpr){
			this.op1 = op1;
			this.op2 = op2;
		}
	}
}