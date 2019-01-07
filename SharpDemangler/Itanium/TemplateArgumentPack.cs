namespace SharpDemangler.Itanium
{
	internal class TemplateArgumentPack : Node
	{
		public readonly NodeArray Elements;

		public TemplateArgumentPack(NodeArray elements) : base(ItaniumDemangleNodeType.TemplateArgumentPack){
			this.Elements = elements;
		}
	}
}