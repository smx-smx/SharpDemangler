namespace SharpDemangler.Itanium
{
	internal class MemberExpr : Node
	{
		private Node lhs;
		private string kind;
		private Node rhs;

		public MemberExpr(Node lhs, string kind, Node rhs): base(ItaniumDemangleNodeType.MemberExpr){
			this.lhs = lhs;
			this.kind = kind;
			this.rhs = rhs;
		}
	}
}