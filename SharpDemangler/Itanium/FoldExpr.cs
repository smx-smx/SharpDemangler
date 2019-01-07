namespace SharpDemangler.Itanium
{
	internal class FoldExpr : Node
	{
		readonly bool isLeftFold;
		readonly string operatorName;
		readonly Node pack;
		readonly Node init;

		public FoldExpr(bool isLeftFold, string operatorName, Node pack, Node init) : base(ItaniumDemangleNodeType.FoldExpr){
			this.isLeftFold = isLeftFold;
			this.operatorName = operatorName;
			this.pack = pack;
			this.init = init;
		}
	}
}