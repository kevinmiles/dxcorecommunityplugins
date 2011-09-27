using System;
using System.Collections.Generic;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;

namespace CR_Dispos_o_matic
{
  public class FieldShouldBeDisposedSearcher : BaseCodeIssueSearcher
  {
    public override void CheckCodeIssues(CheckCodeIssuesEventArgs ea)
    {
      IEnumerable<IElement> enumerable = ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.Class));
      foreach (IElement element in enumerable)
      {
        IClassElement iClassElement = element as IClassElement;
        if (iClassElement == null)
          continue;
        if (!PlugIn1.AlreadyImplementsIDisposable(iClassElement))
          continue;
        // We DO implement IDisposable! Let's make sure all the fields are disposed....
        IIfStatement parentIfDisposing;
        IList<IFieldElement> disposableFields = PlugIn1.GetDisposableFieldsThatHaveNotBeenDisposed(ea.ResolveScope(), ea.Scope as ISourceFile, iClassElement, out parentIfDisposing);
        if (disposableFields.Count > 0)
          foreach (IFieldElement disposableField in disposableFields)
            ea.AddWarning(disposableField.FirstNameRange, "Fields should be disposed");
      }
    }
  }
}
