// <copyright file="SA1123TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

#pragma warning disable 162

    /// <summary>
    /// Test code for SA1123 rule - do not use regions within code elements.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:DoNotUseRegions", Justification = "This is test code SA1123 rule.")]
    public class SA1123TestCode
    {
        private string propertyName;
        private EventHandler eventName;

        private SA1123TestCode(string propertyName)
        {
            #region ctor
            this.propertyName = propertyName;
            #endregion
        }

        private event EventHandler EventName
        {
            add
            {
                #region add
                this.eventName = (EventHandler)Delegate.Combine(this.eventName, value);
                #endregion
            }

            remove
            {
                #region remove
                this.eventName = (EventHandler)Delegate.Remove(this.eventName, value);
                #endregion
            }
        }

        private string PropertyName
        {
            get
            {
                #region getter
                return this.propertyName;
                #endregion
            }

            set
            {
                #region setter
                this.propertyName = value;
                #endregion
            }
        }

        private void MethodName()
        {
            #region method
            if (true)
            {
                #region if
                return;
                #endregion
            }

            while (true)
            {
                #region while
                return;
                #endregion
            }

            lock (this)
            {
                #region lock
                return;
                #endregion
            }

            for (int i = 0; i < 10; i++)
            {
                #region for
                return;
                #endregion
            }

            foreach (int item in Enumerable.Empty<int>())
            {
                #region foreach
                return;
                #endregion
            }

            switch ("foo")
            {
                #region switch
                case "foo":
                    #region case
                    return;
                    #endregion
                default:
                    #region default
                    return;
                    #endregion
                #endregion
            }
            #endregion
        }
    }
}
