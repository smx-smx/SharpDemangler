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

		#region BackReference
		[Test]
		public void BackReference1() {
			AssertMangling("?f1@@YAXPBD0@Z", "void __cdecl f1(char const *, char const *)");
		}
		[Test]
		public void BackReference2() {
			AssertMangling("?f2@@YAXPBDPAD@Z", "void __cdecl f2(char const *, char *)");
		}
		[Test]
		public void BackReference3() {
			AssertMangling("?f3@@YAXHPBD0@Z", "void __cdecl f3(int, char const *, char const *)");
		}
		[Test]
		public void BackReference4() {
			AssertMangling("?f4@@YAPBDPBD0@Z", "char const * __cdecl f4(char const *, char const *)");
		}
		[Test]
		public void BackReference5() {
			AssertMangling("?f5@@YAXPBDIDPBX0I@Z", "void __cdecl f5(char const *, unsigned int, char, void const *, char const *, unsigned int)");
		}
		[Test]
		public void BackReference6() {
			AssertMangling("?f6@@YAX_N0@Z", "void __cdecl f6(bool, bool)");
		}
		[Test]
		public void BackReference7() {
			AssertMangling("?f7@@YAXHPAHH0_N1PA_N@Z", "void __cdecl f7(int, int *, int, int *, bool, bool, bool *)");
		}
		[Test]
		public void BackReference8() {
			AssertMangling("?g1@@YAXUS@@@Z", "void __cdecl g1(struct S)");
		}
		[Test]
		public void BackReference9() {
			AssertMangling("?g2@@YAXUS@@0@Z", "void __cdecl g2(struct S, struct S)");
		}
		[Test]
		public void BackReference10() {
			AssertMangling("?g3@@YAXUS@@0PAU1@1@Z", "void __cdecl g3(struct S, struct S, struct S *, struct S *)");
		}
		[Test]
		public void BackReference11() {
			AssertMangling("?g4@@YAXPBDPAUS@@01@Z", "void __cdecl g4(char const *, struct S *, char const *, struct S *)");
		}
		[Test]
		public void BackReference12() {
			AssertMangling("?mbb@S@@QAEX_N0@Z", "public: void __thiscall S::mbb(bool, bool)");
		}
		[Test]
		public void BackReference13() {
			AssertMangling("?h1@@YAXPBD0P6AXXZ1@Z", "void __cdecl h1(char const *, char const *, void (__cdecl *)(void), void (__cdecl *)(void))");
		}
		[Test]
		public void BackReference14() {
			AssertMangling("?h2@@YAXP6AXPAX@Z0@Z", "void __cdecl h2(void (__cdecl *)(void *), void *)");
		}
		[Test]
		public void BackReference15() {
			AssertMangling("?h3@@YAP6APAHPAH0@ZP6APAH00@Z10@Z", "int * (__cdecl * __cdecl h3(int * (__cdecl *)(int *, int *), int * (__cdecl *)(int *, int *), int *))(int *, int *)");
		}
		[Test]
		public void BackReference16() {
			AssertMangling("?foo@0@YAXXZ", "void __cdecl foo::foo(void)");
		}
		[Test]
		public void BackReference17() {
			AssertMangling("??$?HH@S@@QEAAAEAU0@H@Z", "public: struct S & __cdecl S::operator+<int>(int)");
		}
		[Test]
		public void BackReference18() {
			AssertMangling("?foo_abbb@@YAXV?$A@V?$B@D@@V1@V1@@@@Z", "void __cdecl foo_abbb(class A<class B<char>, class B<char>, class B<char>>)");
		}
		[Test]
		public void BackReference19() {
			AssertMangling("?foo_abb@@YAXV?$A@DV?$B@D@@V1@@@@Z", "void __cdecl foo_abb(class A<char, class B<char>, class B<char>>)");
		}
		[Test]
		public void BackReference20() {
			AssertMangling("?foo_abc@@YAXV?$A@DV?$B@D@@V?$C@D@@@@@Z", "void __cdecl foo_abc(class A<char, class B<char>, class C<char>>)");
		}
		[Test]
		public void BackReference21() {
			AssertMangling("?foo_bt@@YAX_NV?$B@$$A6A_N_N@Z@@@Z", "void __cdecl foo_bt(bool, class B<bool __cdecl(bool)>)");
		}
		[Test]
		public void BackReference22() {
			AssertMangling("?foo_abbb@@YAXV?$A@V?$B@D@N@@V12@V12@@N@@@Z", "void __cdecl foo_abbb(class N::A<class N::B<char>, class N::B<char>, class N::B<char>>)");
		}
		[Test]
		public void BackReference23() {
			AssertMangling("?foo_abb@@YAXV?$A@DV?$B@D@N@@V12@@N@@@Z", "void __cdecl foo_abb(class N::A<char, class N::B<char>, class N::B<char>>)");
		}
		[Test]
		public void BackReference24() {
			AssertMangling("?foo_abc@@YAXV?$A@DV?$B@D@N@@V?$C@D@2@@N@@@Z", "void __cdecl foo_abc(class N::A<char, class N::B<char>, class N::C<char>>)");
		}
		[Test]
		public void BackReference25() {
			AssertMangling("?abc_foo@@YA?AV?$A@DV?$B@D@N@@V?$C@D@2@@N@@XZ", "class N::A<char, class N::B<char>, class N::C<char>> __cdecl abc_foo(void)");
		}
		[Test]
		public void BackReference26() {
			AssertMangling("?z_foo@@YA?AVZ@N@@V12@@Z", "class N::Z __cdecl z_foo(class N::Z)");
		}
		[Test]
		public void BackReference27() {
			AssertMangling("?b_foo@@YA?AV?$B@D@N@@V12@@Z", "class N::B<char> __cdecl b_foo(class N::B<char>)");
		}
		[Test]
		public void BackReference28() {
			AssertMangling("?d_foo@@YA?AV?$D@DD@N@@V12@@Z", "class N::D<char, char> __cdecl d_foo(class N::D<char, char>)");
		}
		[Test]
		public void BackReference29() {
			AssertMangling("?abc_foo_abc@@YA?AV?$A@DV?$B@D@N@@V?$C@D@2@@N@@V12@@Z", "class N::A<char, class N::B<char>, class N::C<char>> __cdecl abc_foo_abc(class N::A<char, class N::B<char>, class N::C<char>>)");
		}
		[Test]
		public void BackReference30() {
			AssertMangling("?foo5@@YAXV?$Y@V?$Y@V?$Y@V?$Y@VX@NA@@@NB@@@NA@@@NB@@@NA@@@Z", "void __cdecl foo5(class NA::Y<class NB::Y<class NA::Y<class NB::Y<class NA::X>>>>)");
		}
		[Test]
		public void BackReference31() {
			AssertMangling("?foo11@@YAXV?$Y@VX@NA@@@NA@@V1NB@@@Z", "void __cdecl foo11(class NA::Y<class NA::X>, class NB::Y<class NA::X>)");
		}
		[Test]
		public void BackReference32() {
			AssertMangling("?foo112@@YAXV?$Y@VX@NA@@@NA@@V?$Y@VX@NB@@@NB@@@Z", "void __cdecl foo112(class NA::Y<class NA::X>, class NB::Y<class NB::X>)");
		}
		[Test]
		public void BackReference33() {
			AssertMangling("?foo22@@YAXV?$Y@V?$Y@VX@NA@@@NB@@@NA@@V?$Y@V?$Y@VX@NA@@@NA@@@NB@@@Z", "void __cdecl foo22(class NA::Y<class NB::Y<class NA::X>>, class NB::Y<class NA::Y<class NA::X>>)");
		}
		[Test]
		public void BackReference34() {
			AssertMangling("?foo@L@PR13207@@QAEXV?$I@VA@PR13207@@@2@@Z", "public: void __thiscall PR13207::L::foo(class PR13207::I<class PR13207::A>)");
		}
		[Test]
		public void BackReference35() {
			AssertMangling("?foo@PR13207@@YAXV?$I@VA@PR13207@@@1@@Z", "void __cdecl PR13207::foo(class PR13207::I<class PR13207::A>)");
		}
		[Test]
		public void BackReference36() {
			AssertMangling("?foo2@PR13207@@YAXV?$I@VA@PR13207@@@1@0@Z", "void __cdecl PR13207::foo2(class PR13207::I<class PR13207::A>, class PR13207::I<class PR13207::A>)");
		}
		[Test]
		public void BackReference37() {
			AssertMangling("?bar@PR13207@@YAXV?$J@VA@PR13207@@VB@2@@1@@Z", "void __cdecl PR13207::bar(class PR13207::J<class PR13207::A, class PR13207::B>)");
		}
		[Test]
		public void BackReference38() {
			AssertMangling("?spam@PR13207@@YAXV?$K@VA@PR13207@@VB@2@VC@2@@1@@Z", "void __cdecl PR13207::spam(class PR13207::K<class PR13207::A, class PR13207::B, class PR13207::C>)");
		}
		[Test]
		public void BackReference39() {
			AssertMangling("?baz@PR13207@@YAXV?$K@DV?$F@D@PR13207@@V?$I@D@2@@1@@Z", "void __cdecl PR13207::baz(class PR13207::K<char, class PR13207::F<char>, class PR13207::I<char>>)");
		}
		[Test]
		public void BackReference40() {
			AssertMangling("?qux@PR13207@@YAXV?$K@DV?$I@D@PR13207@@V12@@1@@Z", "void __cdecl PR13207::qux(class PR13207::K<char, class PR13207::I<char>, class PR13207::I<char>>)");
		}
		[Test]
		public void BackReference41() {
			AssertMangling("?foo@NA@PR13207@@YAXV?$Y@VX@NA@PR13207@@@12@@Z", "void __cdecl PR13207::NA::foo(class PR13207::NA::Y<class PR13207::NA::X>)");
		}
		[Test]
		public void BackReference42() {
			AssertMangling("?foofoo@NA@PR13207@@YAXV?$Y@V?$Y@VX@NA@PR13207@@@NA@PR13207@@@12@@Z", "void __cdecl PR13207::NA::foofoo(class PR13207::NA::Y<class PR13207::NA::Y<class PR13207::NA::X>>)");
		}
		[Test]
		public void BackReference43() {
			AssertMangling("?foo@NB@PR13207@@YAXV?$Y@VX@NA@PR13207@@@12@@Z", "void __cdecl PR13207::NB::foo(class PR13207::NB::Y<class PR13207::NA::X>)");
		}
		[Test]
		public void BackReference44() {
			AssertMangling("?bar@NB@PR13207@@YAXV?$Y@VX@NB@PR13207@@@NA@2@@Z", "void __cdecl PR13207::NB::bar(class PR13207::NA::Y<class PR13207::NB::X>)");
		}
		[Test]
		public void BackReference45() {
			AssertMangling("?spam@NB@PR13207@@YAXV?$Y@VX@NA@PR13207@@@NA@2@@Z", "void __cdecl PR13207::NB::spam(class PR13207::NA::Y<class PR13207::NA::X>)");
		}
		[Test]
		public void BackReference46() {
			AssertMangling("?foobar@NB@PR13207@@YAXV?$Y@V?$Y@VX@NB@PR13207@@@NB@PR13207@@@NA@2@V312@@Z", "void __cdecl PR13207::NB::foobar(class PR13207::NA::Y<class PR13207::NB::Y<class PR13207::NB::X>>, class PR13207::NB::Y<class PR13207::NB::Y<class PR13207::NB::X>>)");
		}
		[Test]
		public void BackReference47() {
			AssertMangling("?foobarspam@NB@PR13207@@YAXV?$Y@VX@NB@PR13207@@@12@V?$Y@V?$Y@VX@NB@PR13207@@@NB@PR13207@@@NA@2@V412@@Z", "void __cdecl PR13207::NB::foobarspam(class PR13207::NB::Y<class PR13207::NB::X>, class PR13207::NA::Y<class PR13207::NB::Y<class PR13207::NB::X>>, class PR13207::NB::Y<class PR13207::NB::Y<class PR13207::NB::X>>)");
		}
		[Test]
		public void BackReference48() {
			AssertMangling("?foobarbaz@NB@PR13207@@YAXV?$Y@VX@NB@PR13207@@@12@V?$Y@V?$Y@VX@NB@PR13207@@@NB@PR13207@@@NA@2@V412@2@Z", "void __cdecl PR13207::NB::foobarbaz(class PR13207::NB::Y<class PR13207::NB::X>, class PR13207::NA::Y<class PR13207::NB::Y<class PR13207::NB::X>>, class PR13207::NB::Y<class PR13207::NB::Y<class PR13207::NB::X>>, class PR13207::NB::Y<class PR13207::NB::Y<class PR13207::NB::X>>)");
		}
		[Test]
		public void BackReference49() {
			AssertMangling("?foobarbazqux@NB@PR13207@@YAXV?$Y@VX@NB@PR13207@@@12@V?$Y@V?$Y@VX@NB@PR13207@@@NB@PR13207@@@NA@2@V412@2V?$Y@V?$Y@V?$Y@VX@NB@PR13207@@@NB@PR13207@@@NB@PR13207@@@52@@Z", "void __cdecl PR13207::NB::foobarbazqux(class PR13207::NB::Y<class PR13207::NB::X>, class PR13207::NA::Y<class PR13207::NB::Y<class PR13207::NB::X>>, class PR13207::NB::Y<class PR13207::NB::Y<class PR13207::NB::X>>, class PR13207::NB::Y<class PR13207::NB::Y<class PR13207::NB::X>>, class PR13207::NA::Y<class PR13207::NB::Y<class PR13207::NB::Y<class PR13207::NB::X>>>)");
		}
		[Test]
		public void BackReference50() {
			AssertMangling("?foo@NC@PR13207@@YAXV?$Y@VX@NB@PR13207@@@12@@Z", "void __cdecl PR13207::NC::foo(class PR13207::NC::Y<class PR13207::NB::X>)");
		}
		[Test]
		public void BackReference51() {
			AssertMangling("?foobar@NC@PR13207@@YAXV?$Y@V?$Y@V?$Y@VX@NA@PR13207@@@NA@PR13207@@@NB@PR13207@@@12@@Z", "void __cdecl PR13207::NC::foobar(class PR13207::NC::Y<class PR13207::NB::Y<class PR13207::NA::Y<class PR13207::NA::X>>>)");
		}
		[Test]
		public void BackReference52() {
			AssertMangling("?fun_normal@fn_space@@YA?AURetVal@1@H@Z", "struct fn_space::RetVal __cdecl fn_space::fun_normal(int)");
		}
		[Test]
		public void BackReference53() {
			AssertMangling("??$fun_tmpl@H@fn_space@@YA?AURetVal@0@ABH@Z", "struct fn_space::RetVal __cdecl fn_space::fun_tmpl<int>(int const &)");
		}
		[Test]
		public void BackReference54() {
			AssertMangling("??$fun_tmpl_recurse@H$1??$fun_tmpl_recurse@H$1?ident@fn_space@@YA?AURetVal@2@H@Z@fn_space@@YA?AURetVal@1@H@Z@fn_space@@YA?AURetVal@0@H@Z", "struct fn_space::RetVal __cdecl fn_space::fun_tmpl_recurse<int, &struct fn_space::RetVal __cdecl fn_space::fun_tmpl_recurse<int, &struct fn_space::RetVal __cdecl fn_space::ident(int)>(int)>(int)");
		}
		[Test]
		public void BackReference55() {
			AssertMangling("??$fun_tmpl_recurse@H$1?ident@fn_space@@YA?AURetVal@2@H@Z@fn_space@@YA?AURetVal@0@H@Z", "struct fn_space::RetVal __cdecl fn_space::fun_tmpl_recurse<int, &struct fn_space::RetVal __cdecl fn_space::ident(int)>(int)");
		}
		[Test]
		public void BackReference56() {
			AssertMangling("?AddEmitPasses@EmitAssemblyHelper@?A0x43583946@@AEAA_NAEAVPassManager@legacy@llvm@@W4BackendAction@clang@@AEAVraw_pwrite_stream@5@PEAV85@@Z", "bool __cdecl `anonymous namespace'::EmitAssemblyHelper::AddEmitPasses(class llvm::legacy::PassManager &, enum clang::BackendAction, class llvm::raw_pwrite_stream &, class llvm::raw_pwrite_stream *)");
		}
		[Test]
		public void BackReference57() {
			AssertMangling("??$forward@P8?$DecoderStream@$01@media@@AEXXZ@std@@YA$$QAP8?$DecoderStream@$01@media@@AEXXZAAP812@AEXXZ@Z", "void (__thiscall media::DecoderStream<2>::*&& __cdecl std::forward<void (__thiscall media::DecoderStream<2>::*)(void)>(void (__thiscall media::DecoderStream<2>::*&)(void)))(void)");
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

		#region ConversionOperators
		[Test]
		public void ConversionOperator1() {
			AssertMangling("??$?BH@TemplateOps@@QAEHXZ", "public: int __thiscall TemplateOps::operator<int> int(void)");
		}
		[Test]
		public void ConversionOperator2() {
			AssertMangling("??BOps@@QAEHXZ", "public: int __thiscall Ops::operator int(void)");
		}
		[Test]
		public void ConversionOperator3() {
			AssertMangling("??BConstOps@@QAE?BHXZ", "public: int const __thiscall ConstOps::operator int const(void)");
		}
		[Test]
		public void ConversionOperator4() {
			AssertMangling("??BVolatileOps@@QAE?CHXZ", "public: int volatile __thiscall VolatileOps::operator int volatile(void)");
		}
		[Test]
		public void ConversionOperator5() {
			AssertMangling("??BConstVolatileOps@@QAE?DHXZ", "public: int const volatile __thiscall ConstVolatileOps::operator int const volatile(void)");
		}
		[Test]
		public void ConversionOperator6() {
			AssertMangling("??$?BN@TemplateOps@@QAENXZ", "public: double __thiscall TemplateOps::operator<double> double(void)");
		}
		[Test]
		public void ConversionOperator7() {
			AssertMangling("??BOps@@QAENXZ", "public: double __thiscall Ops::operator double(void)");
		}
		[Test]
		public void ConversionOperator8() {
			AssertMangling("??BConstOps@@QAE?BNXZ", "public: double const __thiscall ConstOps::operator double const(void)");
		}
		[Test]
		public void ConversionOperator9() {
			AssertMangling("??BVolatileOps@@QAE?CNXZ", "public: double volatile __thiscall VolatileOps::operator double volatile(void)");
		}
		[Test]
		public void ConversionOperator10() {
			AssertMangling("??BConstVolatileOps@@QAE?DNXZ", "public: double const volatile __thiscall ConstVolatileOps::operator double const volatile(void)");
		}
		[Test]
		public void ConversionOperator11() {
			AssertMangling("??BCompoundTypeOps@@QAEPAHXZ", "public: int * __thiscall CompoundTypeOps::operator int *(void)");
		}
		[Test]
		public void ConversionOperator12() {
			AssertMangling("??BCompoundTypeOps@@QAEPBHXZ", "public: int const * __thiscall CompoundTypeOps::operator int const *(void)");
		}
		[Test]
		public void ConversionOperator13() {
			AssertMangling("??BCompoundTypeOps@@QAE$$QAHXZ", "public: int && __thiscall CompoundTypeOps::operator int &&(void)");
		}
		[Test]
		public void ConversionOperator14() {
			AssertMangling("??BCompoundTypeOps@@QAE?AU?$Foo@H@@XZ", "public: struct Foo<int> __thiscall CompoundTypeOps::operator struct Foo<int>(void)");
		}
		[Test]
		public void ConversionOperator15() {
			AssertMangling("??$?BH@CompoundTypeOps@@QAE?AU?$Bar@U?$Foo@H@@@@XZ", "public: struct Bar<struct Foo<int>> __thiscall CompoundTypeOps::operator<int> struct Bar<struct Foo<int>>(void)");
		}
		[Test]
		public void ConversionOperator16() {
			AssertMangling("??$?BPAH@TemplateOps@@QAEPAHXZ", "public: int * __thiscall TemplateOps::operator<int *> int *(void)");
		}
		#endregion

		#region Cxx11
		[Test]
		public void Cxx11_1() {
			AssertMangling("?a@FTypeWithQuals@@3U?$S@$$A8@@BAHXZ@1@A", "struct FTypeWithQuals::S<int __cdecl(void) const> FTypeWithQuals::a");
		}
		[Test]
		public void Cxx11_2() {
			AssertMangling("?b@FTypeWithQuals@@3U?$S@$$A8@@CAHXZ@1@A", "struct FTypeWithQuals::S<int __cdecl(void) volatile> FTypeWithQuals::b");
		}
		[Test]
		public void Cxx11_3() {
			AssertMangling("?c@FTypeWithQuals@@3U?$S@$$A8@@IAAHXZ@1@A", "struct FTypeWithQuals::S<int __cdecl(void) __restrict> FTypeWithQuals::c");
		}
		[Test]
		public void Cxx11_4() {
			AssertMangling("?d@FTypeWithQuals@@3U?$S@$$A8@@GBAHXZ@1@A", "struct FTypeWithQuals::S<int __cdecl(void) const &> FTypeWithQuals::d");
		}
		[Test]
		public void Cxx11_5() {
			AssertMangling("?e@FTypeWithQuals@@3U?$S@$$A8@@GCAHXZ@1@A", "struct FTypeWithQuals::S<int __cdecl(void) volatile &> FTypeWithQuals::e");
		}
		[Test]
		public void Cxx11_6() {
			AssertMangling("?f@FTypeWithQuals@@3U?$S@$$A8@@IGAAHXZ@1@A", "struct FTypeWithQuals::S<int __cdecl(void) __restrict &> FTypeWithQuals::f");
		}
		[Test]
		public void Cxx11_7() {
			AssertMangling("?g@FTypeWithQuals@@3U?$S@$$A8@@HBAHXZ@1@A", "struct FTypeWithQuals::S<int __cdecl(void) const &&> FTypeWithQuals::g");
		}
		[Test]
		public void Cxx11_8() {
			AssertMangling("?h@FTypeWithQuals@@3U?$S@$$A8@@HCAHXZ@1@A", "struct FTypeWithQuals::S<int __cdecl(void) volatile &&> FTypeWithQuals::h");
		}
		[Test]
		public void Cxx11_9() {
			AssertMangling("?i@FTypeWithQuals@@3U?$S@$$A8@@IHAAHXZ@1@A", "struct FTypeWithQuals::S<int __cdecl(void) __restrict &&> FTypeWithQuals::i");
		}
		[Test]
		public void Cxx11_10() {
			AssertMangling("?j@FTypeWithQuals@@3U?$S@$$A6AHXZ@1@A", "struct FTypeWithQuals::S<int __cdecl(void)> FTypeWithQuals::j");
		}
		[Test]
		public void Cxx11_11() {
			AssertMangling("?k@FTypeWithQuals@@3U?$S@$$A8@@GAAHXZ@1@A", "struct FTypeWithQuals::S<int __cdecl(void) &> FTypeWithQuals::k");
		}
		[Test]
		public void Cxx11_12() {
			AssertMangling("?l@FTypeWithQuals@@3U?$S@$$A8@@HAAHXZ@1@A", "struct FTypeWithQuals::S<int __cdecl(void) &&> FTypeWithQuals::l");
		}
		[Test]
		public void Cxx11_13() {
			AssertMangling("?Char16Var@@3_SA", "char16_t Char16Var");
		}
		[Test]
		public void Cxx11_14() {
			AssertMangling("?Char32Var@@3_UA", "char32_t Char32Var");
		}
		[Test]
		public void Cxx11_15() {
			AssertMangling("?LRef@@YAXAAH@Z", "void __cdecl LRef(int &)");
		}
		[Test]
		public void Cxx11_16() {
			AssertMangling("?RRef@@YAH$$QAH@Z", "int __cdecl RRef(int &&)");
		}
		[Test]
		public void Cxx11_17() {
			AssertMangling("?Null@@YAX$$T@Z", "void __cdecl Null(std::nullptr_t)");
		}
		[Test]
		public void Cxx11_18() {
			AssertMangling("?fun@PR18022@@YA?AU<unnamed-type-a>@1@U21@0@Z", "struct PR18022::<unnamed-type-a> __cdecl PR18022::fun(struct PR18022::<unnamed-type-a>, struct PR18022::<unnamed-type-a>)");
		}
		[Test]
		public void Cxx11_19() {
			AssertMangling("?lambda@?1??define_lambda@@YAHXZ@4V<lambda_1>@?0??1@YAHXZ@A", "class `int __cdecl define_lambda(void)'::`1'::<lambda_1> `int __cdecl define_lambda(void)'::`2'::lambda");
		}
		[Test]
		public void Cxx11_20() {
			AssertMangling("??R<lambda_1>@?0??define_lambda@@YAHXZ@QBE@XZ", "public: __thiscall `int __cdecl define_lambda(void)'::`1'::<lambda_1>::operator()(void) const");
		}
		[Test]
		public void Cxx11_21() {
			AssertMangling("?local@?2???R<lambda_1>@?0??define_lambda@@YAHXZ@QBE@XZ@4HA", "__thiscall `int __cdecl define_lambda(void)'::`1'::<lambda_1>::operator()(void) const");
		}
		[Test]
		public void Cxx11_22() {
			AssertMangling("??$use_lambda_arg@V<lambda_1>@?0??call_with_lambda_arg1@@YAXXZ@@@YAXV<lambda_1>@?0??call_with_lambda_arg1@@YAXXZ@@Z", "void __cdecl use_lambda_arg<class `void __cdecl call_with_lambda_arg1(void)'::`1'::<lambda_1>>(class `void __cdecl call_with_lambda_arg1(void)'::`1'::<lambda_1>)");
		}
		[Test]
		public void Cxx11_23() {
			AssertMangling("?foo@A@PR19361@@QIGAEXXZ", "public: void __thiscall PR19361::A::foo(void) __restrict &");
		}
		[Test]
		public void Cxx11_24() {
			AssertMangling("?foo@A@PR19361@@QIHAEXXZ", "public: void __thiscall PR19361::A::foo(void) __restrict &&");
		}
		[Test]
		public void Cxx11_25() {
			AssertMangling("??__K_deg@@YAHO@Z", "int __cdecl operator \"\"_deg(long double)");
		}
		[Test]
		public void Cxx11_26() {
			AssertMangling("??$templ_fun_with_pack@$S@@YAXXZ", "void __cdecl templ_fun_with_pack<>(void)");
		}
		[Test]
		public void Cxx11_27() {
			AssertMangling("??$func@H$$ZH@@YAHAEBU?$Foo@H@@0@Z", "int __cdecl func<int, int>(struct Foo<int> const &, struct Foo<int> const &)");
		}
		[Test]
		public void Cxx11_28() {
			AssertMangling("??$templ_fun_with_ty_pack@$$$V@@YAXXZ", "void __cdecl templ_fun_with_ty_pack<>(void)");
		}
		[Test]
		public void Cxx11_29() {
			AssertMangling("??$templ_fun_with_ty_pack@$$V@@YAXXZ", "void __cdecl templ_fun_with_ty_pack<>(void)");
		}
		[Test]
		public void Cxx11_30() {
			AssertMangling("??$f@$$YAliasA@PR20047@@@PR20047@@YAXXZ", "void __cdecl PR20047::f<PR20047::AliasA>(void)");
		}
		[Test]
		public void Cxx11_31() {
			AssertMangling("?f@UnnamedType@@YAXAAU<unnamed-type-TD>@A@1@@Z", "void __cdecl UnnamedType::f(struct UnnamedType::A::<unnamed-type-TD> &)");
		}
		[Test]
		public void Cxx11_32() {
			AssertMangling("?f@UnnamedType@@YAXPAW4<unnamed-type-e>@?$B@H@1@@Z", "void __cdecl UnnamedType::f(enum UnnamedType::B<int>::<unnamed-type-e> *)");
		}
		[Test]
		public void Cxx11_33() {
			AssertMangling("??$f@W4<unnamed-type-E>@?1??g@PR24651@@YAXXZ@@PR24651@@YAXW4<unnamed-type-E>@?1??g@0@YAXXZ@@Z", "void __cdecl PR24651::f<enum `void __cdecl PR24651::g(void)'::`2'::<unnamed-type-E>>(enum `void __cdecl PR24651::g(void)'::`2'::<unnamed-type-E>)");
		}
		[Test]
		public void Cxx11_34() {
			AssertMangling("??$f@T<unnamed-type-$S1>@PR18204@@@PR18204@@YAHPAT<unnamed-type-$S1>@0@@Z", "int __cdecl PR18204::f<union PR18204::<unnamed-type-$S1>>(union PR18204::<unnamed-type-$S1> *)");
		}
		[Test]
		public void Cxx11_35() {
			AssertMangling("??R<lambda_0>@?0??PR26105@@YAHXZ@QBE@H@Z", "public: __thiscall `int __cdecl PR26105(void)'::`1'::<lambda_0>::operator()(int) const");
		}
		[Test]
		public void Cxx11_36() {
			AssertMangling("??R<lambda_1>@?0???R<lambda_0>@?0??PR26105@@YAHXZ@QBE@H@Z@QBE@H@Z", "public: __thiscall `public: __thiscall `int __cdecl PR26105(void)'::`1'::<lambda_0>::operator()(int) const'::`1'::<lambda_1>::operator()(int) const");
		}
		[Test]
		public void Cxx11_37() {
			AssertMangling("?unaligned_foo1@@YAPFAHXZ", "int __unaligned * __cdecl unaligned_foo1(void)");
		}
		[Test]
		public void Cxx11_38() {
			AssertMangling("?unaligned_foo2@@YAPFAPFAHXZ", "int __unaligned *__unaligned * __cdecl unaligned_foo2(void)");
		}
		[Test]
		public void Cxx11_39() {
			AssertMangling("?unaligned_foo3@@YAHXZ", "int __cdecl unaligned_foo3(void)");
		}
		[Test]
		public void Cxx11_40() {
			AssertMangling("?unaligned_foo4@@YAXPFAH@Z", "void __cdecl unaligned_foo4(int __unaligned *)");
		}
		[Test]
		public void Cxx11_41() {
			AssertMangling("?unaligned_foo5@@YAXPIFAH@Z", "void __cdecl unaligned_foo5(int __unaligned *__restrict)");
		}
		[Test]
		public void Cxx11_42() {
			AssertMangling("??$unaligned_foo6@PAH@@YAPAHPAH@Z", "int * __cdecl unaligned_foo6<int *>(int *)");
		}
		[Test]
		public void Cxx11_43() {
			AssertMangling("??$unaligned_foo6@PFAH@@YAPFAHPFAH@Z", "int __unaligned * __cdecl unaligned_foo6<int __unaligned *>(int __unaligned *)");
		}
		[Test]
		public void Cxx11_44() {
			AssertMangling("?unaligned_foo8@unaligned_foo8_S@@QFCEXXZ", "public: void __thiscall unaligned_foo8_S::unaligned_foo8(void) volatile __unaligned");
		}
		[Test]
		public void Cxx11_45() {
			AssertMangling("??R<lambda_1>@x@A@PR31197@@QBE@XZ", "public: __thiscall PR31197::A::x::<lambda_1>::operator()(void) const");
		}
		[Test]
		public void Cxx11_46() {
			AssertMangling("?white@?1???R<lambda_1>@x@A@PR31197@@QBE@XZ@4HA", "public: int `public: __thiscall PR31197::A::x::<lambda_1>::operator()(void) const'::`2'::white");
		}
		[Test]
		public void Cxx11_47() {
			AssertMangling("?f@@YAXW4<unnamed-enum-enumerator>@@@Z", "void __cdecl f(enum <unnamed-enum-enumerator>)");
		}
		#endregion

		#region Cxx14
		[Test]
		public void Cxx14_1() {
			AssertMangling("??$x@X@@3HA", "int x<void>");
		}
		[Test]
		public void Cxx14_2() {
			AssertMangling("?FunctionWithLocalType@@YA?A?<auto>@@XZ", "<auto> __cdecl FunctionWithLocalType(void)");
		}
		[Test]
		public void Cxx14_3() {
			AssertMangling("?ValueFromFunctionWithLocalType@@3ULocalType@?1??FunctionWithLocalType@@YA?A?<auto>@@XZ@A", "struct `<auto> __cdecl FunctionWithLocalType(void)'::`2'::LocalType ValueFromFunctionWithLocalType");
		}
		[Test]
		public void Cxx14_4() {
			AssertMangling("??R<lambda_0>@@QBE?A?<auto>@@XZ", "public: <auto> __thiscall <lambda_0>::operator()(void) const");
		}
		[Test]
		public void Cxx14_5() {
			AssertMangling("?ValueFromLambdaWithLocalType@@3ULocalType@?1???R<lambda_0>@@QBE?A?<auto>@@XZ@A", "struct `public: <auto> __thiscall <lambda_0>::operator()(void) const'::`2'::LocalType ValueFromLambdaWithLocalType");
		}
		[Test]
		public void Cxx14_6() {
			AssertMangling("?ValueFromTemplateFuncionWithLocalLambda@@3ULocalType@?2???R<lambda_1>@?0???$TemplateFuncionWithLocalLambda@H@@YA?A?<auto>@@H@Z@QBE?A?3@XZ@A", "struct `public: <auto> __thiscall `<auto> __cdecl TemplateFuncionWithLocalLambda<int>(int)'::`1'::<lambda_1>::operator()(void) const'::`3'::LocalType ValueFromTemplateFuncionWithLocalLambda");
		}
		[Test]
		public void Cxx14_7() {
			AssertMangling("??$TemplateFuncionWithLocalLambda@H@@YA?A?<auto>@@H@Z", "<auto> __cdecl TemplateFuncionWithLocalLambda<int>(int)");
		}
		[Test]
		public void Cxx14_8() {
			AssertMangling("??R<lambda_1>@?0???$TemplateFuncionWithLocalLambda@H@@YA?A?<auto>@@H@Z@QBE?A?1@XZ", "public: <auto> __thiscall `<auto> __cdecl TemplateFuncionWithLocalLambda<int>(int)'::`1'::<lambda_1>::operator()(void) const");
		}
		[Test]
		public void Cxx14_9() {
			AssertMangling("??$WithPMD@$GA@A@?0@@3HA", "int WithPMD<{0, 0, -1}>");
		}
		[Test]
		public void Cxx14_10() {
			AssertMangling("?Zoo@@3U?$Foo@$1??$x@H@@3HA$1?1@3HA@@A", "struct Foo<&int x<int>, &int x<int>> Zoo");
		}
		[Test]
		public void Cxx14_11() {
			AssertMangling("??$unaligned_x@PFAH@@3PFAHA", "int __unaligned *unaligned_x<int __unaligned *>");
		}
		#endregion

		#region Mangle
		[Test]
		public void Mangle1() {
			AssertMangling("?a@@3HA", "int a");
		}
		[Test]
		public void Mangle2() {
			AssertMangling("?b@N@@3HA", "int N::b");
		}
		[Test]
		public void Mangle3() {
			AssertMangling("?anonymous@?A@N@@3HA", "int N::`anonymous namespace'::anonymous");
		}
		[Test]
		public void Mangle4() {
			AssertMangling("?$RT1@NeedsReferenceTemporary@@3ABHB", "int const &NeedsReferenceTemporary::$RT1");
		}
		[Test]
		public void Mangle5() {
			AssertMangling("?$RT1@NeedsReferenceTemporary@@3AEBHEB", "int const &NeedsReferenceTemporary::$RT1");
		}
		[Test]
		public void Mangle6() {
			AssertMangling("?_c@@YAHXZ", "int __cdecl _c(void)");
		}
		[Test]
		public void Mangle7() {
			AssertMangling("?d@foo@@0FB", "private: static short const foo::d");
		}
		[Test]
		public void Mangle8() {
			AssertMangling("?e@foo@@1JC", "protected: static long volatile foo::e");
		}
		[Test]
		public void Mangle9() {
			AssertMangling("?f@foo@@2DD", "public: static char const volatile foo::f");
		}
		[Test]
		public void Mangle10() {
			AssertMangling("??0foo@@QAE@XZ", "public: __thiscall foo::foo(void)");
		}
		[Test]
		public void Mangle11() {
			AssertMangling("??0foo@@QEAA@XZ", "public: __cdecl foo::foo(void)");
		}
		[Test]
		public void Mangle12() {
			AssertMangling("??1foo@@QAE@XZ", "public: __thiscall foo::~foo(void)");
		}
		[Test]
		public void Mangle13() {
			AssertMangling("??1foo@@QEAA@XZ", "public: __cdecl foo::~foo(void)");
		}
		[Test]
		public void Mangle14() {
			AssertMangling("??0foo@@QAE@H@Z", "public: __thiscall foo::foo(int)");
		}
		[Test]
		public void Mangle15() {
			AssertMangling("??0foo@@QEAA@H@Z", "public: __cdecl foo::foo(int)");
		}
		[Test]
		public void Mangle16() {
			AssertMangling("??0foo@@QAE@PAD@Z", "public: __thiscall foo::foo(char *)");
		}
		[Test]
		public void Mangle17() {
			AssertMangling("??0foo@@QEAA@PEAD@Z", "public: __cdecl foo::foo(char *)");
		}
		[Test]
		public void Mangle18() {
			AssertMangling("?bar@@YA?AVfoo@@XZ", "class foo __cdecl bar(void)");
		}
		[Test]
		public void Mangle19() {
			AssertMangling("?bar@@YA?AVfoo@@XZ", "class foo __cdecl bar(void)");
		}
		[Test]
		public void Mangle20() {
			AssertMangling("??Hfoo@@QAEHH@Z", "public: int __thiscall foo::operator+(int)");
		}
		[Test]
		public void Mangle21() {
			AssertMangling("??Hfoo@@QEAAHH@Z", "public: int __cdecl foo::operator+(int)");
		}
		[Test]
		public void Mangle22() {
			AssertMangling("??$?HH@S@@QEAAAEANH@Z", "public: double & __cdecl S::operator+<int>(int)");
		}
		[Test]
		public void Mangle23() {
			AssertMangling("?static_method@foo@@SAPAV1@XZ", "public: static class foo * __cdecl foo::static_method(void)");
		}
		[Test]
		public void Mangle24() {
			AssertMangling("?static_method@foo@@SAPEAV1@XZ", "public: static class foo * __cdecl foo::static_method(void)");
		}
		[Test]
		public void Mangle25() {
			AssertMangling("?g@bar@@2HA", "public: static int bar::g");
		}
		[Test]
		public void Mangle26() {
			AssertMangling("?h1@@3QAHA", "int *const h1");
		}
		[Test]
		public void Mangle27() {
			AssertMangling("?h2@@3QBHB", "int const *const h2");
		}
		[Test]
		public void Mangle28() {
			AssertMangling("?h3@@3QIAHIA", "int *const __restrict h3");
		}
		[Test]
		public void Mangle29() {
			AssertMangling("?h3@@3QEIAHEIA", "int *const __restrict h3");
		}
		[Test]
		public void Mangle30() {
			AssertMangling("?i@@3PAY0BE@HA", "int (*i)[20]");
		}
		[Test]
		public void Mangle31() {
			AssertMangling("?FunArr@@3PAY0BE@P6AHHH@ZA", "int (__cdecl *(*FunArr)[20])(int, int)");
		}
		[Test]
		public void Mangle32() {
			AssertMangling("?j@@3P6GHCE@ZA", "int (__stdcall *j)(signed char, unsigned char)");
		}
		[Test]
		public void Mangle33() {
			AssertMangling("?funptr@@YAP6AHXZXZ", "int (__cdecl * __cdecl funptr(void))(void)");
		}
		[Test]
		public void Mangle34() {
			AssertMangling("?k@@3PTfoo@@DT1@", "char const volatile foo::*k");
		}
		[Test]
		public void Mangle35() {
			AssertMangling("?k@@3PETfoo@@DET1@", "char const volatile foo::*k");
		}
		[Test]
		public void Mangle36() {
			AssertMangling("?l@@3P8foo@@AEHH@ZQ1@", "int (__thiscall foo::*l)(int)");
		}
		[Test]
		public void Mangle37() {
			AssertMangling("?g_cInt@@3HB", "int const g_cInt");
		}
		[Test]
		public void Mangle38() {
			AssertMangling("?g_vInt@@3HC", "int volatile g_vInt");
		}
		[Test]
		public void Mangle39() {
			AssertMangling("?g_cvInt@@3HD", "int const volatile g_cvInt");
		}
		[Test]
		public void Mangle40() {
			AssertMangling("?beta@@YI_N_J_W@Z", "bool __fastcall beta(__int64, wchar_t)");
		}
		[Test]
		public void Mangle41() {
			AssertMangling("?beta@@YA_N_J_W@Z", "bool __cdecl beta(__int64, wchar_t)");
		}
		[Test]
		public void Mangle42() {
			AssertMangling("?alpha@@YGXMN@Z", "void __stdcall alpha(float, double)");
		}
		[Test]
		public void Mangle43() {
			AssertMangling("?alpha@@YAXMN@Z", "void __cdecl alpha(float, double)");
		}
		[Test]
		public void Mangle44() {
			AssertMangling("?gamma@@YAXVfoo@@Ubar@@Tbaz@@W4quux@@@Z", "void __cdecl gamma(class foo, struct bar, union baz, enum quux)");
		}
		[Test]
		public void Mangle45() {
			AssertMangling("?gamma@@YAXVfoo@@Ubar@@Tbaz@@W4quux@@@Z", "void __cdecl gamma(class foo, struct bar, union baz, enum quux)");
		}
		[Test]
		public void Mangle46() {
			AssertMangling("?delta@@YAXQAHABJ@Z", "void __cdecl delta(int *const, long const &)");
		}
		[Test]
		public void Mangle47() {
			AssertMangling("?delta@@YAXQEAHAEBJ@Z", "void __cdecl delta(int *const, long const &)");
		}
		[Test]
		public void Mangle48() {
			AssertMangling("?epsilon@@YAXQAY19BE@H@Z", "void __cdecl epsilon(int (*const)[10][20])");
		}
		[Test]
		public void Mangle49() {
			AssertMangling("?epsilon@@YAXQEAY19BE@H@Z", "void __cdecl epsilon(int (*const)[10][20])");
		}
		[Test]
		public void Mangle50() {
			AssertMangling("?zeta@@YAXP6AHHH@Z@Z", "void __cdecl zeta(int (__cdecl *)(int, int))");
		}
		[Test]
		public void Mangle51() {
			AssertMangling("?zeta@@YAXP6AHHH@Z@Z", "void __cdecl zeta(int (__cdecl *)(int, int))");
		}
		[Test]
		public void Mangle52() {
			AssertMangling("??2@YAPAXI@Z", "void * __cdecl operator new(unsigned int)");
		}
		[Test]
		public void Mangle53() {
			AssertMangling("??3@YAXPAX@Z", "void __cdecl operator delete(void *)");
		}
		[Test]
		public void Mangle54() {
			AssertMangling("??_U@YAPAXI@Z", "void * __cdecl operator new[](unsigned int)");
		}
		[Test]
		public void Mangle55() {
			AssertMangling("??_V@YAXPAX@Z", "void __cdecl operator delete[](void *)");
		}
		[Test]
		public void Mangle56() {
			AssertMangling("?color1@@3PANA", "double *color1");
		}
		[Test]
		public void Mangle57() {
			AssertMangling("?color2@@3QBNB", "double const *const color2");
		}
		[Test]
		public void Mangle58() {
			AssertMangling("?color3@@3QAY02$$CBNA", "double const (*const color3)[3]");
		}
		[Test]
		public void Mangle59() {
			AssertMangling("?color4@@3QAY02$$CBNA", "double const (*const color4)[3]");
		}
		[Test]
		public void Mangle60() {
			AssertMangling("?memptr1@@3RESB@@HES1@", "int volatile B::*volatile memptr1");
		}
		[Test]
		public void Mangle61() {
			AssertMangling("?memptr2@@3PESB@@HES1@", "int volatile B::*memptr2");
		}
		[Test]
		public void Mangle62() {
			AssertMangling("?memptr3@@3REQB@@HEQ1@", "int B::*volatile memptr3");
		}
		[Test]
		public void Mangle63() {
			AssertMangling("?funmemptr1@@3RESB@@R6AHXZES1@", "int (__cdecl *volatile B::*volatile funmemptr1)(void)");
		}
		[Test]
		public void Mangle64() {
			AssertMangling("?funmemptr2@@3PESB@@R6AHXZES1@", "int (__cdecl *volatile B::*funmemptr2)(void)");
		}
		[Test]
		public void Mangle65() {
			AssertMangling("?funmemptr3@@3REQB@@P6AHXZEQ1@", "int (__cdecl *B::*volatile funmemptr3)(void)");
		}
		[Test]
		public void Mangle66() {
			AssertMangling("?memptrtofun1@@3R8B@@EAAXXZEQ1@", "void (__cdecl B::*volatile memptrtofun1)(void)");
		}
		[Test]
		public void Mangle67() {
			AssertMangling("?memptrtofun2@@3P8B@@EAAXXZEQ1@", "void (__cdecl B::*memptrtofun2)(void)");
		}
		[Test]
		public void Mangle68() {
			AssertMangling("?memptrtofun3@@3P8B@@EAAXXZEQ1@", "void (__cdecl B::*memptrtofun3)(void)");
		}
		[Test]
		public void Mangle69() {
			AssertMangling("?memptrtofun4@@3R8B@@EAAHXZEQ1@", "int (__cdecl B::*volatile memptrtofun4)(void)");
		}
		[Test]
		public void Mangle70() {
			AssertMangling("?memptrtofun5@@3P8B@@EAA?CHXZEQ1@", "int volatile (__cdecl B::*memptrtofun5)(void)");
		}
		[Test]
		public void Mangle71() {
			AssertMangling("?memptrtofun6@@3P8B@@EAA?BHXZEQ1@", "int const (__cdecl B::*memptrtofun6)(void)");
		}
		[Test]
		public void Mangle72() {
			AssertMangling("?memptrtofun7@@3R8B@@EAAP6AHXZXZEQ1@", "int (__cdecl * (__cdecl B::*volatile memptrtofun7)(void))(void)");
		}
		[Test]
		public void Mangle73() {
			AssertMangling("?memptrtofun8@@3P8B@@EAAR6AHXZXZEQ1@", "int (__cdecl *volatile (__cdecl B::*memptrtofun8)(void))(void)");
		}
		[Test]
		public void Mangle74() {
			AssertMangling("?memptrtofun9@@3P8B@@EAAQ6AHXZXZEQ1@", "int (__cdecl *const (__cdecl B::*memptrtofun9)(void))(void)");
		}
		[Test]
		public void Mangle75() {
			AssertMangling("?fooE@@YA?AW4E@@XZ", "enum E __cdecl fooE(void)");
		}
		[Test]
		public void Mangle76() {
			AssertMangling("?fooE@@YA?AW4E@@XZ", "enum E __cdecl fooE(void)");
		}
		[Test]
		public void Mangle77() {
			AssertMangling("?fooX@@YA?AVX@@XZ", "class X __cdecl fooX(void)");
		}
		[Test]
		public void Mangle78() {
			AssertMangling("?fooX@@YA?AVX@@XZ", "class X __cdecl fooX(void)");
		}
		[Test]
		public void Mangle79() {
			AssertMangling("?s0@PR13182@@3PADA", "char *PR13182::s0");
		}
		[Test]
		public void Mangle80() {
			AssertMangling("?s1@PR13182@@3PADA", "char *PR13182::s1");
		}
		[Test]
		public void Mangle81() {
			AssertMangling("?s2@PR13182@@3QBDB", "char const *const PR13182::s2");
		}
		[Test]
		public void Mangle82() {
			AssertMangling("?s3@PR13182@@3QBDB", "char const *const PR13182::s3");
		}
		[Test]
		public void Mangle83() {
			AssertMangling("?s4@PR13182@@3RCDC", "char volatile *volatile PR13182::s4");
		}
		[Test]
		public void Mangle84() {
			AssertMangling("?s5@PR13182@@3SDDD", "char const volatile *const volatile PR13182::s5");
		}
		[Test]
		public void Mangle85() {
			AssertMangling("?s6@PR13182@@3PBQBDB", "char const *const *PR13182::s6");
		}
		[Test]
		public void Mangle86() {
			AssertMangling("?local@?1??extern_c_func@@9@4HA", "int `extern \"C\" extern_c_func'::`2'::local");
		}
		[Test]
		public void Mangle87() {
			AssertMangling("?local@?1??extern_c_func@@9@4HA", "int `extern \"C\" extern_c_func'::`2'::local");
		}
		[Test]
		public void Mangle88() {
			AssertMangling("?v@?1??f@@YAHXZ@4U<unnamed-type-v>@?1??1@YAHXZ@A", "struct `int __cdecl f(void)'::`2'::<unnamed-type-v> `int __cdecl f(void)'::`2'::v");
		}
		[Test]
		public void Mangle89() {
			AssertMangling("?v@?1???$f@H@@YAHXZ@4U<unnamed-type-v>@?1???$f@H@@YAHXZ@A", "struct `int __cdecl f<int>(void)'::`2'::<unnamed-type-v> `int __cdecl f<int>(void)'::`2'::v");
		}
		[Test]
		public void Mangle90() {
			AssertMangling("??2OverloadedNewDelete@@SAPAXI@Z", "public: static void * __cdecl OverloadedNewDelete::operator new(unsigned int)");
		}
		[Test]
		public void Mangle91() {
			AssertMangling("??_UOverloadedNewDelete@@SAPAXI@Z", "public: static void * __cdecl OverloadedNewDelete::operator new[](unsigned int)");
		}
		[Test]
		public void Mangle92() {
			AssertMangling("??3OverloadedNewDelete@@SAXPAX@Z", "public: static void __cdecl OverloadedNewDelete::operator delete(void *)");
		}
		[Test]
		public void Mangle93() {
			AssertMangling("??_VOverloadedNewDelete@@SAXPAX@Z", "public: static void __cdecl OverloadedNewDelete::operator delete[](void *)");
		}
		[Test]
		public void Mangle94() {
			AssertMangling("??HOverloadedNewDelete@@QAEHH@Z", "public: int __thiscall OverloadedNewDelete::operator+(int)");
		}
		[Test]
		public void Mangle95() {
			AssertMangling("??2OverloadedNewDelete@@SAPEAX_K@Z", "public: static void * __cdecl OverloadedNewDelete::operator new(unsigned __int64)");
		}
		[Test]
		public void Mangle96() {
			AssertMangling("??_UOverloadedNewDelete@@SAPEAX_K@Z", "public: static void * __cdecl OverloadedNewDelete::operator new[](unsigned __int64)");
		}
		[Test]
		public void Mangle97() {
			AssertMangling("??3OverloadedNewDelete@@SAXPEAX@Z", "public: static void __cdecl OverloadedNewDelete::operator delete(void *)");
		}
		[Test]
		public void Mangle98() {
			AssertMangling("??_VOverloadedNewDelete@@SAXPEAX@Z", "public: static void __cdecl OverloadedNewDelete::operator delete[](void *)");
		}
		[Test]
		public void Mangle99() {
			AssertMangling("??HOverloadedNewDelete@@QEAAHH@Z", "public: int __cdecl OverloadedNewDelete::operator+(int)");
		}
		[Test]
		public void Mangle100() {
			AssertMangling("??2TypedefNewDelete@@SAPAXI@Z", "public: static void * __cdecl TypedefNewDelete::operator new(unsigned int)");
		}
		[Test]
		public void Mangle101() {
			AssertMangling("??_UTypedefNewDelete@@SAPAXI@Z", "public: static void * __cdecl TypedefNewDelete::operator new[](unsigned int)");
		}
		[Test]
		public void Mangle102() {
			AssertMangling("??3TypedefNewDelete@@SAXPAX@Z", "public: static void __cdecl TypedefNewDelete::operator delete(void *)");
		}
		[Test]
		public void Mangle103() {
			AssertMangling("??_VTypedefNewDelete@@SAXPAX@Z", "public: static void __cdecl TypedefNewDelete::operator delete[](void *)");
		}
		[Test]
		public void Mangle104() {
			AssertMangling("?vector_func@@YQXXZ", "void __vectorcall vector_func(void)");
		}
		[Test]
		public void Mangle105() {
			AssertMangling("??$fn_tmpl@$1?extern_c_func@@YAXXZ@@YAXXZ", "void __cdecl fn_tmpl<&void __cdecl extern_c_func(void)>(void)");
		}
		[Test]
		public void Mangle106() {
			AssertMangling("?overloaded_fn@@$$J0YAXXZ", "extern \"C\" void __cdecl overloaded_fn(void)");
		}
		[Test]
		public void Mangle107() {
			AssertMangling("?f@UnnamedType@@YAXQAPAU<unnamed-type-T1>@S@1@@Z", "void __cdecl UnnamedType::f(struct UnnamedType::S::<unnamed-type-T1> **const)");
		}
		[Test]
		public void Mangle108() {
			AssertMangling("?f@UnnamedType@@YAXUT2@S@1@@Z", "void __cdecl UnnamedType::f(struct UnnamedType::S::T2)");
		}
		[Test]
		public void Mangle109() {
			AssertMangling("?f@UnnamedType@@YAXPAUT4@S@1@@Z", "void __cdecl UnnamedType::f(struct UnnamedType::S::T4 *)");
		}
		[Test]
		public void Mangle110() {
			AssertMangling("?f@UnnamedType@@YAXUT4@S@1@@Z", "void __cdecl UnnamedType::f(struct UnnamedType::S::T4)");
		}
		[Test]
		public void Mangle111() {
			AssertMangling("?f@UnnamedType@@YAXUT5@S@1@@Z", "void __cdecl UnnamedType::f(struct UnnamedType::S::T5)");
		}
		[Test]
		public void Mangle112() {
			AssertMangling("?f@UnnamedType@@YAXUT2@S@1@@Z", "void __cdecl UnnamedType::f(struct UnnamedType::S::T2)");
		}
		[Test]
		public void Mangle113() {
			AssertMangling("?f@UnnamedType@@YAXUT4@S@1@@Z", "void __cdecl UnnamedType::f(struct UnnamedType::S::T4)");
		}
		[Test]
		public void Mangle114() {
			AssertMangling("?f@UnnamedType@@YAXUT5@S@1@@Z", "void __cdecl UnnamedType::f(struct UnnamedType::S::T5)");
		}
		[Test]
		public void Mangle115() {
			AssertMangling("?f@Atomic@@YAXU?$_Atomic@H@__clang@@@Z", "void __cdecl Atomic::f(struct __clang::_Atomic<int>)");
		}
		[Test]
		public void Mangle116() {
			AssertMangling("?f@Complex@@YAXU?$_Complex@H@__clang@@@Z", "void __cdecl Complex::f(struct __clang::_Complex<int>)");
		}
		[Test]
		public void Mangle117() {
			AssertMangling("?f@Float16@@YAXU_Float16@__clang@@@Z", "void __cdecl Float16::f(struct __clang::_Float16)");
		}
		[Test]
		public void Mangle118() {
			AssertMangling("??0?$L@H@NS@@QEAA@XZ", "public: __cdecl NS::L<int>::L<int>(void)");
		}
		[Test]
		public void Mangle119() {
			AssertMangling("??0Bar@Foo@@QEAA@XZ", "public: __cdecl Foo::Bar::Bar(void)");
		}
		[Test]
		public void Mangle120() {
			AssertMangling("??0?$L@V?$H@PAH@PR26029@@@PR26029@@QAE@XZ", "public: __thiscall PR26029::L<class PR26029::H<int *>>::L<class PR26029::H<int *>>(void)");
		}
		#endregion

		#region Md5
		[Test]
		public void Md5_1() {
			AssertMangling("??@a6a285da2eea70dba6b578022be61d81@", "??@a6a285da2eea70dba6b578022be61d81@");
		}

		#endregion

		#region NestedScopes
		[Test]
		public void NestedScopes1() {
			AssertMangling("??@a6a285da2eea70dba6b578022be61d81@", "??@a6a285da2eea70dba6b578022be61d81@");
		}

		#endregion

		#region Operators
		[Test]
		public void Operators1() {
			AssertMangling("??0Base@@QEAA@XZ", "public: __cdecl Base::Base(void)");
		}
		[Test]
		public void Operators2() {
			AssertMangling("??1Base@@UEAA@XZ", "public: virtual __cdecl Base::~Base(void)");
		}
		[Test]
		public void Operators3() {
			AssertMangling("??2@YAPEAX_K@Z", "void * __cdecl operator new(unsigned __int64)");
		}
		[Test]
		public void Operators4() {
			AssertMangling("??3@YAXPEAX_K@Z", "void __cdecl operator delete(void *, unsigned __int64)");
		}
		[Test]
		public void Operators5() {
			AssertMangling("??4Base@@QEAAHH@Z", "public: int __cdecl Base::operator=(int)");
		}
		[Test]
		public void Operators6() {
			AssertMangling("??6Base@@QEAAHH@Z", "public: int __cdecl Base::operator<<(int)");
		}
		[Test]
		public void Operators7() {
			AssertMangling("??5Base@@QEAAHH@Z", "public: int __cdecl Base::operator>>(int)");
		}
		[Test]
		public void Operators8() {
			AssertMangling("??7Base@@QEAAHXZ", "public: int __cdecl Base::operator!(void)");
		}
		[Test]
		public void Operators9() {
			AssertMangling("??8Base@@QEAAHH@Z", "public: int __cdecl Base::operator==(int)");
		}
		[Test]
		public void Operators10() {
			AssertMangling("??9Base@@QEAAHH@Z", "public: int __cdecl Base::operator!=(int)");
		}
		[Test]
		public void Operators11() {
			AssertMangling("??ABase@@QEAAHH@Z", "public: int __cdecl Base::operator[](int)");
		}
		[Test]
		public void Operators12() {
			AssertMangling("??BBase@@QEAAHXZ", "public: __cdecl Base::operator int(void)");
		}
		[Test]
		public void Operators13() {
			AssertMangling("??CBase@@QEAAHXZ", "public: int __cdecl Base::operator->(void)");
		}
		[Test]
		public void Operators14() {
			AssertMangling("??DBase@@QEAAHXZ", "public: int __cdecl Base::operator*(void)");
		}
		[Test]
		public void Operators15() {
			AssertMangling("??EBase@@QEAAHXZ", "public: int __cdecl Base::operator++(void)");
		}
		[Test]
		public void Operators16() {
			AssertMangling("??EBase@@QEAAHH@Z", "public: int __cdecl Base::operator++(int)");
		}
		[Test]
		public void Operators17() {
			AssertMangling("??FBase@@QEAAHXZ", "public: int __cdecl Base::operator--(void)");
		}
		[Test]
		public void Operators18() {
			AssertMangling("??FBase@@QEAAHH@Z", "public: int __cdecl Base::operator--(int)");
		}
		[Test]
		public void Operators19() {
			AssertMangling("??GBase@@QEAAHH@Z", "public: int __cdecl Base::operator-(int)");
		}
		[Test]
		public void Operators20() {
			AssertMangling("??HBase@@QEAAHH@Z", "public: int __cdecl Base::operator+(int)");
		}
		[Test]
		public void Operators21() {
			AssertMangling("??IBase@@QEAAHH@Z", "public: int __cdecl Base::operator&(int)");
		}
		[Test]
		public void Operators22() {
			AssertMangling("??JBase@@QEAAHH@Z", "public: int __cdecl Base::operator->*(int)");
		}
		[Test]
		public void Operators23() {
			AssertMangling("??KBase@@QEAAHH@Z", "public: int __cdecl Base::operator/(int)");
		}
		[Test]
		public void Operators24() {
			AssertMangling("??LBase@@QEAAHH@Z", "public: int __cdecl Base::operator%(int)");
		}
		[Test]
		public void Operators25() {
			AssertMangling("??MBase@@QEAAHH@Z", "public: int __cdecl Base::operator<(int)");
		}
		[Test]
		public void Operators26() {
			AssertMangling("??NBase@@QEAAHH@Z", "public: int __cdecl Base::operator<=(int)");
		}
		[Test]
		public void Operators27() {
			AssertMangling("??OBase@@QEAAHH@Z", "public: int __cdecl Base::operator>(int)");
		}
		[Test]
		public void Operators28() {
			AssertMangling("??PBase@@QEAAHH@Z", "public: int __cdecl Base::operator>=(int)");
		}
		[Test]
		public void Operators29() {
			AssertMangling("??QBase@@QEAAHH@Z", "public: int __cdecl Base::operator,(int)");
		}
		[Test]
		public void Operators30() {
			AssertMangling("??RBase@@QEAAHXZ", "public: int __cdecl Base::operator()(void)");
		}
		[Test]
		public void Operators31() {
			AssertMangling("??SBase@@QEAAHXZ", "public: int __cdecl Base::operator~(void)");
		}
		[Test]
		public void Operators32() {
			AssertMangling("??TBase@@QEAAHH@Z", "public: int __cdecl Base::operator^(int)");
		}
		[Test]
		public void Operators33() {
			AssertMangling("??UBase@@QEAAHH@Z", "public: int __cdecl Base::operator|(int)");
		}
		[Test]
		public void Operators34() {
			AssertMangling("??VBase@@QEAAHH@Z", "public: int __cdecl Base::operator&&(int)");
		}
		[Test]
		public void Operators35() {
			AssertMangling("??WBase@@QEAAHH@Z", "public: int __cdecl Base::operator||(int)");
		}
		[Test]
		public void Operators36() {
			AssertMangling("??XBase@@QEAAHH@Z", "public: int __cdecl Base::operator*=(int)");
		}
		[Test]
		public void Operators37() {
			AssertMangling("??YBase@@QEAAHH@Z", "public: int __cdecl Base::operator+=(int)");
		}
		[Test]
		public void Operators38() {
			AssertMangling("??ZBase@@QEAAHH@Z", "public: int __cdecl Base::operator-=(int)");
		}
		[Test]
		public void Operators39() {
			AssertMangling("??_0Base@@QEAAHH@Z", "public: int __cdecl Base::operator/=(int)");
		}
		[Test]
		public void Operators40() {
			AssertMangling("??_1Base@@QEAAHH@Z", "public: int __cdecl Base::operator%=(int)");
		}
		[Test]
		public void Operators41() {
			AssertMangling("??_2Base@@QEAAHH@Z", "public: int __cdecl Base::operator>>=(int)");
		}
		[Test]
		public void Operators42() {
			AssertMangling("??_3Base@@QEAAHH@Z", "public: int __cdecl Base::operator<<=(int)");
		}
		[Test]
		public void Operators43() {
			AssertMangling("??_4Base@@QEAAHH@Z", "public: int __cdecl Base::operator&=(int)");
		}
		[Test]
		public void Operators44() {
			AssertMangling("??_5Base@@QEAAHH@Z", "public: int __cdecl Base::operator|=(int)");
		}
		[Test]
		public void Operators45() {
			AssertMangling("??_6Base@@QEAAHH@Z", "public: int __cdecl Base::operator^=(int)");
		}
		[Test]
		public void Operators46() {
			AssertMangling("??_7Base@@6B@", "const Base::`vftable'");
		}
		[Test]
		public void Operators47() {
			AssertMangling("??_7A@B@@6BC@D@@@", "const B::A::`vftable'{for `D::C'}");
		}
		[Test]
		public void Operators48() {
			AssertMangling("??_8Middle2@@7B@", "const Middle2::`vbtable'");
		}
		[Test]
		public void Operators49() {
			AssertMangling("??_9Base@@$B7AA", "[thunk]: __cdecl Base::`vcall'{8, {flat}}");
		}
		[Test]
		public void Operators50() {
			AssertMangling("??_B?1??getS@@YAAAUS@@XZ@51", "`struct S & __cdecl getS(void)'::`2'::`local static guard'{2}");
		}
		[Test]
		public void Operators51() {
			AssertMangling("??_C@_02PCEFGMJL@hi?$AA@", "\"hi\"");
		}
		[Test]
		public void Operators52() {
			AssertMangling("??_DDiamond@@QEAAXXZ", "public: void __cdecl Diamond::`vbase dtor'(void)");
		}
		[Test]
		public void Operators53() {
			AssertMangling("??_EBase@@UEAAPEAXI@Z", "public: virtual void * __cdecl Base::`vector deleting dtor'(unsigned int)");
		}
		[Test]
		public void Operators54() {
			AssertMangling("??_EBase@@G3AEPAXI@Z", "[thunk]: private: void * __thiscall Base::`vector deleting dtor'`adjustor{4}'(unsigned int)");
		}
		[Test]
		public void Operators55() {
			AssertMangling("??_F?$SomeTemplate@H@@QAEXXZ", "public: void __thiscall SomeTemplate<int>::`default ctor closure'(void)");
		}
		[Test]
		public void Operators56() {
			AssertMangling("??_GBase@@UEAAPEAXI@Z", "public: virtual void * __cdecl Base::`scalar deleting dtor'(unsigned int)");
		}
		[Test]
		public void Operators57() {
			AssertMangling("??_H@YAXPEAX_K1P6APEAX0@Z@Z", "void __cdecl `vector ctor iterator'(void *, unsigned __int64, unsigned __int64, void * (__cdecl *)(void *))");
		}
		[Test]
		public void Operators58() {
			AssertMangling("??_I@YAXPEAX_K1P6AX0@Z@Z", "void __cdecl `vector dtor iterator'(void *, unsigned __int64, unsigned __int64, void (__cdecl *)(void *))");
		}
		[Test]
		public void Operators59() {
			AssertMangling("??_JBase@@UEAAPEAXI@Z", "public: virtual void * __cdecl Base::`vector vbase ctor iterator'(unsigned int)");
		}
		[Test]
		public void Operators60() {
			AssertMangling("??_KBase@@UEAAPEAXI@Z", "public: virtual void * __cdecl Base::`virtual displacement map'(unsigned int)");
		}
		[Test]
		public void Operators61() {
			AssertMangling("??_LBase@@UEAAPEAXI@Z", "public: virtual void * __cdecl Base::`eh vector ctor iterator'(unsigned int)");
		}
		[Test]
		public void Operators62() {
			AssertMangling("??_MBase@@UEAAPEAXI@Z", "public: virtual void * __cdecl Base::`eh vector dtor iterator'(unsigned int)");
		}
		[Test]
		public void Operators63() {
			AssertMangling("??_NBase@@UEAAPEAXI@Z", "public: virtual void * __cdecl Base::`eh vector vbase ctor iterator'(unsigned int)");
		}
		[Test]
		public void Operators64() {
			AssertMangling("??_O?$SomeTemplate@H@@QAEXXZ", "public: void __thiscall SomeTemplate<int>::`copy ctor closure'(void)");
		}
		[Test]
		public void Operators65() {
			AssertMangling("??_SBase@@6B@", "const Base::`local vftable'");
		}
		[Test]
		public void Operators66() {
			AssertMangling("??_TDerived@@QEAAXXZ", "public: void __cdecl Derived::`local vftable ctor closure'(void)");
		}
		[Test]
		public void Operators67() {
			AssertMangling("??_U@YAPEAX_KAEAVklass@@@Z", "void * __cdecl operator new[](unsigned __int64, class klass &)");
		}
		[Test]
		public void Operators68() {
			AssertMangling("??_V@YAXPEAXAEAVklass@@@Z", "void __cdecl operator delete[](void *, class klass &)");
		}
		[Test]
		public void Operators69() {
			AssertMangling("??_R0?AUBase@@@8", "struct Base `RTTI Type Descriptor'");
		}
		[Test]
		public void Operators70() {
			AssertMangling("??_R1A@?0A@EA@Base@@8", "Base::`RTTI Base Class Descriptor at (0, -1, 0, 64)'");
		}
		[Test]
		public void Operators71() {
			AssertMangling("??_R2Base@@8", "Base::`RTTI Base Class Array'");
		}
		[Test]
		public void Operators72() {
			AssertMangling("??_R3Base@@8", "Base::`RTTI Class Hierarchy Descriptor'");
		}
		[Test]
		public void Operators73() {
			AssertMangling("??_R4Base@@6B@", "const Base::`RTTI Complete Object Locator'");
		}
		[Test]
		public void Operators74() {
			AssertMangling("??__EFoo@@YAXXZ", "void __cdecl `dynamic initializer for 'Foo''(void)");
		}
		[Test]
		public void Operators75() {
			AssertMangling("??__FFoo@@YAXXZ", "void __cdecl `dynamic atexit destructor for 'Foo''(void)");
		}
		[Test]
		public void Operators76() {
			AssertMangling("??__F_decisionToDFA@XPathLexer@@0V?$vector@VDFA@dfa@antlr4@@V?$allocator@VDFA@dfa@antlr4@@@std@@@std@@A@YAXXZ", "void __cdecl `dynamic atexit destructor for `private: static class std::vector<class antlr4::dfa::DFA, class std::allocator<class antlr4::dfa::DFA>> XPathLexer::_decisionToDFA''(void)");
		}
		[Test]
		public void Operators77() {
			AssertMangling("??__K_deg@@YAHO@Z", "int __cdecl operator \"\"_deg(long double)");
		}
		#endregion

		#region ReturnQualifiers
		[Test]
		public void ReturnQualifiers1() {
			AssertMangling("?a1@@YAXXZ", "void __cdecl a1(void)");
		}
		[Test]
		public void ReturnQualifiers2() {
			AssertMangling("?a2@@YAHXZ", "int __cdecl a2(void)");
		}
		[Test]
		public void ReturnQualifiers3() {
			AssertMangling("?a3@@YA?BHXZ", "int const __cdecl a3(void)");
		}
		[Test]
		public void ReturnQualifiers4() {
			AssertMangling("?a4@@YA?CHXZ", "int volatile __cdecl a4(void)");
		}
		[Test]
		public void ReturnQualifiers5() {
			AssertMangling("?a5@@YA?DHXZ", "int const volatile __cdecl a5(void)");
		}
		[Test]
		public void ReturnQualifiers6() {
			AssertMangling("?a6@@YAMXZ", "float __cdecl a6(void)");
		}
		[Test]
		public void ReturnQualifiers7() {
			AssertMangling("?b1@@YAPAHXZ", "int * __cdecl b1(void)");
		}
		[Test]
		public void ReturnQualifiers8() {
			AssertMangling("?b2@@YAPBDXZ", "char const * __cdecl b2(void)");
		}
		[Test]
		public void ReturnQualifiers9() {
			AssertMangling("?b3@@YAPAMXZ", "float * __cdecl b3(void)");
		}
		[Test]
		public void ReturnQualifiers10() {
			AssertMangling("?b4@@YAPBMXZ", "float const * __cdecl b4(void)");
		}
		[Test]
		public void ReturnQualifiers11() {
			AssertMangling("?b5@@YAPCMXZ", "float volatile * __cdecl b5(void)");
		}
		[Test]
		public void ReturnQualifiers12() {
			AssertMangling("?b6@@YAPDMXZ", "float const volatile * __cdecl b6(void)");
		}
		[Test]
		public void ReturnQualifiers13() {
			AssertMangling("?b7@@YAAAMXZ", "float & __cdecl b7(void)");
		}
		[Test]
		public void ReturnQualifiers14() {
			AssertMangling("?b8@@YAABMXZ", "float const & __cdecl b8(void)");
		}
		[Test]
		public void ReturnQualifiers15() {
			AssertMangling("?b9@@YAACMXZ", "float volatile & __cdecl b9(void)");
		}
		[Test]
		public void ReturnQualifiers16() {
			AssertMangling("?b10@@YAADMXZ", "float const volatile & __cdecl b10(void)");
		}
		[Test]
		public void ReturnQualifiers17() {
			AssertMangling("?b11@@YAPAPBDXZ", "char const ** __cdecl b11(void)");
		}
		[Test]
		public void ReturnQualifiers18() {
			AssertMangling("?c1@@YA?AVA@@XZ", "class A __cdecl c1(void)");
		}
		[Test]
		public void ReturnQualifiers19() {
			AssertMangling("?c2@@YA?BVA@@XZ", "class A const __cdecl c2(void)");
		}
		[Test]
		public void ReturnQualifiers20() {
			AssertMangling("?c3@@YA?CVA@@XZ", "class A volatile __cdecl c3(void)");
		}
		[Test]
		public void ReturnQualifiers21() {
			AssertMangling("?c4@@YA?DVA@@XZ", "class A const volatile __cdecl c4(void)");
		}
		[Test]
		public void ReturnQualifiers22() {
			AssertMangling("?c5@@YAPBVA@@XZ", "class A const * __cdecl c5(void)");
		}
		[Test]
		public void ReturnQualifiers23() {
			AssertMangling("?c6@@YAPCVA@@XZ", "class A volatile * __cdecl c6(void)");
		}
		[Test]
		public void ReturnQualifiers24() {
			AssertMangling("?c7@@YAPDVA@@XZ", "class A const volatile * __cdecl c7(void)");
		}
		[Test]
		public void ReturnQualifiers25() {
			AssertMangling("?c8@@YAAAVA@@XZ", "class A & __cdecl c8(void)");
		}
		[Test]
		public void ReturnQualifiers26() {
			AssertMangling("?c9@@YAABVA@@XZ", "class A const & __cdecl c9(void)");
		}
		[Test]
		public void ReturnQualifiers27() {
			AssertMangling("?c10@@YAACVA@@XZ", "class A volatile & __cdecl c10(void)");
		}
		[Test]
		public void ReturnQualifiers28() {
			AssertMangling("?c11@@YAADVA@@XZ", "class A const volatile & __cdecl c11(void)");
		}
		[Test]
		public void ReturnQualifiers29() {
			AssertMangling("?d1@@YA?AV?$B@H@@XZ", "class B<int> __cdecl d1(void)");
		}
		[Test]
		public void ReturnQualifiers30() {
			AssertMangling("?d2@@YA?AV?$B@PBD@@XZ", "class B<char const *> __cdecl d2(void)");
		}
		[Test]
		public void ReturnQualifiers31() {
			AssertMangling("?d3@@YA?AV?$B@VA@@@@XZ", "class B<class A> __cdecl d3(void)");
		}
		[Test]
		public void ReturnQualifiers32() {
			AssertMangling("?d4@@YAPAV?$B@VA@@@@XZ", "class B<class A> * __cdecl d4(void)");
		}
		[Test]
		public void ReturnQualifiers33() {
			AssertMangling("?d5@@YAPBV?$B@VA@@@@XZ", "class B<class A> const * __cdecl d5(void)");
		}
		[Test]
		public void ReturnQualifiers34() {
			AssertMangling("?d6@@YAPCV?$B@VA@@@@XZ", "class B<class A> volatile * __cdecl d6(void)");
		}
		[Test]
		public void ReturnQualifiers35() {
			AssertMangling("?d7@@YAPDV?$B@VA@@@@XZ", "class B<class A> const volatile * __cdecl d7(void)");
		}
		[Test]
		public void ReturnQualifiers36() {
			AssertMangling("?d8@@YAAAV?$B@VA@@@@XZ", "class B<class A> & __cdecl d8(void)");
		}
		[Test]
		public void ReturnQualifiers37() {
			AssertMangling("?d9@@YAABV?$B@VA@@@@XZ", "class B<class A> const & __cdecl d9(void)");
		}
		[Test]
		public void ReturnQualifiers38() {
			AssertMangling("?d10@@YAACV?$B@VA@@@@XZ", "class B<class A> volatile & __cdecl d10(void)");
		}
		[Test]
		public void ReturnQualifiers39() {
			AssertMangling("?d11@@YAADV?$B@VA@@@@XZ", "class B<class A> const volatile & __cdecl d11(void)");
		}
		[Test]
		public void ReturnQualifiers40() {
			AssertMangling("?e1@@YA?AW4Enum@@XZ", "enum Enum __cdecl e1(void)");
		}
		[Test]
		public void ReturnQualifiers41() {
			AssertMangling("?e2@@YA?BW4Enum@@XZ", "enum Enum const __cdecl e2(void)");
		}
		[Test]
		public void ReturnQualifiers42() {
			AssertMangling("?e3@@YAPAW4Enum@@XZ", "enum Enum * __cdecl e3(void)");
		}
		[Test]
		public void ReturnQualifiers43() {
			AssertMangling("?e4@@YAAAW4Enum@@XZ", "enum Enum & __cdecl e4(void)");
		}
		[Test]
		public void ReturnQualifiers44() {
			AssertMangling("?f1@@YA?AUS@@XZ", "struct S __cdecl f1(void)");
		}
		[Test]
		public void ReturnQualifiers45() {
			AssertMangling("?f2@@YA?BUS@@XZ", "struct S const __cdecl f2(void)");
		}
		[Test]
		public void ReturnQualifiers46() {
			AssertMangling("?f3@@YAPAUS@@XZ", "struct S * __cdecl f3(void)");
		}
		[Test]
		public void ReturnQualifiers47() {
			AssertMangling("?f4@@YAPBUS@@XZ", "struct S const * __cdecl f4(void)");
		}
		[Test]
		public void ReturnQualifiers48() {
			AssertMangling("?f5@@YAPDUS@@XZ", "struct S const volatile * __cdecl f5(void)");
		}
		[Test]
		public void ReturnQualifiers49() {
			AssertMangling("?f6@@YAAAUS@@XZ", "struct S & __cdecl f6(void)");
		}
		[Test]
		public void ReturnQualifiers50() {
			AssertMangling("?f7@@YAQAUS@@XZ", "struct S *const __cdecl f7(void)");
		}
		[Test]
		public void ReturnQualifiers51() {
			AssertMangling("?f8@@YAPQS@@HXZ", "int S::* __cdecl f8(void)");
		}
		[Test]
		public void ReturnQualifiers52() {
			AssertMangling("?f9@@YAQQS@@HXZ", "int S::*const __cdecl f9(void)");
		}
		[Test]
		public void ReturnQualifiers53() {
			AssertMangling("?f10@@YAPIQS@@HXZ", "int S::*__restrict __cdecl f10(void)");
		}
		[Test]
		public void ReturnQualifiers54() {
			AssertMangling("?f11@@YAQIQS@@HXZ", "int S::*const __restrict __cdecl f11(void)");
		}
		[Test]
		public void ReturnQualifiers55() {
			AssertMangling("?g1@@YAP6AHH@ZXZ", "int (__cdecl * __cdecl g1(void))(int)");
		}
		[Test]
		public void ReturnQualifiers56() {
			AssertMangling("?g2@@YAQ6AHH@ZXZ", "int (__cdecl *const __cdecl g2(void))(int)");
		}
		[Test]
		public void ReturnQualifiers57() {
			AssertMangling("?g3@@YAPAP6AHH@ZXZ", "int (__cdecl ** __cdecl g3(void))(int)");
		}
		[Test]
		public void ReturnQualifiers58() {
			AssertMangling("?g4@@YAPBQ6AHH@ZXZ", "int (__cdecl *const * __cdecl g4(void))(int)");
		}
		[Test]
		public void ReturnQualifiers59() {
			AssertMangling("?h1@@YAAIAHXZ", "int &__restrict __cdecl h1(void)");
		}
		#endregion

		#region StringLiterals
		[Test]
		public void StringLiteral1() {
			AssertMangling("??_C@_0CF@LABBIIMO@012345678901234567890123456789AB@", "\"012345678901234567890123456789AB\"...");
		}
		#endregion

		#region TemplateCallbacks
		[Test]
		public void TemplateCallback1() {
			AssertMangling("?callback_void@@3V?$C@$$A6AXXZ@@A", "class C<void __cdecl(void)> callback_void");
		}
		[Test]
		public void TemplateCallback2() {
			AssertMangling("?callback_void_volatile@@3V?$C@$$A6AXXZ@@C", "class C<void __cdecl(void)> volatile callback_void_volatile");
		}
		[Test]
		public void TemplateCallback3() {
			AssertMangling("?callback_int@@3V?$C@$$A6AHXZ@@A", "class C<int __cdecl(void)> callback_int");
		}
		[Test]
		public void TemplateCallback4() {
			AssertMangling("?callback_Type@@3V?$C@$$A6A?AVType@@XZ@@A", "class C<class Type __cdecl(void)> callback_Type");
		}
		[Test]
		public void TemplateCallback5() {
			AssertMangling("?callback_void_int@@3V?$C@$$A6AXH@Z@@A", "class C<void __cdecl(int)> callback_void_int");
		}
		[Test]
		public void TemplateCallback6() {
			AssertMangling("?callback_int_int@@3V?$C@$$A6AHH@Z@@A", "class C<int __cdecl(int)> callback_int_int");
		}
		[Test]
		public void TemplateCallback7() {
			AssertMangling("?callback_void_Type@@3V?$C@$$A6AXVType@@@Z@@A", "class C<void __cdecl(class Type)> callback_void_Type");
		}
		[Test]
		public void TemplateCallback8() {
			AssertMangling("?foo@@YAXV?$C@$$A6AXXZ@@@Z", "void __cdecl foo(class C<void __cdecl(void)>)");
		}
		[Test]
		public void TemplateCallback9() {
			AssertMangling("?function@@YAXV?$C@$$A6AXXZ@@@Z", "void __cdecl function(class C<void __cdecl(void)>)");
		}
		[Test]
		public void TemplateCallback10() {
			AssertMangling("?function_pointer@@YAXV?$C@P6AXXZ@@@Z", "void __cdecl function_pointer(class C<void (__cdecl *)(void)>)");
		}
		[Test]
		public void TemplateCallback11() {
			AssertMangling("?member_pointer@@YAXV?$C@P8Z@@AEXXZ@@@Z", "void __cdecl member_pointer(class C<void (__thiscall Z::*)(void)>)");
		}
		[Test]
		public void TemplateCallback12() {
			AssertMangling("??$bar@P6AHH@Z@@YAXP6AHH@Z@Z", "void __cdecl bar<int (__cdecl *)(int)>(int (__cdecl *)(int))");
		}
		[Test]
		public void TemplateCallback13() {
			AssertMangling("??$WrapFnPtr@$1?VoidFn@@YAXXZ@@YAXXZ", "void __cdecl WrapFnPtr<&void __cdecl VoidFn(void)>(void)");
		}
		[Test]
		public void TemplateCallback14() {
			AssertMangling("??$WrapFnRef@$1?VoidFn@@YAXXZ@@YAXXZ", "void __cdecl WrapFnRef<&void __cdecl VoidFn(void)>(void)");
		}
		[Test]
		public void TemplateCallback15() {
			AssertMangling("??$WrapFnPtr@$1?VoidStaticMethod@Thing@@SAXXZ@@YAXXZ", "void __cdecl WrapFnPtr<&public: static void __cdecl Thing::VoidStaticMethod(void)>(void)");
		}
		[Test]
		public void TemplateCallback16() {
			AssertMangling("??$WrapFnRef@$1?VoidStaticMethod@Thing@@SAXXZ@@YAXXZ", "void __cdecl WrapFnRef<&public: static void __cdecl Thing::VoidStaticMethod(void)>(void)");
		}

		#endregion
		
		#region Templates
		[Test]
		public void Template1() {
			AssertMangling("??0?$Class@VTypename@@@@QAE@XZ", "public: __thiscall Class<class Typename>::Class<class Typename>(void)");
		}
		[Test]
		public void Template2() {
			AssertMangling("??0?$Class@VTypename@@@@QEAA@XZ", "public: __cdecl Class<class Typename>::Class<class Typename>(void)");
		}
		[Test]
		public void Template3() {
			AssertMangling("??0?$Class@$$CBVTypename@@@@QAE@XZ", "public: __thiscall Class<class Typename const>::Class<class Typename const>(void)");
		}
		[Test]
		public void Template4() {
			AssertMangling("??0?$Class@$$CBVTypename@@@@QEAA@XZ", "public: __cdecl Class<class Typename const>::Class<class Typename const>(void)");
		}
		[Test]
		public void Template5() {
			AssertMangling("??0?$Class@$$CCVTypename@@@@QAE@XZ", "public: __thiscall Class<class Typename volatile>::Class<class Typename volatile>(void)");
		}
		[Test]
		public void Template6() {
			AssertMangling("??0?$Class@$$CCVTypename@@@@QEAA@XZ", "public: __cdecl Class<class Typename volatile>::Class<class Typename volatile>(void)");
		}
		[Test]
		public void Template7() {
			AssertMangling("??0?$Class@$$CDVTypename@@@@QAE@XZ", "public: __thiscall Class<class Typename const volatile>::Class<class Typename const volatile>(void)");
		}
		[Test]
		public void Template8() {
			AssertMangling("??0?$Class@$$CDVTypename@@@@QEAA@XZ", "public: __cdecl Class<class Typename const volatile>::Class<class Typename const volatile>(void)");
		}
		[Test]
		public void Template9() {
			AssertMangling("??0?$Class@V?$Nested@VTypename@@@@@@QAE@XZ", "public: __thiscall Class<class Nested<class Typename>>::Class<class Nested<class Typename>>(void)");
		}
		[Test]
		public void Template10() {
			AssertMangling("??0?$Class@V?$Nested@VTypename@@@@@@QEAA@XZ", "public: __cdecl Class<class Nested<class Typename>>::Class<class Nested<class Typename>>(void)");
		}
		[Test]
		public void Template11() {
			AssertMangling("??0?$Class@QAH@@QAE@XZ", "public: __thiscall Class<int *const>::Class<int *const>(void)");
		}
		[Test]
		public void Template12() {
			AssertMangling("??0?$Class@QEAH@@QEAA@XZ", "public: __cdecl Class<int *const>::Class<int *const>(void)");
		}
		[Test]
		public void Template13() {
			AssertMangling("??0?$Class@$$A6AHXZ@@QAE@XZ", "public: __thiscall Class<int __cdecl(void)>::Class<int __cdecl(void)>(void)");
		}
		[Test]
		public void Template14() {
			AssertMangling("??0?$Class@$$A6AHXZ@@QEAA@XZ", "public: __cdecl Class<int __cdecl(void)>::Class<int __cdecl(void)>(void)");
		}
		[Test]
		public void Template15() {
			AssertMangling("??0?$Class@$$BY0A@H@@QAE@XZ", "public: __thiscall Class<int[]>::Class<int[]>(void)");
		}
		[Test]
		public void Template16() {
			AssertMangling("??0?$Class@$$BY0A@H@@QEAA@XZ", "public: __cdecl Class<int[]>::Class<int[]>(void)");
		}
		[Test]
		public void Template17() {
			AssertMangling("??0?$Class@$$BY04H@@QAE@XZ", "public: __thiscall Class<int[5]>::Class<int[5]>(void)");
		}
		[Test]
		public void Template18() {
			AssertMangling("??0?$Class@$$BY04H@@QEAA@XZ", "public: __cdecl Class<int[5]>::Class<int[5]>(void)");
		}
		[Test]
		public void Template19() {
			AssertMangling("??0?$Class@$$BY04$$CBH@@QAE@XZ", "public: __thiscall Class<int const[5]>::Class<int const[5]>(void)");
		}
		[Test]
		public void Template20() {
			AssertMangling("??0?$Class@$$BY04$$CBH@@QEAA@XZ", "public: __cdecl Class<int const[5]>::Class<int const[5]>(void)");
		}
		[Test]
		public void Template21() {
			AssertMangling("??0?$Class@$$BY04QAH@@QAE@XZ", "public: __thiscall Class<int *const[5]>::Class<int *const[5]>(void)");
		}
		[Test]
		public void Template22() {
			AssertMangling("??0?$Class@$$BY04QEAH@@QEAA@XZ", "public: __cdecl Class<int *const[5]>::Class<int *const[5]>(void)");
		}
		[Test]
		public void Template23() {
			AssertMangling("??0?$BoolTemplate@$0A@@@QAE@XZ", "public: __thiscall BoolTemplate<0>::BoolTemplate<0>(void)");
		}
		[Test]
		public void Template24() {
			AssertMangling("??0?$BoolTemplate@$0A@@@QEAA@XZ", "public: __cdecl BoolTemplate<0>::BoolTemplate<0>(void)");
		}
		[Test]
		public void Template25() {
			AssertMangling("??0?$BoolTemplate@$00@@QAE@XZ", "public: __thiscall BoolTemplate<1>::BoolTemplate<1>(void)");
		}
		[Test]
		public void Template26() {
			AssertMangling("??0?$BoolTemplate@$00@@QEAA@XZ", "public: __cdecl BoolTemplate<1>::BoolTemplate<1>(void)");
		}
		[Test]
		public void Template27() {
			AssertMangling("??$Foo@H@?$BoolTemplate@$00@@QAEXH@Z", "public: void __thiscall BoolTemplate<1>::Foo<int>(int)");
		}
		[Test]
		public void Template28() {
			AssertMangling("??$Foo@H@?$BoolTemplate@$00@@QEAAXH@Z", "public: void __cdecl BoolTemplate<1>::Foo<int>(int)");
		}
		[Test]
		public void Template29() {
			AssertMangling("??0?$IntTemplate@$0A@@@QAE@XZ", "public: __thiscall IntTemplate<0>::IntTemplate<0>(void)");
		}
		[Test]
		public void Template30() {
			AssertMangling("??0?$IntTemplate@$0A@@@QEAA@XZ", "public: __cdecl IntTemplate<0>::IntTemplate<0>(void)");
		}
		[Test]
		public void Template31() {
			AssertMangling("??0?$IntTemplate@$04@@QAE@XZ", "public: __thiscall IntTemplate<5>::IntTemplate<5>(void)");
		}
		[Test]
		public void Template32() {
			AssertMangling("??0?$IntTemplate@$04@@QEAA@XZ", "public: __cdecl IntTemplate<5>::IntTemplate<5>(void)");
		}
		[Test]
		public void Template33() {
			AssertMangling("??0?$IntTemplate@$0L@@@QAE@XZ", "public: __thiscall IntTemplate<11>::IntTemplate<11>(void)");
		}
		[Test]
		public void Template34() {
			AssertMangling("??0?$IntTemplate@$0L@@@QEAA@XZ", "public: __cdecl IntTemplate<11>::IntTemplate<11>(void)");
		}
		[Test]
		public void Template35() {
			AssertMangling("??0?$IntTemplate@$0BAA@@@QAE@XZ", "public: __thiscall IntTemplate<256>::IntTemplate<256>(void)");
		}
		[Test]
		public void Template36() {
			AssertMangling("??0?$IntTemplate@$0BAA@@@QEAA@XZ", "public: __cdecl IntTemplate<256>::IntTemplate<256>(void)");
		}
		[Test]
		public void Template37() {
			AssertMangling("??0?$IntTemplate@$0CAB@@@QAE@XZ", "public: __thiscall IntTemplate<513>::IntTemplate<513>(void)");
		}
		[Test]
		public void Template38() {
			AssertMangling("??0?$IntTemplate@$0CAB@@@QEAA@XZ", "public: __cdecl IntTemplate<513>::IntTemplate<513>(void)");
		}
		[Test]
		public void Template39() {
			AssertMangling("??0?$IntTemplate@$0EAC@@@QAE@XZ", "public: __thiscall IntTemplate<1026>::IntTemplate<1026>(void)");
		}
		[Test]
		public void Template40() {
			AssertMangling("??0?$IntTemplate@$0EAC@@@QEAA@XZ", "public: __cdecl IntTemplate<1026>::IntTemplate<1026>(void)");
		}
		[Test]
		public void Template41() {
			AssertMangling("??0?$IntTemplate@$0PPPP@@@QAE@XZ", "public: __thiscall IntTemplate<65535>::IntTemplate<65535>(void)");
		}
		[Test]
		public void Template42() {
			AssertMangling("??0?$IntTemplate@$0PPPP@@@QEAA@XZ", "public: __cdecl IntTemplate<65535>::IntTemplate<65535>(void)");
		}
		[Test]
		public void Template43() {
			AssertMangling("??0?$IntTemplate@$0?0@@QAE@XZ", "public: __thiscall IntTemplate<-1>::IntTemplate<-1>(void)");
		}
		[Test]
		public void Template44() {
			AssertMangling("??0?$IntTemplate@$0?0@@QEAA@XZ", "public: __cdecl IntTemplate<-1>::IntTemplate<-1>(void)");
		}
		[Test]
		public void Template45() {
			AssertMangling("??0?$IntTemplate@$0?8@@QAE@XZ", "public: __thiscall IntTemplate<-9>::IntTemplate<-9>(void)");
		}
		[Test]
		public void Template46() {
			AssertMangling("??0?$IntTemplate@$0?8@@QEAA@XZ", "public: __cdecl IntTemplate<-9>::IntTemplate<-9>(void)");
		}
		[Test]
		public void Template47() {
			AssertMangling("??0?$IntTemplate@$0?9@@QAE@XZ", "public: __thiscall IntTemplate<-10>::IntTemplate<-10>(void)");
		}
		[Test]
		public void Template48() {
			AssertMangling("??0?$IntTemplate@$0?9@@QEAA@XZ", "public: __cdecl IntTemplate<-10>::IntTemplate<-10>(void)");
		}
		[Test]
		public void Template49() {
			AssertMangling("??0?$IntTemplate@$0?L@@@QAE@XZ", "public: __thiscall IntTemplate<-11>::IntTemplate<-11>(void)");
		}
		[Test]
		public void Template50() {
			AssertMangling("??0?$IntTemplate@$0?L@@@QEAA@XZ", "public: __cdecl IntTemplate<-11>::IntTemplate<-11>(void)");
		}
		[Test]
		public void Template51() {
			AssertMangling("??0?$UnsignedIntTemplate@$0PPPPPPPP@@@QAE@XZ", "public: __thiscall UnsignedIntTemplate<4294967295>::UnsignedIntTemplate<4294967295>(void)");
		}
		[Test]
		public void Template52() {
			AssertMangling("??0?$UnsignedIntTemplate@$0PPPPPPPP@@@QEAA@XZ", "public: __cdecl UnsignedIntTemplate<4294967295>::UnsignedIntTemplate<4294967295>(void)");
		}
		[Test]
		public void Template53() {
			AssertMangling("??0?$LongLongTemplate@$0?IAAAAAAAAAAAAAAA@@@QAE@XZ", "public: __thiscall LongLongTemplate<-9223372036854775808>::LongLongTemplate<-9223372036854775808>(void)");
		}
		[Test]
		public void Template54() {
			AssertMangling("??0?$LongLongTemplate@$0?IAAAAAAAAAAAAAAA@@@QEAA@XZ", "public: __cdecl LongLongTemplate<-9223372036854775808>::LongLongTemplate<-9223372036854775808>(void)");
		}
		[Test]
		public void Template55() {
			AssertMangling("??0?$LongLongTemplate@$0HPPPPPPPPPPPPPPP@@@QAE@XZ", "public: __thiscall LongLongTemplate<9223372036854775807>::LongLongTemplate<9223372036854775807>(void)");
		}
		[Test]
		public void Template56() {
			AssertMangling("??0?$LongLongTemplate@$0HPPPPPPPPPPPPPPP@@@QEAA@XZ", "public: __cdecl LongLongTemplate<9223372036854775807>::LongLongTemplate<9223372036854775807>(void)");
		}
		[Test]
		public void Template57() {
			AssertMangling("??0?$UnsignedLongLongTemplate@$0?0@@QAE@XZ", "public: __thiscall UnsignedLongLongTemplate<-1>::UnsignedLongLongTemplate<-1>(void)");
		}
		[Test]
		public void Template58() {
			AssertMangling("??0?$UnsignedLongLongTemplate@$0?0@@QEAA@XZ", "public: __cdecl UnsignedLongLongTemplate<-1>::UnsignedLongLongTemplate<-1>(void)");
		}
		[Test]
		public void Template59() {
			AssertMangling("??$foo@H@space@@YAABHABH@Z", "int const & __cdecl space::foo<int>(int const &)");
		}
		[Test]
		public void Template60() {
			AssertMangling("??$foo@H@space@@YAAEBHAEBH@Z", "int const & __cdecl space::foo<int>(int const &)");
		}
		[Test]
		public void Template61() {
			AssertMangling("??$FunctionPointerTemplate@$1?spam@@YAXXZ@@YAXXZ", "void __cdecl FunctionPointerTemplate<&void __cdecl spam(void)>(void)");
		}
		[Test]
		public void Template62() {
			AssertMangling("??$variadic_fn_template@HHHH@@YAXABH000@Z", "void __cdecl variadic_fn_template<int, int, int, int>(int const &, int const &, int const &, int const &)");
		}
		[Test]
		public void Template63() {
			AssertMangling("??$variadic_fn_template@HHD$$BY01D@@YAXABH0ABDAAY01$$CBD@Z", "void __cdecl variadic_fn_template<int, int, char, char[2]>(int const &, int const &, char const &, char const (&)[2])");
		}
		[Test]
		public void Template64() {
			AssertMangling("??0?$VariadicClass@HD_N@@QAE@XZ", "public: __thiscall VariadicClass<int, char, bool>::VariadicClass<int, char, bool>(void)");
		}
		[Test]
		public void Template65() {
			AssertMangling("??0?$VariadicClass@_NDH@@QAE@XZ", "public: __thiscall VariadicClass<bool, char, int>::VariadicClass<bool, char, int>(void)");
		}
		[Test]
		public void Template66() {
			AssertMangling("?template_template_fun@@YAXU?$Type@U?$Thing@USecond@@$00@@USecond@@@@@Z", "void __cdecl template_template_fun(struct Type<struct Thing<struct Second, 1>, struct Second>)");
		}
		[Test]
		public void Template67() {
			AssertMangling("??$template_template_specialization@$$A6AXU?$Type@U?$Thing@USecond@@$00@@USecond@@@@@Z@@YAXXZ", "void __cdecl template_template_specialization<void __cdecl(struct Type<struct Thing<struct Second, 1>, struct Second>)>(void)");
		}
		[Test]
		public void Template68() {
			AssertMangling("?f@@YAXU?$S1@$0A@@@@Z", "void __cdecl f(struct S1<0>)");
		}
		[Test]
		public void Template69() {
			AssertMangling("?recref@@YAXU?$type1@$E?inst@@3Urecord@@B@@@Z", "void __cdecl recref(struct type1<struct record const inst>)");
		}
		[Test]
		public void Template70() {
			AssertMangling("?fun@@YAXU?$UUIDType1@Uuuid@@$1?_GUID_12345678_1234_1234_1234_1234567890ab@@3U__s_GUID@@B@@@Z", "void __cdecl fun(struct UUIDType1<struct uuid, &struct __s_GUID const _GUID_12345678_1234_1234_1234_1234567890ab>)");
		}
		[Test]
		public void Template71() {
			AssertMangling("?fun@@YAXU?$UUIDType2@Uuuid@@$E?_GUID_12345678_1234_1234_1234_1234567890ab@@3U__s_GUID@@B@@@Z", "void __cdecl fun(struct UUIDType2<struct uuid, struct __s_GUID const _GUID_12345678_1234_1234_1234_1234567890ab>)");
		}
		[Test]
		public void Template72() {
			AssertMangling("?FunctionDefinedWithInjectedName@@YAXU?$TypeWithFriendDefinition@H@@@Z", "void __cdecl FunctionDefinedWithInjectedName(struct TypeWithFriendDefinition<int>)");
		}
		[Test]
		public void Template73() {
			AssertMangling("?bar@?$UUIDType4@$1?_GUID_12345678_1234_1234_1234_1234567890ab@@3U__s_GUID@@B@@QAEXXZ", "public: void __thiscall UUIDType4<&struct __s_GUID const _GUID_12345678_1234_1234_1234_1234567890ab>::bar(void)");
		}
		[Test]
		public void Template74() {
			AssertMangling("??$f@US@@$1?g@1@QEAAXXZ@@YAXXZ", "void __cdecl f<struct S, &public: void __cdecl S::g(void)>(void)");
		}
		[Test]
		public void Template75() {
			AssertMangling("??$?0N@?$Foo@H@@QEAA@N@Z", "public: __cdecl Foo<int>::Foo<int><double>(double)");
		}
		#endregion
		
		#region TemplatesMemptrs
		[Test]
		public void TemplateMemptr1() {
			AssertMangling("??$CallMethod@UC@NegativeNVOffset@@$I??_912@$BA@AEPPPPPPPM@A@@@YAXAAUC@NegativeNVOffset@@@Z", "void __cdecl CallMethod<struct NegativeNVOffset::C, {[thunk]: __thiscall NegativeNVOffset::C::`vcall\'{0, {flat}}, 4294967292, 0}>(struct NegativeNVOffset::C &)");
		}
		[Test]
		public void TemplateMemptr2() {
			AssertMangling("??$CallMethod@UM@@$0A@@@YAXAAUM@@@Z", "void __cdecl CallMethod<struct M, 0>(struct M &)");
		}
		[Test]
		public void TemplateMemptr3() {
			AssertMangling("??$CallMethod@UM@@$H??_91@$BA@AEA@@@YAXAAUM@@@Z", "void __cdecl CallMethod<struct M, {[thunk]: __thiscall M::`vcall\'{0, {flat}}, 0}>(struct M &)");
		}
		[Test]
		public void TemplateMemptr4() {
			AssertMangling("??$CallMethod@UM@@$H?f@1@QAEXXZA@@@YAXAAUM@@@Z", "void __cdecl CallMethod<struct M, {public: void __thiscall M::f(void), 0}>(struct M &)");
		}
		[Test]
		public void TemplateMemptr5() {
			AssertMangling("??$CallMethod@UO@@$H??_91@$BA@AE3@@YAXAAUO@@@Z", "void __cdecl CallMethod<struct O, {[thunk]: __thiscall O::`vcall\'{0, {flat}}, 4}>(struct O &)");
		}
		[Test]
		public void TemplateMemptr6() {
			AssertMangling("??$CallMethod@US@@$0A@@@YAXAAUS@@@Z", "void __cdecl CallMethod<struct S, 0>(struct S &)");
		}
		[Test]
		public void TemplateMemptr7() {
			AssertMangling("??$CallMethod@US@@$1??_91@$BA@AE@@YAXAAUS@@@Z", "void __cdecl CallMethod<struct S, &[thunk]: __thiscall S::`vcall\'{0, {flat}}>(struct S &)");
		}
		[Test]
		public void TemplateMemptr8() {
			AssertMangling("??$CallMethod@US@@$1?f@1@QAEXXZ@@YAXAAUS@@@Z", "void __cdecl CallMethod<struct S, &public: void __thiscall S::f(void)>(struct S &)");
		}
		[Test]
		public void TemplateMemptr9() {
			AssertMangling("??$CallMethod@UU@@$0A@@@YAXAAUU@@@Z", "void __cdecl CallMethod<struct U, 0>(struct U &)");
		}
		[Test]
		public void TemplateMemptr10() {
			AssertMangling("??$CallMethod@UU@@$J??_91@$BA@AEA@A@A@@@YAXAAUU@@@Z", "void __cdecl CallMethod<struct U, {[thunk]: __thiscall U::`vcall\'{0, {flat}}, 0, 0, 0}>(struct U &)");
		}
		[Test]
		public void TemplateMemptr11() {
			AssertMangling("??$CallMethod@UU@@$J?f@1@QAEXXZA@A@A@@@YAXAAUU@@@Z", "void __cdecl CallMethod<struct U, {public: void __thiscall U::f(void), 0, 0, 0}>(struct U &)");
		}
		[Test]
		public void TemplateMemptr12() {
			AssertMangling("??$CallMethod@UV@@$0A@@@YAXAAUV@@@Z", "void __cdecl CallMethod<struct V, 0>(struct V &)");
		}
		[Test]
		public void TemplateMemptr13() {
			AssertMangling("??$CallMethod@UV@@$I??_91@$BA@AEA@A@@@YAXAAUV@@@Z", "void __cdecl CallMethod<struct V, {[thunk]: __thiscall V::`vcall\'{0, {flat}}, 0, 0}>(struct V &)");
		}
		[Test]
		public void TemplateMemptr14() {
			AssertMangling("??$CallMethod@UV@@$I?f@1@QAEXXZA@A@@@YAXAAUV@@@Z", "void __cdecl CallMethod<struct V, {public: void __thiscall V::f(void), 0, 0}>(struct V &)");
		}
		[Test]
		public void TemplateMemptr15() {
			AssertMangling("??$ReadField@UA@@$0?0@@YAHAAUA@@@Z", "int __cdecl ReadField<struct A, -1>(struct A &)");
		}
		[Test]
		public void TemplateMemptr16() {
			AssertMangling("??$ReadField@UA@@$0A@@@YAHAAUA@@@Z", "int __cdecl ReadField<struct A, 0>(struct A &)");
		}
		[Test]
		public void TemplateMemptr17() {
			AssertMangling("??$ReadField@UI@@$03@@YAHAAUI@@@Z", "int __cdecl ReadField<struct I, 4>(struct I &)");
		}
		[Test]
		public void TemplateMemptr18() {
			AssertMangling("??$ReadField@UI@@$0A@@@YAHAAUI@@@Z", "int __cdecl ReadField<struct I, 0>(struct I &)");
		}
		[Test]
		public void TemplateMemptr19() {
			AssertMangling("??$ReadField@UM@@$0A@@@YAHAAUM@@@Z", "int __cdecl ReadField<struct M, 0>(struct M &)");
		}
		[Test]
		public void TemplateMemptr20() {
			AssertMangling("??$ReadField@UM@@$0BA@@@YAHAAUM@@@Z", "int __cdecl ReadField<struct M, 16>(struct M &)");
		}
		[Test]
		public void TemplateMemptr21() {
			AssertMangling("??$ReadField@UM@@$0M@@@YAHAAUM@@@Z", "int __cdecl ReadField<struct M, 12>(struct M &)");
		}
		[Test]
		public void TemplateMemptr22() {
			AssertMangling("??$ReadField@US@@$03@@YAHAAUS@@@Z", "int __cdecl ReadField<struct S, 4>(struct S &)");
		}
		[Test]
		public void TemplateMemptr23() {
			AssertMangling("??$ReadField@US@@$07@@YAHAAUS@@@Z", "int __cdecl ReadField<struct S, 8>(struct S &)");
		}
		[Test]
		public void TemplateMemptr24() {
			AssertMangling("??$ReadField@US@@$0A@@@YAHAAUS@@@Z", "int __cdecl ReadField<struct S, 0>(struct S &)");
		}
		[Test]
		public void TemplateMemptr25() {
			AssertMangling("??$ReadField@UU@@$0A@@@YAHAAUU@@@Z", "int __cdecl ReadField<struct U, 0>(struct U &)");
		}
		[Test]
		public void TemplateMemptr26() {
			AssertMangling("??$ReadField@UU@@$G3A@A@@@YAHAAUU@@@Z", "int __cdecl ReadField<struct U, {4, 0, 0}>(struct U &)");
		}
		[Test]
		public void TemplateMemptr27() {
			AssertMangling("??$ReadField@UU@@$G7A@A@@@YAHAAUU@@@Z", "int __cdecl ReadField<struct U, {8, 0, 0}>(struct U &)");
		}
		[Test]
		public void TemplateMemptr28() {
			AssertMangling("??$ReadField@UV@@$0A@@@YAHAAUV@@@Z", "int __cdecl ReadField<struct V, 0>(struct V &)");
		}
		[Test]
		public void TemplateMemptr29() {
			AssertMangling("??$ReadField@UV@@$F7A@@@YAHAAUV@@@Z", "int __cdecl ReadField<struct V, {8, 0}>(struct V &)");
		}
		[Test]
		public void TemplateMemptr30() {
			AssertMangling("??$ReadField@UV@@$FM@A@@@YAHAAUV@@@Z", "int __cdecl ReadField<struct V, {12, 0}>(struct V &)");
		}
		[Test]
		public void TemplateMemptr31() {
			AssertMangling("?Q@@3$$QEAP8Foo@@EAAXXZEA", "void (__cdecl Foo::*&&Q)(void)");
		}
		#endregion

		#region TemplateMemptrs2
		[Test]
		public void TemplateMemptr_2_1() {
			AssertMangling("?m@@3U?$J@UM@@$0A@@@A", "struct J<struct M, 0> m");
		}
		[Test]
		public void TemplateMemptr_2_2() {
			AssertMangling("?m2@@3U?$K@UM@@$0?0@@A", "struct K<struct M, -1> m2");
		}
		[Test]
		public void TemplateMemptr_2_3() {
			AssertMangling("?n@@3U?$J@UN@@$HA@@@A", "struct J<struct N, {0}> n");
		}
		[Test]
		public void TemplateMemptr_2_4() {
			AssertMangling("?n2@@3U?$K@UN@@$0?0@@A", "struct K<struct N, -1> n2");
		}
		[Test]
		public void TemplateMemptr_2_5() {
			AssertMangling("?o@@3U?$J@UO@@$IA@A@@@A", "struct J<struct O, {0, 0}> o");
		}
		[Test]
		public void TemplateMemptr_2_6() {
			AssertMangling("?o2@@3U?$K@UO@@$FA@?0@@A", "struct K<struct O, {0, -1}> o2");
		}
		[Test]
		public void TemplateMemptr_2_7() {
			AssertMangling("?p@@3U?$J@UP@@$JA@A@?0@@A", "struct J<struct P, {0, 0, -1}> p");
		}
		[Test]
		public void TemplateMemptr_2_8() {
			AssertMangling("?p2@@3U?$K@UP@@$GA@A@?0@@A", "struct K<struct P, {0, 0, -1}> p2");
		}
		[Test]
		public void TemplateMemptr_2_9() {
			AssertMangling("??0?$ClassTemplate@$J??_9MostGeneral@@$BA@AEA@M@3@@QAE@XZ", "public: __thiscall ClassTemplate<{[thunk]: __thiscall MostGeneral::`vcall\'{0, {flat}}, 0, 12, 4}>::ClassTemplate<{[thunk]: __thiscall MostGeneral::`vcall\'{0, {flat}}, 0, 12, 4}>(void)");
		}
		#endregion

		#region Thunks
		[Test]
		public void Thunk1() {
			AssertMangling("?f@C@@WBA@EAAHXZ", "[thunk]: public: virtual int __cdecl C::f`adjustor{16}\'(void)");
		}
		[Test]
		public void Thunk2() {
			AssertMangling("??_EDerived@@$4PPPPPPPM@A@EAAPEAXI@Z", "[thunk]: public: virtual void * __cdecl Derived::`vector deleting dtor\'`vtordisp{-4, 0}\'(unsigned int)");
		}
		[Test]
		public void Thunk3() {
			AssertMangling("?f@A@simple@@$R477PPPPPPPM@7AEXXZ", "[thunk]: public: virtual void __thiscall simple::A::f`vtordispex{8, 8, -4, 8}\'(void)");
		}
		[Test]
		public void Thunk4() {
			AssertMangling("??_9Base@@$B7AA", "[thunk]: __cdecl Base::`vcall\'{8, {flat}}");
		}

		#endregion
		
		#region Windows
		[Test]
		public void Windows1() {
			AssertMangling("?bar@Foo@@SGXXZ", "public: static void __stdcall Foo::bar(void)");
		}
		[Test]
		public void Windows2() {
			AssertMangling("?bar@Foo@@QAGXXZ", "public: void __stdcall Foo::bar(void)");
		}
		[Test]
		public void Windows3() {
			AssertMangling("?f2@@YIXXZ", "void __fastcall f2(void)");
		}
		[Test]
		public void Windows4() {
			AssertMangling("?f1@@YGXXZ", "void __stdcall f1(void)");
		}
		#endregion
	}
}