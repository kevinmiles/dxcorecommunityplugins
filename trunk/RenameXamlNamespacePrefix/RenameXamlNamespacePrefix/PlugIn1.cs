using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Refactor.Core;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;

namespace RenameXamlNamespacePrefix
{
  using DevExpress.CodeRush.StructuralParser;

  public partial class PlugIn1 : StandardPlugIn
  {
    // DXCore-generated code...
    #region InitializePlugIn
    public override void InitializePlugIn()
    {
      base.InitializePlugIn();

      //
      // TODO: Add your initialization code here.
      //
    }
    #endregion
    #region FinalizePlugIn
    public override void FinalizePlugIn()
    {
      //
      // TODO: Add your finalization code here.
      //

      base.FinalizePlugIn();
    }
    #endregion

    
    void rpRenameXamlNamespacePrefix_LanguageSupported(LanguageSupportedEventArgs ea)
    {
      ea.Handled = CodeRush.Language.IsXAMLLanguage(ea.LanguageID);
    }

    void rpRenameXamlNamespacePrefix_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
    {
			ea.Available = TagPrefix.AtCaret(ea.Element) != null;
    }

		private static IEnumerable<TagPrefix> GetTagPrefixes(LanguageElement element)
		{
			return TagPrefix.FindMatching(element.FileNode, TagPrefix.AtCaret(element));
		}

		void rpRenameXamlNamespacePrefix_Apply(object sender, ApplyContentEventArgs ea)
    {
      IEnumerable<TagPrefix> tagPrefixes = GetTagPrefixes(ea.Element);

			ILinkedIdentifierList linkedIdentifiers = CodeRush.LinkedIdentifiers.ActiveStorage.NewList();
			foreach (TagPrefix tp in tagPrefixes)
        linkedIdentifiers.Add(tp.Range);

      CodeRush.LinkedIdentifiers.ActiveStorage.Invalidate();
    }

		private void rpRenameXamlNamespacePrefix_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
		{
			IEnumerable<TagPrefix> tagPrefixes = GetTagPrefixes(ea.Element);
			foreach (TagPrefix tagPrefix in tagPrefixes)
				ea.AddBrushStrokeHighlighter(tagPrefix.Range, RefactorColors.ChangeCode);
		}
  }
}