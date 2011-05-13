// <copyright file="SA1006TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;

#pragma warning disable 1030

#    if NOTDEFINED
#    error error
#  else
#   warning warning
#   endif
# line 200
    # region X
    #  endregion

#if NOTDEFINED
#error error
#endif
#warning warning
}
