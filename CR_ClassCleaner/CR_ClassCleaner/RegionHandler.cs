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
using System.Text;
using DevExpress.CodeRush.Core;
using System.Globalization;

namespace CR_ClassCleaner
{
    public static class RegionHandler
    {
        private const string FullRegionNameTemplate = "{0} {1}{2}{1}";

        private static readonly string endRegionKeyword = CodeRush.Language.ActiveExtension.RegionKeywords.End;

        private static readonly string RegionRegexReplace =
                     "\\r?\\n[ \\t]*(" +
                    startRegionKeyword + "|" + endRegionKeyword +
                    ")[ ~!@#\\$%\\^&\\*_\\+=-`'\";:\\|\\\\\\<\\>\\.,/\\?\\{\\}\\[\\]\\(\\)\\w\\t\\-]*\\r?\\n";

        private static readonly string startRegionKeyword = CodeRush.Language.ActiveExtension.RegionKeywords.Start;

        /// <summary>
        /// Adds the begin region.
        /// </summary>
        /// <param name="codeToInsert">The code to insert.</param>
        /// <param name="regionName">Name of the region.</param>
        public static void AddBeginRegion(StringBuilder codeToInsert, string regionName)
        {
            if (IsRegionKeywordsAvailable() == false)
                return;

            string vbQuotes = string.Empty;

            //VB.NET needs the region tag in quotes
            if (CodeRush.Language.IsBasic == true)
            {
                vbQuotes = "\"";
            }

            codeToInsert.AppendFormat(CultureInfo.InvariantCulture,
                                              FullRegionNameTemplate,
                                              startRegionKeyword,
                                              vbQuotes,
                                              regionName);

            Utilities.AddEmptyLines(codeToInsert, 2);
        }

        /// <summary>
        /// Adds the end region.
        /// </summary>
        /// <param name="codeToInsert">The code to insert.</param>
        public static void AddEndRegion(StringBuilder codeToInsert)
        {
            if (IsRegionKeywordsAvailable() == false)
                return;

            codeToInsert.Append(endRegionKeyword);
            Utilities.AddEmptyLines(codeToInsert, 2);
        }

        /// <summary>
        /// Removes the regions.
        /// </summary>
        /// <param name="textDocument">The text document.</param>
        public static void RemoveRegions(TextDocument textDocument)
        {
            Utilities.ExecuteRegexReplace(textDocument,
                                                    RegionRegexReplace,
                                                    string.Empty);
        }

        private static bool IsRegionKeywordsAvailable()
        {
            return CodeRush.Language != null &&
                     CodeRush.Language.ActiveExtension != null &&
                     CodeRush.Language.ActiveExtension.RegionKeywords != null;
        }

    }
}