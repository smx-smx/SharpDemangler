using System.Text;

namespace SharpDemangler.Itanium
{
	public class ForwardTemplateReference : Node
	{
		public int Index;
		public Node Ref = null;

		bool printing = false;

		public ForwardTemplateReference(int index) : base(ItaniumDemangleNodeType.ForwardTemplateReference) {
			this.Index = index;
		}

		public override bool HasRHSComponent {
			get {
				if (printing)
					return false;

				bool ret = false;
				SwapAndRestore.Execute(ref printing, true, () => {
					ret = Ref.HasRHSComponent;
				});

				return ret;
			}
		}

		public override bool HasArray {
			get {
				if (printing)
					return false;

				bool ret = false;
				SwapAndRestore.Execute(ref printing, true, () => {
					ret = Ref.HasArray;
				});

				return ret;
			}
		}

		public override bool HasFunction {
			get {
				if (printing)
					return false;

				bool ret = false;
				SwapAndRestore.Execute(ref printing, true, () => {
					ret = Ref.HasFunction;
				});

				return ret;
			}
		}

		public override Node SyntaxNode {
			get {
				if (printing)
					return this;

				Node ret = null;
				SwapAndRestore.Execute(ref printing, true, () => {
					ret = Ref.SyntaxNode;
				});
				return ret;
			}
		}

		public override void PrintLeft(OutputStream sb) {
			if (printing)
				return;

			SwapAndRestore.Execute(ref printing, true, () => {
				Ref.PrintLeft(sb);
			});
		}

		public override void PrintRight(OutputStream sb) {
			if (printing)
				return;

			SwapAndRestore.Execute(ref printing, true, () => {
				Ref.PrintRight(sb);
			});
		}
	}
}