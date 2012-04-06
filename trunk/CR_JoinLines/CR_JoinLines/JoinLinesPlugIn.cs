using System;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.Diagnostics.Commands;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_JoinLines
{
	/// <summary>
	/// Joins one or more lines in the code editor.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This plugin offers the ability for one or more text lines to be joined together
	/// in the code editor.  This functionality is typically bound to a shortcut key
	/// such as <c>Ctrl+J</c>.
	/// </para>
	/// </remarks>
	public class JoinLinesPlugIn : StandardPlugIn
	{
		/// <summary>
		/// Prefix for all log messages
		/// </summary>
		private const string LOG_PREFIX = "JoinLines:";

		/// <summary>
		/// Design-time support object.
		/// </summary>
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Primary action for this plugin.
		/// </summary>
		private DevExpress.CodeRush.Core.Action actionJoinLines;

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

		/// <summary>
		/// Initializes a new instance of the <see cref="CR_JoinLines.JoinLinesPlugIn" /> class.
		/// </summary>
		public JoinLinesPlugIn()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Executes the "join lines" action.
		/// </summary>
		/// <param name="ea">
		/// A <see cref="DevExpress.CodeRush.Core.ExecuteEventArgs"/> that contains the event data.
		/// </param>
		/// <remarks>
		/// <para>
		/// This method provides the primary functionality for the Join Lines plugin.
		/// </para>
		/// <para>
		/// If there is no selection, the line the caret is currently on will be joined
		/// with the subsequent line.
		/// </para>
		/// <para>
		/// If there is a selection, the selected lines will be joined and the caret will
		/// be placed at the beginning of the complete joined line.
		/// </para>
		/// </remarks>
		private void actionJoinLines_Execute(DevExpress.CodeRush.Core.ExecuteEventArgs ea)
		{
			// Don't do anything if we're not available.
			if (!this.Available)
			{
				return;
			}

			// Don't do anything if we don't have an active text document.
			if (CodeRush.Documents.ActiveTextDocument == null)
			{
				Log.SendError("{0}Active text document is null.", LOG_PREFIX);
				return;
			}

			SourcePoint origLocation = CodeRush.Caret.SourcePoint;

			Log.SendMsg(ImageType.Info, "{0}Joining lines.", LOG_PREFIX);
			CodeRush.UndoStack.BeginUpdate("JoinLines");
			try
			{
				string delimiter = ea.Action.Parameters.GetString("Delimiter", "");
				if (CodeRush.TextViews.Active.Selection.Exists)
				{
					// Join the selected lines
					CodeRush.TextViews.Active.Selection.ExtendToWholeLines();
					int topLine = CodeRush.TextViews.Active.Selection.StartLine;
					int numLines = CodeRush.TextViews.Active.Selection.EndLine - topLine - 1;
					if (numLines == 0)
					{
						numLines = 1;
					}
					for (int i = 0; i < numLines; i++)
					{
						ExecuteJoin(topLine, delimiter);
					}
					CodeRush.Caret.MoveTo(topLine, 0);
				}
				else
				{
					// Join the current line with the next line
					ExecuteJoin(origLocation.Line, delimiter);
					CodeRush.Caret.MoveTo(origLocation);
				}
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
		/// If <see cref="CR_JoinLines.JoinLinesPlugIn.Available"/> is <see langword="true" />,
		/// this command is enabled and supported.  If <see cref="CR_JoinLines.JoinLinesPlugIn.Available"/>
		/// is <see langword="false" />, this command is unsupported.
		/// </para>
		/// </remarks>
		private void actionJoinLines_QueryStatus(DevExpress.CodeRush.Core.QueryStatusEventArgs ea)
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

		/// <summary>
		/// Executes the bulk of a line join.
		/// </summary>
		/// <param name="line">The line number to join with the subsequent line.</param>
		/// <param name="delimiter">The string to insert between joined lines.</param>
		/// <remarks>
		/// <para>
		/// Moves the caret to the end of the specified line, deletes all left and right
		/// whitespace, removes the newline at the end of the line, then deletes any
		/// whitespace at the beginning of the joined line.
		/// </para>
		/// </remarks>
		private void ExecuteJoin(int line, string delimiter)
		{
			CodeRush.Caret.MoveTo(line, CodeRush.Documents.ActiveTextDocument.GetLastCharacterOffset(line));
			CodeRush.Caret.DeleteLeftWhiteSpace();
			CodeRush.Caret.DeleteRightWhiteSpace();
			CodeRush.Caret.DeleteRight(System.Environment.NewLine.Length);
			CodeRush.Caret.DeleteRightWhiteSpace();
			if (String.IsNullOrEmpty(delimiter))
			{
				CodeRush.Caret.Insert(delimiter);
			}
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(JoinLinesPlugIn));
			DevExpress.CodeRush.Core.Parameter parameter1 = new DevExpress.CodeRush.Core.Parameter(new DevExpress.CodeRush.Core.StringParameterType());
			this.actionJoinLines = new DevExpress.CodeRush.Core.Action(this.components);
			((System.ComponentModel.ISupportInitialize)(this.actionJoinLines)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// actionJoinLines
			// 
			this.actionJoinLines.ActionName = "Join Lines";
			this.actionJoinLines.ButtonText = "Join Lines";
			this.actionJoinLines.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.actionJoinLines.Description = "Joins lines in the editor.";
			this.actionJoinLines.Image = ((System.Drawing.Bitmap)(resources.GetObject("actionJoinLines.Image")));
			this.actionJoinLines.ImageBackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(254)), ((System.Byte)(0)));
			parameter1.DefaultValue = "";
			parameter1.Description = "String that will be inserted between lines when joining.  Helpful if a space, com" +
				"ma, pipe, or other delimiter needs to be inserted between joined lines.";
			parameter1.Name = "Delimiter";
			parameter1.Optional = true;
			this.actionJoinLines.Parameters.Add(parameter1);
			this.actionJoinLines.RegisterInVS = true;
			this.actionJoinLines.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actionJoinLines_Execute);
			this.actionJoinLines.QueryStatus += new DevExpress.CodeRush.Core.QueryStatusEventHandler(this.actionJoinLines_QueryStatus);
			((System.ComponentModel.ISupportInitialize)(this.actionJoinLines)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}
	}
}