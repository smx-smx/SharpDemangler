using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Microsoft
{
	public enum NodeKind
	{
		Unknown,
		Md5Symbol,
		PrimitiveType,
		FunctionSignature,
		Identifier,
		NamedIdentifier,
		VcallThunkIdentifier,
		LocalStaticGuardIdentifier,
		IntrinsicFunctionIdentifier,
		ConversionOperatorIdentifier,
		DynamicStructorIdentifier,
		StructorIdentifier,
		LiteralOperatorIdentifier,
		ThunkSignature,
		PointerType,
		TagType,
		ArrayType,
		Custom,
		IntrinsicType,
		NodeArray,
		QualifiedName,
		TemplateParameterReference,
		EncodedStringLiteral,
		IntegerLiteral,
		RttiBaseClassDescriptor,
		LocalStaticGuardVariable,
		FunctionSymbol,
		VariableSymbol,
		SpecialTableSymbol
	}
}
