namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;
    
    internal interface ICodeIssueLocator
    {
        IEnumerable<StyleCopCodeIssue> GetCodeIssues(ISourceCode sourceCode, Func<ElementTypeFilter, IEnumerable<IElement>> enumerate, Violation violation, CsElement csElement);
    }
}
