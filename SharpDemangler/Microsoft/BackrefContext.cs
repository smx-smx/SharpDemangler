namespace SharpDemangler.Microsoft
{
	public class BackrefContext
	{
		public const int Max = 10;

		public TypeNode[] FunctionParams = new TypeNode[Max];
		public int FunctionParamCount = 0;

		public NamedIdentifierNode[] Names = new NamedIdentifierNode[Max];
		public int NamesCount = 0;
	}
}