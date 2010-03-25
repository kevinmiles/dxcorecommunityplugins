using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.Core;
using DevExpress.Refactor.Core;
using DevExpress.CodeRush.StructuralParser;

namespace CR_ExtractHqlNamedQuery
{
  public class LinkedTextHelper
  {
    /// <summary>
    /// Main method to link the source ranges and start refactoring
    /// </summary>
    /// <param name="context"></param>
    /// <param name="ranges"></param>
    public static void ApplyRename(RefactoringContext context, FileSourceRangeCollection ranges)
    {
      if ((ranges != null) && (ranges.Count != 0))
      {
        LinkedTextHelper.breakLinks(context, ranges);
        LinkedTextHelper.setUpLinks(context, ranges);
      }
    }

    /// <summary>
    /// Breaks preexisting links between the source ranges passed as parameters
    /// </summary>
    /// <param name="context"></param>
    /// <param name="ranges"></param>
    private static void breakLinks(RefactoringContext context, FileSourceRangeCollection ranges)
    {
      if ((ranges != null) && (ranges.Count != 0))
      {
        TextDocument document = context.Document;
        foreach (SourceRange range in getDocumentRanges(document, ranges))
        {
          foreach (ILinkedIdentifier identifier in CodeRush.LinkedIdentifiers.Find(document, range))
          {
            ILinkedIdentifierList list = identifier.List;
            if (list.MultiDocumentContainer != null)
            {
              list.MultiDocumentContainer.BreakAllLinks();
            }
          }
          CodeRush.LinkedIdentifiers.BreakAllLinksInRange(document, range);
        }
      }
    }

    /// <summary>
    /// Returns a collection of source ranges that belongs to the document passed as parameter
    /// </summary>
    /// <param name="document"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    private static SourceRangeCollection getDocumentRanges(TextDocument document, FileSourceRangeCollection ranges)
    {
      SourceRangeCollection ranges2 = new SourceRangeCollection(document);
      if ((document != null) && (ranges != null))
      {
          foreach (FileSourceRange range in ranges)
          {
              TextDocument textDocument = getTextDocument(range.File);
              if ((textDocument == null) || (textDocument != document))
              {
                  continue;
              }
              ranges2.Add(range.Range);
          }
      }
      return ranges2;
    }

    /// <summary>
    /// Returns the text document associated with the SourceFile passed in fileNode parameter
    /// </summary>
    /// <param name="fileNode"></param>
    /// <returns></returns>
    private static TextDocument getTextDocument(SourceFile fileNode)
    {
      if (fileNode == null)
      {
        return null;
      }
      IDocument document = fileNode.Document;
      if (document == null)
      {
        return getTextDocument(fileNode);
      }
      return (document as TextDocument);
    }

    /// <summary>
    /// Indicates if all the ranges passed as parameter belongs to the same document
    /// </summary>
    /// <param name="document"></param>
    /// <param name="ranges"></param>
    /// <returns></returns>
    private static bool isOneDocumentLidList(TextDocument document, FileSourceRangeCollection ranges)
    {
      if ((ranges == null) || (document == null))
      {
        return false;
      }
      string strA = document.FullName;
      int count = ranges.Count;
      for (int i = 0; i < count; i++)
      {
        SourceFile file = ranges[i].File;
        if (strA == null)
        {
          strA = file.Name;
        }
        else if (string.Compare(strA, file.Name, true) != 0)
        {
          return false;
        }
      }
      return true;
    }

    /// <summary>
    /// Creates links between all the ranges passed as parameter
    /// </summary>
    /// <param name="context"></param>
    /// <param name="ranges"></param>
    private static void setUpLinks(RefactoringContext context, FileSourceRangeCollection ranges)
    {
      if (LinkedTextHelper.isOneDocumentLidList(context.Document, ranges))
      {
        LinkedTextHelper.setUpLinksForDocument(context.Document, ranges);
      }
      else
      {
        CodeRush.LinkedIdentifiers.NewMultiDocumentContainer().AddRange(ranges);
      }

      CodeRush.LinkedIdentifiers.Invalidate(context.Document);
    }

    /// <summary>
    /// Links ranges in the document passed as parameter
    /// </summary>
    /// <param name="document"></param>
    /// <param name="ranges"></param>
    private static void setUpLinksForDocument(TextDocument document, FileSourceRangeCollection ranges)
    {
      if ((ranges != null) && (document != null))
      {
        ILinkedIdentifierList list = CodeRush.LinkedIdentifiers.GetStorage(document).NewList();
        int count = ranges.Count;
        for (int i = 0; i < count; i++)
        {
          FileSourceRange range = ranges[i];
          ILinkedIdentifier identifier = list.Add(range.Range);
          if (identifier != null)
          {
            identifier.Affix = range.Data as LinkedIdentifierAffix;
          }
        }
      }
    }

  }
}
