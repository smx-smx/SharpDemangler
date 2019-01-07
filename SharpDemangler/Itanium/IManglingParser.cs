using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public interface IManglingParser
	{
		void Reset(string mangledName);

		string ParseNumber(bool AllowNegative = false);
		Qualifiers ParseCVQualifiers();
		bool ParsePositiveInteger(out int Out);
		string ParseBareSourceName();
		bool ParseSeqId(out int Out);
		Node ParseSubstitution();
		Node ParseTemplateParam();
		Node ParseTemplateArgs(bool TagTemplates = false);
		Node ParseTemplateArg();
		Node ParseExpr();
		Node ParsePrefixExpr(string kind);
		Node ParseBinaryExpr(string kind);
		Node ParseIntegerLiteral(string lit);
		Node ParseExprPrimary();

		Node ParseFloatingLiteral(FloatingData dataType);

		Node ParseFunctionParam();
		Node ParseNewExpr();
		Node ParseConversionExpr();
		Node ParseBracedExpr();
		Node ParseFoldExpr();
		Node ParseType();
		Node ParseFunctionType();
		Node ParseVectorType();
		Node ParseDeclType();
		Node ParseArrayType();
		Node ParsePointerToMemberType();
		Node ParseClassEnumType();
		Node ParseQualifiedType();
		Node ParseEncoding();
		bool ParseCallOffset();
		Node ParseSpecialName();
		Node ParseName(NameState state = null);
		Node ParseLocalName(NameState state);
		Node ParseUnqualifiedName(NameState state);
		Node ParseUnnamedTypeName(NameState state);
		Node ParseSourceName(NameState state);
		Node ParseUnscopedName(NameState state);
		Node ParseNestedName(NameState state);
		Node ParseCtorDtorName(Node SoFar, NameState State);
		Node ParseAbiTags(Node N);
		Node ParseUnresolvedName();
		Node ParseSimpleId();
		Node ParseBaseUnresolvedName();
		Node ParseUnresolvedType();
		Node ParseDestructorName();
		Node Parse();
		Node ParseOperatorName(NameState state);
	}
}
