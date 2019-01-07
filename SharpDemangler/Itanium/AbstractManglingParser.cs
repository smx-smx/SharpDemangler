using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public abstract class AbstractManglingParser : IManglingParser
	{
		string data;

		List<Node> names = new List<Node>(32);
		List<Node> subs = new List<Node>(32);
		List<Node> templateParams = new List<Node>(8);
		public readonly List<ForwardTemplateReference> ForwardTemplateRefs = new List<ForwardTemplateReference>(4);

		//ICollection<TAlloc> astAllocator;

		bool tryToParseTemplateArgs = true;
		bool permitForwardTemplateReferences = false;
		bool parsingLambdaParams = false;


		private readonly IManglingParser parser;

		public AbstractManglingParser(string data) {
			this.data = data;
			this.parser = this;
		}

		public void Reset(string data) {
			this.data = data;

			names.Clear();
			subs.Clear();
			templateParams.Clear();

			parsingLambdaParams = false;
			tryToParseTemplateArgs = true;
			permitForwardTemplateReferences = false;

			//astAllocator.Clear();
		}

		static NodeArray MakeNodeArray<T>(IEnumerable<T> nodeList) where T : Node {
			return new NodeArray(nodeList.ToArray());
		}

		NodeArray PopTrailingNodeArray(int fromPosition) {
			Debug.Assert(fromPosition <= names.Count);
			NodeArray res = MakeNodeArray(names.Skip(fromPosition));
			names.RemoveRange(fromPosition, names.Count - fromPosition);
			return res;
		}

		bool ConsumeIf(string s) {
			if (data.StartsWith(s)) {
				data = data.Substring(s.Length);
				return true;
			}
			return false;
		}

		bool ConsumeIf(char c) {
			if (data.Length > 0 && data[0] == c) {
				data = data.Substring(1);
				return true;
			}
			return false;
		}

		char Consume() {
			if (data.Length == 0)
				return '\0';
			char ch = data[0];
			data = data.Substring(1);
			return ch;
		}

		public string ParseDiscriminator(string data) {
			if (data.Length < 1)
				return data;

			if (data[0] == '_') {
				string t1 = data.Substring(1);
				if (t1.Length > 0) {
					if (char.IsDigit(t1[0]))
						data = t1.Substring(1);
					else if (t1[0] == '_') {
						for (t1 = t1.Substring(1); t1.Length > 0 && char.IsDigit(t1[0]); t1 = t1.Substring(1)) ;
						if (t1.Length > 0 && t1[0] == '_')
							data = t1.Substring(1);
					}
				}
			} else if (char.IsDigit(data[0])) {
				string t1 = data.Substring(1);
				for (; t1.Length > 0 && char.IsDigit(t1[0]); t1 = t1.Substring(1)) ;
				if (t1.Length == 0)
					data = null; //$TODO(SMX)
			}

			return data;
		}

		public bool ResolveForwardTemplateRefs(NameState state) {
			int I = state.ForwardTemplateRefsBegin;
			int E = ForwardTemplateRefs.Count;
			for(; I < E; I++) {
				int idx = ForwardTemplateRefs[I].Index;
				if (idx >= templateParams.Count)
					return true;
				ForwardTemplateRefs[I].Ref = templateParams[idx];
			}
			ForwardTemplateRefs.RemoveRange(state.ForwardTemplateRefsBegin, ForwardTemplateRefs.Count - state.ForwardTemplateRefsBegin);
			return false;
		}

		char Look(int lookahead = 0) {
			if (lookahead >= data.Length)
				return '\0';
			return data[lookahead];
		}

		public Node ParseName(NameState state = null) {
			ConsumeIf('L');

			if (Look() == 'N') {
				return parser.ParseNestedName(state);
			}
			if (Look() == 'Z') {
				return parser.ParseLocalName(state);
			}

			if (Look() == 'S' && Look(1) != 't') {
				Node s = parser.ParseSubstitution();
				if (s == null)
					return null;

				if (Look() != 'I')
					return null;

				Node ta = parser.ParseTemplateArgs(state != null);
				if (ta == null)
					return null;

				if (state != null)
					state.EndsWithTemplateArgs = true;

				return new NameWithTemplateArgs(s, ta);
			}

			Node n = parser.ParseUnscopedName(state);
			if (n == null)
				return null;

			if (Look() == 'I') {
				subs.Add(n);
				Node ta = parser.ParseTemplateArgs(state != null);
				if (ta == null)
					return null;
				if (state != null)
					state.EndsWithTemplateArgs = true;
				return new NameWithTemplateArgs(n, ta);
			}

			return n;
		}

		public string ParseNumber(bool allowNegative = false) {
			string tmp = data;
			if (allowNegative)
				ConsumeIf('n');

			if (data.Length == 0 || !char.IsDigit(data[0]))
				return string.Empty;

			while (data.Length != 0 && char.IsDigit(data[0])) {
				data = data.Substring(1);
			}
			return tmp;
		}

		public Node ParseLocalName(NameState state) {
			if (!ConsumeIf('Z'))
				return null;

			Node encoding = parser.ParseEncoding();
			if (encoding == null || !ConsumeIf('E')) {
				return null;
			}

			if (ConsumeIf('s')) {
				data = ParseDiscriminator(data);
				NameType stringLitName = new NameType("string literal");
				return new LocalName(encoding, stringLitName);
			}

			if (ConsumeIf('d')) {
				ParseNumber(true);
				if (!ConsumeIf('_'))
					return null;

				Node n = parser.ParseName(state);
				if (n == null)
					return null;

				return new LocalName(encoding, n);
			}

			Node entity = parser.ParseName(state);
			if (entity == null)
				return null;

			data = ParseDiscriminator(data);
			return new LocalName(encoding, entity);
		}

		public Node ParseUnscopedName(NameState state) {
			if (ConsumeIf("StL") || ConsumeIf("St")) {
				Node r = parser.ParseUnqualifiedName(state);
				if (r == null)
					return null;
				return new StdQualifiedName(r);
			}
			return parser.ParseUnqualifiedName(state);
		}

		public Node ParseUnqualifiedName(NameState state) {
			Node result;
			if (Look() == 'U') {
				result = parser.ParseUnnamedTypeName(state);
			} else if (Look() >= '1' && Look() <= '9') {
				result = parser.ParseSourceName(state);
			} else if (ConsumeIf("DC")) {
				int bindingsBegin = names.Count;
				do {
					Node binding = parser.ParseSourceName(state);
					if (binding == null)
						return null;
					names.Add(binding);
				} while (!ConsumeIf('E'));
				result = new StructuredBindingName(PopTrailingNodeArray(bindingsBegin));
			} else {
				result = parser.ParseOperatorName(state);
			}
			if (result != null)
				result = parser.ParseAbiTags(result);
			return result;
		}

		public Node ParseUnnamedTypeName(NameState state) {
			if (ConsumeIf("Ut")) {
				string count = ParseNumber();
				if (!ConsumeIf('_')) {
					return null;
				}
				return new UnnamedTypeName(count);
			}
			if (ConsumeIf("Ul")) {
				NodeArray @params = null;
				return  SwapAndRestore.Execute<Node, bool>(ref parsingLambdaParams, true, () => {
					if (!ConsumeIf("vE")) {
						int paramsBegin = names.Count;
						do {
							Node p = parser.ParseType();
							if (p == null) {
								return null;
							}
							names.Add(p);
						} while (!ConsumeIf('E'));
						@params = PopTrailingNodeArray(paramsBegin);
					}
					string count = ParseNumber();
					if (!ConsumeIf('_')) {
						return null;
					}

					return new ClosureTypeName(@params, count);
				});
			}
			return null;
		}

		public Node ParseSourceName(NameState state) {
			if (ParsePositiveInteger(out int length)) {
				return null;
			}
			if (data.Length < length || length == 0)
				return null;

			string name = data.Substring(0, length);
			data = data.Substring(length);

			if (name.StartsWith("_GLOBAL__N"))
				return new NameType("(anonymous namespace)");
			return new NameType(name);
		}

		public Node ParseOperatorName(NameState state) {
			switch (Look()) {
				case 'a':
					switch (Look(1)) {
						case 'a':
							data = data.Substring(2);
							return new NameType("operator&&");
						case 'd':
						case 'n':
							data = data.Substring(2);
							return new NameType("operator&");
						case 'N':
							data = data.Substring(2);
							return new NameType("operator&=");
						case 'S':
							data = data.Substring(2);
							return new NameType("operator=");
					}
					return null;
				case 'c':
					switch (Look(1)) {
						case 'l':
							data = data.Substring(2);
							return new NameType("operator()");
						case 'm':
							data = data.Substring(2);
							return new NameType("operator,");
						case 'o':
							data = data.Substring(2);
							return new NameType("operator~");
						case 'v':
							data = data.Substring(2);
							return SwapAndRestore.Execute(ref tryToParseTemplateArgs, false, () => {
								return SwapAndRestore.Execute<Node, bool>(ref permitForwardTemplateReferences, permitForwardTemplateReferences || state != null, () => {
									Node ty = parser.ParseType();
									if (ty == null) {
										return null;
									}

									if (state != null)
										state.CtorDtorConversion = true;

									return new ConversionOperatorType(ty);
								});
							});
					}
					return null;
				case 'd':
					switch (Look(1)) {
						case 'a':
							data = data.Substring(2);
							return new NameType("operator delete[]");
						case 'e':
							data = data.Substring(2);
							return new NameType("operator*");
						case 'l':
							data = data.Substring(2);
							return new NameType("operator delete");
						case 'v':
							data = data.Substring(2);
							return new NameType("operator/");
						case 'V':
							data = data.Substring(2);
							return new NameType("operator/=");
					}
					return null;
				case 'e':
					switch (Look(1)) {
						case 'o':
							data = data.Substring(2);
							return new NameType("operator^");
						case 'O':
							data = data.Substring(2);
							return new NameType("operator^=");
						case 'q':
							data = data.Substring(2);
							return new NameType("operator==");
					}
					return null;
				case 'g':
					switch (Look(1)) {
						case 'e':
							data = data.Substring(2);
							return new NameType("operator>=");
						case 't':
							data = data.Substring(2);
							return new NameType("operator>");
					}
					return null;
				case 'i':
					if (Look(1) == 'x') {
						data = data.Substring(2);
						return new NameType("operator[]");
					}
					return null;
				case 'l':
					switch (Look(1)) {
						case 'e':
							data = data.Substring(2);
							return new NameType("operator<=");
						case 'i':
							data = data.Substring(2);
							Node sn = parser.ParseSourceName(state);
							if (sn == null)
								return null;
							return new LiteralOperator(sn);

						case 's':
							data = data.Substring(2);
							return new NameType("operator<<");
						case 'S':
							data = data.Substring(2);
							return new NameType("operator<<=");
						case 't':
							data = data.Substring(2);
							return new NameType("operator<");
					}
					return null;
				case 'm':
					switch (Look(1)) {
						case 'i':
							data = data.Substring(2);
							return new NameType("operator-");
						case 'I':
							data = data.Substring(2);
							return new NameType("operator-=");
						case 'l':
							data = data.Substring(2);
							return new NameType("operator*");
						case 'L':
							data = data.Substring(2);
							return new NameType("operator*=");
						case 'm':
							data = data.Substring(2);
							return new NameType("operator--");
					}
					return null;
				case 'n':
					switch (Look(1)) {
						case 'a':
							data = data.Substring(2);
							return new NameType("operator new[]");
						case 'e':
							data = data.Substring(2);
							return new NameType("operator!=");
						case 'g':
							data = data.Substring(2);
							return new NameType("operator-");
						case 't':
							data = data.Substring(2);
							return new NameType("operator!");
						case 'w':
							data = data.Substring(2);
							return new NameType("operator new");
					}
					return null;
				case 'o':
					switch (Look(1)) {
						case 'o':
							data = data.Substring(2);
							return new NameType("operator||");
						case 'r':
							data = data.Substring(2);
							return new NameType("operator|");
						case 'R':
							data = data.Substring(2);
							return new NameType("operator|=");
					}
					return null;
				case 'p':
					switch (Look(1)) {
						case 'm':
							data = data.Substring(2);
							return new NameType("operator->*");
						case 'l':
							data = data.Substring(2);
							return new NameType("operator+");
						case 'L':
							data = data.Substring(2);
							return new NameType("operator+=");
						case 'p':
							data = data.Substring(2);
							return new NameType("operator++");
						case 's':
							data = data.Substring(2);
							return new NameType("operator+");
						case 't':
							data = data.Substring(2);
							return new NameType("operator->");
					}
					return null;
				case 'q':
					if (Look(1) == 'u') {
						data = data.Substring(2);
						return new NameType("operator?");
					}
					return null;
				case 'r':
					switch (Look(1)) {
						case 'm':
							data = data.Substring(2);
							return new NameType("operator%");
						case 'M':
							data = data.Substring(2);
							return new NameType("operator%=");
						case 's':
							data = data.Substring(2);
							return new NameType("operator>>");
						case 'S':
							data = data.Substring(2);
							return new NameType("operator>>=");
					}
					return null;
				case 's':
					if (Look(1) == 's') {
						data = data.Substring(2);
						return new NameType("operator<=>");
					}
					return null;
				case 'v':
					if (char.IsDigit(Look(1))) {
						data = data.Substring(2);
						Node sn = parser.ParseSourceName(state);
						if (sn == null)
							return null;
						return new ConversionOperatorType(sn);
					}
					return null;
			}
			return null;
		}

		public Node ParseCtorDtorName(Node soFar, NameState state) {
			if (soFar.Kind == ItaniumDemangleNodeType.SpecialSubstitution) {
				SpecialSubKind ssk = ((SpecialSubstitution)soFar).SSK;
				switch (ssk) {
					case SpecialSubKind.@string:
					case SpecialSubKind.istream:
					case SpecialSubKind.ostream:
					case SpecialSubKind.iostream:
						soFar = new ExpandedSpecialSubstitution(ssk);
						break;
					default:
						break;
				}
			}

			if (ConsumeIf('C')) {
				bool isInherited = ConsumeIf('I');
				switch (Look()) {
					case '1':
					case '2':
					case '3':
					case '5':
						break;
					default:
						return null;
				}

				int variant = Look() - '0';
				data = data.Substring(1);

				if (state != null)
					state.CtorDtorConversion = true;

				if (isInherited) {
					if (parser.ParseName(state) == null)
						return null;
				}
				return new CtorDtorName(soFar, false, variant);
			}

			if (Look() == 'D') {
				switch (Look(1)) {
					case '0':
					case '1':
					case '2':
					case '5':
						int variant = Look(1) - '0';
						data = data.Substring(2);
						if (state != null)
							state.CtorDtorConversion = true;
						return new CtorDtorName(soFar, true, variant);
					default:
						break;
				}
			}

			return null;
		}

		public Node ParseNestedName(NameState state) {
			if (!ConsumeIf('N'))
				return null;

			Qualifiers cvTmp = ParseCVQualifiers();
			if (state != null)
				state.CVQualifiers = cvTmp;

			if (ConsumeIf('O')) {
				if (state != null)
					state.ReferenceQualifier = FunctionRefQual.RValue;
			} else if (ConsumeIf('R')) {
				if (state != null)
					state.ReferenceQualifier = FunctionRefQual.LValue;
			} else {
				if (state != null)
					state.ReferenceQualifier = FunctionRefQual.None;
			}

			Node soFar = null;

			bool PushComponent(Node comp) {
				if (comp == null)
					return false;
				if (soFar != null)
					soFar = new NestedName(soFar, comp);
				else
					soFar = comp;
				if (state != null)
					state.EndsWithTemplateArgs = false;
				return soFar != null;
			}

			if (ConsumeIf("St")) {
				soFar = new NameType("std");
			}

			while (!ConsumeIf('E')) {
				ConsumeIf('L');

				if (ConsumeIf('M')) {
					if (soFar == null)
						return null;
					continue;
				}

				if (Look() == 'T') {
					if (!PushComponent(parser.ParseTemplateParam()))
						return null;
					subs.Add(soFar);
					continue;
				}

				if (Look() == 'I') {
					Node ta = parser.ParseTemplateArgs(state != null);
					if (ta == null || soFar == null)
						return null;

					soFar = new NameWithTemplateArgs(soFar, ta);
					if (state != null)
						state.EndsWithTemplateArgs = true;
					subs.Add(soFar);
					continue;
				}

				if (Look() == 'D') {
					switch (Look(1)) {
						case 't':
						case 'T':
							if (!PushComponent(parser.ParseDeclType())) {
								return null;
							}
							subs.Add(soFar);
							continue;
					}
				}

				if (Look() == 'S' && Look(1) != 't') {
					Node s = parser.ParseSubstitution();
					if (!PushComponent(s))
						return null;
					if (soFar != s)
						subs.Add(s);
					continue;
				}

				if (Look() == 'C' || (Look() == 'D' && Look(1) != 'C')) {
					if (soFar == null)
						return null;
					if (!PushComponent(parser.ParseCtorDtorName(soFar, state)))
						return null;
					soFar = parser.ParseAbiTags(soFar);
					if (soFar == null)
						return null;
					subs.Add(soFar);
					continue;
				}

				if (!PushComponent(parser.ParseUnqualifiedName(state)))
					return null;
				subs.Add(soFar);
			}

			if (soFar == null || subs.Count == 0) {
				return null;
			}

			subs.RemoveAt(subs.Count - 1);
			return soFar;
		}


		public Node ParseSimpleId() {
			Node sn = parser.ParseSourceName(null);
			if (sn == null)
				return null;
			if (Look() == 'I') {
				Node ta = parser.ParseTemplateArgs();
				if (ta == null)
					return null;
				return new NameWithTemplateArgs(sn, ta);
			}
			return sn;
		}

		public Node ParseDestructorName() {
			Node result;
			if (char.IsDigit(Look())) {
				result = parser.ParseSimpleId();
			} else {
				result = parser.ParseUnresolvedType();
			}
			if (result == null)
				return null;
			return new DtorName(result);
		}

		public Node ParseUnresolvedType() {
			if (Look() == 'T') {
				Node tp = parser.ParseTemplateParam();
				if (tp == null)
					return null;
				subs.Add(tp);
				return tp;
			}
			if (Look() == 'D') {
				Node dt = parser.ParseDeclType();
				if (dt == null)
					return null;
				subs.Add(dt);
				return dt;
			}
			return parser.ParseSubstitution();
		}

		public Node ParseBaseUnresolvedName() {
			if (char.IsDigit(Look())) {
				return parser.ParseSimpleId();
			}

			if (ConsumeIf("dn")) {
				return parser.ParseDestructorName();
			}

			ConsumeIf("on");

			Node oper = parser.ParseOperatorName(null);
			if (oper == null)
				return null;

			if (Look() == 'I') {
				Node ta = parser.ParseTemplateArgs();
				if (ta == null)
					return null;
				return new NameWithTemplateArgs(oper, ta);
			}
			return oper;
		}

		public Node ParseUnresolvedName() {
			Node soFar = null;
			Node baseNode;

			if (ConsumeIf("srN")) {
				soFar = parser.ParseUnresolvedType();
				if (soFar == null)
					return null;

				if (Look() == 'I') {
					Node ta = parser.ParseTemplateArgs();
					if (ta == null)
						return null;

					soFar = new NameWithTemplateArgs(soFar, ta);
				}

				while (!ConsumeIf('E')) {
					Node qual = parser.ParseSimpleId();
					if (qual == null)
						return null;

					soFar = new QualifiedName(soFar, qual);
				}

				baseNode = parser.ParseBaseUnresolvedName();
				if (baseNode == null)
					return null;

				return new QualifiedName(soFar, baseNode);
			}

			bool global = ConsumeIf("gs");

			if (!ConsumeIf("sr")) {
				soFar = parser.ParseBaseUnresolvedName();
				if (soFar == null)
					return null;

				if (global) {
					soFar = new GlobalQualifiedName(soFar);
				}
				return soFar;
			}

			if (char.IsDigit(Look())) {
				do {
					Node qual = parser.ParseSimpleId();
					if (qual == null)
						return null;
					if (soFar != null)
						soFar = new QualifiedName(soFar, qual);
					else if (global)
						soFar = new GlobalQualifiedName(qual);
					else
						soFar = qual;
					if (soFar == null)
						return null;
				} while (!ConsumeIf('E'));
			} else {
				soFar = parser.ParseUnresolvedType();
				if (soFar == null)
					return null;

				if (Look() == 'I') {
					Node ta = parser.ParseTemplateArgs();
					if (ta == null)
						return null;
					soFar = new NameWithTemplateArgs(soFar, ta);
					if (soFar == null)
						return null;
				}
			}

			Debug.Assert(soFar != null);

			baseNode = parser.ParseBaseUnresolvedName();
			if (baseNode == null)
				return null;
			return new QualifiedName(soFar, baseNode);
		}

		public Node ParseAbiTags(Node n) {
			while (ConsumeIf('B')) {
				string sn = ParseBareSourceName();
				if (string.IsNullOrEmpty(sn))
					return null;
				n = new AbiTagAttr(n, sn);
			}
			return n;
		}

		public bool ParsePositiveInteger(out int result) {
			result = 0;
			if (Look() < '0' || Look() > '9')
				return true;
			while (Look() >= '0' && Look() <= '9') {
				result *= 10;
				result += Consume() - '0';
			}
			return false;
		}

		public string ParseBareSourceName() {
			if (ParsePositiveInteger(out int value) || data.Length < value)
				return string.Empty;

			string r = data.Substring(0, value);
			data = data.Substring(value);
			return r;
		}

		public Node ParseFunctionType() {
			Qualifiers cvQuals = ParseCVQualifiers();

			Node exceptionSpec = null;
			if (ConsumeIf("Do")) {
				exceptionSpec = new NameType("noexcept");
			} else if (ConsumeIf("DO")) {
				Node e = parser.ParseExpr();
				if (e == null || !ConsumeIf('E'))
					return null;
				exceptionSpec = new NoexceptSpec(e);
			} else if (ConsumeIf("Dw")) {
				int specs = names.Count;
				while (!ConsumeIf('E')) {
					Node t = parser.ParseType();
					if (t == null)
						return null;
					names.Add(t);
				}

				exceptionSpec = new DynamicExceptionSpec(PopTrailingNodeArray(specs));
				if (exceptionSpec == null)
					return null;
			}

			ConsumeIf("Dx");

			if (!ConsumeIf('F'))
				return null;

			ConsumeIf('Y');

			Node returnType = parser.ParseType();
			if (returnType == null)
				return null;

			FunctionRefQual referenceQualifier = FunctionRefQual.None;
			int nParams = names.Count;

			while (true) {
				if (ConsumeIf('E'))
					break;
				if (ConsumeIf('v'))
					continue;
				if (ConsumeIf("RE")) {
					referenceQualifier = FunctionRefQual.LValue;
					break;
				}
				if (ConsumeIf("OE")) {
					referenceQualifier = FunctionRefQual.RValue;
					break;
				}
				Node t = parser.ParseType();
				if (t == null)
					return null;
				names.Add(t);
			}
			NodeArray @params = PopTrailingNodeArray(nParams);
			return new FunctionType(returnType, @params, cvQuals, referenceQualifier, exceptionSpec);
		}

		public Node ParseVectorType() {
			if (!ConsumeIf("Dv"))
				return null;

			Node elemType;
			if (Look() >= '1' && Look() <= '9') {
				string dimensionNumber = ParseNumber();
				if (!ConsumeIf('_'))
					return null;
				if (ConsumeIf('p'))
					return new PixelVectorType(new NodeOrString(dimensionNumber));

				elemType = parser.ParseType();
				if (elemType == null)
					return null;
				return new VectorType(elemType, new NodeOrString(dimensionNumber));
			}

			if (!ConsumeIf('_')) {
				Node dimExpr = parser.ParseExpr();
				if (dimExpr == null)
					return null;
				if (!ConsumeIf('_'))
					return null;

				elemType = parser.ParseType();
				if (elemType == null)
					return null;

				return new VectorType(elemType, new NodeOrString(dimExpr));
			}


			elemType = parser.ParseType();
			if (elemType == null)
				return null;
			return new VectorType(elemType, new NodeOrString(string.Empty));
		}

		public Node ParseDeclType() {
			if (!ConsumeIf('D'))
				return null;
			if (!ConsumeIf('t') && !ConsumeIf('T'))
				return null;
			Node e = parser.ParseExpr();
			if (e == null)
				return null;
			if (!ConsumeIf('E'))
				return null;

			return new EnclosingExpr("decltype(", e, ")");
		}

		public Node ParseArrayType() {
			if (!ConsumeIf('A'))
				return null;

			NodeOrString dimension = null;

			if (char.IsDigit(Look())) {
				dimension = new NodeOrString(ParseNumber());
				if (!ConsumeIf('_'))
					return null;
			} else if (!ConsumeIf('_')) {
				Node dimExpr = parser.ParseExpr();
				if (dimExpr == null)
					return null;
				if (!ConsumeIf('_'))
					return null;
				dimension = new NodeOrString(dimExpr);
			}

			Node ty = parser.ParseType();
			if (ty == null)
				return null;

			return new ArrayType(ty, dimension);
		}

		public Node ParsePointerToMemberType() {
			if (!ConsumeIf('M'))
				return null;
			Node classType = parser.ParseType();
			if (classType == null)
				return null;
			Node memberType = parser.ParseType();
			if (memberType == null)
				return null;

			return new PointerToMemberType(classType, memberType);
		}

		public Node ParseClassEnumType() {
			string elabSpef = null;
			if (ConsumeIf("Ts"))
				elabSpef = "struct";
			else if (ConsumeIf("Tu"))
				elabSpef = "union";
			else if (ConsumeIf("Te"))
				elabSpef = "enum";

			Node name = parser.ParseName();
			if (name == null)
				return null;

			if (!string.IsNullOrEmpty(elabSpef))
				return new ElaboratedTypeSpefType(elabSpef, name);

			return name;
		}

		public Node ParseQualifiedType() {
			if (ConsumeIf('U')) {
				string qual = ParseBareSourceName();
				if (string.IsNullOrEmpty(qual))
					return null;

				Node child;
				if (qual.StartsWith("objcproto")) {
					string protoSourceName = qual.Substring("objcproto".Length);

					string proto = null;
					SwapAndRestore.Execute(ref data, protoSourceName, () => {
						proto = ParseBareSourceName();
					});

					if (string.IsNullOrEmpty(proto))
						return null;

					child = parser.ParseQualifiedType();
					if (child == null)
						return null;
					return new ObjCProtoName(child, proto);
				}

				child = parser.ParseQualifiedType();
				if (child == null)
					return null;

				return new VendorExtQualType(child, qual);
			}

			Qualifiers quals = ParseCVQualifiers();
			Node ty = parser.ParseType();
			if (ty == null)
				return null;
			if (quals != Qualifiers.None)
				ty = new QualType(ty, quals);
			return ty;
		}

		public Node ParseType() {
			Node result = null;

			Node node = null;

			switch (Look()) {
				case 'r':
				case 'V':
				case 'K':
					int afterQuals = 0;
					if (Look(afterQuals) == 'r') afterQuals++;
					if (Look(afterQuals) == 'V') afterQuals++;
					if (Look(afterQuals) == 'K') afterQuals++;

					if (Look(afterQuals) == 'F' || (
						Look(afterQuals) == 'D' && (
						Look(afterQuals + 1) == 'o' || Look(afterQuals + 1) == 'O' ||
						Look(afterQuals + 1) == 'w' || Look(afterQuals + 1) == 'x'))
					) {
						result = parser.ParseFunctionType();
						break;
					}
					goto case 'U';
				case 'U':
					result = parser.ParseQualifiedType();
					break;
				case 'v':
					data = data.Substring(1);
					return new NameType("void");
				case 'w':
					data = data.Substring(1);
					return new NameType("wchar_t");
				case 'b':
					data = data.Substring(1);
					return new NameType("bool");
				case 'c':
					data = data.Substring(1);
					return new NameType("char");
				case 'a':
					data = data.Substring(1);
					return new NameType("signed char");
				case 'h':
					data = data.Substring(1);
					return new NameType("unsigned char");
				case 's':
					data = data.Substring(1);
					return new NameType("short");
				case 't':
					data = data.Substring(1);
					return new NameType("unsigned short");
				case 'i':
					data = data.Substring(1);
					return new NameType("int");
				case 'j':
					data = data.Substring(1);
					return new NameType("unsigned int");
				case 'l':
					data = data.Substring(1);
					return new NameType("long");
				case 'm':
					data = data.Substring(1);
					return new NameType("unsigned long");
				case 'x':
					data = data.Substring(1);
					return new NameType("long long");
				case 'y':
					data = data.Substring(1);
					return new NameType("unsigned long long");
				case 'n':
					data = data.Substring(1);
					return new NameType("__int128");
				case 'o':
					data = data.Substring(1);
					return new NameType("unsigned __int128");
				case 'f':
					data = data.Substring(1);
					return new NameType("float");
				case 'd':
					data = data.Substring(1);
					return new NameType("double");
				case 'e':
					data = data.Substring(1);
					return new NameType("long double");
				case 'g':
					data = data.Substring(1);
					return new NameType("__float128");
				case 'z':
					data = data.Substring(1);
					return new NameType("...");
				case 'u':
					data = data.Substring(1);
					string res = ParseBareSourceName();
					if (string.IsNullOrEmpty(res))
						return null;
					return new NameType(res);
				case 'D':
					switch (Look(1)) {
						case 'd':
							data = data.Substring(2);
							return new NameType("decimal64");
						case 'e':
							data = data.Substring(2);
							return new NameType("decimal128");
						case 'f':
							data = data.Substring(2);
							return new NameType("decimal32");
						case 'h':
							data = data.Substring(2);
							return new NameType("decimal16");
						case 'i':
							data = data.Substring(2);
							return new NameType("char32_t");
						case 's':
							data = data.Substring(2);
							return new NameType("char16_t");
						case 'a':
							data = data.Substring(2);
							return new NameType("auto");
						case 'c':
							data = data.Substring(2);
							return new NameType("decltype(auto)");
						case 'n':
							data = data.Substring(2);
							return new NameType("std::nullptr_t");
						case 't':
						case 'T':
							result = parser.ParseDeclType();
							break;
						case 'v':
							result = parser.ParseVectorType();
							break;
						case 'p':
							data = data.Substring(2);
							Node child = parser.ParseType();
							if (child == null)
								return null;
							result = new ParameterPackExpansion(child);
							break;
						case 'o':
						case 'O':
						case 'w':
						case 'x':
							result = parser.ParseFunctionType();
							break;
					}
					break;
				case 'F':
					result = parser.ParseFunctionType();
					break;
				case 'A':
					result = parser.ParseArrayType();
					break;
				case 'M':
					result = parser.ParsePointerToMemberType();
					break;
				case 'T':
					switch (Look(1)) {
						case 's':
						case 'u':
						case 'e':
							result = parser.ParseClassEnumType();
							break;
					}

					result = parser.ParseTemplateParam();
					if (result == null)
						return null;

					if (tryToParseTemplateArgs && Look() == 'I') {
						Node ta = parser.ParseTemplateArgs();
						if (ta == null)
							return null;
						result = new NameWithTemplateArgs(result, ta);
					}
					break;
				case 'P':
					data = data.Substring(1);
					Node ptr = parser.ParseType();
					if (ptr == null)
						return null;
					result = new PointerType(ptr);
					break;
				case 'R':
					data = data.Substring(1);
					node = parser.ParseType();
					if (node == null)
						return null;
					result = new ReferenceType(node, ReferenceKind.LValue);
					break;
				case 'O':
					data = data.Substring(1);
					node = parser.ParseType();
					if (node == null)
						return null;
					result = new ReferenceType(node, ReferenceKind.RValue);
					break;
				case 'C':
					data = data.Substring(1);
					node = parser.ParseType();
					if (node == null)
						return null;
					result = new PostfixQualifiedType(node, " complex");
					break;
				case 'G':
					data = data.Substring(1);
					node = parser.ParseType();
					if (node == null)
						return null;
					result = new PostfixQualifiedType(node, " imaginary");
					break;
				case 'S':
					if (Look(1) != '\0' && Look(1) != 't') {
						Node sub = parser.ParseSubstitution();
						if (sub == null)
							return null;

						if (tryToParseTemplateArgs && Look() == 'I') {
							Node ta = parser.ParseTemplateArgs();
							if (ta == null)
								return null;
							result = new NameWithTemplateArgs(sub, ta);
							break;
						}

						return sub;
					}
					goto default;
				default:
					result = parser.ParseClassEnumType();
					break;
			}

			if (result != null)
				subs.Add(result);
			return result;
		}

		public Node ParsePrefixExpr(string kind) {
			Node e = parser.ParseExpr();
			if (e == null)
				return null;
			return new PrefixExpr(kind, e);
		}

		public Node ParseBinaryExpr(string kind) {
			Node lhs = parser.ParseExpr();
			if (lhs == null)
				return null;
			Node rhs = parser.ParseExpr();
			if (rhs == null)
				return null;
			return new BinaryExpr(lhs, kind, rhs);
		}

		public Node ParseIntegerLiteral(string lit) {
			string tmp = ParseNumber(true);
			if (string.IsNullOrEmpty(tmp) && ConsumeIf('E')) {
				return new IntegerLiteral(lit, tmp);
			}
			return null;
		}

		public Qualifiers ParseCVQualifiers() {
			Qualifiers cvr = Qualifiers.None;
			if (ConsumeIf('r'))
				cvr |= Qualifiers.Restrict;
			if (ConsumeIf('V'))
				cvr |= Qualifiers.Volatile;
			if (ConsumeIf('K'))
				cvr |= Qualifiers.Const;
			return cvr;
		}

		public Node ParseFunctionParam() {
			if (ConsumeIf("fp")) {
				ParseCVQualifiers();
				string num = ParseNumber();
				if (!ConsumeIf('_'))
					return null;
				return new FunctionParam(num);
			}
			if (ConsumeIf("fL")) {
				if (string.IsNullOrEmpty(ParseNumber()))
					return null;
				if (!ConsumeIf('p'))
					return null;
				ParseCVQualifiers();
				string num = ParseNumber();
				if (!ConsumeIf('_'))
					return null;
				return new FunctionParam(num);
			}
			return null;
		}

		public Node ParseNewExpr() {
			bool global = ConsumeIf("gs");
			bool isArray = Look(1) == 'a';
			if (!ConsumeIf("nw") && !ConsumeIf("na"))
				return null;
			int exprs = names.Count;
			while (!ConsumeIf('_')) {
				Node ex = parser.ParseExpr();
				if (ex == null)
					return null;
				names.Add(ex);
			}
			NodeArray exprList = PopTrailingNodeArray(exprs);
			Node ty = parser.ParseType();
			if (ty == null)
				return ty;
			if (ConsumeIf("pi")) {
				int numInits = names.Count;
				while (!ConsumeIf('E')) {
					Node init = parser.ParseExpr();
					if (init == null)
						return null;
					names.Add(init);
				}
				NodeArray inits = PopTrailingNodeArray(numInits);
				return new NewExpr(exprList, ty, inits, global, isArray);
			} else if (!ConsumeIf('E')) {
				return null;
			}
			return new NewExpr(exprList, ty, new NodeArray(), global, isArray);
		}

		public Node ParseConversionExpr() {
			if (!ConsumeIf("cv"))
				return null;

			Node ty = null;
			SwapAndRestore.Execute(ref tryToParseTemplateArgs, false, () => {
				ty = parser.ParseType();
			});

			if (ty == null)
				return null;

			if (ConsumeIf('_')) {
				int nExprs = names.Count;
				while (!ConsumeIf('E')) {
					Node exp = parser.ParseExpr();
					if (exp == null)
						return null;
					names.Add(exp);
				}
				NodeArray exprs = PopTrailingNodeArray(nExprs);
				return new ConversionExpr(ty, exprs);
			}

			Node[] e = new Node[1];
			e[0] = parser.ParseExpr();
			if (e[0] == null)
				return null;

			return new ConversionExpr(ty, MakeNodeArray(e));
		}

		public Node ParseExprPrimary() {
			if (!ConsumeIf('L'))
				return null;
			switch (Look()) {
				case 'w':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("wchar_t");
				case 'b':
					if (ConsumeIf("b0E"))
						return new BoolExpr(false);
					if (ConsumeIf("b1E"))
						return new BoolExpr(true);
					return null;
				case 'c':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("char");
				case 'a':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("signed char");
				case 'h':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("unsigned char");
				case 's':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("short");
				case 't':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("unsigned short");
				case 'i':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("");
				case 'j':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("u");
				case 'l':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("l");
				case 'm':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("ul");
				case 'x':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("ll");
				case 'y':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("ull");
				case 'n':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("__int128");
				case 'o':
					data = data.Substring(1);
					return parser.ParseIntegerLiteral("unsigned __int128");
				case 'f':
					data = data.Substring(1);
					return parser.ParseFloatingLiteral(new FloatData());
				case 'd':
					data = data.Substring(1);
					return parser.ParseFloatingLiteral(new DoubleData());
				case 'e':
					data = data.Substring(1);
					return parser.ParseFloatingLiteral(new LongDoubleData(FloatDataArch.Default));
				case '_':
					if (ConsumeIf("_Z")) {
						Node r = parser.ParseEncoding();
						if (r != null && ConsumeIf('E'))
							return r;
					}
					return null;
				case 'T':
					return null;
				default:
					Node t = parser.ParseType();
					if (t == null)
						return null;
					string n = ParseNumber();
					if (string.IsNullOrEmpty(n)) {
						if (!ConsumeIf('E'))
							return null;
						return new IntegerCastExpr(t, n);
					}
					if (ConsumeIf('E'))
						return t;
					return null;
			}
		}

		public Node ParseBracedExpr() {
			Node init;
			if (Look() == 'd') {
				switch (Look(1)) {
					case 'i':
						data = data.Substring(2);
						Node field = parser.ParseSourceName(null);
						if (field == null)
							return null;
						init = parser.ParseBracedExpr();
						if (init == null)
							return null;
						return new BracedExpr(field, init, false);
					case 'x':
						data = data.Substring(2);
						Node index = parser.ParseExpr();
						if (index == null)
							return null;
						init = parser.ParseBracedExpr();
						if (init == null)
							return null;
						return new BracedExpr(index, init, true);
					case 'X':
						data = data.Substring(2);
						Node rangeBegin = parser.ParseExpr();
						if (rangeBegin == null)
							return null;
						Node rangeEnd = parser.ParseExpr();
						if (rangeEnd == null)
							return null;
						init = parser.ParseBracedExpr();
						if (init == null)
							return null;
						return new BracedRangeExpr(rangeBegin, rangeEnd, init);
				}
			}
			return parser.ParseBracedExpr();
		}

		public Node ParseFoldExpr() {
			if (!ConsumeIf('f'))
				return null;

			char foldKind = Look();
			bool isLeftFold, hasInitializer;
			hasInitializer = foldKind == 'L' || foldKind == 'R';
			if (foldKind == 'l' || foldKind == 'L')
				isLeftFold = true;
			else if (foldKind == 'r' || foldKind == 'R')
				isLeftFold = false;
			else
				return null;
			data = data.Substring(1);

			string operatorName;
			if (ConsumeIf("aa")) operatorName = "&&";
			else if (ConsumeIf("an")) operatorName = "&";
			else if (ConsumeIf("aN")) operatorName = "&=";
			else if (ConsumeIf("aS")) operatorName = "=";
			else if (ConsumeIf("cm")) operatorName = ",";
			else if (ConsumeIf("ds")) operatorName = ".*";
			else if (ConsumeIf("dv")) operatorName = "/";
			else if (ConsumeIf("dV")) operatorName = "/=";
			else if (ConsumeIf("eo")) operatorName = "^";
			else if (ConsumeIf("eO")) operatorName = "^=";
			else if (ConsumeIf("eq")) operatorName = "==";
			else if (ConsumeIf("ge")) operatorName = ">=";
			else if (ConsumeIf("gt")) operatorName = ">";
			else if (ConsumeIf("le")) operatorName = "<=";
			else if (ConsumeIf("ls")) operatorName = "<<";
			else if (ConsumeIf("lS")) operatorName = "<<=";
			else if (ConsumeIf("lt")) operatorName = "<";
			else if (ConsumeIf("mi")) operatorName = "-";
			else if (ConsumeIf("mI")) operatorName = "-=";
			else if (ConsumeIf("ml")) operatorName = "*";
			else if (ConsumeIf("mL")) operatorName = "*=";
			else if (ConsumeIf("ne")) operatorName = "!=";
			else if (ConsumeIf("oo")) operatorName = "||";
			else if (ConsumeIf("or")) operatorName = "|";
			else if (ConsumeIf("oR")) operatorName = "|=";
			else if (ConsumeIf("pl")) operatorName = "+";
			else if (ConsumeIf("pL")) operatorName = "+=";
			else if (ConsumeIf("rm")) operatorName = "%";
			else if (ConsumeIf("rM")) operatorName = "%=";
			else if (ConsumeIf("rs")) operatorName = ">>";
			else if (ConsumeIf("rS")) operatorName = ">>=";
			else return null;

			Node pack = parser.ParseExpr();
			Node init = null;

			if (pack == null)
				return null;

			if (hasInitializer) {
				init = parser.ParseExpr();
				if (init == null)
					return null;
			}

			if (isLeftFold && init != null) {
				Node tmp = pack;
				pack = init;
				init = tmp;
			}

			return new FoldExpr(isLeftFold, operatorName, pack, init);
		}

		public Node ParseExpr() {
			bool global = ConsumeIf("gs");
			Node ty;
			Node ex;
			if (data.Length < 2)
				return null;

			switch (data[0]) {
				case 'L':
					return parser.ParseExprPrimary();
				case 'T':
					return parser.ParseTemplateParam();
				case 'f':
					if (Look(1) == 'p' || (Look(1) == 'L' && char.IsDigit(Look(2))))
						return parser.ParseFunctionParam();
					return parser.ParseFoldExpr();
				case 'a':
					switch (data[1]) {
						case 'a':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("&&");
						case 'd':
							data = data.Substring(2);
							return parser.ParsePrefixExpr("&");
						case 'n':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("&");
						case 'N':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("&=");
						case 'S':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("=");
						case 't':
							data = data.Substring(2);
							ty = parser.ParseType();
							if (ty == null)
								return null;
							return new EnclosingExpr("alignof (", ty, ")");
						case 'z':
							data = data.Substring(2);
							ty = parser.ParseExpr();
							if (ty == null)
								return null;
							return new EnclosingExpr("alignof (", ty, ")");
					}
					return null;
				case 'c':
					switch (data[1]) {
						case 'c':
							data = data.Substring(2);
							ty = parser.ParseType();
							if (ty == null)
								return ty;
							ex = parser.ParseExpr();
							if (ex == null)
								return ex;
							return new CastExpr("const_cast", ty, ex);
						case 'l':
							data = data.Substring(2);
							Node callee = parser.ParseExpr();
							if (callee == null)
								return null;
							int numExprs = names.Count;
							while (!ConsumeIf('E')) {
								Node e = parser.ParseExpr();
								if (e == null)
									return null;
								names.Add(e);
							}
							return new CallExpr(callee, PopTrailingNodeArray(numExprs));
						case 'm':
							data = data.Substring(2);
							return parser.ParseBinaryExpr(",");
						case 'o':
							data = data.Substring(2);
							return parser.ParsePrefixExpr("~");
						case 'v':
							return parser.ParseConversionExpr();
					}
					return null;
				case 'd':
					Node lhs;
					Node rhs;
					switch (data[1]) {
						case 'a':
							data = data.Substring(2);
							ex = parser.ParseExpr();
							if (ex == null)
								return null;
							return new DeleteExpr(ex, global, true);
						case 'c':
							data = data.Substring(2);
							Node t = parser.ParseType();
							if (t == null)
								return null;
							ex = parser.ParseExpr();
							if (ex == null)
								return ex;
							return new CastExpr("dynamic_cast", t, ex);
						case 'e':
							data = data.Substring(2);
							return parser.ParsePrefixExpr("*");
						case 'l':
							data = data.Substring(2);
							Node e = parser.ParseExpr();
							if (e == null)
								return null;
							return new DeleteExpr(e, global, false);
						case 'n':
							return parser.ParseUnresolvedName();
						case 's':
							data = data.Substring(2);
							lhs = parser.ParseExpr();
							if (lhs == null)
								return null;
							rhs = parser.ParseExpr();
							if (rhs == null)
								return null;
							return new MemberExpr(lhs, ".*", rhs);
						case 't':
							data = data.Substring(2);
							lhs = parser.ParseExpr();
							if (lhs == null)
								return null;
							rhs = parser.ParseExpr();
							if (rhs == null)
								return null;
							return new MemberExpr(lhs, ".", rhs);
						case 'v':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("/");
						case 'V':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("/=");
					}
					return null;
				case 'e':
					switch (data[1]) {
						case 'o':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("^");
						case 'O':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("^=");
						case 'q':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("==");
					}
					return null;
				case 'g':
					switch (data[1]) {
						case 'e':
							data = data.Substring(2);
							return parser.ParseBinaryExpr(">=");
						case 't':
							data = data.Substring(2);
							return parser.ParseBinaryExpr(">");
					}
					return null;
				case 'i':
					switch (data[1]) {
						case 'x':
							data = data.Substring(2);
							Node baseNode = parser.ParseExpr();
							if (baseNode == null)
								return null;
							Node index = parser.ParseExpr();
							if (index == null)
								return null;
							return new ArraySubscriptExpr(baseNode, index);
						case 'l':
							data = data.Substring(2);
							int numNames = names.Count;
							while (!ConsumeIf('E')) {
								Node e = parser.ParseBracedExpr();
								if (e == null)
									return null;
								names.Add(e);
							}
							return new InitListExpr(null, PopTrailingNodeArray(numNames));
					}
					return null;
				case 'l':
					switch (data[1]) {
						case 'e':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("<=");
						case 's':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("<<");
						case 'S':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("<<=");
						case 't':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("<");
					}
					return null;
				case 'm':
					switch (data[1]) {
						case 'i':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("-");
						case 'I':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("-=");
						case 'l':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("*");
						case 'L':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("*=");
						case 'm':
							data = data.Substring(2);
							if (ConsumeIf('_')) {
								return parser.ParsePrefixExpr("--");
							}
							ex = parser.ParseExpr();
							if (ex == null)
								return null;
							return new PostfixExpr(ex, "--");
					}
					return null;
				case 'n':
					switch (data[1]) {
						case 'a':
						case 'w':
							return parser.ParseNewExpr();
						case 'e':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("!=");
						case 'g':
							data = data.Substring(2);
							return parser.ParsePrefixExpr("-");
						case 't':
							data = data.Substring(2);
							return parser.ParsePrefixExpr("!");
						case 'x':
							data = data.Substring(2);
							ex = parser.ParseExpr();
							if (ex == null)
								return null;
							return new EnclosingExpr("noexcept (", ex, ")");
					}
					return null;
				case 'o':
					switch (data[1]) {
						case 'n':
							return parser.ParseUnresolvedName();
						case 'o':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("||");
						case 'r':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("|");
						case 'R':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("|=");
					}
					return null;
				case 'p':
					switch (data[1]) {
						case 'm':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("->*");
						case 'l':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("+");
						case 'L':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("+=");
						case 'p':
							data = data.Substring(2);
							if (!ConsumeIf('_')) {
								return parser.ParsePrefixExpr("++");
							}
							ex = parser.ParseExpr();
							if (ex == null)
								return null;
							return new PostfixExpr(ex, "++");
						case 's':
							data = data.Substring(2);
							return parser.ParsePrefixExpr("+");
						case 't':
							data = data.Substring(2);
							Node l = parser.ParseExpr();
							if (l == null)
								return null;
							Node r = parser.ParseExpr();
							if (r == null)
								return r;
							return new MemberExpr(l, "->", r);
					}
					return null;
				case 'q':
					if (data[1] == 'u') {
						data = data.Substring(2);
						Node cond = parser.ParseExpr();
						if (cond == null)
							return null;
						lhs = parser.ParseExpr();
						if (lhs == null)
							return null;
						rhs = parser.ParseExpr();
						if (rhs == null)
							return null;
						return new ConditionalExpr(cond, lhs, rhs);
					}
					return null;
				case 'r':
					switch (data[1]) {
						case 'c':
							data = data.Substring(2);
							Node t = parser.ParseType();
							if (t == null)
								return null;
							ex = parser.ParseExpr();
							if (ex == null)
								return ex;
							return new CastExpr("reinterpret_cast", t, ex);
						case 'm':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("%");
						case 'M':
							data = data.Substring(2);
							return parser.ParseBinaryExpr("%=");
						case 's':
							data = data.Substring(2);
							return parser.ParseBinaryExpr(">>");
						case 'S':
							data = data.Substring(2);
							return parser.ParseBinaryExpr(">>=");
					}
					return null;
				case 's':
					switch (data[1]) {
						case 'c':
							data = data.Substring(2);
							Node t = parser.ParseType();
							if (t == null)
								return null;
							ex = parser.ParseExpr();
							if (ex == null)
								return null;
							return new CastExpr("static_cast", t, ex);
						case 'p':
							data = data.Substring(2);
							Node child = parser.ParseExpr();
							if (child == null)
								return null;
							return new ParameterPackExpansion(child);
						case 'r':
							return parser.ParseUnresolvedName();
						case 't':
							data = data.Substring(2);
							ty = parser.ParseType();
							if (ty == null)
								return null;
							return new EnclosingExpr("sizeof (", ty, ")");
						case 'z':
							data = data.Substring(2);
							ex = parser.ParseExpr();
							if (ex == null)
								return null;
							return new EnclosingExpr("sizeof (", ex, ")");
						case 'Z':
							data = data.Substring(2);
							if (Look() == 'T') {
								Node r = parser.ParseTemplateParam();
								if (r == null)
									return null;
								return new SizeofParamPackExpr(r);
							} else if (Look() == 'f') {
								Node fp = parser.ParseFunctionParam();
								if (fp == null)
									return null;
								return new EnclosingExpr("sizeof... (", fp, ")");
							}
							return null;
						case 'P':
							data = data.Substring(2);
							int nArgs = names.Count;
							while (!ConsumeIf('E')) {
								Node arg = parser.ParseTemplateArg();
								if (arg == null)
									return null;
								names.Add(arg);
							}
							NodeArrayNode pack = new NodeArrayNode(PopTrailingNodeArray(nArgs));
							return new EnclosingExpr("sizeof... (", pack, ")");
					}
					return null;
				case 't':
					switch (data[1]) {
						case 'e':
							data = data.Substring(2);
							ex = parser.ParseExpr();
							if (ex == null)
								return null;
							return new EnclosingExpr("typeid (", ex, ")");
						case 'i':
							data = data.Substring(2);
							ty = parser.ParseType();
							if (ty == null)
								return ty;
							return new EnclosingExpr("typeid (", ty, ")");
						case 'l':
							data = data.Substring(2);
							ty = parser.ParseType();
							if (ty == null)
								return null;
							int numInits = names.Count;
							while (!ConsumeIf('E')) {
								Node e = parser.ParseBracedExpr();
								if (e == null)
									return null;
								names.Add(e);
							}
							return new InitListExpr(ty, PopTrailingNodeArray(numInits));
						case 'r':
							data = data.Substring(2);
							return new NameType("throw");
						case 'w':
							data = data.Substring(2);
							ex = parser.ParseExpr();
							if (ex == null)
								return null;
							return new ThrowExpr(ex);
					}
					return null;
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
					return parser.ParseUnresolvedName();
			}
			return null;
		}

		public bool ParseCallOffset() {
			if (ConsumeIf('h'))
				return string.IsNullOrEmpty(ParseNumber(true)) || !ConsumeIf('_');
			if (ConsumeIf('v'))
				return string.IsNullOrEmpty(ParseNumber(true)) || !ConsumeIf('_') ||
					string.IsNullOrEmpty(ParseNumber(true)) || !ConsumeIf('_');
			return true;
		}

		public Node ParseSpecialName() {
			Node ty;
			Node name;
			switch (Look()) {
				case 'T':
					switch (Look(1)) {
						case 'V':
							data = data.Substring(2);
							ty = parser.ParseType();
							if (ty == null)
								return null;
							return new SpecialName("vtable for ", ty);
						case 'T':
							data = data.Substring(2);
							ty = parser.ParseType();
							if (ty == null)
								return null;
							return new SpecialName("VTT for ", ty);
						case 'I':
							data = data.Substring(2);
							ty = parser.ParseType();
							if (ty == null)
								return null;
							return new SpecialName("typeinfo for ", ty);
						case 'S':
							data = data.Substring(2);
							ty = parser.ParseType();
							if (ty == null)
								return null;
							return new SpecialName("typeinfo name for ", ty);
						case 'c':
							data = data.Substring(2);
							if (ParseCallOffset() || ParseCallOffset())
								return null;
							Node encoding = parser.ParseEncoding();
							if (encoding == null)
								return null;
							return new SpecialName("covariant return thunk to ", encoding);
						case 'C':
							data = data.Substring(2);
							Node firstType = parser.ParseType();
							if (firstType == null)
								return null;
							if (string.IsNullOrEmpty(ParseNumber(true)) || !ConsumeIf('_'))
								return null;
							Node secondType = parser.ParseType();
							if (secondType == null)
								return null;
							return new CtorVtableSpecialName(secondType, firstType);
						case 'W':
							data = data.Substring(2);
							name = parser.ParseName();
							if (name == null)
								return null;
							return new SpecialName("thread-local wrapper routine for ", name);
						case 'H':
							data = data.Substring(2);
							name = parser.ParseName();
							if (name == null)
								return null;
							return new SpecialName("thread-local initialization routine for ", name);
						default:
							data = data.Substring(1);
							bool isVirt = Look() == 'v';
							if (ParseCallOffset())
								return null;
							Node baseEncoding = parser.ParseEncoding();
							if (baseEncoding == null)
								return null;
							if (isVirt)
								return new SpecialName("virtual thunk to ", baseEncoding);
							else
								return new SpecialName("non-virtual thunk to ", baseEncoding);
					}
				case 'G':
					switch (Look(1)) {
						case 'V':
							data = data.Substring(2);
							name = parser.ParseName();
							if (name == null)
								return null;
							return new SpecialName("guard variable for ", name);
						case 'R':
							data = data.Substring(2);
							name = parser.ParseName();
							if (name == null)
								return null;
							bool parsedSeqId = ParseSeqId(out int count);
							if (!ConsumeIf('_') && parsedSeqId)
								return null;
							return new SpecialName("reference temporary for ", name);
					}
					break;
			}
			return null;
		}

		public bool ParseSeqId(out int count) {
			if (!(Look() >= '0' && Look() <= '9') &&
				!(Look() >= 'A' && Look() <= 'Z')) {
				count = -1;
				return true;
			}

			int id = 0;
			while (true) {
				if(Look() >= '0' && Look() <= '9') {
					id *= 36;
					id += Look() - '0';
				} else if(Look() >= 'A' && Look() <= 'Z') {
					id *= 36;
					id += Look() - 'A' + 10;
				} else {
					count = id;
					return false;
				}
				data = data.Substring(1);
			}
		}

		public Node ParseEncoding() {
			switch (Look()) {
				case 'G':
				case 'T':
					return parser.ParseSpecialName();
			}

			bool IsEndOfEncoding() {
				if (data.Length == 0)
					return true;
				switch (Look()) {
					case 'E':
					case '.':
					case '_':
						return true;
					default:
						return false;
				}
			}

			NameState nameInfo = new NameState(this);
			Node name = parser.ParseName(nameInfo);
			if (name == null)
				return null;

			if (ResolveForwardTemplateRefs(nameInfo))
				return null;

			if (IsEndOfEncoding())
				return name;

			Node attrs = null;
			if (ConsumeIf("Ua9enable_ifI")) {
				int beforeArgs = names.Count;
				while (!ConsumeIf('E')) {
					Node arg = parser.ParseTemplateArg();
					if (arg == null)
						return null;
					names.Add(arg);
				}

				attrs = new EnableIfAttr(PopTrailingNodeArray(beforeArgs));
			}

			Node returnType = null;
			if (!nameInfo.CtorDtorConversion && nameInfo.EndsWithTemplateArgs) {
				returnType = parser.ParseType();
				if (returnType == null)
					return null;
			}

			if (ConsumeIf('v')) {
				return new FunctionEncoding(
					returnType, name, new NodeArray(),
					attrs, nameInfo.CVQualifiers,
					nameInfo.ReferenceQualifier
				);
			}

			int paramsBegin = names.Count;
			do {
				Node ty = parser.ParseType();
				if (ty == null)
					return null;
				names.Add(ty);
			} while (!IsEndOfEncoding());

			return new FunctionEncoding(
				returnType, name, PopTrailingNodeArray(paramsBegin),
				attrs, nameInfo.CVQualifiers,
				nameInfo.ReferenceQualifier
			);
		}

		public Node ParseSubstitution() {
			if (!ConsumeIf('S'))
				return null;

			if (char.IsLower(Look())) {
				Node specialSub;
				switch (Look()) {
					case 'a':
						data = data.Substring(1);
						specialSub = new SpecialSubstitution(SpecialSubKind.allocator);
						break;
					case 'b':
						data = data.Substring(1);
						specialSub = new SpecialSubstitution(SpecialSubKind.basic_string);
						break;
					case 's':
						data = data.Substring(1);
						specialSub = new SpecialSubstitution(SpecialSubKind.@string);
						break;
					case 'i':
						data = data.Substring(1);
						specialSub = new SpecialSubstitution(SpecialSubKind.istream);
						break;
					case 'o':
						data = data.Substring(1);
						specialSub = new SpecialSubstitution(SpecialSubKind.ostream);
						break;
					case 'd':
						data = data.Substring(1);
						specialSub = new SpecialSubstitution(SpecialSubKind.iostream);
						break;
					default:
						return null;
				}
				if (specialSub == null)
					return null;

				Node withTags = parser.ParseAbiTags(specialSub);
				if(withTags != specialSub) {
					subs.Add(withTags);
					specialSub = withTags;
				}
				return specialSub;
			}

			if (ConsumeIf('_')) {
				if (subs.Count == 0)
					return null;
				return subs[0];
			}

			if(ParseSeqId(out int index)) {
				return null;
			}

			if (!ConsumeIf('_') || index >= subs.Count)
				return null;

			return subs[index];
		}

		public Node ParseTemplateParam() {
			if (!ConsumeIf('T'))
				return null;

			int index = 0;
			if (!ConsumeIf('_')){
				if (ParsePositiveInteger(out index))
					return null;
				index++;
				if (!ConsumeIf('_'))
					return null;
			}

			if (parsingLambdaParams)
				return new NameType("auto");

			if (permitForwardTemplateReferences) {
				Node forwardRef = new ForwardTemplateReference(index);
				if (forwardRef == null)
					return null;
				Debug.Assert(forwardRef.Kind == ItaniumDemangleNodeType.ForwardTemplateReference);
				ForwardTemplateRefs.Add((ForwardTemplateReference)forwardRef);
				return forwardRef;
			}

			if (index >= templateParams.Count)
				return null;
			return templateParams[index];
		}

		public Node ParseTemplateArg() {
			Node arg;
			switch (Look()) {
				case 'X':
					data = data.Substring(1);
					arg = parser.ParseExpr();
					if (arg == null || !ConsumeIf('E'))
						return null;
					return arg;
				case 'J':
					data = data.Substring(1);
					int numArgs = names.Count;
					while (!ConsumeIf('E')) {
						arg = parser.ParseTemplateArg();
						if (arg == null)
							return null;
						names.Add(arg);
					}
					NodeArray args = PopTrailingNodeArray(numArgs);
					return new TemplateArgumentPack(args);
				case 'L':
					if(Look(1) == 'Z') {
						data = data.Substring(2);
						arg = parser.ParseEncoding();
						if (arg == null || !ConsumeIf('E'))
							return null;
						return arg;
					}
					return parser.ParseExprPrimary();
				default:
					return parser.ParseType();
			}
		}

		public Node ParseTemplateArgs(bool tagTemplates) {
			if (!ConsumeIf('I'))
				return null;

			if (tagTemplates)
				templateParams.Clear();

			int argsbegin = names.Count;
			Node arg;
			while (!ConsumeIf('E')) {
				if (tagTemplates) {
					List<Node> oldParams = templateParams.ToList();
					arg = parser.ParseTemplateArg();
					templateParams = oldParams.ToList();
					if (arg == null)
						return null;
					names.Add(arg);
					Node tableEntry = arg;
					if(arg.Kind == ItaniumDemangleNodeType.TemplateArgumentPack) {
						tableEntry = new ParameterPack(((TemplateArgumentPack)tableEntry).Elements);
						if (tableEntry == null)
							return null;
					}
					templateParams.Add(tableEntry);
				} else {
					arg = parser.ParseTemplateArg();
					if (arg == null)
						return null;
					names.Add(arg);
				}
			}
			return new TemplateArgs(PopTrailingNodeArray(argsbegin));
		}

		public Node Parse() {
			Node encoding;
			if (ConsumeIf("_Z")) {
				encoding = parser.ParseEncoding();
				if (encoding == null)
					return null;
				if(Look() == '.') {
					encoding = new DotSuffix(encoding, data);
					data = data.Substring(data.Length);
				}
				if (data.Length != 0)
					return null;
				return encoding;
			}

			if (ConsumeIf("___Z")) {
				encoding = parser.ParseEncoding();
				if (encoding == null || !ConsumeIf("_block_invoke"))
					return null;
				bool requireNumber = ConsumeIf('_');
				if(string.IsNullOrEmpty(ParseNumber()) && requireNumber) {
					return null;
				}
				if (Look() == '.')
					data = data.Substring(data.Length);

				if (data.Length != 0)
					return null;

				return new SpecialName("invocation function for block in ", encoding);
			}

			Node ty = parser.ParseType();
			if (data.Length != 0)
				return null;

			return ty;
		}

		public static bool IsXDigit(char ch) {
			if ('0' <= ch && ch <= '9') return true;
			if ('a' <= ch && ch <= 'f') return true;
			if ('A' <= ch && ch <= 'F') return true;
			return false;
		}

		public Node ParseFloatingLiteral(FloatingData fData) {
			int n = fData.MangledSize;
			if (data.Length <= n)
				return null;

			string literalData = data.Substring(0, n);
			foreach(char c in literalData) {
				if (!IsXDigit(c))
					return null;
			}
			data = data.Substring(n);
			if (!ConsumeIf('E'))
				return null;

			switch (fData) {
				case FloatData floatData:
					return new FloatLiteralImpl<float>(literalData);
				case DoubleData doubleData:
					return new FloatLiteralImpl<double>(literalData);
				case LongDoubleData longDoubleData:
					return new FloatLiteralImpl<Decimal>(literalData);
			}

			return null;
		}
	}
}
