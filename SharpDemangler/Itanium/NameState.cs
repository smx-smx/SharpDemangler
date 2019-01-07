namespace SharpDemangler.Itanium
{
	public class NameState
	{
		public bool CtorDtorConversion = false;
		public bool EndsWithTemplateArgs = false;
		public Qualifiers CVQualifiers = Qualifiers.None;
		public FunctionRefQual ReferenceQualifier = FunctionRefQual.None;
		public int ForwardTemplateRefsBegin;

		public NameState(AbstractManglingParser Enclosing) {
			ForwardTemplateRefsBegin = Enclosing.ForwardTemplateRefs.Count;
		}
	}
}