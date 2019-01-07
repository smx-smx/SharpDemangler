namespace SharpDemangler.Itanium
{
	internal class CastExpr : Node
	{
		private string castKind;
		private Node to;
		private Node from;

		public CastExpr(string castKind, Node to, Node from) : base(ItaniumDemangleNodeType.CastExpr){
			this.castKind = castKind;
			this.to = to;
			this.from = from;
		}
	}
}