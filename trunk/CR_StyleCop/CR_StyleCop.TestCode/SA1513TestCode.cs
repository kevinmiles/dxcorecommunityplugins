namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1513 rule - closing curly bracket must be followed by blank line.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "This is about SA1513 rule.")]
    public class SA1513TestCode
    {
        private object syncRoot = new object();

        private EventHandler eventName;

        private event EventHandler EventName
        {
            add
            {
                this.eventName = (EventHandler)Delegate.Combine(this.eventName, value);
            }
            remove
            {
                this.eventName = (EventHandler)Delegate.Remove(this.eventName, value);
            }
        }
        private object SyncRoot
        {
            get
            {
                return this.syncRoot;
            }
            set
            {
                this.syncRoot = value;
            }
        }
        private void MethodName(string x)
        {
            if (string.IsNullOrEmpty(x))
            {
                throw new ArgumentException("x is null or empty.", " x");
            }
            while (string.IsNullOrEmpty(x))
            {
                x = x.Substring(1);
            }
            lock (this.syncRoot)
            {
                x = x.Substring(1);
            }
            for (int i = 0; i < 5; i++)
            {
                x = x.Substring(1);
            }
            foreach (int s in new int[] { 1, 2 })
            {
                x = x.Substring(1);
            }
            using (new System.IO.StreamReader("aaa"))
            {
                x = x.Substring(1);
            }
            do
            {
                x = x.Substring(1);
            }
            while (string.IsNullOrEmpty(x));
        }
        private void Anothermethod()
        {
        }
    }
}
