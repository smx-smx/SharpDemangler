namespace SharpDemangler.Itanium
{
	internal class NewExpr : Node
	{
		readonly NodeArray exprList;
		readonly Node type;
		readonly NodeArray initList;
		readonly bool isGlobal;
		readonly bool isArray;

		public NewExpr(NodeArray exprList, Node type, NodeArray initList, bool isGlobal, bool isArray) : base(ItaniumDemangleNodeType.NewExpr){
			this.exprList = exprList;
			this.type = type;
			this.initList = initList;
			this.isGlobal = isGlobal;
			this.isArray = isArray;
		}
	}
}