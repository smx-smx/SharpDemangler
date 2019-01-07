namespace SharpDemangler.Itanium
{
	internal class BracedRangeExpr : Node
	{
		readonly Node first;
		readonly Node last;
		readonly Node init;

		public BracedRangeExpr(Node first, Node last, Node init) : base(ItaniumDemangleNodeType.BracedRangeExpr){
			this.first = first;
			this.last = last;
			this.init = init;
		}
	}
}