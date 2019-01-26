using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Microsoft
{
	public enum SpecialIntrinsicKind
	{
		None,
		Vftable,
		Vbtable,
		Typeof,
		VcallThunk,
		LocalStaticGuard,
		StringLiteralSymbol,
		UdtReturning,
		Unknown,
		DynamicInitializer,
		DynamicAtexitDestructor,
		RttiTypeDescriptor,
		RttiBaseClassDescriptor,
		RttiBaseClassArray,
		RttiClassHierarchyDescriptor,
		RttiCompleteObjLocator,
		LocalVftable,
		LocalStaticThreadGuard
	}
}
