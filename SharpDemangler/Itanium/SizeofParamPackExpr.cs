namespace SharpDemangler.Itanium
{
	internal class SizeofParamPackExpr : Node
	{
		private Node pack;

		public SizeofParamPackExpr(Node pack) : base(ItaniumDemangleNodeType.SizeofParamPackExpr){
			this.pack = pack;
		}
	}
}