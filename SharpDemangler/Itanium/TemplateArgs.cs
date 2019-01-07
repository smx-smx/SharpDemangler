namespace SharpDemangler.Itanium
{
	internal class TemplateArgs : Node
	{
		private NodeArray @params;

		public TemplateArgs(NodeArray @params) : base(ItaniumDemangleNodeType.TemplateArgs){
			this.@params = @params;
		}
	}
}