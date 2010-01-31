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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DevExpress.CodeRush.StructuralParser;
using XmlNode = System.Xml.XmlNode;

namespace CR_ClassCleaner
{
    public abstract class CodeGroup
    {
        internal const string CodeGroupKey = "CodeGroup";

        internal const string CodeGroupTypeKey = "CodeGroupType";

        internal const string CommentKey = "Comment";

        internal const string DisplayTextKey = "DisplayText";

        /// <summary>
        /// Key is the class group key, vlaue is the list of code blocks.
        /// </summary>
        protected SortedList<string, string> codeBlocks = new SortedList<string, string>();

        private string comment = string.Empty;

        private string displayText = string.Empty;

        /// <summary>
        /// Initializes a new instance of the CodeGroup class.
        /// </summary>
        public CodeGroup()
        {
        }

        /// <summary>
        /// Initializes a new instance of the CodeGroup class.
        /// </summary>
        /// <param name="displayText"></param>
        /// <param name="comment"></param>
        public CodeGroup(string displayText, string comment)
        {
            this.displayText = displayText;
            this.comment = comment;
        }

        /// <summary>
        /// Initializes a new instance of the CodeGroup class.
        /// </summary>
        /// <param name="displayText"></param>
        public CodeGroup(string displayText)
        {
            this.displayText = displayText;
        }

        /// <summary>
        /// Gets the type of the code group.
        /// </summary>
        /// <value>The type of the code group.</value>
        public abstract string CodeGroupType { get; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>The comment.</value>
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        /// <summary>
        /// Gets or sets the display value.
        /// </summary>
        /// <value>The display value.</value>
        public string DisplayText
        {
            get { return displayText; }
            set { displayText = value; }
        }

        /// <summary>
        /// Gets the edit button text.
        /// </summary>
        /// <value>The edit button text.</value>
        public string EditButtonText
        {
            get { return "Edit"; }
        }

        /// <summary>
        /// Appends the XML.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <param name="rootNode">The root node.</param>
        public virtual void AppendXml(XmlDocument doc, XmlNode rootNode)
        {
            XmlNode displayNode = doc.CreateElement(DisplayTextKey);
            rootNode.AppendChild(displayNode);
            displayNode.InnerText = DisplayText;
            XmlNode commentNode = doc.CreateElement(CommentKey);
            rootNode.AppendChild(commentNode);
            commentNode.InnerText = Comment;
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public virtual void Clear()
        {
            codeBlocks.Clear();
        }

        /// <summary>
        /// Determines whether the specified element is match.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// 	<c>true</c> if the specified element is match; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsMatch(LanguageElement element);

        /// <summary>
        /// Saves the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public virtual bool Save(LanguageElement element)
        {
            bool saved = false;

            SourceRange fullRange =
                 element.GetFullBlockRange(BlockElements.AllSupportElements);

            if (fullRange != SourceRange.Empty)
            {
                string fullMethodName = GetMethodKey(element);
                string fullBlock = Utilities.GetRangeString(element, fullRange);
                codeBlocks.Add(fullMethodName, fullBlock);

                saved = true;
            }

            return saved;
        }

        /// <summary>
        /// Writes this instance.
        /// </summary>
        /// <returns></returns>
        public virtual string Write(bool addRegions)
        {
            StringBuilder codeBuilder = new StringBuilder();

            CheckAddBeginRegion(displayText, codeBuilder, addRegions);

            foreach (string codeBlock in codeBlocks.Values)
            {
                if (string.IsNullOrEmpty(codeBlock) == false)
                {
                    codeBuilder.Append(codeBlock);
                    Utilities.AddEmptyLines(codeBuilder, 2);
                }
            }

            CheckAddEndRegion(codeBuilder, addRegions);

            return codeBuilder.ToString();
        }

        protected void CheckAddBeginRegion(
                     string regionName, StringBuilder codeToInsert, bool addRegions)
        {
            if (addRegions == true && codeBlocks.Count > 0)
            {
                RegionHandler.AddBeginRegion(codeToInsert, regionName);
            }
        }

        protected void CheckAddEndRegion(StringBuilder codeToInsert, bool addRegions)
        {
            if (addRegions == true && codeBlocks.Count > 0)
            {
                RegionHandler.AddEndRegion(codeToInsert);
            }
        }

        /// <summary>
        /// Gets the method key.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        protected string GetMethodKey(LanguageElement element)
        {
            string name = element.Name;
            //Append a guid to make sure the key is unique
            return name + Guid.NewGuid().ToString();
        }

    }
}