// <copyright file="SA1213TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1213 rule - add must come before remove.
    /// </summary>
    public class SA1213TestCode
    {
        private EventHandler validEvent;
        private EventHandler invalidEvent;

        private event EventHandler ValidEvent
        {
            add { this.validEvent = (EventHandler)Delegate.Combine(this.validEvent, value); }
            remove { this.validEvent = (EventHandler)Delegate.Remove(this.validEvent, value); }
        }

        private event EventHandler InvalidEvent
        {
            remove { this.invalidEvent = (EventHandler)Delegate.Remove(this.invalidEvent, value); }
            add { this.invalidEvent = (EventHandler)Delegate.Combine(this.invalidEvent, value); }
        }
    }
}
