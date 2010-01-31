//////////////////////////////////////////////////////////////////
//CR_ClassCleaner plugin provides organization capabilties to 
//Visual Studio when used with the DXCore framework.
//Copyright (C) 2006  John Luif
//
//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation version 2
//of the License.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
//////////////////////////////////////////////////////////////////
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using EnvDTE;
using DX = DevExpress.CodeRush.Core;
using TextDocument = DevExpress.CodeRush.Core.TextDocument;
using System.Text;

namespace CR_ClassCleaner
{
    public static class Utilities
    {
        private const string WhitespaceRegexReplace =
                     "(?:((?<=\\r?\\n)([ \\t]*|\\r?\\n))\\r?\\n){2,}";

        private static int _propertyName;

        public static int PropertyName
        {
            get { return _propertyName; }
            set
            {
                _propertyName = value;
            }
        }

        /// <summary>
        /// Empties the lines.
        /// </summary>
        /// <param name="emptyLines">The empty lines.</param>
        /// <returns></returns>
        public static void AddEmptyLines(StringBuilder builder, int emptyLines)
        {
            for (int i = 1; i <= emptyLines; i++)
            {
                builder.AppendLine();
            }
        }

        /// <summary>
        /// Deletes the whitespace.
        /// </summary>
        /// <param name="textDocument">The text document.</param>
        public static void DeleteWhitespace(TextDocument textDocument)
        {
            ExecuteRegexReplace(textDocument,
                                      WhitespaceRegexReplace,
                                      Environment.NewLine);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        public static void ExecuteCommand(string commandName)
        {
            try
            {
                Command command = CodeRush.Command.Get(commandName);
                if (command != null && command.IsAvailable)
                {
                    CodeRush.Command.Execute(commandName);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        /// <summary>
        /// Executes the regex replace.
        /// </summary>
        /// <param name="textDocument">The text document.</param>
        /// <param name="regex">The regex.</param>
        /// <param name="replaceWith">The replace with.</param>
        public static void ExecuteRegexReplace(TextDocument textDocument,
                                                            string regex,
                                                            string replaceWith)
        {
            if (textDocument == null)
                return;

            SourcePoint cursorPosition = CodeRush.Caret.SourcePoint;

            try
            {
                textDocument.Lock();

                string text = textDocument.Text;

                text =
                     Regex.Replace(text,
                                        regex,
                                        replaceWith,
                                        RegexOptions.Singleline | RegexOptions.Compiled);

                textDocument.SetText(textDocument.Range, text);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                if (cursorPosition != SourcePoint.Empty)
                    CodeRush.Caret.MoveTo(cursorPosition);

                textDocument.Unlock();
            }
        }

        /// <summary>
        /// Gets the range string.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public static string GetRangeString(LanguageElement element, SourceRange range)
        {
            TextReader reader = element.Document.GetSourceReader(range).GetStream();
            string block = reader.ReadToEnd();

            return block;
        }

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void HandleException(Exception ex)
        {
            MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
        }

        /// <summary>
        /// Selects the current member.
        /// </summary>
        /// <param name="caret">The caret.</param>
        /// <param name="textDocument">The text document.</param>
        /// <returns></returns>
        public static bool SelectCurrentMember(CaretServices caret, TextDocument textDocument)
        {
            bool result = false;

            if (caret == null || textDocument == null)
                return result;

            if (caret.InsideCode == true)
            {
                SourcePoint sourcePoint = caret.SourcePoint;
                LanguageElement element = textDocument.GetNodeAt(sourcePoint);

                if (element.CanBeDocumented == false ||
                     element.ElementType == LanguageElementType.EnumElement)
                    element = element.GetParentElementThatCanBeDocumented();

                SourceRange range = element.GetFullBlockRange(BlockElements.AllSupportElements);
                CodeRush.Selection.SelectRange(range);

                result = true;
            }

            return result;
        }

    }
}