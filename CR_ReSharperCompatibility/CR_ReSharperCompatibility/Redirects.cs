using System;
using DevExpress.CodeRush.Core;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CR_ReSharperCompatibility
{
  public class Redirects : List<CompatibilityRedirect>
  {
    private const string STR_ThisRefactoringWasNotFound = "This refactoring was not found.";  // Translate
    
    private const string STR_RefactorCommand = "Refactor";

    private void AddCommand(string command, string parameters, string displayName, string description, string availabilityHint, bool isAvailable)
    {
      Add(new CompatibilityRedirect(displayName,
                                    command,
                                    parameters,
                                    description,
                                    GetShortcut(command, parameters),
                                    availabilityHint,
                                    isAvailable));
    }
    // public methods...
    #region AddRefactoring
    public void AddRefactoring(string refactoringName, string availabilityHint)
    {
      RefactoringProviderBase refactoring = CodeRush.Refactoring.Get(refactoringName);
      if (refactoring == null)
        Add(new CompatibilityRedirect(refactoringName,
                                              STR_RefactorCommand,
                                              refactoringName,
                                              String.Empty,
                                              STR_ThisRefactoringWasNotFound,
                                              false));
      else
        AddCommand(STR_RefactorCommand, refactoring.ProviderName, refactoring.DisplayName, refactoring.Description, availabilityHint, CodeRush.Refactoring.IsAvailable(refactoringName));
    }
    #endregion

    private static string GetShortcut(string commandName, string embeddingName)
    {
      if (CodeRush.Key.HasEnabledShortcut(commandName, embeddingName))
      {
        ICommandKeyBinding iCommandKeyBinding;

        NameValueCollection allBindings = CodeRush.Key.GetAllBindings(String.Format("{1}({0})", embeddingName, "Embed"));
        if (allBindings != null && allBindings.Count > 0)
          return allBindings[0];
      }
      return String.Empty;
    }
    #region AddSelectionEmbedding
    public void AddSelectionEmbedding(string embeddingName, string description)
    {
      AddCommand("Embed", embeddingName, embeddingName, description, String.Empty, true);
    }
    #endregion
    #region AddOptionsPage
    public void AddOptionsPage(string buttonText, string pageName, string description)
    {
      //if (CodeRush.Key.HasEnabledShortcut("Embed", embeddingName))
      //{
      //  
      //}
      Add(new CompatibilityRedirect(buttonText,
                                    "Options",
                                    pageName,
                                    description,
                                    String.Empty, String.Empty,
                                    true));
    }
    #endregion
  }
}
