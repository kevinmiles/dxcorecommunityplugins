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
using System.Text.RegularExpressions;
using System.Xml;
using DevExpress.CodeRush.StructuralParser;
using XmlNode = System.Xml.XmlNode;

namespace CR_ClassCleaner
{
    public class RegexCodeGroup : CodeGroup
    {
        internal const string RegexKey = "Regex";

        private string groupRegex = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexCodeGroup"/> class.
        /// </summary>
        /// <param name="displayText">The display text.</param>
        /// <param name="regex">The group regex.</param>
        public RegexCodeGroup(string displayText, string regex, string comment)
            : base(displayText, comment)
        {
            groupRegex = regex;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegexCodeGroup"/> class.
        /// </summary>
        /// <param name="displayText">The display text.</param>
        /// <param name="regex">The regex.</param>
        public RegexCodeGroup(string displayText, string regex)
            : base(displayText)
        {
            groupRegex = regex;
        }

        /// <summary>
        /// Initializes a new instance of the RegexCodeGroup class.
        /// </summary>
        public RegexCodeGroup()
            : base()
        {
        }

        /// <summary>
        /// Gets the type of the code group.
        /// </summary>
        /// <value>The type of the code group.</value>
        public override string CodeGroupType
        {
            get { return RegexKey; }
        }

        /// <summary>
        /// Gets or sets the part regex.
        /// </summary>
        /// <value>The part regex.</value>
        public string GroupRegex
        {
            get { return groupRegex; }
            set { groupRegex = value; }
        }

        /// <summary>
        /// Appends the XML.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="rootNode">The root node.</param>
        public override void AppendXml(XmlDocument doc, XmlNode rootNode)
        {
            XmlNode codeGroupNode = doc.CreateElement(CodeGroupKey);
            rootNode.AppendChild(codeGroupNode);
            XmlNode codeGroupTypeNode = doc.CreateElement(CodeGroupTypeKey);
            codeGroupTypeNode.InnerText = RegexKey;
            codeGroupNode.AppendChild(codeGroupTypeNode);
            XmlNode groupRegexNode = doc.CreateElement(RegexKey);
            groupRegexNode.InnerText = groupRegex.ToString();
            codeGroupNode.AppendChild(groupRegexNode);

            base.AppendXml(doc, codeGroupNode);
        }

        /// <summary>
        /// Determines whether the specified element is match.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// 	<c>true</c> if the specified element is match; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsMatch(LanguageElement element)
        {
            SourceRange codeRange = element.Range;
            string code = Utilities.GetRangeString(element, codeRange);

            if (Regex.IsMatch(
                      code,
                      GroupRegex,
                      RegexOptions.Compiled | RegexOptions.Singleline) == true)
                return true;
            else
                return false;
        }

    }
}