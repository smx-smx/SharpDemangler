namespace SharpDemangler.Itanium
{
	internal class BracedExpr : Node
	{
		readonly Node elem;
		readonly Node init;
		readonly bool isArray;

		public BracedExpr(Node elem, Node init, bool isArray) : base(ItaniumDemangleNodeType.BracedExpr) {
			this.elem = elem;
			this.init = init;
			this.isArray = isArray;
		}
	}
}