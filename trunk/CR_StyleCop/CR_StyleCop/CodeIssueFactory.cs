namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CR_StyleCop.CodeIssues;
    using StyleCop;

    internal class CodeIssueFactory
    {
        private readonly Dictionary<string, IStyleCopRule> handlers = new Dictionary<string, IStyleCopRule>();
        private readonly IStyleCopRule emptyRule = new EmptyStyleCopRule();

        public CodeIssueFactory()
        {
            var ruleType = typeof(IStyleCopRule);
            foreach (var type in ruleType.Assembly.GetTypes().Where(type => !type.IsAbstract && ruleType.IsAssignableFrom(type)))
            {
                var rule = (IStyleCopRule)Activator.CreateInstance(type);
                this.handlers.Add(type.Name.Substring(0, 6), rule);
            }
        }

        public IStyleCopRule GetRuleFor(Violation violation)
        {
            IStyleCopRule rule = null;
            this.handlers.TryGetValue(violation.Rule.CheckId, out rule);
            return rule ?? this.emptyRule;
        }

        private class EmptyStyleCopRule : StyleCopRule
        {
            public EmptyStyleCopRule()
                : base(new NullIssueLocator())
            {
            }
        }
    }
}
