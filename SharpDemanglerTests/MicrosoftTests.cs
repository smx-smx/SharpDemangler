using System;
using NUnit.Framework;
using SharpDemangler.Microsoft;

namespace SharpDemanglerTests
{
	[TestFixture]
	public class MicrosoftTests
	{
		private static void AssertMangling(string mangled, string expected) {
			Assert.AreEqual(expected, MicrosoftDemangler.Demangle(mangled, out MicrosoftDemangler.DemangleStatus status));
			Assert.AreEqual(MicrosoftDemangler.DemangleStatus.Success, status);
		}

		#region ArgQualifiers

		[Test]
		public void ArgQualifiers1() {
			AssertMangling("?foo@@YAXI@Z", "void __cdecl foo(unsigned int)");
		}
		[Test]
		public void ArgQualifiers2() {
			AssertMangling("?foo@@YAXN@Z", "void __cdecl foo(double)");
		}
		[Test]
		public void ArgQualifiers3() {
			AssertMangling("?foo_pad@@YAXPAD@Z", "void __cdecl foo_pad(char *)");
		}
		[Test]
		public void ArgQualifiers4() {
			AssertMangling("?foo_pad@@YAXPEAD@Z", "void __cdecl foo_pad(char *)");
		}
		[Test]
		public void ArgQualifiers5() {
			AssertMangling("?foo_pbd@@YAXPBD@Z", "void __cdecl foo_pbd(char const *)");
		}
		[Test]
		public void ArgQualifiers6() {
			AssertMangling("?foo_pbd@@YAXPEBD@Z", "void __cdecl foo_pbd(char const *)");
		}
		[Test]
		public void ArgQualifiers7() {
			AssertMangling("?foo_pcd@@YAXPCD@Z", "void __cdecl foo_pcd(char volatile *)");
		}
		[Test]
		public void ArgQualifiers8() {
			AssertMangling("?foo_pcd@@YAXPECD@Z", "void __cdecl foo_pcd(char volatile *)");
		}
		[Test]
		public void ArgQualifiers9() {
			AssertMangling("?foo_qad@@YAXQAD@Z", "void __cdecl foo_qad(char *const)");
		}
		[Test]
		public void ArgQualifiers10() {
			AssertMangling("?foo_qad@@YAXQEAD@Z", "void __cdecl foo_qad(char *const)");
		}
		[Test]
		public void ArgQualifiers11() {
			AssertMangling("?foo_rad@@YAXRAD@Z", "void __cdecl foo_rad(char *volatile)");
		}
		[Test]
		public void ArgQualifiers12() {
			AssertMangling("?foo_rad@@YAXREAD@Z", "void __cdecl foo_rad(char *volatile)");
		}
		[Test]
		public void ArgQualifiers13() {
			AssertMangling("?foo_sad@@YAXSAD@Z", "void __cdecl foo_sad(char *const volatile)");
		}
		[Test]
		public void ArgQualifiers14() {
			AssertMangling("?foo_sad@@YAXSEAD@Z", "void __cdecl foo_sad(char *const volatile)");
		}
		[Test]
		public void ArgQualifiers15() {
			AssertMangling("?foo_piad@@YAXPIAD@Z", "void __cdecl foo_piad(char *__restrict)");
		}
		[Test]
		public void ArgQualifiers16() {
			AssertMangling("?foo_piad@@YAXPEIAD@Z", "void __cdecl foo_piad(char *__restrict)");
		}
		[Test]
		public void ArgQualifiers17() {
			AssertMangling("?foo_qiad@@YAXQIAD@Z", "void __cdecl foo_qiad(char *const __restrict)");
		}
		[Test]
		public void ArgQualifiers18() {
			AssertMangling("?foo_qiad@@YAXQEIAD@Z", "void __cdecl foo_qiad(char *const __restrict)");
		}
		[Test]
		public void ArgQualifiers19() {
			AssertMangling("?foo_riad@@YAXRIAD@Z", "void __cdecl foo_riad(char *volatile __restrict)");
		}
		[Test]
		public void ArgQualifiers20() {
			AssertMangling("?foo_riad@@YAXREIAD@Z", "void __cdecl foo_riad(char *volatile __restrict)");
		}
		[Test]
		public void ArgQualifiers21() {
			AssertMangling("?foo_siad@@YAXSIAD@Z", "void __cdecl foo_siad(char *const volatile __restrict)");
		}
		[Test]
		public void ArgQualifiers22() {
			AssertMangling("?foo_siad@@YAXSEIAD@Z", "void __cdecl foo_siad(char *const volatile __restrict)");
		}
		[Test]
		public void ArgQualifiers23() {
			AssertMangling("?foo_papad@@YAXPAPAD@Z", "void __cdecl foo_papad(char **)");
		}
		[Test]
		public void ArgQualifiers24() {
			AssertMangling("?foo_papad@@YAXPEAPEAD@Z", "void __cdecl foo_papad(char **)");
		}
		[Test]
		public void ArgQualifiers25() {
			AssertMangling("?foo_papbd@@YAXPAPBD@Z", "void __cdecl foo_papbd(char const **)");
		}
		[Test]
		public void ArgQualifiers26() {
			AssertMangling("?foo_papbd@@YAXPEAPEBD@Z", "void __cdecl foo_papbd(char const **)");
		}
		[Test]
		public void ArgQualifiers27() {
			AssertMangling("?foo_papcd@@YAXPAPCD@Z", "void __cdecl foo_papcd(char volatile **)");
		}
		[Test]
		public void ArgQualifiers28() {
			AssertMangling("?foo_papcd@@YAXPEAPECD@Z", "void __cdecl foo_papcd(char volatile **)");
		}
		[Test]
		public void ArgQualifiers29() {
			AssertMangling("?foo_pbqad@@YAXPBQAD@Z", "void __cdecl foo_pbqad(char *const *)");
		}
		[Test]
		public void ArgQualifiers30() {
			AssertMangling("?foo_pbqad@@YAXPEBQEAD@Z", "void __cdecl foo_pbqad(char *const *)");
		}
		[Test]
		public void ArgQualifiers31() {
			AssertMangling("?foo_pcrad@@YAXPCRAD@Z", "void __cdecl foo_pcrad(char *volatile *)");
		}
		[Test]
		public void ArgQualifiers32() {
			AssertMangling("?foo_pcrad@@YAXPECREAD@Z", "void __cdecl foo_pcrad(char *volatile *)");
		}
		[Test]
		public void ArgQualifiers33() {
			AssertMangling("?foo_qapad@@YAXQAPAD@Z", "void __cdecl foo_qapad(char **const)");
		}
		[Test]
		public void ArgQualifiers34() {
			AssertMangling("?foo_qapad@@YAXQEAPEAD@Z", "void __cdecl foo_qapad(char **const)");
		}
		[Test]
		public void ArgQualifiers35() {
			AssertMangling("?foo_rapad@@YAXRAPAD@Z", "void __cdecl foo_rapad(char **volatile)");
		}
		[Test]
		public void ArgQualifiers36() {
			AssertMangling("?foo_rapad@@YAXREAPEAD@Z", "void __cdecl foo_rapad(char **volatile)");
		}
		[Test]
		public void ArgQualifiers37() {
			AssertMangling("?foo_pbqbd@@YAXPBQBD@Z", "void __cdecl foo_pbqbd(char const *const *)");
		}
		[Test]
		public void ArgQualifiers38() {
			AssertMangling("?foo_pbqbd@@YAXPEBQEBD@Z", "void __cdecl foo_pbqbd(char const *const *)");
		}
		[Test]
		public void ArgQualifiers39() {
			AssertMangling("?foo_pbqcd@@YAXPBQCD@Z", "void __cdecl foo_pbqcd(char volatile *const *)");
		}
		[Test]
		public void ArgQualifiers40() {
			AssertMangling("?foo_pbqcd@@YAXPEBQECD@Z", "void __cdecl foo_pbqcd(char volatile *const *)");
		}
		[Test]
		public void ArgQualifiers41() {
			AssertMangling("?foo_pcrbd@@YAXPCRBD@Z", "void __cdecl foo_pcrbd(char const *volatile *)");
		}
		[Test]
		public void ArgQualifiers42() {
			AssertMangling("?foo_pcrbd@@YAXPECREBD@Z", "void __cdecl foo_pcrbd(char const *volatile *)");
		}
		[Test]
		public void ArgQualifiers43() {
			AssertMangling("?foo_pcrcd@@YAXPCRCD@Z", "void __cdecl foo_pcrcd(char volatile *volatile *)");
		}
		[Test]
		public void ArgQualifiers44() {
			AssertMangling("?foo_pcrcd@@YAXPECRECD@Z", "void __cdecl foo_pcrcd(char volatile *volatile *)");
		}
		[Test]
		public void ArgQualifiers45() {
			AssertMangling("?foo_aad@@YAXAEAD@Z", "void __cdecl foo_aad(char &)");
		}
		[Test]
		public void ArgQualifiers46() {
			AssertMangling("?foo_abd@@YAXABD@Z", "void __cdecl foo_abd(char const &)");
		}
		[Test]
		public void ArgQualifiers47() {
			AssertMangling("?foo_abd@@YAXAEBD@Z", "void __cdecl foo_abd(char const &)");
		}
		[Test]
		public void ArgQualifiers48() {
			AssertMangling("?foo_aapad@@YAXAAPAD@Z", "void __cdecl foo_aapad(char *&)");
		}
		[Test]
		public void ArgQualifiers49() {
			AssertMangling("?foo_aapad@@YAXAEAPEAD@Z", "void __cdecl foo_aapad(char *&)");
		}
		[Test]
		public void ArgQualifiers50() {
			AssertMangling("?foo_aapbd@@YAXAAPBD@Z", "void __cdecl foo_aapbd(char const *&)");
		}
		[Test]
		public void ArgQualifiers51() {
			AssertMangling("?foo_aapbd@@YAXAEAPEBD@Z", "void __cdecl foo_aapbd(char const *&)");
		}
		[Test]
		public void ArgQualifiers52() {
			AssertMangling("?foo_abqad@@YAXABQAD@Z", "void __cdecl foo_abqad(char *const &)");
		}
		[Test]
		public void ArgQualifiers53() {
			AssertMangling("?foo_abqad@@YAXAEBQEAD@Z", "void __cdecl foo_abqad(char *const &)");
		}
		[Test]
		public void ArgQualifiers54() {
			AssertMangling("?foo_abqbd@@YAXABQBD@Z", "void __cdecl foo_abqbd(char const *const &)");
		}
		[Test]
		public void ArgQualifiers55() {
			AssertMangling("?foo_abqbd@@YAXAEBQEBD@Z", "void __cdecl foo_abqbd(char const *const &)");
		}
		[Test]
		public void ArgQualifiers56() {
			AssertMangling("?foo_aay144h@@YAXAAY144H@Z", "void __cdecl foo_aay144h(int (&)[5][5])");
		}
		[Test]
		public void ArgQualifiers57() {
			AssertMangling("?foo_aay144h@@YAXAEAY144H@Z", "void __cdecl foo_aay144h(int (&)[5][5])");
		}
		[Test]
		public void ArgQualifiers58() {
			AssertMangling("?foo_aay144cbh@@YAXAAY144$$CBH@Z", "void __cdecl foo_aay144cbh(int const (&)[5][5])");
		}
		[Test]
		public void ArgQualifiers59() {
			AssertMangling("?foo_aay144cbh@@YAXAEAY144$$CBH@Z", "void __cdecl foo_aay144cbh(int const (&)[5][5])");
		}
		[Test]
		public void ArgQualifiers60() {
			AssertMangling("?foo_qay144h@@YAX$$QAY144H@Z", "void __cdecl foo_qay144h(int (&&)[5][5])");
		}
		[Test]
		public void ArgQualifiers61() {
			AssertMangling("?foo_qay144h@@YAX$$QEAY144H@Z", "void __cdecl foo_qay144h(int (&&)[5][5])");
		}
		[Test]
		public void ArgQualifiers62() {
			AssertMangling("?foo_qay144cbh@@YAX$$QAY144$$CBH@Z", "void __cdecl foo_qay144cbh(int const (&&)[5][5])");
		}
		[Test]
		public void ArgQualifiers63() {
			AssertMangling("?foo_qay144cbh@@YAX$$QEAY144$$CBH@Z", "void __cdecl foo_qay144cbh(int const (&&)[5][5])");
		}
		[Test]
		public void ArgQualifiers64() {
			AssertMangling("?foo_p6ahxz@@YAXP6AHXZ@Z", "void __cdecl foo_p6ahxz(int (__cdecl *)(void))");
		}
		[Test]
		public void ArgQualifiers65() {
			AssertMangling("?foo_p6ahxz@@YAXP6AHXZ@Z", "void __cdecl foo_p6ahxz(int (__cdecl *)(void))");
		}
		[Test]
		public void ArgQualifiers66() {
			AssertMangling("?foo_a6ahxz@@YAXA6AHXZ@Z", "void __cdecl foo_a6ahxz(int (__cdecl &)(void))");
		}
		[Test]
		public void ArgQualifiers67() {
			AssertMangling("?foo_a6ahxz@@YAXA6AHXZ@Z", "void __cdecl foo_a6ahxz(int (__cdecl &)(void))");
		}
		[Test]
		public void ArgQualifiers68() {
			AssertMangling("?foo_q6ahxz@@YAX$$Q6AHXZ@Z", "void __cdecl foo_q6ahxz(int (__cdecl &&)(void))");
		}
		[Test]
		public void ArgQualifiers69() {
			AssertMangling("?foo_q6ahxz@@YAX$$Q6AHXZ@Z", "void __cdecl foo_q6ahxz(int (__cdecl &&)(void))");
		}
		[Test]
		public void ArgQualifiers70() {
			AssertMangling("?foo_qay04h@@YAXQEAY04H@Z", "void __cdecl foo_qay04h(int (*const)[5])");
		}
		[Test]
		public void ArgQualifiers71() {
			AssertMangling("?foo_qay04cbh@@YAXQAY04$$CBH@Z", "void __cdecl foo_qay04cbh(int const (*const)[5])");
		}
		[Test]
		public void ArgQualifiers72() {
			AssertMangling("?foo_qay04cbh@@YAXQEAY04$$CBH@Z", "void __cdecl foo_qay04cbh(int const (*const)[5])");
		}
		[Test]
		public void ArgQualifiers73() {
			AssertMangling("?foo@@YAXPAY02N@Z", "void __cdecl foo(double (*)[3])");
		}
		[Test]
		public void ArgQualifiers74() {
			AssertMangling("?foo@@YAXPEAY02N@Z", "void __cdecl foo(double (*)[3])");
		}
		[Test]
		public void ArgQualifiers75() {
			AssertMangling("?foo@@YAXQAN@Z", "void __cdecl foo(double *const)");
		}
		[Test]
		public void ArgQualifiers76() {
			AssertMangling("?foo@@YAXQEAN@Z", "void __cdecl foo(double *const)");
		}
		[Test]
		public void ArgQualifiers77() {
			AssertMangling("?foo_const@@YAXQBN@Z", "void __cdecl foo_const(double const *const)");
		}
		[Test]
		public void ArgQualifiers78() {
			AssertMangling("?foo_const@@YAXQEBN@Z", "void __cdecl foo_const(double const *const)");
		}
		[Test]
		public void ArgQualifiers79() {
			AssertMangling("?foo_volatile@@YAXQCN@Z", "void __cdecl foo_volatile(double volatile *const)");
		}
		[Test]
		public void ArgQualifiers80() {
			AssertMangling("?foo_volatile@@YAXQECN@Z", "void __cdecl foo_volatile(double volatile *const)");
		}
		[Test]
		public void ArgQualifiers81() {
			AssertMangling("?foo@@YAXPAY02NQBNN@Z", "void __cdecl foo(double (*)[3], double const *const, double)");
		}
		[Test]
		public void ArgQualifiers82() {
			AssertMangling("?foo@@YAXPEAY02NQEBNN@Z", "void __cdecl foo(double (*)[3], double const *const, double)");
		}
		[Test]
		public void ArgQualifiers83() {
			AssertMangling("?foo_fnptrconst@@YAXP6AXQAH@Z@Z", "void __cdecl foo_fnptrconst(void (__cdecl *)(int *const))");
		}
		[Test]
		public void ArgQualifiers84() {
			AssertMangling("?foo_fnptrconst@@YAXP6AXQEAH@Z@Z", "void __cdecl foo_fnptrconst(void (__cdecl *)(int *const))");
		}
		[Test]
		public void ArgQualifiers85() {
			AssertMangling("?foo_fnptrarray@@YAXP6AXQAH@Z@Z", "void __cdecl foo_fnptrarray(void (__cdecl *)(int *const))");
		}
		[Test]
		public void ArgQualifiers86() {
			AssertMangling("?foo_fnptrarray@@YAXP6AXQEAH@Z@Z", "void __cdecl foo_fnptrarray(void (__cdecl *)(int *const))");
		}
		[Test]
		public void ArgQualifiers87() {
			AssertMangling("?foo_fnptrbackref1@@YAXP6AXQAH@Z1@Z", "void __cdecl foo_fnptrbackref1(void (__cdecl *)(int *const), void (__cdecl *)(int *const))");
		}
		[Test]
		public void ArgQualifiers88() {
			AssertMangling("?foo_fnptrbackref1@@YAXP6AXQEAH@Z1@Z", "void __cdecl foo_fnptrbackref1(void (__cdecl *)(int *const), void (__cdecl *)(int *const))");
		}
		[Test]
		public void ArgQualifiers89() {
			AssertMangling("?foo_fnptrbackref2@@YAXP6AXQAH@Z1@Z", "void __cdecl foo_fnptrbackref2(void (__cdecl *)(int *const), void (__cdecl *)(int *const))");
		}
		[Test]
		public void ArgQualifiers90() {
			AssertMangling("?foo_fnptrbackref2@@YAXP6AXQEAH@Z1@Z", "void __cdecl foo_fnptrbackref2(void (__cdecl *)(int *const), void (__cdecl *)(int *const))");
		}
		[Test]
		public void ArgQualifiers91() {
			AssertMangling("?foo_fnptrbackref3@@YAXP6AXQAH@Z1@Z", "void __cdecl foo_fnptrbackref3(void (__cdecl *)(int *const), void (__cdecl *)(int *const))");
		}
		[Test]
		public void ArgQualifiers92() {
			AssertMangling("?foo_fnptrbackref3@@YAXP6AXQEAH@Z1@Z", "void __cdecl foo_fnptrbackref3(void (__cdecl *)(int *const), void (__cdecl *)(int *const))");
		}
		[Test]
		public void ArgQualifiers93() {
			AssertMangling("?foo_fnptrbackref4@@YAXP6AXPAH@Z1@Z", "void __cdecl foo_fnptrbackref4(void (__cdecl *)(int *), void (__cdecl *)(int *))");
		}
		[Test]
		public void ArgQualifiers94() {
			AssertMangling("?foo_fnptrbackref4@@YAXP6AXPEAH@Z1@Z", "void __cdecl foo_fnptrbackref4(void (__cdecl *)(int *), void (__cdecl *)(int *))");
		}
		[Test]
		public void ArgQualifiers95() {
			AssertMangling("?ret_fnptrarray@@YAP6AXQAH@ZXZ", "void (__cdecl * __cdecl ret_fnptrarray(void))(int *const)");
		}
		[Test]
		public void ArgQualifiers96() {
			AssertMangling("?ret_fnptrarray@@YAP6AXQEAH@ZXZ", "void (__cdecl * __cdecl ret_fnptrarray(void))(int *const)");
		}
		[Test]
		public void ArgQualifiers97() {
			AssertMangling("?mangle_no_backref0@@YAXQAHPAH@Z", "void __cdecl mangle_no_backref0(int *const, int *)");
		}
		[Test]
		public void ArgQualifiers98() {
			AssertMangling("?mangle_no_backref0@@YAXQEAHPEAH@Z", "void __cdecl mangle_no_backref0(int *const, int *)");
		}
		[Test]
		public void ArgQualifiers99() {
			AssertMangling("?mangle_no_backref1@@YAXQAHQAH@Z", "void __cdecl mangle_no_backref1(int *const, int *const)");
		}
		[Test]
		public void ArgQualifiers100() {
			AssertMangling("?mangle_no_backref1@@YAXQEAHQEAH@Z", "void __cdecl mangle_no_backref1(int *const, int *const)");
		}
		[Test]
		public void ArgQualifiers101() {
			AssertMangling("?mangle_no_backref2@@YAXP6AXXZP6AXXZ@Z", "void __cdecl mangle_no_backref2(void (__cdecl *)(void), void (__cdecl *)(void))");
		}
		[Test]
		public void ArgQualifiers102() {
			AssertMangling("?mangle_no_backref2@@YAXP6AXXZP6AXXZ@Z", "void __cdecl mangle_no_backref2(void (__cdecl *)(void), void (__cdecl *)(void))");
		}
		[Test]
		public void ArgQualifiers103() {
			AssertMangling("?mangle_yes_backref0@@YAXQAH0@Z", "void __cdecl mangle_yes_backref0(int *const, int *const)");
		}
		[Test]
		public void ArgQualifiers104() {
			AssertMangling("?mangle_yes_backref0@@YAXQEAH0@Z", "void __cdecl mangle_yes_backref0(int *const, int *const)");
		}
		[Test]
		public void ArgQualifiers105() {
			AssertMangling("?mangle_yes_backref1@@YAXQAH0@Z", "void __cdecl mangle_yes_backref1(int *const, int *const)");
		}
		[Test]
		public void ArgQualifiers106() {
			AssertMangling("?mangle_yes_backref1@@YAXQEAH0@Z", "void __cdecl mangle_yes_backref1(int *const, int *const)");
		}
		[Test]
		public void ArgQualifiers107() {
			AssertMangling("?mangle_yes_backref2@@YAXQBQ6AXXZ0@Z", "void __cdecl mangle_yes_backref2(void (__cdecl *const *const)(void), void (__cdecl *const *const)(void))");
		}
		[Test]
		public void ArgQualifiers108() {
			AssertMangling("?mangle_yes_backref2@@YAXQEBQ6AXXZ0@Z", "void __cdecl mangle_yes_backref2(void (__cdecl *const *const)(void), void (__cdecl *const *const)(void))");
		}
		[Test]
		public void ArgQualifiers109() {
			AssertMangling("?mangle_yes_backref3@@YAXQAP6AXXZ0@Z", "void __cdecl mangle_yes_backref3(void (__cdecl **const)(void), void (__cdecl **const)(void))");
		}
		[Test]
		public void ArgQualifiers110() {
			AssertMangling("?mangle_yes_backref3@@YAXQEAP6AXXZ0@Z", "void __cdecl mangle_yes_backref3(void (__cdecl **const)(void), void (__cdecl **const)(void))");
		}
		[Test]
		public void ArgQualifiers111() {
			AssertMangling("?mangle_yes_backref4@@YAXQIAH0@Z", "void __cdecl mangle_yes_backref4(int *const __restrict, int *const __restrict)");
		}
		[Test]
		public void ArgQualifiers112() {
			AssertMangling("?mangle_yes_backref4@@YAXQEIAH0@Z", "void __cdecl mangle_yes_backref4(int *const __restrict, int *const __restrict)");
		}
		[Test]
		public void ArgQualifiers113() {
			AssertMangling("?pr23325@@YAXQBUS@@0@Z", "void __cdecl pr23325(struct S const *const, struct S const *const)");
		}
		[Test]
		public void ArgQualifiers114() {
			AssertMangling("?pr23325@@YAXQEBUS@@0@Z", "void __cdecl pr23325(struct S const *const, struct S const *const)");
		}
		#endregion

		#region Basic
		[Test]
		public void Basic1() {
			AssertMangling("?x@@3HA", "int x");
		}
		[Test]
		public void Basic2() {
			AssertMangling("?x@@3PEAHEA", "int *x");
		}
		[Test]
		public void Basic3() {
			AssertMangling("?x@@3PEAPEAHEA", "int **x");
		}
		[Test]
		public void Basic4() {
			AssertMangling("?x@@3PEAY02HEA", "int (*x)[3]");
		}
		[Test]
		public void Basic5() {
			AssertMangling("?x@@3PEAY124HEA", "int (*x)[3][5]");
		}
		[Test]
		public void Basic6() {
			AssertMangling("?x@@3PEAY02$$CBHEA", "int const (*x)[3]");
		}
		[Test]
		public void Basic7() {
			AssertMangling("?x@@3PEAEEA", "unsigned char *x");
		}
		[Test]
		public void Basic8() {
			AssertMangling("?x@@3PEAY1NKM@5HEA", "int (*x)[3500][6]");
		}
		[Test]
		public void Basic9() {
			AssertMangling("?x@@YAXMH@Z", "void __cdecl x(float, int)");
		}
		[Test]
		public void Basic10() {
			AssertMangling("?x@@3P6AHMNH@ZEA", "int (__cdecl *x)(float, double, int)");
		}
		[Test]
		public void Basic11() {
			AssertMangling("?x@@3P6AHP6AHM@ZN@ZEA", "int (__cdecl *x)(int (__cdecl *)(float), double)");
		}
		[Test]
		public void Basic12() {
			AssertMangling("?x@@3P6AHP6AHM@Z0@ZEA", "int (__cdecl *x)(int (__cdecl *)(float), int (__cdecl *)(float))");
		}
		[Test]
		public void Basic13() {
			AssertMangling("?x@ns@@3HA", "int ns::x");
		}
		[Test]
		public void Basic14() {
			AssertMangling("?x@@3PEAHEA", "int *x");
		}
		[Test]
		public void Basic15() {
			AssertMangling("?x@@3PEBHEB", "int const *x");
		}
		[Test]
		public void Basic16() {
			AssertMangling("?x@@3QEAHEA", "int *const x");
		}
		[Test]
		public void Basic17() {
			AssertMangling("?x@@3QEBHEB", "int const *const x");
		}
		[Test]
		public void Basic18() {
			AssertMangling("?x@@3AEBHEB", "int const &x");
		}
		[Test]
		public void Basic19() {
			AssertMangling("?x@@3PEAUty@@EA", "struct ty *x");
		}
		[Test]
		public void Basic20() {
			AssertMangling("?x@@3PEATty@@EA", "union ty *x");
		}
		[Test]
		public void Basic21() {
			AssertMangling("?x@@3PEAVty@@EA", "class ty *x");
		}
		[Test]
		public void Basic22() {
			AssertMangling("?x@@3PEAW4ty@@EA", "enum ty *x");
		}
		[Test]
		public void Basic23() {
			AssertMangling("?x@@3PEAV?$tmpl@H@@EA", "class tmpl<int> *x");
		}
		[Test]
		public void Basic24() {
			AssertMangling("?x@@3PEAU?$tmpl@H@@EA", "struct tmpl<int> *x");
		}
		[Test]
		public void Basic25() {
			AssertMangling("?x@@3PEAT?$tmpl@H@@EA", "union tmpl<int> *x");
		}
		[Test]
		public void Basic26() {
			AssertMangling("?instance@@3Vklass@@A", "class klass instance");
		}
		[Test]
		public void Basic27() {
			AssertMangling("?instance$initializer$@@3P6AXXZEA", "void (__cdecl *instance$initializer$)(void)");
		}
		[Test]
		public void Basic28() {
			AssertMangling("??0klass@@QEAA@XZ", "public: __cdecl klass::klass(void)");
		}
		[Test]
		public void Basic29() {
			AssertMangling("??1klass@@QEAA@XZ", "public: __cdecl klass::~klass(void)");
		}
		[Test]
		public void Basic30() {
			AssertMangling("?x@@YAHPEAVklass@@AEAV1@@Z", "int __cdecl x(class klass *, class klass &)");
		}
		[Test]
		public void Basic31() {
			AssertMangling("?x@ns@@3PEAV?$klass@HH@1@EA", "class ns::klass<int, int> *ns::x");
		}
		[Test]
		public void Basic32() {
			AssertMangling("?fn@?$klass@H@ns@@QEBAIXZ", "public: unsigned int __cdecl ns::klass<int>::fn(void) const");
		}
		[Test]
		public void Basic33() {
			AssertMangling("??4klass@@QEAAAEBV0@AEBV0@@Z", "public: class klass const & __cdecl klass::operator=(class klass const &)");
		}
		[Test]
		public void Basic34() {
			AssertMangling("??7klass@@QEAA_NXZ", "public: bool __cdecl klass::operator!(void)");
		}
		[Test]
		public void Basic35() {
			AssertMangling("??8klass@@QEAA_NAEBV0@@Z", "public: bool __cdecl klass::operator==(class klass const &)");
		}
		[Test]
		public void Basic36() {
			AssertMangling("??9klass@@QEAA_NAEBV0@@Z", "public: bool __cdecl klass::operator!=(class klass const &)");
		}
		[Test]
		public void Basic37() {
			AssertMangling("??Aklass@@QEAAH_K@Z", "public: int __cdecl klass::operator[](unsigned __int64)");
		}
		[Test]
		public void Basic38() {
			AssertMangling("??Cklass@@QEAAHXZ", "public: int __cdecl klass::operator->(void)");
		}
		[Test]
		public void Basic39() {
			AssertMangling("??Dklass@@QEAAHXZ", "public: int __cdecl klass::operator*(void)");
		}
		[Test]
		public void Basic40() {
			AssertMangling("??Eklass@@QEAAHXZ", "public: int __cdecl klass::operator++(void)");
		}
		[Test]
		public void Basic41() {
			AssertMangling("??Eklass@@QEAAHH@Z", "public: int __cdecl klass::operator++(int)");
		}
		[Test]
		public void Basic42() {
			AssertMangling("??Fklass@@QEAAHXZ", "public: int __cdecl klass::operator--(void)");
		}
		[Test]
		public void Basic43() {
			AssertMangling("??Fklass@@QEAAHH@Z", "public: int __cdecl klass::operator--(int)");
		}
		[Test]
		public void Basic44() {
			AssertMangling("??Hklass@@QEAAHH@Z", "public: int __cdecl klass::operator+(int)");
		}
		[Test]
		public void Basic45() {
			AssertMangling("??Gklass@@QEAAHH@Z", "public: int __cdecl klass::operator-(int)");
		}
		[Test]
		public void Basic46() {
			AssertMangling("??Iklass@@QEAAHH@Z", "public: int __cdecl klass::operator&(int)");
		}
		[Test]
		public void Basic47() {
			AssertMangling("??Jklass@@QEAAHH@Z", "public: int __cdecl klass::operator->*(int)");
		}
		[Test]
		public void Basic48() {
			AssertMangling("??Kklass@@QEAAHH@Z", "public: int __cdecl klass::operator/(int)");
		}
		[Test]
		public void Basic49() {
			AssertMangling("??Mklass@@QEAAHH@Z", "public: int __cdecl klass::operator<(int)");
		}
		[Test]
		public void Basic50() {
			AssertMangling("??Nklass@@QEAAHH@Z", "public: int __cdecl klass::operator<=(int)");
		}
		[Test]
		public void Basic51() {
			AssertMangling("??Oklass@@QEAAHH@Z", "public: int __cdecl klass::operator>(int)");
		}
		[Test]
		public void Basic52() {
			AssertMangling("??Pklass@@QEAAHH@Z", "public: int __cdecl klass::operator>=(int)");
		}
		[Test]
		public void Basic53() {
			AssertMangling("??Qklass@@QEAAHH@Z", "public: int __cdecl klass::operator,(int)");
		}
		[Test]
		public void Basic54() {
			AssertMangling("??Rklass@@QEAAHH@Z", "public: int __cdecl klass::operator()(int)");
		}
		[Test]
		public void Basic55() {
			AssertMangling("??Sklass@@QEAAHXZ", "public: int __cdecl klass::operator~(void)");
		}
		[Test]
		public void Basic56() {
			AssertMangling("??Tklass@@QEAAHH@Z", "public: int __cdecl klass::operator^(int)");
		}
		[Test]
		public void Basic57() {
			AssertMangling("??Uklass@@QEAAHH@Z", "public: int __cdecl klass::operator|(int)");
		}
		[Test]
		public void Basic58() {
			AssertMangling("??Vklass@@QEAAHH@Z", "public: int __cdecl klass::operator&&(int)");
		}
		[Test]
		public void Basic59() {
			AssertMangling("??Wklass@@QEAAHH@Z", "public: int __cdecl klass::operator||(int)");
		}
		[Test]
		public void Basic60() {
			AssertMangling("??Xklass@@QEAAHH@Z", "public: int __cdecl klass::operator*=(int)");
		}
		[Test]
		public void Basic61() {
			AssertMangling("??Yklass@@QEAAHH@Z", "public: int __cdecl klass::operator+=(int)");
		}
		[Test]
		public void Basic62() {
			AssertMangling("??Zklass@@QEAAHH@Z", "public: int __cdecl klass::operator-=(int)");
		}
		[Test]
		public void Basic63() {
			AssertMangling("??_0klass@@QEAAHH@Z", "public: int __cdecl klass::operator/=(int)");
		}
		[Test]
		public void Basic64() {
			AssertMangling("??_1klass@@QEAAHH@Z", "public: int __cdecl klass::operator%=(int)");
		}
		[Test]
		public void Basic65() {
			AssertMangling("??_2klass@@QEAAHH@Z", "public: int __cdecl klass::operator>>=(int)");
		}
		[Test]
		public void Basic66() {
			AssertMangling("??_3klass@@QEAAHH@Z", "public: int __cdecl klass::operator<<=(int)");
		}
		[Test]
		public void Basic67() {
			AssertMangling("??_6klass@@QEAAHH@Z", "public: int __cdecl klass::operator^=(int)");
		}
		[Test]
		public void Basic68() {
			AssertMangling("??6@YAAEBVklass@@AEBV0@H@Z", "class klass const & __cdecl operator<<(class klass const &, int)");
		}
		[Test]
		public void Basic69() {
			AssertMangling("??5@YAAEBVklass@@AEBV0@_K@Z", "class klass const & __cdecl operator>>(class klass const &, unsigned __int64)");
		}
		[Test]
		public void Basic70() {
			AssertMangling("??2@YAPEAX_KAEAVklass@@@Z", "void * __cdecl operator new(unsigned __int64, class klass &)");
		}
		[Test]
		public void Basic71() {
			AssertMangling("??_U@YAPEAX_KAEAVklass@@@Z", "void * __cdecl operator new[](unsigned __int64, class klass &)");
		}
		[Test]
		public void Basic72() {
			AssertMangling("??3@YAXPEAXAEAVklass@@@Z", "void __cdecl operator delete(void *, class klass &)");
		}
		[Test]
		public void Basic73() {
			AssertMangling("??_V@YAXPEAXAEAVklass@@@Z", "void __cdecl operator delete[](void *, class klass &)");
		}
		#endregion
	}
}
