using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Microsoft
{
	public class MicrosoftDemangler
	{
		bool error;
		bool StartsWithDigit(StringView s) => !s.IsEmpty && char.IsDigit(s.First());

		void SetError() {
			error = true;
		}

		BackrefContext backrefs = new BackrefContext();

		bool IsMemberPointer(StringView mangledName, out bool error) {
			error = false;
			switch (mangledName.PopFront()) {
				case '$':
					return false;
				case 'A':
					return false;
				case 'P':
				case 'Q':
				case 'R':
				case 'S':
					break;
				default:
					SetError();
					return false;
			}

			if (StartsWithDigit(mangledName)) {
				Debug.Assert(mangledName[0] == '6' || mangledName[0] == '8');
				return mangledName[0] == '8';
			}

			mangledName.ConsumeFront('E');
			mangledName.ConsumeFront('I');
			mangledName.ConsumeFront('F');

			Debug.Assert(!mangledName.IsEmpty);

			switch (mangledName.First()) {
				case 'A':
				case 'B':
				case 'C':
				case 'D':
					return false;
				case 'Q':
				case 'R':
				case 'S':
				case 'T':
					return true;
				default:
					SetError();
					return false;
			}
		}

		bool IsArrayType(StringView s) => s[0] == 'Y';
		bool IsFunctionType(StringView s) => s.StartsWith("$$A8@@") || s.StartsWith("$$A6");

		SpecialIntrinsicKind ConsumeSpecialIntrinsicKind(ref StringView mangledName) {
			if (mangledName.ConsumeFront("?_7"))
				return SpecialIntrinsicKind.Vftable;
			if (mangledName.ConsumeFront("?_8"))
				return SpecialIntrinsicKind.Vbtable;
			if (mangledName.ConsumeFront("?_9"))
				return SpecialIntrinsicKind.VcallThunk;
			if (mangledName.ConsumeFront("?_A"))
				return SpecialIntrinsicKind.Typeof;
			if (mangledName.ConsumeFront("?_B"))
				return SpecialIntrinsicKind.LocalStaticGuard;
			if (mangledName.ConsumeFront("?_C"))
				return SpecialIntrinsicKind.StringLiteralSymbol;
			if (mangledName.ConsumeFront("?_P"))
				return SpecialIntrinsicKind.UdtReturning;
			if (mangledName.ConsumeFront("?_R0"))
				return SpecialIntrinsicKind.RttiTypeDescriptor;
			if (mangledName.ConsumeFront("?_R1"))
				return SpecialIntrinsicKind.RttiBaseClassDescriptor;
			if (mangledName.ConsumeFront("?_R2"))
				return SpecialIntrinsicKind.RttiBaseClassArray;
			if (mangledName.ConsumeFront("?_R3"))
				return SpecialIntrinsicKind.RttiClassHierarchyDescriptor;
			if (mangledName.ConsumeFront("?_R4"))
				return SpecialIntrinsicKind.RttiCompleteObjLocator;
			if (mangledName.ConsumeFront("?_S"))
				return SpecialIntrinsicKind.LocalVftable;
			if (mangledName.ConsumeFront("?__E"))
				return SpecialIntrinsicKind.DynamicInitializer;
			if (mangledName.ConsumeFront("?__F"))
				return SpecialIntrinsicKind.DynamicAtexitDestructor;
			if (mangledName.ConsumeFront("?__J"))
				return SpecialIntrinsicKind.LocalStaticThreadGuard;
			return SpecialIntrinsicKind.None;
		}

		bool StartsWithLocalScopePattern(ref StringView s) {
			if (!s.ConsumeFront('?'))
				return false;
			if (s.Length < 2)
				return false;

			int end = s.IndexOf('?');
			if (end < 0)
				return false;

			StringView candidate = s.Substring(0, end);
			if (candidate.IsEmpty)
				return false;

			if (candidate.Length == 1) {
				return candidate[0] == '@' || (candidate[0] >= '0' && candidate[0] <= '9');
			}

			if (candidate.Last() != '@')
				return false;


			candidate = candidate.DropBack();

			if (candidate[0] < 'B' || candidate[0] > 'P')
				return false;

			candidate = candidate.DropFront();
			while (!candidate.IsEmpty) {
				if (candidate[0] < 'A' || candidate[0] > 'P')
					return false;
				candidate = candidate.DropFront();
			}
			return true;

		}

		bool IsTagType(StringView s) {
			switch (s.First()) {
				case 'T': // union
				case 'U': // struct
				case 'V': // class
				case 'W': // enum
					return true;
			}
			return false;
		}

		bool IsCustomType(StringView s) => s.First() == '?';

		bool IsPointerType(StringView s) {
			if (s.StartsWith("$$Q"))
				return true;

			switch (s.First()) {
				case 'A': // foo &
				case 'P': // foo *
				case 'Q': // foo *const
				case 'R': // foo *volatile
				case 'S': // foo *const volatile
					return true;
			}
			return false;
		}

		FunctionRefQualifier DemangleFunctionRefQualifier(ref StringView mangledName) {
			if (mangledName.ConsumeFront('G'))
				return FunctionRefQualifier.Reference;
			else if (mangledName.ConsumeFront('H'))
				return FunctionRefQualifier.RValueReference;
			return FunctionRefQualifier.None;
		}

		(Qualifiers, PointerAffinity) DemanglePointerCVQualifiers(ref StringView mangledName) {
			if (mangledName.ConsumeFront("$$Q")) {
				return (Qualifiers.None, PointerAffinity.Reference);
			}

			switch (mangledName.PopFront()) {
				case 'A':
					return (Qualifiers.None, PointerAffinity.Reference);
				case 'P':
					return (Qualifiers.None, PointerAffinity.Pointer);
				case 'Q':
					return (Qualifiers.Const, PointerAffinity.Pointer);
				case 'R':
					return (Qualifiers.Volatile, PointerAffinity.Pointer);
				case 'S':
					return (Qualifiers.Const | Qualifiers.Volatile, PointerAffinity.Pointer);
				default:
					Debug.Assert(false); //Ty is not a pointer type
					break;
			}

			return (Qualifiers.None, PointerAffinity.Pointer);
		}

		StringView CopyString(StringView borrowed) => new StringView(borrowed);

		QualifiedNameNode DemangleNameScopeChain(ref StringView mangledName, IdentifierNode unqualifiedName) {
			NodeList head = new NodeList();
			head.Node = unqualifiedName;

			int count = 1;
			while (!mangledName.ConsumeFront("@")) {
				count++;
				NodeList newHead = new NodeList();
				newHead.Next = head;
				head = newHead;

				if (mangledName.IsEmpty) {
					SetError();
					return null;
				}

				Debug.Assert(!error);
				IdentifierNode elem = DemangleNameScopePiece(ref mangledName);
				if (error)
					return null;

				head.Node = elem;
			}

			QualifiedNameNode qn = new QualifiedNameNode();
			qn.Components = NodeListToNodeArray(head, count);
			return qn;
		}

		StructorIdentifierNode DemangleStructorIdentifier(ref StringView mangledName, bool isDestructor) {
			StructorIdentifierNode node = new StructorIdentifierNode();
			node.IsDestructor = isDestructor;
			return node;
		}

		ConversionOperatorIdentifierNode DemangleConversionOperatorIdentifier(ref StringView mangledName) {
			ConversionOperatorIdentifierNode node = new ConversionOperatorIdentifierNode();
			return node;
		}

		IdentifierNode DemangleFunctionIdentifierCode(ref StringView mangledName, FunctionIdentifierCodeGroup group) {
			char ch;
			switch (group) {
				case FunctionIdentifierCodeGroup.Basic:
					ch = mangledName.PopFront();
					switch (ch) {
						case '0':
						case '1':
							return DemangleStructorIdentifier(ref mangledName, ch == '1');
						case 'B':
							return DemangleConversionOperatorIdentifier(ref mangledName);
						default:
							return new IntrinsicFunctionIdentifierNode(TranslateIntrinsicFunctionCode(ch, group));
					}
				case FunctionIdentifierCodeGroup.Under:
					return new IntrinsicFunctionIdentifierNode(TranslateIntrinsicFunctionCode(mangledName.PopFront(), group));
				case FunctionIdentifierCodeGroup.DoubleUnder:
					ch = mangledName.PopFront();
					switch (ch) {
						case 'K':
							return DemangleLiteralOperatorIdentifier(mangledName);
						default:
							return new IntrinsicFunctionIdentifierNode(TranslateIntrinsicFunctionCode(ch, group));
					}
			}
			return null;
		}

		LiteralOperatorIdentifierNode DemangleLiteralOperatorIdentifier(StringView mangledName) {
			LiteralOperatorIdentifierNode node = new LiteralOperatorIdentifierNode();
			node.Name = DemangleSimpleString(ref mangledName, false);
			return node;
		}

		StringView DemangleSimpleString(ref StringView mangledName, bool memorize) {
			StringView s;
			for (int i = 0; i < mangledName.Length; i++) {
				if (mangledName[i] != '@')
					continue;
				s = mangledName.Substring(0, i);
				mangledName = mangledName.DropFront(i + 1);

				if (memorize)
					MemorizeString(s);
				return s;
			}

			SetError();
			return null;
		}

		IntrinsicFunctionKind TranslateIntrinsicFunctionCode(char ch, FunctionIdentifierCodeGroup group) {
			IntrinsicFunctionKind[] basic = new IntrinsicFunctionKind[]{
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.New,
				IntrinsicFunctionKind.Delete,
				IntrinsicFunctionKind.Assign,
				IntrinsicFunctionKind.RightShift,
				IntrinsicFunctionKind.LeftShift,
				IntrinsicFunctionKind.LogicalNot,
				IntrinsicFunctionKind.Equals,
				IntrinsicFunctionKind.NotEquals,
				IntrinsicFunctionKind.ArraySubscript,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.Pointer,
				IntrinsicFunctionKind.Dereference,
				IntrinsicFunctionKind.Increment,
				IntrinsicFunctionKind.Decrement,
				IntrinsicFunctionKind.Minus,
				IntrinsicFunctionKind.Plus,
				IntrinsicFunctionKind.BitwiseAnd,
				IntrinsicFunctionKind.MemberPointer,
				IntrinsicFunctionKind.Divide,
				IntrinsicFunctionKind.Modulus,
				IntrinsicFunctionKind.LessThan,
				IntrinsicFunctionKind.LessThanEqual,
				IntrinsicFunctionKind.GreaterThan,
				IntrinsicFunctionKind.GreaterThanEqual,
				IntrinsicFunctionKind.Comma,
				IntrinsicFunctionKind.Parens,
				IntrinsicFunctionKind.BitwiseNot,
				IntrinsicFunctionKind.BitwiseXor,
				IntrinsicFunctionKind.BitwiseOr,
				IntrinsicFunctionKind.LogicalAnd,
				IntrinsicFunctionKind.LogicalOr,
				IntrinsicFunctionKind.TimesEqual,
				IntrinsicFunctionKind.PlusEqual,
				IntrinsicFunctionKind.MinusEqual
			};

			IntrinsicFunctionKind[] under = new IntrinsicFunctionKind[]{
				IntrinsicFunctionKind.DivEqual,
				IntrinsicFunctionKind.ModEqual,
				IntrinsicFunctionKind.RshEqual,
				IntrinsicFunctionKind.LshEqual,
				IntrinsicFunctionKind.BitwiseAndEqual,
				IntrinsicFunctionKind.BitwiseOrEqual,
				IntrinsicFunctionKind.BitwiseXorEqual,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.VbaseDtor,
				IntrinsicFunctionKind.VecDelDtor,
				IntrinsicFunctionKind.DefaultCtorClosure,
				IntrinsicFunctionKind.ScalarDelDtor,
				IntrinsicFunctionKind.VecCtorIter,
				IntrinsicFunctionKind.VecDtorIter,
				IntrinsicFunctionKind.VecVbaseCtorIter,
				IntrinsicFunctionKind.VdispMap,
				IntrinsicFunctionKind.EHVecCtorIter,
				IntrinsicFunctionKind.EHVecDtorIter,
				IntrinsicFunctionKind.EHVecVbaseCtorIter,
				IntrinsicFunctionKind.CopyCtorClosure,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.LocalVftableCtorClosure,
				IntrinsicFunctionKind.ArrayNew,
				IntrinsicFunctionKind.ArrayDelete,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None
			};

			IntrinsicFunctionKind[] doubleUnder = new IntrinsicFunctionKind[] {
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.ManVectorCtorIter,
				IntrinsicFunctionKind.ManVectorDtorIter,
				IntrinsicFunctionKind.EHVectorCopyCtorIter,
				IntrinsicFunctionKind.EHVectorVbaseCopyCtorIter,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.VectorCopyCtorIter,
				IntrinsicFunctionKind.VectorVbaseCopyCtorIter,
				IntrinsicFunctionKind.ManVectorVbaseCopyCtorIter,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.CoAwait,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None,
				IntrinsicFunctionKind.None
			};

			int index = (ch >= '0' && ch <= '9') ? (ch - '0') : (ch - 'A' + 10);
			switch (group) {
				case FunctionIdentifierCodeGroup.Basic:
					return basic[index];
				case FunctionIdentifierCodeGroup.Under:
					return under[index];
				case FunctionIdentifierCodeGroup.DoubleUnder:
					return doubleUnder[index];
			}
			Debug.Assert(false);
			return IntrinsicFunctionKind.None;
		}

		IdentifierNode DemangleFunctionIdentifierCode(ref StringView mangledName) {
			Debug.Assert(mangledName.StartsWith('?'));
			mangledName = mangledName.DropFront();

			if (mangledName.ConsumeFront("__"))
				return DemangleFunctionIdentifierCode(ref mangledName, FunctionIdentifierCodeGroup.DoubleUnder);
			else if (mangledName.ConsumeFront('_'))
				return DemangleFunctionIdentifierCode(ref mangledName, FunctionIdentifierCodeGroup.Under);
			return DemangleFunctionIdentifierCode(ref mangledName, FunctionIdentifierCodeGroup.Basic);
		}

		IdentifierNode DemangleUnqualifiedSymbolName(ref StringView mangledName, NameBackrefBehaviour nbb) {
			if (StartsWithDigit(mangledName))
				return DemangleBackRefName(ref mangledName);
			if (mangledName.StartsWith("?$"))
				return DemangleTemplateInstantiationName(ref mangledName, nbb);
			if (mangledName.StartsWith('?'))
				return DemangleFunctionIdentifierCode(ref mangledName);
			return DemangleSimpleName(ref mangledName, nbb.HasFlag(NameBackrefBehaviour.Simple));
		}

		IdentifierNode DemangleTemplateInstantiationName(ref StringView mangledName, NameBackrefBehaviour nbb) {
			Debug.Assert(mangledName.StartsWith("?$"));
			mangledName.ConsumeFront("?$");

			BackrefContext outerContext = new BackrefContext();
			Utils.Swap(ref outerContext, ref backrefs);

			IdentifierNode identifier = DemangleUnqualifiedSymbolName(ref mangledName, NameBackrefBehaviour.Simple);
			if (!error)
				identifier.TemplateParams = DemangleTemplateParameterList(ref mangledName);

			Utils.Swap(ref outerContext, ref backrefs);
			if (error)
				return null;

			if (nbb.HasFlag(NameBackrefBehaviour.Template))
				MemorizeIdentifier(identifier);

			return identifier;
		}

		NodeArrayNode DemangleTemplateParameterList(ref StringView mangledName) {
			NodeList head = null;
			ref NodeList current = ref head;

			int count = 0;

			while (!error && !mangledName.StartsWith('@')) {
				if (mangledName.ConsumeFront("$S") || mangledName.ConsumeFront("$$V") ||
					mangledName.ConsumeFront("$$$V") || mangledName.ConsumeFront("$$Z")) {
					continue;
				}

				count++;
				current = new NodeList();
				NodeList tp = current;

				TemplateParameterReferenceNode tprn = null;
				if (mangledName.ConsumeFront("$$Y")) {
					tp.Node = DemangleFullyQualifiedTypeName(ref mangledName);
				} else if (mangledName.ConsumeFront("$$B")) {
					tp.Node = DemangleType(ref mangledName, QualifierMangleNode.Drop);
				} else if (mangledName.ConsumeFront("$$C")) {
					tp.Node = DemangleType(ref mangledName, QualifierMangleNode.Mangle);
				} else if (mangledName.StartsWith("$1") || mangledName.StartsWith("$H") ||
					mangledName.StartsWith("$I") || mangledName.StartsWith("$J")) {
					tp.Node = tprn = new TemplateParameterReferenceNode();
					tprn.IsMemberPointer = true;

					mangledName = mangledName.DropFront();
					char inheritanceSpecifier = mangledName.PopFront();
					SymbolNode s = null;
					if (mangledName.StartsWith('?')) {
						s = Parse(mangledName);
						MemorizeIdentifier(s.Name.UnqualifiedIdentifier);
					}

					switch (inheritanceSpecifier) {
						case 'J':
							tprn.ThunkOffsets[tprn.ThunkOffsetCount++] = DemangleSigned(ref mangledName);
							goto case 'I';
						case 'I':
							tprn.ThunkOffsets[tprn.ThunkOffsetCount++] = DemangleSigned(ref mangledName);
							goto case 'H';
						case 'H':
							tprn.ThunkOffsets[tprn.ThunkOffsetCount++] = DemangleSigned(ref mangledName);
							goto case '1';
						case '1':
							break;
						default:
							SetError();
							break;
					}
					tprn.Affinity = PointerAffinity.Pointer;
					tprn.Symbol = s;
				} else if (mangledName.StartsWith("$E?")) {
					mangledName.ConsumeFront("$E");
					tp.Node = new TemplateParameterReferenceNode();
					tprn.Symbol = Parse(mangledName);
					tprn.Affinity = PointerAffinity.Reference;
				} else if (mangledName.StartsWith("$F") || mangledName.StartsWith("$G")) {
					tp.Node = tprn = new TemplateParameterReferenceNode();

					mangledName = mangledName.DropFront();
					char inheritanceSpecifier = mangledName.PopFront();

					switch (inheritanceSpecifier) {
						case 'G':
							tprn.ThunkOffsets[tprn.ThunkOffsetCount++] = DemangleSigned(ref mangledName);
							goto case 'F';
						case 'F':
							tprn.ThunkOffsets[tprn.ThunkOffsetCount++] = DemangleSigned(ref mangledName);
							tprn.ThunkOffsets[tprn.ThunkOffsetCount++] = DemangleSigned(ref mangledName);
							goto case '0';
						case '0':
							break;
						default:
							SetError();
							break;
					}
					tprn.IsMemberPointer = true;
				} else if (mangledName.ConsumeFront("$0")) {
					bool isNegative = false;
					ulong value = 0;
					(value, isNegative) = DemangleNumber(ref mangledName);

					tp.Node = new IntegerLiteralNode(value, isNegative);
				} else {
					tp.Node = DemangleType(ref mangledName, QualifierMangleNode.Drop);
				}

				if (error)
					return null;

				current = ref tp.Next;
			}

			if (error)
				return null;

			if (mangledName.ConsumeFront('@'))
				return NodeListToNodeArray(head, count);

			SetError();
			return null;
		}

		void MemorizeIdentifier(IdentifierNode identifier) {
			OutputStream os = new OutputStream();
			identifier.Output(os, OutputFlags.Default);
			os.Append('\0');
			string name = os.ToString();

			StringView owned = new StringView(name);
			MemorizeString(owned);
		}

		void MemorizeString(StringView str) {
			if (backrefs.NamesCount >= BackrefContext.Max)
				return;

			for (int i = 0; i < backrefs.NamesCount; i++) {
				if (str == backrefs.Names[i].Name)
					return;
			}
			NamedIdentifierNode node = new NamedIdentifierNode();
			node.Name = str;
			backrefs.Names[backrefs.NamesCount++] = node;
		}

		NameIdentifierNode DemangleAnonymousNamespaceName(ref StringView mangledName) {
			Debug.Assert(mangledName.StartsWith("?A"));
			mangledName.ConsumeFront("?A");

			NameIdentifierNode node = new NameIdentifierNode();
			node.Name = "`anonymous namespace'";

			int endPos = mangledName.IndexOf('@');
			if (endPos < 0) {
				SetError();
				return null;
			}

			StringView namespaceKey = mangledName.Substring(0, endPos);
			MemorizeString(namespaceKey);
			mangledName = mangledName.Substring(endPos + 1);
			return node;
		}

		IdentifierNode DemangleNameScopePiece(ref StringView mangledName) {
			if (StartsWithDigit(mangledName))
				return DemangleBackRefName(ref mangledName);

			if (mangledName.StartsWith("?$"))
				return DemangleTemplateInstantiationName(ref mangledName, NameBackrefBehaviour.Template);

			if (mangledName.StartsWith("?A"))
				return DemangleAnonymousNamespaceName(ref mangledName);

			if (StartsWithLocalScopePattern(ref mangledName))
				return DemangleLocallyScopedNamePiece(ref mangledName);

			return DemangleSimpleName(ref mangledName, true);
		}

		long DemangleSigned(ref StringView mangledName) {
			bool isNegative = false;
			ulong number = 0;
			(number, isNegative) = DemangleNumber(ref mangledName);
			if (number > long.MaxValue)
				SetError();
			long i = (long)number;
			return isNegative ? -i : i;
		}


		FunctionSymbolNode DemangleFunctionEncoding(ref StringView mangledName) {
			FuncClass extraFlags = FuncClass.None;

			if (mangledName.ConsumeFront("$$J0"))
				extraFlags = FuncClass.ExternC;

			FuncClass fc = DemangleFunctionClass(ref mangledName);
			fc |= extraFlags;

			FunctionSignatureNode fsn = null;
			ThunkSignatureNode ttn = null;

			if (fc.HasFlag(FuncClass.StaticThisAdjust)) {
				ttn = new ThunkSignatureNode();
				ttn.ThisAdjust.StaticOffset = (uint)DemangleSigned(ref mangledName);
			} else if (fc.HasFlag(FuncClass.VirtualThisAdjust)) {
				ttn = new ThunkSignatureNode();
				if (fc.HasFlag(FuncClass.VirtualThisAdjustEx)) {
					ttn.ThisAdjust.VBPtrOffset = (int)DemangleSigned(ref mangledName);
					ttn.ThisAdjust.VBOffsetOffset = (int)DemangleSigned(ref mangledName);
				}
				ttn.ThisAdjust.VtordispOffset = (int)DemangleSigned(ref mangledName);
				ttn.ThisAdjust.StaticOffset = (uint)DemangleSigned(ref mangledName);
			}
			if (fc.HasFlag(FuncClass.NoParameterList)) {
				fsn = new FunctionSignatureNode();
			} else {
				bool hasThisQuals = !fc.HasFlag(FuncClass.Global) && !fc.HasFlag(FuncClass.Static);
				fsn = DemangleFunctionType(ref mangledName, hasThisQuals);
			}

			if (ttn != null) {
				ttn.Affinity = fsn.Affinity;
				ttn.CallConvention = fsn.CallConvention;
				ttn.FunctionClass = fsn.FunctionClass;
				ttn.IsVariadic = fsn.IsVariadic;
				ttn.Kind = fsn.Kind;
				ttn.Params = fsn.Params;
				ttn.Quals = fsn.Quals;
				ttn.RefQualifier = fsn.RefQualifier;
				ttn.ReturnType = fsn.ReturnType;
			}

			fsn.FunctionClass = fc;

			FunctionSymbolNode symbol = new FunctionSymbolNode();
			symbol.Signature = fsn;
			return symbol;
		}

		VariableSymbolNode DemangleVariableEncoding(ref StringView mangledName, StorageClass sc) {
			VariableSymbolNode vsn = new VariableSymbolNode();
			vsn.Type = DemangleType(ref mangledName, QualifierMangleNode.Drop);
			vsn.sc = sc;

			switch (vsn.Type.Kind) {
				case NodeKind.PointerType:
					PointerTypeNode ptn = (PointerTypeNode)(vsn.Type);
					Qualifiers extraChildQuals = Qualifiers.None;
					ptn.Quals = vsn.Type.Quals | DemanglePointerExtQualifiers(ref mangledName);

					bool isMember = false;
					(extraChildQuals, isMember) = DemangleQualifiers(ref mangledName);

					if (ptn.ClassParent != null) {
						QualifiedNameNode backRefName = DemangleFullyQualifiedTypeName(ref mangledName);
					}
					ptn.Pointee.Quals = ptn.Pointee.Quals | extraChildQuals;
					break;
				default:
					vsn.Type.Quals = DemangleQualifiers(ref mangledName).Item1;
					break;
			}
			return vsn;
		}

		SymbolNode DemangleEncodedSymbol(ref StringView mangledName, QualifiedNameNode name) {
			switch (mangledName.First()) {
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
					StorageClass sc = DemangleVariableStorageClass(ref mangledName);
					return DemangleVariableEncoding(ref mangledName, sc);
				case '8':
					return null;
			}
			FunctionSymbolNode fsn = DemangleFunctionEncoding(ref mangledName);

			IdentifierNode uqn = name.UnqualifiedIdentifier;
			if (uqn.Kind == NodeKind.ConversionOperatorIdentifier) {
				ConversionOperatorIdentifierNode coin = (ConversionOperatorIdentifierNode)uqn;
				coin.TargetType = fsn.Signature.ReturnType;
			}

			return fsn;
		}

		NamedIdentifierNode SynthesizeNamedIdentifier(StringView name) {
			NamedIdentifierNode id = new NamedIdentifierNode();
			id.Name = name;
			return id;
		}

		VariableSymbolNode DemangleUntypedVariable(ref StringView mangledName, StringView variableName) {
			NamedIdentifierNode ni = SynthesizeNamedIdentifier(variableName);
			QualifiedNameNode qn = DemangleNameScopeChain(ref mangledName, ni);
			VariableSymbolNode vsn = new VariableSymbolNode();
			vsn.Name = qn;
			if (mangledName.ConsumeFront('8'))
				return vsn;

			SetError();
			return null;
		}

		
		QualifiedNameNode SynthesizeQualifiedName(StringView name) {
			NamedIdentifierNode id = SynthesizeNamedIdentifier(name);
			return SynthesizeQualifiedName(id);
		}

		ulong DemangleUnsigned(ref StringView mangledName) {
			bool isNegative = false;
			ulong number = 0;
			(number, isNegative) = DemangleNumber(ref mangledName);
			if (isNegative)
				SetError();
			return number;
		}

		LocalStaticGuardVariableNode DemangleLocalStaticGuard(ref StringView mangledName) {
			LocalStaticGuardIdentifierNode lsgi = new LocalStaticGuardIdentifierNode();
			QualifiedNameNode qn = DemangleNameScopeChain(ref mangledName, lsgi);
			LocalStaticGuardVariableNode lsgvn = new LocalStaticGuardVariableNode();

			lsgvn.Name = qn;
			if (mangledName.ConsumeFront("4IA")) {
				lsgvn.IsVisible = false;
			} else if (mangledName.ConsumeFront('5')) {
				lsgvn.IsVisible = true;
			} else {
				SetError();
				return null;
			}

			if (mangledName.IsEmpty) {
				lsgi.ScopeIndex = (uint)DemangleUnsigned(ref mangledName);
			}
			return lsgvn;
		}

		static void WriteHexDigit(ref char buffer, byte digit) {
			Debug.Assert(digit <= 15);
			buffer = (char)((digit < 10) ? ('0' + digit) : ('a' + digit - 10));
		}

		static void OutputHex(OutputStream os, uint c) {
			if (c == 0) {
				os.Append("\\x00");
				return;
			}

			char[] tempBuffer = new char[17];
			int maxPos = 15;

			int pos = maxPos - 1;
			while (c != 0) {
				for (int i = 0; i < 2; i++) {
					WriteHexDigit(ref tempBuffer[pos--], (byte)(c % 16));
					c /= 16;
				}
				tempBuffer[pos--] = 'x';
				tempBuffer[pos--] = '\\';
				Debug.Assert(pos >= 0);
			}
			os.Append(new StringView(tempBuffer).ToString());
		}

		uint CountTrailingNullBytes(byte[] stringBytes, int length) {
			uint count = 0;
			int i = 0;
			while (length > 0 && stringBytes[length - 1 - i] == 0) {
				length--;
				count++;
			}
			return count;
		}

		uint CountEmbeddedNulls(byte[] stringBytes, int length) {
			uint result = 0;
			for (uint i = 0; i < length; i++) {
				if (stringBytes[i] == 0)
					result++;
			}
			return result;
		}

		int GuessCharByteSize(byte[] stringBytes, int numChars, int numBytes) {
			Debug.Assert(numBytes > 0);
			if (numBytes % 2 == 1)
				return 1;

			if (numBytes < 32) {
				uint trailingNulls = CountTrailingNullBytes(stringBytes, numChars);
				if (trailingNulls >= 4)
					return 4;
				if (trailingNulls >= 2)
					return 2;
				return 1;
			}

			uint nulls = CountEmbeddedNulls(stringBytes, numChars);
			if (nulls >= 2 * numChars / 3)
				return 4;
			if (nulls >= numChars / 3)
				return 2;
			return 1;
		}

		bool IsRebasedHexDigit(char c) {
			return c >= 'A' && c <= 'P';
		}

		byte RebasedHexDigitToNumber(char c) {
			Debug.Assert(IsRebasedHexDigit(c));
			return (byte)((c <= 'J') ? (c - 'A') : (10 + c - 'K'));
		}

		void OutputEscapedChar(OutputStream os, uint c) {
			switch (c) {
				case '\'': // single quote
					os.Append("\\\'");
					return;
				case '\"': // double quote
					os.Append("\\\"");
					return;
				case '\\': // backslash
					os.Append("\\\\");
					return;
				case '\a': // bell
					os.Append("\\a");
					return;
				case '\b': // backspace
					os.Append("\\b");
					return;
				case '\f': // form feed
					os.Append("\\f");
					return;
				case '\n': // new line
					os.Append("\\n");
					return;
				case '\r': // carriage return
					os.Append("\\r");
					return;
				case '\t': // tab
					os.Append("\\t");
					return;
				case '\v': // vertical tab
					os.Append("\\v");
					return;
				default:
					break;
			}

			if (c > 0x1f && c < 0x7f) {
				os.Append((char)c);
				return;
			}

			OutputHex(os, c);
		}

		char DemangleCharLiteral(ref StringView mangledName) {
			if (!mangledName.StartsWith('?'))
				return mangledName.PopFront();

			mangledName = mangledName.DropFront();
			if (mangledName.IsEmpty)
				goto charLiteralError;

			if (mangledName.ConsumeFront('$')) {
				if (mangledName.Length < 2)
					goto charLiteralError;

				StringView nibbles = mangledName.Substring(0, 2);
				if (!IsRebasedHexDigit(nibbles[0]) || !IsRebasedHexDigit(nibbles[1])) {
					goto charLiteralError;
				}

				byte c1 = RebasedHexDigitToNumber(nibbles[0]);
				byte c2 = RebasedHexDigitToNumber(nibbles[1]);
				mangledName = mangledName.DropFront(2);
				return (char)((byte)((c1 << 4) | c2));
			}

			if (StartsWithDigit(mangledName)) {
				string lookup = ",/\\:. \n\t'-";
				char c = lookup[mangledName[0] - '0'];
				mangledName = mangledName.DropFront();
				return c;
			}

			if (mangledName[0] >= 'a' && mangledName[0] <= 'z') {
				char[] lookup = new char[] {
					'\xE1', '\xE2', '\xE3', '\xE4', '\xE5', '\xE6', '\xE7',
					'\xE8', '\xE9', '\xEA', '\xEB', '\xEC', '\xED', '\xEE',
					'\xEF', '\xF0', '\xF1', '\xF2', '\xF3', '\xF4', '\xF5',
					'\xF6', '\xF7', '\xF8', '\xF9', '\xFA'
				};
				char c = lookup[mangledName[0] - 'a'];
				mangledName = mangledName.DropFront();
				return c;
			}

			if (mangledName[0] >= 'A' && mangledName[0] <= 'Z') {
				char[] lookup = new char[] {
					'\xC1', '\xC2', '\xC3', '\xC4', '\xC5', '\xC6', '\xC7',
					'\xC8', '\xC9', '\xCA', '\xCB', '\xCC', '\xCD', '\xCE',
					'\xCF', '\xD0', '\xD1', '\xD2', '\xD3', '\xD4', '\xD5',
					'\xD6', '\xD7', '\xD8', '\xD9', '\xDA'
				};
				char c = lookup[mangledName[0] - 'A'];
				mangledName = mangledName.DropFront();
				return c;
			}

			charLiteralError:
			SetError();
			return '\0';
		}

		char DemangleWcharLiteral(ref StringView mangledName) {
			char c1, c2;

			c1 = DemangleCharLiteral(ref mangledName);
			if (error)
				goto wcharLiteralError;
			c2 = DemangleCharLiteral(ref mangledName);
			if (error)
				goto wcharLiteralError;

			return Convert.ToChar((ushort)(Convert.ToByte(c1) << 8 | Convert.ToByte(c2)));

			wcharLiteralError:
			SetError();
			return '\0';
		}

		uint DecodeMultiByteChar(byte[] stringBytes, int charIndex, int charBytes) {
			Debug.Assert(charBytes == 1 || charBytes == 2 || charBytes == 4);
			int offset = charIndex * charBytes;
			uint result = 0;

			for (int i = 0; i < charBytes; i++) {
				uint c = (uint)(stringBytes[offset + i]);
				result |= (uint)((int)c << (8 * i));
			}
			return result;
		}

		EncodedStringLiteralNode DemangleStringLiteral(ref StringView mangledName) {
			OutputStream os = new OutputStream();
			StringView crc;
			int stringByteSize;
			bool isWcharT = false;
			bool isNegative = false;
			int crcEndPos = 0;
			string resultBuffer;

			EncodedStringLiteralNode result = new EncodedStringLiteralNode();
			if (mangledName.ConsumeFront("@_"))
				goto stringLiteralError;
			if (mangledName.IsEmpty)
				goto stringLiteralError;

			switch (mangledName.PopFront()) {
				case '1':
					isWcharT = true;
					goto case '0';
				case '0':
					break;
				default:
					goto stringLiteralError;
			}

			ulong stringByteSizeTmp;
			(stringByteSizeTmp, isNegative) = DemangleNumber(ref mangledName);

			stringByteSize = (int)stringByteSizeTmp;
			if (error || isNegative)
				goto stringLiteralError;

			crcEndPos = mangledName.IndexOf('@');
			if (crcEndPos < 0)
				goto stringLiteralError;

			crc = mangledName.Substring(0, crcEndPos);
			mangledName = mangledName.DropFront(crcEndPos + 1);
			if (mangledName.IsEmpty)
				goto stringLiteralError;

			if (isWcharT) {
				result.Char = CharKind.Wchar;
				if (stringByteSize > 64)
					result.IsTruncated = true;

				while (!mangledName.ConsumeFront('@')) {
					Debug.Assert(stringByteSize >= 2);

					char w = DemangleWcharLiteral(ref mangledName);
					if (stringByteSize != 2 || result.IsTruncated) {
						OutputEscapedChar(os, w);
					}
					stringByteSize -= 2;
					if (error)
						goto stringLiteralError;
				}
			} else {
				uint maxStringByteLength = 32 * 4;
				byte[] stringBytes = new byte[maxStringByteLength];

				int bytesDecoded = 0;
				while (!mangledName.ConsumeFront('@')) {
					Debug.Assert(stringByteSize >= 1);
					stringBytes[bytesDecoded++] = Convert.ToByte(DemangleCharLiteral(ref mangledName));
				}

				if (stringByteSize > bytesDecoded)
					result.IsTruncated = true;

				int charBytes = GuessCharByteSize(stringBytes, bytesDecoded, stringByteSize);
				Debug.Assert(stringByteSize % charBytes == 0);
				switch (charBytes) {
					case 1:
						result.Char = CharKind.Char;
						break;
					case 2:
						result.Char = CharKind.Char16;
						break;
					case 4:
						result.Char = CharKind.Char32;
						break;
					default:
						Debug.Assert(false);
						break;
				}

				int numChars = bytesDecoded / charBytes;
				for (int charIndex = 0; charIndex < numChars; charIndex++) {
					uint nextChar = DecodeMultiByteChar(stringBytes, charIndex, charBytes);
					if (charIndex + 1 < numChars || result.IsTruncated)
						OutputEscapedChar(os, nextChar);
				}
			}

			os.Append('\0');
			resultBuffer = os.ToString();
			result.DecodedString = CopyString(resultBuffer);
			return result;

			stringLiteralError:
			SetError();
			return null;
		}

		VariableSymbolNode SynthesizeVariable(TypeNode type, StringView variableName) {
			VariableSymbolNode vsn = new VariableSymbolNode();
			vsn.Type = type;
			vsn.Name = SynthesizeQualifiedName(variableName);
			return vsn;
		}

		VariableSymbolNode DemangleRttiBaseClassDescriptorNode(ref StringView mangledName) {
			RttiBaseClassDescriptorNode rbcdn = new RttiBaseClassDescriptorNode();
			rbcdn.NVOffset = (uint)DemangleUnsigned(ref mangledName);
			rbcdn.VBPtrOffset = (int)DemangleUnsigned(ref mangledName);
			rbcdn.VBTableOffset = (uint)DemangleUnsigned(ref mangledName);
			rbcdn.Flags = (uint)DemangleUnsigned(ref mangledName);
			if (error)
				return null;

			VariableSymbolNode vsn = new VariableSymbolNode();
			vsn.Name = DemangleNameScopeChain(ref mangledName, rbcdn);
			mangledName.ConsumeFront('8');
			return vsn;
		}

		FunctionSymbolNode DemangleVcallThunkNode(ref StringView mangledName) {
			FunctionSymbolNode fsn = new FunctionSymbolNode();
			VcallThunkIdentifierNode vtin = new VcallThunkIdentifierNode();
			fsn.Signature = new ThunkSignatureNode();
			fsn.Signature.FunctionClass = FuncClass.NoParameterList;

			fsn.Name = DemangleNameScopeChain(ref mangledName, vtin);
			if (!error)
				error = !mangledName.ConsumeFront("$B");

			if (!error)
				vtin.OffsetInVtable = DemangleUnsigned(ref mangledName);

			if (!error)
				error = !mangledName.ConsumeFront('A');

			if (!error)
				fsn.Signature.CallConvention = DemangleCallingConvention(ref mangledName);


			return (error) ? null : fsn;

		}

		SymbolNode DemangleSpecialIntrinsic(ref StringView mangledName) {
			SpecialIntrinsicKind sik = ConsumeSpecialIntrinsicKind(ref mangledName);
			if (sik == SpecialIntrinsicKind.None)
				return null;

			switch (sik) {
				case SpecialIntrinsicKind.StringLiteralSymbol:
					return DemangleStringLiteral(ref mangledName);
				case SpecialIntrinsicKind.Vftable:
				case SpecialIntrinsicKind.Vbtable:
				case SpecialIntrinsicKind.LocalVftable:
				case SpecialIntrinsicKind.RttiCompleteObjLocator:
					return DemangleSpecialTableSymbolNode(ref mangledName, sik);
				case SpecialIntrinsicKind.VcallThunk:
					return DemangleVcallThunkNode(ref mangledName);
				case SpecialIntrinsicKind.LocalStaticGuard:
					return DemangleLocalStaticGuard(ref mangledName);
				case SpecialIntrinsicKind.RttiTypeDescriptor:
					TypeNode t = DemangleType(ref mangledName, QualifierMangleNode.Result);
					if (error)
						break;
					if (!mangledName.ConsumeFront("@8"))
						break;
					if (!mangledName.IsEmpty)
						break;
					return SynthesizeVariable(t, "`RTTI Type Descriptor'");
				case SpecialIntrinsicKind.RttiBaseClassArray:
					return DemangleUntypedVariable(ref mangledName, "`RTTI Base Class Array'");
				case SpecialIntrinsicKind.RttiClassHierarchyDescriptor:
					return DemangleUntypedVariable(ref mangledName, "`RTTI Class Hierarchy Descriptor'");
				case SpecialIntrinsicKind.RttiBaseClassDescriptor:
					return DemangleRttiBaseClassDescriptorNode(ref mangledName);
				case SpecialIntrinsicKind.DynamicInitializer:
					return DemangleInitFiniStub(ref mangledName, false);
				case SpecialIntrinsicKind.DynamicAtexitDestructor:
					return DemangleInitFiniStub(ref mangledName, true);
				default:
					break;
			}
			SetError();
			return null;
		}

		QualifiedNameNode DemangleFullyQualifiedSymbolName(ref StringView mangledName) {
			IdentifierNode identifier = DemangleUnqualifiedSymbolName(ref mangledName, NameBackrefBehaviour.Simple);
			if (error)
				return null;

			QualifiedNameNode qn = DemangleNameScopeChain(ref mangledName, identifier);
			if (error)
				return null;

			if (identifier.Kind == NodeKind.StructorIdentifier) {
				StructorIdentifierNode sin = (StructorIdentifierNode)identifier;
				Debug.Assert(qn.Components.Count() >= 2);
				Node classNode = qn.Components.Nodes[qn.Components.Count() - 2];
				sin.Class = (IdentifierNode)classNode;
			}
			Debug.Assert(qn != null);
			return qn;
		}

		FunctionSymbolNode DemangleInitFiniStub(ref StringView mangledName, bool isDestructor) {
			DynamicStructorIdentifierNode dsin = new DynamicStructorIdentifierNode();
			dsin.IsDestructor = isDestructor;

			bool isKnownStaticDataMember = false;
			if (mangledName.ConsumeFront('?'))
				isKnownStaticDataMember = true;

			QualifiedNameNode qn = DemangleFullyQualifiedSymbolName(ref mangledName);

			SymbolNode symbol = DemangleEncodedSymbol(ref mangledName, qn);
			FunctionSymbolNode fsn = null;
			symbol.Name = qn;

			if (symbol.Kind == NodeKind.VariableSymbol) {
				dsin.Variable = (VariableSymbolNode)symbol;

				int atCount = isKnownStaticDataMember ? 2 : 1;
				for (int i = 0; i < atCount; i++) {
					if (mangledName.ConsumeFront('@'))
						continue;
					SetError();
					return null;
				}

				fsn = DemangleFunctionEncoding(ref mangledName);
				fsn.Name = SynthesizeQualifiedName(dsin);
			} else {
				if (isKnownStaticDataMember) {
					SetError();
					return null;
				}

				fsn = (FunctionSymbolNode)symbol;
				dsin.Name = symbol.Name;
				fsn.Name = SynthesizeQualifiedName(dsin);
			}

			return fsn;
		}

		QualifiedNameNode SynthesizeQualifiedName(IdentifierNode identifier) {
			QualifiedNameNode qn = new QualifiedNameNode();
			qn.Components = new NodeArrayNode();
			qn.Components.Nodes = new Node[1];
			qn.Components.Nodes[0] = identifier;
			return qn;
		}

		SymbolNode Parse(StringView mangledName) {
			if (mangledName.StartsWith("??@")) {
				SymbolNode s = new SymbolNode(NodeKind.Md5Symbol);
				s.Name = SynthesizeQualifiedName(mangledName);
				return s;
			}

			if (!mangledName.StartsWith('?')) {
				SetError();
				return null;
			}

			mangledName.ConsumeFront('?');

			SymbolNode si = DemangleSpecialIntrinsic(ref mangledName);
			if (si != null) {
				return si;
			}

			QualifiedNameNode qn = DemangleFullyQualifiedTypeName(ref mangledName);
			if (error)
				return null;

			SymbolNode symbol = DemangleEncodedSymbol(ref mangledName, qn);
			if (symbol != null) {
				symbol.Name = qn;
			}

			if (error)
				return null;

			return symbol;
		}

		[Flags]
		public enum MSDemangleFlags
		{
			None,
			DumpBackrefs
		}

		public enum DemangleStatus
		{
			UnknownError = -4,
			InvalidArgs = -4,
			InvalidMangledName = -2,
			MemoryAllocFailure = -1,
			Success = 0
		}

		void DumpBackReferences() {
			Console.WriteLine($"{backrefs.FunctionParamCount} function parameter backreferences");

			for(int i=0; i<backrefs.FunctionParamCount; i++) {
				OutputStream os = new OutputStream();
				TypeNode t = backrefs.FunctionParams[i];
				t.Output(os, OutputFlags.Default);

				Console.WriteLine($"  [{i}] - {os.ToString()}");
			}

			if (backrefs.FunctionParamCount > 0)
				Console.WriteLine();

			Console.WriteLine($"{backrefs.NamesCount} name backreferences");
			for(int i=0; i<backrefs.NamesCount; i++) {
				Console.WriteLine($"  [{i}] - {backrefs.Names[i].Name.ToString()}");
			}

			if (backrefs.NamesCount > 0)
				Console.WriteLine();
		}

		public static string Demangle(string mangledName, out DemangleStatus status, MSDemangleFlags flags = MSDemangleFlags.None) {
			status = DemangleStatus.Success;

			MicrosoftDemangler demangler = new MicrosoftDemangler();

			StringView name = new StringView(mangledName);
			SymbolNode ast = demangler.Parse(name);
			OutputStream buf = new OutputStream();

			if (flags.HasFlag(MSDemangleFlags.DumpBackrefs))
				demangler.DumpBackReferences();

			if (demangler.error)
				status = DemangleStatus.InvalidMangledName;
			else {
				ast.Output(buf, OutputFlags.Default);
				buf.Append('\0');
			}

			return status == DemangleStatus.Success ? buf.ToString() : null;

		}

		NamedIdentifierNode DemangleLocallyScopedNamePiece(ref StringView mangledName) {
			Debug.Assert(StartsWithLocalScopePattern(ref mangledName));

			NamedIdentifierNode identifier = new NamedIdentifierNode();
			mangledName.ConsumeFront('?');

			var number = DemangleNumber(ref mangledName);
			Debug.Assert(!number.Item2);

			mangledName.ConsumeFront('?');

			Debug.Assert(!error);
			Node scope = Parse(mangledName);
			if (error)
				return null;

			OutputStream os = new OutputStream();
			os.Append('`');
			scope.Output(os, OutputFlags.Default);
			os.Append('\'');
			os.Append("::`");
			os.Append(number.Item1);
			os.Append('\'');

			string result = os.ToString();
			identifier.Name = result;
			return identifier;
		}

		NamedIdentifierNode DemangleSimpleName(ref StringView mangledName, bool memorize) {
			StringView s = DemangleSimpleString(ref mangledName, memorize);
			if (error)
				return null;

			NamedIdentifierNode name = new NamedIdentifierNode();
			name.Name = s;
			return name;
		}

		NodeArrayNode NodeListToNodeArray(NodeList head, int count) {
			NodeArrayNode n = new NodeArrayNode();
			n.Nodes = new Node[count];
			for (int i = 0; i < count; i++) {
				n.Nodes[i] = head.Node;
				head = head.Next;
			}
			return n;
		}

		FuncClass DemangleFunctionClass(ref StringView mangledName) {
			switch (mangledName.PopFront()) {
				case '9':
					return FuncClass.ExternC | FuncClass.NoParameterList;
				case 'A':
					return FuncClass.Private;
				case 'B':
					return FuncClass.Private | FuncClass.Far;
				case 'C':
					return FuncClass.Private | FuncClass.Static;
				case 'D':
					return FuncClass.Private | FuncClass.Static;
				case 'E':
					return FuncClass.Private | FuncClass.Virtual;
				case 'F':
					return FuncClass.Private | FuncClass.Virtual;
				case 'G':
					return FuncClass.Private | FuncClass.StaticThisAdjust;
				case 'H':
					return FuncClass.Private | FuncClass.StaticThisAdjust | FuncClass.Far;
				case 'I':
					return FuncClass.Protected;
				case 'J':
					return FuncClass.Protected | FuncClass.Far;
				case 'K':
					return FuncClass.Protected | FuncClass.Static;
				case 'L':
					return FuncClass.Protected | FuncClass.Static | FuncClass.Far;
				case 'M':
					return FuncClass.Protected | FuncClass.Virtual;
				case 'N':
					return FuncClass.Protected | FuncClass.Virtual | FuncClass.Far;
				case 'O':
					return FuncClass.Protected | FuncClass.Virtual | FuncClass.StaticThisAdjust;
				case 'P':
					return FuncClass.Protected | FuncClass.Virtual | FuncClass.StaticThisAdjust | FuncClass.Far;
				case 'Q':
					return FuncClass.Public;
				case 'R':
					return FuncClass.Public | FuncClass.Far;
				case 'S':
					return FuncClass.Public | FuncClass.Static;
				case 'T':
					return FuncClass.Public | FuncClass.Static | FuncClass.Far;
				case 'U':
					return FuncClass.Public | FuncClass.Virtual;
				case 'V':
					return FuncClass.Public | FuncClass.Virtual | FuncClass.Far;
				case 'W':
					return FuncClass.Public | FuncClass.Virtual | FuncClass.StaticThisAdjust;
				case 'X':
					return FuncClass.Public | FuncClass.Virtual | FuncClass.StaticThisAdjust | FuncClass.Far;
				case 'Y':
					return FuncClass.Global;
				case 'Z':
					return FuncClass.Global | FuncClass.Far;
				case '$':
					FuncClass vflag = FuncClass.VirtualThisAdjust;
					if (mangledName.ConsumeFront('R'))
						vflag |= FuncClass.VirtualThisAdjustEx;

					switch (mangledName.PopFront()) {
						case '0':
							return FuncClass.Private | FuncClass.Virtual | vflag;
						case '1':
							return FuncClass.Private | FuncClass.Virtual | vflag | FuncClass.Far;
						case '2':
							return FuncClass.Protected | FuncClass.Virtual | vflag;
						case '3':
							return FuncClass.Protected | FuncClass.Virtual | vflag | FuncClass.Far;
						case '4':
							return FuncClass.Public | FuncClass.Virtual | vflag;
						case '5':
							return FuncClass.Public | FuncClass.Virtual | vflag | FuncClass.Far;
					}
					break;
			}

			SetError();
			return FuncClass.Public;
		}

		CallingConv DemangleCallingConvention(ref StringView mangledName) {
			switch (mangledName.PopFront()) {
				case 'A':
				case 'B':
					return CallingConv.Cdecl;
				case 'C':
				case 'D':
					return CallingConv.Pascal;
				case 'E':
				case 'F':
					return CallingConv.Thiscall;
				case 'G':
				case 'H':
					return CallingConv.Stdcall;
				case 'I':
				case 'J':
					return CallingConv.Fastcall;
				case 'M':
				case 'N':
					return CallingConv.Clrcall;
				case 'O':
				case 'P':
					return CallingConv.Eabi;
				case 'Q':
					return CallingConv.Vectorcall;
			}
			return CallingConv.None;
		}

		StorageClass DemangleVariableStorageClass(ref StringView mangledName) {
			Debug.Assert(char.IsDigit(mangledName.First()));

			switch (mangledName.PopFront()) {
				case '0':
					return StorageClass.PrivateStatic;
				case '1':
					return StorageClass.ProtectedStatic;
				case '2':
					return StorageClass.PublicStatic;
				case '3':
					return StorageClass.Global;
				case '4':
					return StorageClass.FunctionLocalStatic;
			}
			SetError();
			return StorageClass.None;
		}

		(Qualifiers, bool) DemangleQualifiers(ref StringView mangledName) {
			switch (mangledName.PopFront()) {
				case 'Q':
					return (Qualifiers.None, true);
				case 'R':
					return (Qualifiers.Const, true);
				case 'S':
					return (Qualifiers.Volatile, true);
				case 'T':
					return (Qualifiers.Const | Qualifiers.Volatile, true);
				case 'A':
					return (Qualifiers.None, false);
				case 'B':
					return (Qualifiers.Const, false);
				case 'C':
					return (Qualifiers.Volatile, false);
				case 'D':
					return (Qualifiers.Const | Qualifiers.Volatile, false);
			}
			SetError();
			return (Qualifiers.None, false);
		}

		(ulong, bool) DemangleNumber(ref StringView mangledName) {
			bool isNegative = mangledName.ConsumeFront('?');

			ulong ret = 0;
			if (StartsWithDigit(mangledName)) {
				ret = (ulong)(mangledName[0] - '0') + 1;
				return (ret, isNegative);
			}

			for (int i = 0; i < mangledName.Length; i++) {
				char c = mangledName[i];
				if (c == '@') {
					mangledName = mangledName.DropFront(i + 1);
					return (ret, isNegative);
				}
				if ('A' <= c && c <= 'P') {
					ret = (ret << 4) + (ulong)(c - 'A');
					continue;
				}
				break;
			}
			SetError();
			return (0, false);
		}

		ArrayTypeNode DemangleArrayType(ref StringView mangledName) {
			Debug.Assert(mangledName.First() == 'Y');
			mangledName.PopFront();

			ulong rank = 0;
			bool isNegative = false;
			(rank, isNegative) = DemangleNumber(ref mangledName);
			if (isNegative || rank == 0) {
				SetError();
				return null;
			}

			ArrayTypeNode aty = new ArrayTypeNode();
			NodeList head = new NodeList();
			NodeList tail = new NodeList();

			for (ulong i = 0; i < rank; i++) {
				ulong d = 0;
				(d, isNegative) = DemangleNumber(ref mangledName);
				if (isNegative) {
					SetError();
					return null;
				}
				tail.Node = new IntegerLiteralNode(d, isNegative);
				if (i + 1 < rank) {
					tail.Next = new NodeList();
					tail = tail.Next;
				}
			}
			aty.Dimensions = NodeListToNodeArray(head, (int)rank);

			if (mangledName.ConsumeFront("$$C")) {
				bool isMember = false;
				(aty.Quals, isMember) = DemangleQualifiers(ref mangledName);
				if (isMember) {
					SetError();
					return null;
				}
			}
			aty.ElementType = DemangleType(ref mangledName, QualifierMangleNode.Drop);
			return aty;
		}

		CustomTypeNode DemangleCustomType(ref StringView mangledName) {
			Debug.Assert(mangledName.StartsWith('?'));
			mangledName.PopFront();

			CustomTypeNode ctn = new CustomTypeNode();
			ctn.Identifier = DemangleUnqualifiedTypeName(ref mangledName, true);

			if (!mangledName.ConsumeFront('@'))
				SetError();

			if (error)
				return null;
			return ctn;
		}

		TypeNode DemangleType(ref StringView mangledName, QualifierMangleNode qmm) {
			Qualifiers quals = Qualifiers.None;
			bool isMember = false;

			if (qmm == QualifierMangleNode.Mangle) {
				(quals, isMember) = DemangleQualifiers(ref mangledName);
			} else if (qmm == QualifierMangleNode.Result) {
				if (mangledName.ConsumeFront('?'))
					(quals, isMember) = DemangleQualifiers(ref mangledName);
			}

			TypeNode ty = null;
			if (IsTagType(mangledName))
				ty = DemangleClassType(ref mangledName);
			else if (IsPointerType(mangledName)) {
				if (IsMemberPointer(mangledName, out error))
					ty = DemangleMemberPointerType(mangledName);
				else if (!error)
					ty = DemanglePointerType(ref mangledName);
				else
					return null;
			} else if (IsArrayType(mangledName)) {
				ty = DemangleArrayType(ref mangledName);
			} else if (IsFunctionType(mangledName)) {
				if (mangledName.ConsumeFront("$$A8@@"))
					ty = DemangleFunctionType(ref mangledName, true);
				else {
					Debug.Assert(mangledName.StartsWith("$$A6"));
					mangledName.ConsumeFront("$$A6");
					ty = DemangleFunctionType(ref mangledName, false);
				}
			} else if (IsCustomType(mangledName)) {
				ty = DemangleCustomType(ref mangledName);
			} else {
				ty = DemanglePrimitiveType(ref mangledName);
			}

			if (ty == null || error)
				return ty;

			ty.Quals = ty.Quals | quals;
			return ty;
		}

		PrimitiveTypeNode DemanglePrimitiveType(ref StringView manglefdName) {
			if (manglefdName.ConsumeFront("$$T")) {
				return new PrimitiveTypeNode(PrimitiveKind.Nullptr);
			}

			switch (manglefdName.PopFront()) {
				case 'X':
					return new PrimitiveTypeNode(PrimitiveKind.Void);
				case 'D':
					return new PrimitiveTypeNode(PrimitiveKind.Char);
				case 'C':
					return new PrimitiveTypeNode(PrimitiveKind.Schar);
				case 'E':
					return new PrimitiveTypeNode(PrimitiveKind.Uchar);
				case 'F':
					return new PrimitiveTypeNode(PrimitiveKind.Short);
				case 'G':
					return new PrimitiveTypeNode(PrimitiveKind.Ushort);
				case 'H':
					return new PrimitiveTypeNode(PrimitiveKind.Int);
				case 'I':
					return new PrimitiveTypeNode(PrimitiveKind.Uint);
				case 'J':
					return new PrimitiveTypeNode(PrimitiveKind.Long);
				case 'K':
					return new PrimitiveTypeNode(PrimitiveKind.Ulong);
				case 'M':
					return new PrimitiveTypeNode(PrimitiveKind.Float);
				case 'N':
					return new PrimitiveTypeNode(PrimitiveKind.Double);
				case 'O':
					return new PrimitiveTypeNode(PrimitiveKind.Ldouble);
				case '_':
					if (manglefdName.IsEmpty) {
						SetError();
						return null;
					}
					switch (manglefdName.PopFront()) {
						case 'N':
							return new PrimitiveTypeNode(PrimitiveKind.Bool);
						case 'J':
							return new PrimitiveTypeNode(PrimitiveKind.Int64);
						case 'K':
							;
							return new PrimitiveTypeNode(PrimitiveKind.Uint64);
						case 'W':
							return new PrimitiveTypeNode(PrimitiveKind.Wchar);
						case 'S':
							return new PrimitiveTypeNode(PrimitiveKind.Char16);
						case 'U':
							return new PrimitiveTypeNode(PrimitiveKind.Char32);
					}
					break;
			}
			SetError();
			return null;
		}

		TagTypeNode DemangleClassType(ref StringView mangledName) {
			TagTypeNode tt = null;
			switch (mangledName.PopFront()) {
				case 'T':
					tt = new TagTypeNode(TagKind.Union);
					break;
				case 'U':
					tt = new TagTypeNode(TagKind.Struct);
					break;
				case 'V':
					tt = new TagTypeNode(TagKind.Class);
					break;
				case 'W':
					if (mangledName.PopFront() != '4') {
						SetError();
						return null;
					}
					tt = new TagTypeNode(TagKind.Enum);
					break;
				default:
					Debug.Assert(false);
					break;
			}

			tt.QualifiedName = DemangleFullyQualifiedTypeName(ref mangledName);
			return tt;
		}

		Qualifiers DemanglePointerExtQualifiers(ref StringView mangledName) {
			Qualifiers quals = Qualifiers.None;
			if (mangledName.ConsumeFront('E'))
				quals |= Qualifiers.Pointer64;
			if (mangledName.ConsumeFront('I'))
				quals |= Qualifiers.Restrict;
			if (mangledName.ConsumeFront('F'))
				quals |= Qualifiers.Unaligned;

			return quals;
		}

		NodeArrayNode DemangleFunctionParameterList(ref StringView mangledName) {
			if (mangledName.ConsumeFront('X'))
				return new NodeArrayNode();

			NodeList head = new NodeList();
			NodeList current = head;

			int count = 0;
			while (!error && !mangledName.StartsWith('@') && !mangledName.StartsWith('Z')) {
				count++;

				if (StartsWithDigit(mangledName)) {
					int n = mangledName[0] - '0';
					if (n >= backrefs.FunctionParamCount) {
						SetError();
						return new NodeArrayNode();
					}
					mangledName = mangledName.DropFront();

					current = new NodeList();
					current.Node = backrefs.FunctionParams[n];
					current = current.Next;
					continue;
				}

				int oldSize = mangledName.Length;

				current = new NodeList();
				TypeNode tn = DemangleType(ref mangledName, QualifierMangleNode.Drop);
				if (tn == null || error)
					return null;

				current.Node = tn;

				int charsConsumed = oldSize - mangledName.Length;
				Debug.Assert(charsConsumed != 0);

				if (backrefs.FunctionParamCount <= 9 && charsConsumed > 1)
					backrefs.FunctionParams[backrefs.FunctionParamCount++] = tn;

				current = current.Next;
			}

			if (error)
				return new NodeArrayNode();

			NodeArrayNode na = NodeListToNodeArray(head, count);
			if (mangledName.ConsumeFront('@'))
				return na;

			if (mangledName.ConsumeFront('Z'))
				return na;

			SetError();
			return new NodeArrayNode();
		}

		void DemangleThrowSpecification(ref StringView mangledName) {
			if (mangledName.ConsumeFront('Z'))
				return;
			SetError();
		}

		FunctionSignatureNode DemangleFunctionType(ref StringView mangledName, bool hasThisQuals) {
			FunctionSignatureNode fty = new FunctionSignatureNode();

			if (hasThisQuals) {
				fty.Quals = DemanglePointerExtQualifiers(ref mangledName);
				fty.RefQualifier = DemangleFunctionRefQualifier(ref mangledName);
				fty.Quals |= DemangleQualifiers(ref mangledName).Item1;
			}

			fty.CallConvention = DemangleCallingConvention(ref mangledName);

			bool isStructor = mangledName.ConsumeFront('@');
			if (!isStructor)
				fty.ReturnType = DemangleType(ref mangledName, QualifierMangleNode.Result);

			fty.Params = DemangleFunctionParameterList(ref mangledName);

			DemangleThrowSpecification(ref mangledName);
			return fty;
		}

		PointerTypeNode DemanglePointerType(ref StringView mangledName) {
			PointerTypeNode pointer = new PointerTypeNode();

			(pointer.Quals, pointer.Affinity) = DemanglePointerCVQualifiers(ref mangledName);

			if (mangledName.ConsumeFront('6')) {
				pointer.Pointee = DemangleFunctionType(ref mangledName, false);
				return pointer;
			}

			Qualifiers extQuals = DemanglePointerExtQualifiers(ref mangledName);
			pointer.Quals = pointer.Quals | extQuals;

			pointer.Pointee = DemangleType(ref mangledName, QualifierMangleNode.Mangle);
			return pointer;
		}

		PointerTypeNode DemangleMemberPointerType(StringView mangledName) {
			PointerTypeNode pointer = new PointerTypeNode();
			(pointer.Quals, pointer.Affinity) = DemanglePointerCVQualifiers(ref mangledName);
			Debug.Assert(pointer.Affinity == PointerAffinity.Pointer);

			Qualifiers extQuals = DemanglePointerExtQualifiers(ref mangledName);
			pointer.Quals = pointer.Quals | extQuals;

			if (mangledName.ConsumeFront('8')) {
				pointer.ClassParent = DemangleFullyQualifiedTypeName(ref mangledName);
				pointer.Pointee = DemangleFunctionType(ref mangledName, true);
			} else {
				Qualifiers pointeeQuals = Qualifiers.None;
				bool isMember = false;
				(pointeeQuals, isMember) = DemangleQualifiers(ref mangledName);
				Debug.Assert(isMember);
				pointer.ClassParent = DemangleFullyQualifiedTypeName(ref mangledName);

				pointer.Pointee = DemangleType(ref mangledName, QualifierMangleNode.Drop);
				pointer.Pointee.Quals = pointeeQuals;
			}
			return pointer;
		}

		QualifiedNameNode DemangleFullyQualifiedTypeName(ref StringView mangledName) {
			IdentifierNode identifier = DemangleUnqualifiedTypeName(ref mangledName, true);
			if (error)
				return null;
			Debug.Assert(identifier != null);

			QualifiedNameNode qn = DemangleNameScopeChain(ref mangledName, identifier);
			if (error)
				return null;

			Debug.Assert(qn != null);
			return qn;
		}

		IdentifierNode DemangleUnqualifiedTypeName(ref StringView mangledName, bool memorize) {
			if (StartsWithDigit(mangledName)) {
				return DemangleBackRefName(ref mangledName);
			}

			if (mangledName.StartsWith("?$"))
				return DemangleTemplateInstantiationName(ref mangledName, NameBackrefBehaviour.Template);

			return DemangleSimpleName(ref mangledName, memorize);
		}

		NamedIdentifierNode DemangleBackRefName(ref StringView mangledName) {
			Debug.Assert(StartsWithDigit(mangledName));

			int i = mangledName[0] - '0';
			if (i >= backrefs.NamesCount) {
				SetError();
				return null;
			}

			mangledName = mangledName.DropFront();
			return backrefs.Names[i];
		}

		SpecialTableSymbolNode DemangleSpecialTableSymbolNode(ref StringView mangledName, SpecialIntrinsicKind kind) {
			NameIdentifierNode ni = new NameIdentifierNode();
			switch (kind) {
				case SpecialIntrinsicKind.Vftable:
					ni.Name = "`vftable'";
					break;
				case SpecialIntrinsicKind.Vbtable:
					ni.Name = "`vbtable'";
					break;
				case SpecialIntrinsicKind.LocalVftable:
					ni.Name = "`local vftable'";
					break;
				case SpecialIntrinsicKind.RttiCompleteObjLocator:
					ni.Name = "`RTTI Complete Object Locator'";
					break;
				default:
					Debug.Assert(false);
					break;
			}
			QualifiedNameNode qn = DemangleNameScopeChain(ref mangledName, ni);
			SpecialTableSymbolNode stsn = new SpecialTableSymbolNode();
			stsn.Name = qn;
			bool isMember = false;
			char front = mangledName.PopFront();
			if (front != '6' && front != '7') {
				SetError();
				return null;
			}

			(stsn.Quals, isMember) = DemangleQualifiers(ref mangledName);
			if (!mangledName.ConsumeFront('@')) {
				stsn.TargetName = DemangleFullyQualifiedTypeName(ref mangledName);
			}
			return stsn;
		}
	}
}