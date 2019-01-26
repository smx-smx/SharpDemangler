using SharpDemangler.Common;

namespace SharpDemangler.Microsoft
{
	public class IntrinsicFunctionIdentifierNode : IdentifierNode
	{
		public IntrinsicFunctionKind Operator;
		public IntrinsicFunctionIdentifierNode(IntrinsicFunctionKind Operator) : base(NodeKind.IntrinsicFunctionIdentifier) {
			this.Operator = Operator;
		}

		public override void Output(OutputStream os, OutputFlags flags) {
			switch (Operator) {
				case IntrinsicFunctionKind.New:
					os.Append("operator new");
					break;
				case IntrinsicFunctionKind.Delete:
					os.Append("operator delete");
					break;
				case IntrinsicFunctionKind.Assign:
					os.Append("operator=");
					break;
				case IntrinsicFunctionKind.RightShift:
					os.Append("operator>>");
					break;
				case IntrinsicFunctionKind.LeftShift:
					os.Append("operator<<");
					break;
				case IntrinsicFunctionKind.LogicalNot:
					os.Append("operator!");
					break;
				case IntrinsicFunctionKind.Equals:
					os.Append("operator==");
					break;
				case IntrinsicFunctionKind.NotEquals:
					os.Append("operator!=");
					break;
				case IntrinsicFunctionKind.ArraySubscript:
					os.Append("operator[]");
					break;
				case IntrinsicFunctionKind.Pointer:
					os.Append("operator->");
					break;
				case IntrinsicFunctionKind.Increment:
					os.Append("operator++");
					break;
				case IntrinsicFunctionKind.Decrement:
					os.Append("operator--");
					break;
				case IntrinsicFunctionKind.Minus:
					os.Append("operator-");
					break;
				case IntrinsicFunctionKind.Plus:
					os.Append("operator+");
					break;
				case IntrinsicFunctionKind.Dereference:
					os.Append("operator*");
					break;
				case IntrinsicFunctionKind.BitwiseAnd:
					os.Append("operator&");
					break;
				case IntrinsicFunctionKind.MemberPointer:
					os.Append("operator->*");
					break;
				case IntrinsicFunctionKind.Divide:
					os.Append("operator/");
					break;
				case IntrinsicFunctionKind.Modulus:
					os.Append("operator%");
					break;
				case IntrinsicFunctionKind.LessThan:
					os.Append("operator<");
					break;
				case IntrinsicFunctionKind.LessThanEqual:
					os.Append("operator<=");
					break;
				case IntrinsicFunctionKind.GreaterThan:
					os.Append("operator>");
					break;
				case IntrinsicFunctionKind.GreaterThanEqual:
					os.Append("operator>=");
					break;
				case IntrinsicFunctionKind.Comma:
					os.Append("operator,");
					break;
				case IntrinsicFunctionKind.Parens:
					os.Append("operator()");
					break;
				case IntrinsicFunctionKind.BitwiseNot:
					os.Append("operator~");
					break;
				case IntrinsicFunctionKind.BitwiseXor:
					os.Append("operator^");
					break;
				case IntrinsicFunctionKind.BitwiseOr:
					os.Append("operator|");
					break;
				case IntrinsicFunctionKind.LogicalAnd:
					os.Append("operator&&");
					break;
				case IntrinsicFunctionKind.LogicalOr:
					os.Append("operator||");
					break;
				case IntrinsicFunctionKind.TimesEqual:
					os.Append("operator*=");
					break;
				case IntrinsicFunctionKind.PlusEqual:
					os.Append("operator+=");
					break;
				case IntrinsicFunctionKind.MinusEqual:
					os.Append("operator-=");
					break;
				case IntrinsicFunctionKind.DivEqual:
					os.Append("operator/=");
					break;
				case IntrinsicFunctionKind.ModEqual:
					os.Append("operator%=");
					break;
				case IntrinsicFunctionKind.RshEqual:
					os.Append("operator>>=");
					break;
				case IntrinsicFunctionKind.LshEqual:
					os.Append("operator<<=");
					break;
				case IntrinsicFunctionKind.BitwiseAndEqual:
					os.Append("operator&=");
					break;
				case IntrinsicFunctionKind.BitwiseOrEqual:
					os.Append("operator|=");
					break;
				case IntrinsicFunctionKind.BitwiseXorEqual:
					os.Append("operator^=");
					break;
				case IntrinsicFunctionKind.VbaseDtor:
					os.Append("`vbase dtor'");
					break;
				case IntrinsicFunctionKind.VecDelDtor:
					os.Append("`vector deleting dtor'");
					break;
				case IntrinsicFunctionKind.DefaultCtorClosure:
					os.Append("`default ctor closure'");
					break;
				case IntrinsicFunctionKind.ScalarDelDtor:
					os.Append("`scalar deleting dtor'");
					break;
				case IntrinsicFunctionKind.VecCtorIter:
					os.Append("`vector ctor iterator'");
					break;
				case IntrinsicFunctionKind.VecDtorIter:
					os.Append("`vector dtor iterator'");
					break;
				case IntrinsicFunctionKind.VecVbaseCtorIter:
					os.Append("`vector vbase ctor iterator'");
					break;
				case IntrinsicFunctionKind.VdispMap:
					os.Append("`virtual displacement map'");
					break;
				case IntrinsicFunctionKind.EHVecCtorIter:
					os.Append("`eh vector ctor iterator'");
					break;
				case IntrinsicFunctionKind.EHVecDtorIter:
					os.Append("`eh vector dtor iterator'");
					break;
				case IntrinsicFunctionKind.EHVecVbaseCtorIter:
					os.Append("`eh vector vbase ctor iterator'");
					break;
				case IntrinsicFunctionKind.CopyCtorClosure:
					os.Append("`copy ctor closure'");
					break;
				case IntrinsicFunctionKind.LocalVftableCtorClosure:
					os.Append("`local vftable ctor closure'");
					break;
				case IntrinsicFunctionKind.ArrayNew:
					os.Append("operator new[]");
					break;
				case IntrinsicFunctionKind.ArrayDelete:
					os.Append("operator delete[]");
					break;
				case IntrinsicFunctionKind.ManVectorCtorIter:
					os.Append("`managed vector ctor iterator'");
					break;
				case IntrinsicFunctionKind.ManVectorDtorIter:
					os.Append("`managed vector dtor iterator'");
					break;
				case IntrinsicFunctionKind.EHVectorCopyCtorIter:
					os.Append("`EH vector copy ctor iterator'");
					break;
				case IntrinsicFunctionKind.EHVectorVbaseCopyCtorIter:
					os.Append("`EH vector vbase copy ctor iterator'");
					break;
				case IntrinsicFunctionKind.VectorCopyCtorIter:
					os.Append("`vector copy ctor iterator'");
					break;
				case IntrinsicFunctionKind.VectorVbaseCopyCtorIter:
					os.Append("`vector vbase copy constructor iterator'");
					break;
				case IntrinsicFunctionKind.ManVectorVbaseCopyCtorIter:
					os.Append("`managed vector vbase copy constructor iterator'");
					break;
				case IntrinsicFunctionKind.CoAwait:
					os.Append("co_await");
					break;
				case IntrinsicFunctionKind.Spaceship:
					os.Append("operator <=>");
					break;
				case IntrinsicFunctionKind.MaxIntrinsic:
				case IntrinsicFunctionKind.None:
					break;
			}
			OutputTemplateParameters(os, flags);
		}
	}
}