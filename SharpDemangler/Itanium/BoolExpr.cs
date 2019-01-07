namespace SharpDemangler.Itanium
{
	public class BoolExpr : Node
	{
		readonly bool value;

		public BoolExpr(bool value) : base(ItaniumDemangleNodeType.BoolExpr){
			this.value = value;
		}
	}
}