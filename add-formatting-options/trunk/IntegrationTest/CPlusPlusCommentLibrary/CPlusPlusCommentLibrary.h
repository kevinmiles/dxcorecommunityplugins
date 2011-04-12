// CPlusPlusCommentLibrary.h

#pragma once

using namespace System;

namespace CPlusPlusCommentLibrary {

	public ref class Class1
	{
		// This single line comment should not be picked up.

		// This set of single line comments should be
		// allowed to be converted to a single
		// multiline comment.

		// This set of single line comments
		//
		// has line breaks but should still be properly
		//
		// converted to multiline.

		// This set of single line comments...
		// * Has a leading asterisk that should remain.
		//     Has five leading spaces that should remain.
		//Is missing the leading space and that should be OK.

		/* This multiline comment should be convertible to single line. */

		/* This multiline comment
		   should be convertible
		   to a set of single-line comments */

		/* This multiline comment
		 * has the asterisk prefix
		 * and that prefix should
		 * be removed. The asterisks
		 * were manually added, not there
		 * by default in the editor.
		 */
	};
}
