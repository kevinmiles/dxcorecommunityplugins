using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Diagnostics.Commands;

namespace CR_SortLines
{
    /// <summary>
    /// Sorts two or more lines in the code editor.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This plugin offers the ability for two or more text lines to be sorted
    /// in the code editor.
    /// </para>
    /// </remarks>
    public class SortLinesPlugIn : StandardPlugIn
    {

        #region SortLinesPlugIn Variables

        #region Constants

        /// <summary>
        /// Prefix for all log messages
        /// </summary>
        private const string LOG_PREFIX = "SortLines: ";

        #endregion

        #region Instance

        /// <summary>
        /// Design-time support object.
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// The primary action for the plugin.
        /// </summary>
        private DevExpress.CodeRush.Core.Action _actionSortLines;

        /// <summary>
        /// The sort options dialog.
        /// </summary>
        private SortDialog _sortDialog;

        #endregion

        #endregion



        #region SortLinesPlugIn Properties

        /// <summary>
        /// Gets a flag indicating whether or not this command should be made available.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if this command should be made available in menus,
        /// etc.; <see langword="false" /> if not.
        /// </value>
        /// <remarks>
        /// <para>
        /// This property evaluates the context <c>Focus\Documents\Source\Code Editor</c>.
        /// If that context is satisfied, this property is <see langword="true" />.  If
        /// not, or if the context is not found, this property is <see langword="false" />.
        /// </para>
        /// </remarks>
        public bool Available
        {
            get
            {
                return CodeRush.Context.Satisfied(@"Focus\Documents\Source\Code Editor", false);
            }
        }

        #endregion



        #region SortLinesPlugIn Implementation

        #region Constructors

        public SortLinesPlugIn()
        {
            InitializeComponent();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Initializes the plugin by setting event handlers, etc.
        /// </summary>
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            this._sortDialog = new SortDialog();
        }

        /// <summary>
        /// Cleans up and releases resources.
        /// </summary>
        public override void FinalizePlugIn()
        {
            this._sortDialog.Dispose();

            base.FinalizePlugIn();
        }


        #endregion

        #region Event Handlers

        /// <summary>
        /// Executes the "sort lines" action.
        /// </summary>
        /// <param name="ea">
        /// A <see cref="DevExpress.CodeRush.Core.ExecuteEventArgs"/> that contains the event data.
        /// </param>
        /// <remarks>
        /// <para>
        /// This method provides the primary functionality for the Join Lines plugin.
        /// </para>
        /// <para>
        /// If there is no selection, all lines will be sorted and the caret will remain
        /// on the original line (or as close as possible) it was on.
        /// </para>
        /// <para>
        /// If there is a selection, the selected lines will be sorted and will remain selected.
        /// </para>
        /// </remarks>
        private void actionSortLines_Execute(DevExpress.CodeRush.Core.ExecuteEventArgs ea)
        {
            // Ensure context is satisfied
            if (!this.Available)
            {
                return;
            }

            // Ensure we have a document of at least two lines
            if (CodeRush.Documents.ActiveTextDocument == null)
            {
                Log.SendError("{0}Active text document is null.  No sort will occur.", LOG_PREFIX);
                return;
            }
            if (CodeRush.Documents.ActiveTextDocument.LineCount < 2)
            {
                return;
            }

            // Get the sort options
            if (this._sortDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            SourcePoint origLocation = CodeRush.Caret.SourcePoint;
            CodeRush.UndoStack.BeginUpdate("SortLines");
            try
            {
                // Get the correct comparison method
                LineComparer comparer = null;
                if (this._sortDialog.SortCaseSensitive)
                {
                    Log.SendMsg(ImageType.Options, "{0}Sorting case sensitive.", LOG_PREFIX);
                    comparer = new CaseSensitiveLineComparer();
                }
                else
                {
                    Log.SendMsg(ImageType.Options, "{0}Sorting case insensitive.", LOG_PREFIX);
                    comparer = new CaseInsensitiveLineComparer();
                }

                // Get the set of lines to sort
                bool selectFinalSet = false;
                int firstLine = 1;
                int lastLine = 0;
                if (CodeRush.Selection.Exists)
                {
                    // We're sorting the selection
                    selectFinalSet = true;
                    firstLine = CodeRush.TextViews.Active.Selection.Range.Top.Line;
                    lastLine = CodeRush.TextViews.Active.Selection.Range.Bottom.Line;
                    // If we've selected to the start of the next line, we don't actually
                    // want to sort that next line, so subtract 1.
                    if (CodeRush.TextViews.Active.Selection.Range.End.Offset==1)
                    {
                        lastLine--;
                    }
                }
                else
                {
                    // We're sorting the whole file
                    lastLine = CodeRush.Documents.ActiveTextDocument.LineCount;
                }

                // Verify we have something to sort
                if (firstLine >= lastLine)
                {
                    Log.SendMsg(ImageType.Info, "{0}No lines to sort.", LOG_PREFIX);
                    return;
                }

                // Add the lines to the list of all lines
                ArrayList lineList = new ArrayList();
                Regex matchExp = null;
                if (this._sortDialog.UseSortExpression)
                {
                    matchExp = new Regex(this._sortDialog.MatchExpression);
                }
                for (int i = firstLine; i <= lastLine; i++)
                {
                    LinePair lp = new LinePair();
                    lp.OriginalLine = CodeRush.Documents.ActiveTextDocument.GetLine(i);
                    if (matchExp != null)
                    {
                        lp.SortableLine = matchExp.Replace(lp.OriginalLine, this._sortDialog.SortExpression);
                    }
                    else
                    {
                        lp.SortableLine = lp.OriginalLine;
                    }
                    lineList.Add(lp);
                }

                // Sort the lines
                lineList.Sort(comparer);

                // Delete duplicates
                if (this._sortDialog.DeleteDuplicates)
                {
                    Log.SendMsg(ImageType.Options, "{0}Deleting duplicate lines.", LOG_PREFIX);
                    for (int i = lineList.Count - 1; i > 0; i--)
                    {
                        if (((LinePair)lineList[i]).SortableLine == ((LinePair)lineList[i - 1]).SortableLine)
                        {
                            lineList.RemoveAt(i);
                        }
                    }
                }

                // Delete the original set of lines
                CodeRush.Caret.MoveTo(firstLine, 0);
                CodeRush.Documents.ActiveTextDocument.SelectText(firstLine, 1, lastLine, CodeRush.Documents.ActiveTextDocument.GetLineLength(lastLine) + 1);

                // Insert the sorted lines
                string[] newLines = new string[lineList.Count];
                if (this._sortDialog.SortAscending)
                {
                    for (int i = 0; i < lineList.Count; i++)
                    {
                        newLines[i] = ((LinePair)(lineList[i])).OriginalLine;
                    }
                }
                else
                {
                    for (int i = lineList.Count - 1; i >= 0; i--)
                    {
                        int newLineIndex = lineList.Count - i - 1;
                        newLines[newLineIndex] = ((LinePair)(lineList[i])).OriginalLine;
                    }
                }
                CodeRush.Selection.Text = String.Join(System.Environment.NewLine, newLines);

                if (selectFinalSet)
                {
                    // Reselect if necessary
                    int newLastLine = firstLine + lineList.Count - 1;
                    CodeRush.Documents.ActiveTextDocument.SelectText(firstLine, 1, newLastLine, CodeRush.Documents.ActiveTextDocument.GetLineLength(newLastLine) + 1);
                }
                else
                {
                    // Put the caret back in the right spot
                    CodeRush.Caret.MoveTo(origLocation);
                }

                lineList = null;
            }
            catch (Exception err)
            {
                Log.SendException(String.Format("{0}Exception while sorting lines.", LOG_PREFIX), err);
            }
            finally
            {
                CodeRush.UndoStack.EndUpdate();
            }
        }

        /// <summary>
        /// Informs Visual Studio of the availability of the command.
        /// </summary>
        /// <param name="ea">A <see cref="DevExpress.CodeRush.Core.QueryStatusEventArgs"/> that contains the event data</param>
        /// <remarks>
        /// <para>
        /// If <see cref="CR_SortLines.SortLinesPlugIn.Available"/> is <see langword="true" />,
        /// this command is enabled and supported.  If <see cref="CR_SortLines.SortLinesPlugIn.Available"/>
        /// is <see langword="false" />, this command is unsupported.
        /// </para>
        /// </remarks>
        private void actionSortLines_QueryStatus(DevExpress.CodeRush.Core.QueryStatusEventArgs ea)
        {
            if (!this.Available)
            {
                ea.Status = EnvDTE.vsCommandStatus.vsCommandStatusUnsupported;
            }
            else
            {
                ea.Status = EnvDTE.vsCommandStatus.vsCommandStatusEnabled | EnvDTE.vsCommandStatus.vsCommandStatusSupported;
            }
        }

        #endregion

        #region Methods

        #region Static

        #endregion

        #region Instance

        #endregion

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SortLinesPlugIn));
            this._actionSortLines = new DevExpress.CodeRush.Core.Action(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._actionSortLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // _actionSortLines
            // 
            this._actionSortLines.ActionName = "Sort Lines";
            this._actionSortLines.ButtonText = "Sort Lines";
            this._actionSortLines.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
            this._actionSortLines.Description = "Sorts lines in the code editor.";
            this._actionSortLines.Image = ((System.Drawing.Bitmap)(resources.GetObject("_actionSortLines.Image")));
            this._actionSortLines.ImageBackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(254)), ((System.Byte)(0)));
            this._actionSortLines.RegisterInVS = true;
            this._actionSortLines.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actionSortLines_Execute);
            this._actionSortLines.QueryStatus += new DevExpress.CodeRush.Core.QueryStatusEventHandler(this.actionSortLines_QueryStatus);
            ((System.ComponentModel.ISupportInitialize)(this._actionSortLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        #endregion

        #endregion






    }
}