using SharpDemangler.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Itanium
{
	public class ItaniumPartialDemangler
	{
		private Node RootNode = null;
		private Node Context = null;

		public ItaniumPartialDemangler() {

		}

		public bool PartialDemangle(string mangledName) {
			ManglingParser parser = new ManglingParser(null);

			int len = mangledName.Length;
			parser.Reset(mangledName);

			RootNode = parser.Parse();
			return RootNode == null;
		}

		public string FinishDemangle() {
			Assert.True(RootNode != null);
			return PrintNode(RootNode);
		}

		public string PrintNode(Node rootNode) {
			OutputStream sb = new OutputStream();
			rootNode.Print(sb);
			return sb.ToString();
		}

		public string GetFunctionBaseName(ref int? n) {
			if (!IsFunction)
				return null;

			Node name = ((FunctionEncoding)RootNode).Name;

			while (true) {
				switch (name.Kind) {
					case ItaniumDemangleNodeType.AbiTagAttr:
						name = ((AbiTagAttr)name).BaseNode;
						continue;
					case ItaniumDemangleNodeType.StdQualifiedName:
						name = ((StdQualifiedName)name).Child;
						continue;
					case ItaniumDemangleNodeType.NestedName:
						name = ((NestedName)name).Name;
						continue;
					case ItaniumDemangleNodeType.LocalName:
						name = ((LocalName)name).Entity;
						continue;
					case ItaniumDemangleNodeType.NameWithTemplateArgs:
						name = ((NameWithTemplateArgs)name).Name;
						continue;
					default:
						return PrintNode(name);
				}
			}
		}

		public string GetFunctionDeclContextName(string buf, ref int? n) {
			if (!IsFunction)
				return null;

			Node name = ((FunctionEncoding)RootNode).Name;

			OutputStream sb = new OutputStream();

			KeepGoingLocalFunction:
			while (true) {
				if(name.Kind == ItaniumDemangleNodeType.AbiTagAttr) {
					name = ((AbiTagAttr)name).BaseNode;
					continue;
				}
				if(name.Kind == ItaniumDemangleNodeType.NameWithTemplateArgs) {
					name = ((NameWithTemplateArgs)name).Name;
					continue;
				}
				break;
			}

			switch (name.Kind) {
				case ItaniumDemangleNodeType.StdQualifiedName:
					sb.Append("std");
					break;
				case ItaniumDemangleNodeType.NestedName:
					((NestedName)name).Qual.Print(sb);
					break;
				case ItaniumDemangleNodeType.LocalName:
					LocalName ln = ((LocalName)name);
					ln.Encoding.Print(sb);
					sb.Append("::");
					name = ln.Entity;
					goto KeepGoingLocalFunction;
				default:
					break;
			}

			if(n != null) {
				n = sb.Length;
			}
			return sb.ToString();
		}

		public string GetFunctionName() {
			if (!IsFunction)
				return null;

			Node name = ((FunctionEncoding)RootNode).Name;
			return PrintNode(name);
		}

		public string GetFunctionParameters(string buf, ref int? n) {
			if (!IsFunction)
				return null;

			NodeArray Params = ((FunctionEncoding)RootNode).Params;

			OutputStream sb = new OutputStream();
			sb.Append('(');
			Params.PrintWithComma(sb);
			sb.Append(')');

			if(n != null) {
				n = sb.Length;
			}
			return sb.ToString();
		}

		public string GetFunctionReturnType(string buf, ref int? n) {
			if (!IsFunction)
				return null;

			OutputStream sb = new OutputStream();
			Node ret = ((FunctionEncoding)RootNode).ReturnType;
			if (ret != null)
				ret.Print(sb);

			if(n != null) {
				n = sb.Length;
			}
			return sb.ToString();
		}

		public bool IsFunction {
			get {
				Assert.True(RootNode != null);
				return RootNode.Kind == ItaniumDemangleNodeType.FunctionEncoding;
			}
		}

		public bool IsSpecialName {
			get {
				Assert.True(RootNode != null);
				return RootNode.Kind == ItaniumDemangleNodeType.SpecialName || RootNode.Kind == ItaniumDemangleNodeType.CtorVtableSpecialName;
			}
		}

		public bool IsData {
			get {
				return !IsFunction && !IsSpecialName;
			}
		}

		public bool HasFunctionQualifiers {
			get {
				Assert.True(RootNode != null);
				if (!IsFunction)
					return false;
				FunctionEncoding e = (FunctionEncoding)RootNode;
				return e.CVQuals != Qualifiers.None && e.RefQual != FunctionRefQual.None; 
			}
		}

		public bool IsCtorOrDtor {
			get {
				Node n = RootNode;
				while(n != null) {
					switch (n.Kind) {
						default:
							return false;
						case ItaniumDemangleNodeType.CtorDtorName:
							return true;
						case ItaniumDemangleNodeType.AbiTagAttr:
							n = ((AbiTagAttr)n).BaseNode;
							break;
						case ItaniumDemangleNodeType.LocalName:
							n = ((LocalName)n).Entity;
							break;
						case ItaniumDemangleNodeType.NameWithTemplateArgs:
							n = ((NameWithTemplateArgs)n).Name;
							break;
						case ItaniumDemangleNodeType.NestedName:
							n = ((NestedName)n).Name;
							break;
						case ItaniumDemangleNodeType.QualifiedName:
							n = ((StdQualifiedName)n).Child;
							break;
					}
				}
				return false;
			}
		}
	}
}
