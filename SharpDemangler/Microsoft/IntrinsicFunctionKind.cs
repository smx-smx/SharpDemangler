using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDemangler.Microsoft
{
	public enum IntrinsicFunctionKind
	{
		None,
		New,                        // ?2 # operator new
		Delete,                     // ?3 # operator delete
		Assign,                     // ?4 # operator=
		RightShift,                 // ?5 # operator>>
		LeftShift,                  // ?6 # operator<<
		LogicalNot,                 // ?7 # operator!
		Equals,                     // ?8 # operator==
		NotEquals,                  // ?9 # operator!=
		ArraySubscript,             // ?A # operator[]
		Pointer,                    // ?C # operator->
		Dereference,                // ?D # operator*
		Increment,                  // ?E # operator++
		Decrement,                  // ?F # operator--
		Minus,                      // ?G # operator-
		Plus,                       // ?H # operator+
		BitwiseAnd,                 // ?I # operator&
		MemberPointer,              // ?J # operator->*
		Divide,                     // ?K # operator/
		Modulus,                    // ?L # operator%
		LessThan,                   // ?M operator<
		LessThanEqual,              // ?N operator<=
		GreaterThan,                // ?O operator>
		GreaterThanEqual,           // ?P operator>=
		Comma,                      // ?Q operator,
		Parens,                     // ?R operator()
		BitwiseNot,                 // ?S operator~
		BitwiseXor,                 // ?T operator^
		BitwiseOr,                  // ?U operator|
		LogicalAnd,                 // ?V operator&&
		LogicalOr,                  // ?W operator||
		TimesEqual,                 // ?X operator*=
		PlusEqual,                  // ?Y operator+=
		MinusEqual,                 // ?Z operator-=
		DivEqual,                   // ?_0 operator/=
		ModEqual,                   // ?_1 operator%=
		RshEqual,                   // ?_2 operator>>=
		LshEqual,                   // ?_3 operator<<=
		BitwiseAndEqual,            // ?_4 operator&=
		BitwiseOrEqual,             // ?_5 operator|=
		BitwiseXorEqual,            // ?_6 operator^=
		VbaseDtor,                  // ?_D # vbase destructor
		VecDelDtor,                 // ?_E # vector deleting destructor
		DefaultCtorClosure,         // ?_F # default constructor closure
		ScalarDelDtor,              // ?_G # scalar deleting destructor
		VecCtorIter,                // ?_H # vector constructor iterator
		VecDtorIter,                // ?_I # vector destructor iterator
		VecVbaseCtorIter,           // ?_J # vector vbase constructor iterator
		VdispMap,                   // ?_K # virtual displacement map
		EHVecCtorIter,              // ?_L # eh vector constructor iterator
		EHVecDtorIter,              // ?_M # eh vector destructor iterator
		EHVecVbaseCtorIter,         // ?_N # eh vector vbase constructor iterator
		CopyCtorClosure,            // ?_O # copy constructor closure
		LocalVftableCtorClosure,    // ?_T # local vftable constructor closure
		ArrayNew,                   // ?_U operator new[]
		ArrayDelete,                // ?_V operator delete[]
		ManVectorCtorIter,          // ?__A managed vector ctor iterator
		ManVectorDtorIter,          // ?__B managed vector dtor iterator
		EHVectorCopyCtorIter,       // ?__C EH vector copy ctor iterator
		EHVectorVbaseCopyCtorIter,  // ?__D EH vector vbase copy ctor iterator
		VectorCopyCtorIter,         // ?__G vector copy constructor iterator
		VectorVbaseCopyCtorIter,    // ?__H vector vbase copy constructor iterator
		ManVectorVbaseCopyCtorIter, // ?__I managed vector vbase copy constructor
		CoAwait,                    // ?__L co_await
		Spaceship,                  // operator<=>
		MaxIntrinsic
	}
}
