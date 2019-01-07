namespace SharpDemangler.Itanium
{
	internal class FunctionParam : Node
	{
		readonly string number;

		public FunctionParam(string number) : base(ItaniumDemangleNodeType.FunctionParam){
			this.number = number;
		}
	}
}