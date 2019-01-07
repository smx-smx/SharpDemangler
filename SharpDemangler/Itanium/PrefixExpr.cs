namespace SharpDemangler.Itanium
{
	internal class PrefixExpr : Node
	{
		readonly string prefix;
		readonly Node child;

		public PrefixExpr(string prefix, Node child) : base(ItaniumDemangleNodeType.PrefixExpr){
			this.prefix = prefix;
			this.child = child;
		}
	}
}