namespace SharpDemangler.Itanium
{
	internal class ConversionExpr : Node
	{
		readonly Node type;
		readonly NodeArray expressions;

		public ConversionExpr(Node type, NodeArray expressions) : base(ItaniumDemangleNodeType.ConversionExpr){
			this.type = type;
			this.expressions = expressions;
		}
	}
}