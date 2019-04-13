using SharpDemangler.Itanium;
using SharpDemangler.Microsoft;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SharpDemangler.Microsoft.MicrosoftDemangler;

namespace SharpDemangler
{
	class Program
	{
		static void MainItanium(string[] args) {
			int rc = 1;
			ItaniumPartialDemangler demangler = new ItaniumPartialDemangler();
			if (demangler.PartialDemangle("_ZNSt6vectorIcSaIcEE15_M_range_insertIPcEEvN9__gnu_cxx17__normal_iteratorIS3_S1_EET_S7_St20forward_iterator_tag")) {
				Console.Error.WriteLine("PartialDemangle encountered an error");
				goto exit;
			}
			string res = demangler.FinishDemangle();
			if (string.IsNullOrEmpty(res)) {
				Console.Error.WriteLine("FinishDemangle encountered an error");
				goto exit;
			}

			Console.WriteLine(res);
			rc = 0;

			exit:
			Console.ReadLine();
			Environment.Exit(rc);
		}

		static void MainMicrosoft(string[] args) {
			string demangled = MicrosoftDemangler.Demangle("?cout@std@@3V?$basic_ostream@DU?$char_traits@D@std@@@1@A", out DemangleStatus status);
			if(status != DemangleStatus.Success) {
				Console.Error.WriteLine("FAIL");
				return;
			}

			Console.WriteLine(demangled);
			Trace.WriteLine(demangled);
			Console.ReadLine();
		}

		static void Main(string[] args) {
			MainItanium(args);
			MainMicrosoft(args);
		}
	}
}
