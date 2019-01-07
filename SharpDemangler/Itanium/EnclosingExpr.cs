namespace SharpDemangler.Itanium
{
	public class EnclosingExpr : Node
	{
		readonly string prefix;
		readonly Node infix;
		readonly string postfix;

		public EnclosingExpr(string prefix, Node infix, string postfix) : base(ItaniumDemangleNodeType.EnclosingExpr) {
			this.prefix = prefix;
			this.infix = infix;
			this.postfix = postfix;
		}
	}
}