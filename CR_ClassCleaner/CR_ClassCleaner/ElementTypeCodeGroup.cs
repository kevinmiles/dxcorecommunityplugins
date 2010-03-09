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
using System.Collections.Generic;
using System.Xml;
using DevExpress.CodeRush.StructuralParser;
using XmlNode = System.Xml.XmlNode;

namespace CR_ClassCleaner
{
    public class ElementTypeCodeGroup : CodeGroup
    {
        internal const string ElementTypeName = "ElementType";

        internal const string ElementTypesName = "ElementTypes";

        internal const string VisibilitiesName = "Visibilities";

        internal const string VisibilityName = "Visibilty";

        private List<LanguageElementType> elementTypes = new List<LanguageElementType>();

        private List<MemberVisibility> memberVisibilty = new List<MemberVisibility>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementTypeCodeGroup"/> class.
        /// </summary>
        /// <param name="displayText">The display text.</param>
        /// <param name="elementTypes">The element types.</param>
        /// <param name="memberVisibilty">The member visibilty.</param>
        /// <param name="comment">The comment.</param>
        public ElementTypeCodeGroup(string displayText,
                                             List<LanguageElementType> elementTypes,
                                             List<MemberVisibility> memberVisibilty,
                                             string comment)
            : base(displayText, comment)
        {
            this.elementTypes = elementTypes;
            this.memberVisibilty = memberVisibilty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementTypeCodeGroup"/> class.
        /// </summary>
        /// <param name="displayText">The display text.</param>
        /// <param name="elementTypes">The element types.</param>
        /// <param name="memberVisibilty">The member visibilty.</param>
        public ElementTypeCodeGroup(string displayText,
                                             List<LanguageElementType> elementTypes,
                                             List<MemberVisibility> memberVisibilty)
            : base(displayText)
        {
            this.elementTypes = elementTypes;
            this.memberVisibilty = memberVisibilty;
        }

        /// <summary>
        /// Initializes a new instance of the ElementTypeCodeGroup class.
        /// </summary>
        public ElementTypeCodeGroup()
            : base()
        {
        }

        /// <summary>
        /// Gets the type of the code group.
        /// </summary>
        /// <value>The type of the code group.</value>
        public override string CodeGroupType
        {
            get { return ElementTypeName; }
        }

        /// <summary>
        /// Gets or sets the type of the element.
        /// </summary>
        /// <value>The type of the element.</value>
        public List<LanguageElementType> ElementTypes
        {
            get { return elementTypes; }
        }

        /// <summary>
        /// Gets or sets the visibilty.
        /// </summary>
        /// <value>The visibilty.</value>
        public List<MemberVisibility> Visibilty
        {
            get { return memberVisibilty; }
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
            codeGroupTypeNode.InnerText = ElementTypeName;
            codeGroupNode.AppendChild(codeGroupTypeNode);
            XmlNode elementTypesNode = doc.CreateElement(ElementTypesName);
            codeGroupNode.AppendChild(elementTypesNode);

            foreach (LanguageElementType type in elementTypes)
            {
                XmlNode elementTypeNode = doc.CreateElement(ElementTypeName);
                elementTypeNode.InnerText = ((int)type).ToString();
                elementTypesNode.AppendChild(elementTypeNode);
            }

            XmlNode visibilitiesNode = doc.CreateElement(VisibilitiesName);
            codeGroupNode.AppendChild(visibilitiesNode);

            foreach (MemberVisibility visibility in memberVisibilty)
            {
                XmlNode visibiltyNode = doc.CreateElement(VisibilityName);
                visibiltyNode.InnerText = ((int)visibility).ToString();
                visibilitiesNode.AppendChild(visibiltyNode);
            }

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
            //Do specific check for constructors
            if (elementTypes.Contains(LanguageElementType.ConstructorInitializer))
            {
                Method method = element as Method;
                if (method != null
                     && method.IsConstructor == true
                     && ContainsVisibility(element) == true)
                    return true;
            }

            if (elementTypes.Contains(element.ElementType)
                 && ContainsVisibility(element) == true)
                return true;
            else
                return false;
        }

        private bool ContainsVisibility(LanguageElement element)
        {
            Member member = element as Member;
            if (member != null)
            {
                if (memberVisibilty.Contains(member.Visibility))
                    return true;
                else
                    return false;
            }
            else
            {
                //this is returning true because some language elements are not
                //Members such as nested structs
                return true;
            }
        }

    }
}