using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Collections;
using System.Xml.Linq;
using System.Xml;
using DevExpress.CodeRush.Core.Replacement;
using DevExpress.Refactor.Core;

using DXXmlElement = DevExpress.CodeRush.StructuralParser.XmlElement;
using System.Collections.Generic;

namespace CR_ExtractHqlNamedQuery
{
  public partial class ExtractHqlNamedQuery : StandardPlugIn
  {
    // DXCore-generated code...
    #region InitializePlugIn
    public override void InitializePlugIn()
    {
      base.InitializePlugIn();

      loadSettings();
    }

    private bool enabled;
    private int findHqlFileStrategy;
    private string hqlNamedQueryFileName;

    private void loadSettings()
    {
      try
      {
        using (DecoupledStorage storage = OptExtractHqlNamedQuery.Storage)
        {
          enabled = storage.ReadBoolean("ExtractHqlNamedQuery", "Enabled", true);
          findHqlFileStrategy = storage.ReadInt32("ExtractHqlNamedQuery", "FindHqlFileStrategy", 0);
          hqlNamedQueryFileName = storage.ReadString("ExtractHqlNamedQuery", "HqlNamedQueryFileName", "NamedQueries.hbm.xml");
        }
      }
      catch (Exception ex)
      {
        ShowException(ex);
      }
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

    private static bool actionIsAvailable(LanguageElement element)
    {
      return (element != null) &&
              (element.ElementType == LanguageElementType.PrimitiveExpression) &&
              (element as IPrimitiveExpression).IsStringLiteral;
    }

    private void refactoringProvider1_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
    {
      if (enabled)
        ea.Available = actionIsAvailable(ea.Element);
    }

    private void refactoringProvider1_Apply(object sender, ApplyContentEventArgs ea)
    {
      if (enabled)
      {
        try
        {
          if (actionIsAvailable(ea.Element))
          {
            FileSourceRangeCollection collection = extractHqlNamedQuery(ea.Element);

            if (collection != null)
            {
              RefactoringContext context = new RefactoringContext(ea);
              LinkedTextHelper.ApplyRename(context, collection);
            }
          }
        }
        catch (Exception ex)
        {
          ShowException(ex);
        }
      }
    }

    public static void ShowException(Exception ex)
    {
      MessageBox.Show(ex.ToString(), "Error in CR_ExtractHqlNamedQuery plugin", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private static void insertXmlContentInNamedQueriesFile(SourceFile namedQueriesXmlFile, string xmlContent)
    {
      TextDocument namedQueriesXmlDocument = namedQueriesXmlFile.Document as TextDocument;

      DXXmlElement a = (namedQueriesXmlFile.FindChildByName("hibernate-mapping") as DXXmlElement);
      int insertLine = a.LastChild.Range.End.Line + 1;
      namedQueriesXmlDocument.InsertLines(insertLine, new string[1] { xmlContent });
      namedQueriesXmlDocument.FormatLine(insertLine);
    }

    private SourceFile getNamedQueriesXmlFile(ProjectElement currentProject)
    {
      SourceFile result = null;

      switch (findHqlFileStrategy)
      {
        case 1:
          result = findFileInSolution(currentProject);
          break;
        default:
          result = findFileInProject(currentProject);
          break;
      }

      return result;
    }

    private SourceFile findFileInSolution(ProjectElement currentProject)
    {
      foreach (var project in currentProject.Solution.AllProjects)
      {
        IEnumerable<SourceFile> files = (project as ProjectElement).GetFilesByName(hqlNamedQueryFileName);
        if (files.Count() > 0)
          return files.First();
      }
      
      return null;
    }

    private SourceFile findFileInProject(ProjectElement currentProject)
    {
      IEnumerable<SourceFile> files = currentProject.GetFilesByName(hqlNamedQueryFileName);
      if (files.Count() > 0)
        return files.First();
      else
        return null;
    }

    private FileSourceRangeCollection extractHqlNamedQuery(LanguageElement hqlQueryElement)
    {
      string hqlQuery = (hqlQueryElement as IPrimitiveExpression).Value.ToString();

      SourceFile namedQueriesXmlFile = getNamedQueriesXmlFile(hqlQueryElement.Project);

      if (namedQueriesXmlFile != null)
      {
        string tempQueryName = getTempQueryName();

        if (!namedQueriesXmlFile.IsOpened)
          CodeRush.File.Activate(namedQueriesXmlFile.FilePath);

        TextDocument namedQueriesXmlDocument = namedQueriesXmlFile.Document as TextDocument;
        TextDocument currentDocument = hqlQueryElement.Document as TextDocument;
        string currentFilePath = hqlQueryElement.FileNode.FilePath;
        SourceFile currentSourceFile = hqlQueryElement.FileNode;

        string xmlContent = getXmlContent(hqlQuery, tempQueryName);

        insertXmlContentInNamedQueriesFile(namedQueriesXmlFile, xmlContent);

        string quotedTempQueryName = String.Format("\"{0}\"", tempQueryName);

        currentDocument.Replace(hqlQueryElement.Range, quotedTempQueryName, string.Empty, true);
        currentDocument.ParseIfTextChanged();

        FileSourceRangeCollection rangesToLink = new FileSourceRangeCollection();
        SourceRange queryNameElementRange = findElementByName(currentDocument.FileNode, quotedTempQueryName).Range;
        SourceRange queryNameElementRangeWithoutQuotes = new SourceRange(
          queryNameElementRange.Start.Line, 
          queryNameElementRange.Start.Offset + 1, 
          queryNameElementRange.End.Line, 
          queryNameElementRange.End.Offset - 1);

        rangesToLink.Add(new FileSourceRange(currentSourceFile, queryNameElementRangeWithoutQuotes));

        SourcePoint queryNameLocationInNamedQueriesXmlDocument = namedQueriesXmlDocument.FindText(tempQueryName);
        SourceRange queryNameElementRangeInNamedQueriesXmlDocument = new SourceRange(
          queryNameLocationInNamedQueriesXmlDocument, 
          new SourcePoint(queryNameLocationInNamedQueriesXmlDocument.Line, queryNameLocationInNamedQueriesXmlDocument.Offset + tempQueryName.Length));

        rangesToLink.Add(new FileSourceRange(namedQueriesXmlFile, queryNameElementRangeInNamedQueriesXmlDocument));

        CodeRush.File.Activate(currentFilePath);
        TextDocument.Active.SelectText(queryNameElementRangeWithoutQuotes);

        return rangesToLink;
      }
      else
      {
        MessageBox.Show("No file for hql named queries found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }

      return null;
    }

    private static string getXmlContent(string hqlQuery, string tempQueryName)
    {
      XmlDocument tempXmlDoc = new XmlDocument();

      System.Xml.XmlNode tempNode = tempXmlDoc.CreateElement("query", "");
      System.Xml.XmlAttribute tempAttr = tempXmlDoc.CreateAttribute("name");
      tempAttr.Value = tempQueryName;

      tempNode.Attributes.Append(tempAttr);
      tempNode.InnerXml = tempXmlDoc.CreateCDataSection(hqlQuery).OuterXml;

      string xmlContent = tempNode.OuterXml;
      return xmlContent;
    }

    private static LanguageElement findElementByName(LanguageElement rootLanguageElement, string elementName)
    {
      foreach (var aux in rootLanguageElement)
      {
        LanguageElement element = aux as LanguageElement;
        if ((element != null) && (element.Name.Equals(elementName)))
          return element;
      }

      return null;
    }

    private static string getTempQueryName()
    {
      return "NewQuery" + Guid.NewGuid().ToString().GetHashCode().ToString();
    }

    private void ExtractHqlNamedQuery_OptionsChanged(OptionsChangedEventArgs ea)
    {
      loadSettings();
    }

  }
}