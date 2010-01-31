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
using System.IO;
using System.Xml;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using XmlElement = System.Xml.XmlElement;
using XmlNode = System.Xml.XmlNode;

namespace CR_ClassCleaner
{
    internal class ClassCleanerConfig
    {
        private const string ClassCleanerConfigElementName = "ClassCleanerConfig";

        private const string ConfigKey = "ClassCleanerConfig";

        public static readonly string CatagoryName = @"Editor\ClassCleaner";

        public static readonly string PageName = "ClassCleaner";

        private static ClassCleanerConfig current;

        /// <summary>
        /// holds the group key and teh display value that is used for regions.
        /// </summary>
        private List<CodeGroup> groups = new List<CodeGroup>();

        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>The current.</value>
        public static ClassCleanerConfig Current
        {
            get
            {
                if (current == null)
                {
                    current = new ClassCleanerConfig();
                    current.LoadCodeGroups();
                }

                return current;
            }
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <value>The groups.</value>
        public List<CodeGroup> Groups
        {
            get { return groups; }
        }

        /// <summary>
        /// Exports the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public bool Export(string fileName)
        {
            bool exported = true;
            try
            {
                File.WriteAllText(fileName, ToXml());
            }
            catch (Exception ex)
            {
                exported = false;
                Utilities.HandleException(ex);
            }

            return exported;
        }

        /// <summary>
        /// Imports the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public bool Import(string fileName)
        {
            bool imported = true;
            try
            {
                string xml = File.ReadAllText(fileName);

                groups.Clear();
                LoadFromXmlString(xml);
                Save();
            }
            catch (Exception ex)
            {
                imported = false;
                Utilities.HandleException(ex);
            }

            return imported;
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public void Load()
        {
            LoadCodeGroups();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            groups.Clear();
            LoadDefaults();
            Save();
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            string xml = ToXml();

            using (DecoupledStorage storage = GetStorage())
            {
                storage.WriteString(CatagoryName, ConfigKey, xml);
            }
        }

        private List<LanguageElementType> CreateElementTypeList(params LanguageElementType[] types)
        {
            List<LanguageElementType> elementTypes = new List<LanguageElementType>();
            foreach (LanguageElementType item in types)
            {
                elementTypes.Add(item);
            }

            return elementTypes;
        }

        private List<MemberVisibility> CreateMemberVisibiltyList(
                     params MemberVisibility[] visibilities)
        {
            List<MemberVisibility> visibilityList = new List<MemberVisibility>();
            foreach (MemberVisibility visibilty in visibilities)
            {
                visibilityList.Add(visibilty);
            }

            return visibilityList;
        }

        private List<MemberVisibility> CreateMemberVisibiltyListAll()
        {
            List<MemberVisibility> visibilityList = new List<MemberVisibility>();
            visibilityList.Add(MemberVisibility.Illegal);
            visibilityList.Add(MemberVisibility.Internal);
            visibilityList.Add(MemberVisibility.Local);
            visibilityList.Add(MemberVisibility.Param);
            visibilityList.Add(MemberVisibility.Private);
            visibilityList.Add(MemberVisibility.Protected);
            visibilityList.Add(MemberVisibility.ProtectedInternal);
            visibilityList.Add(MemberVisibility.Public);
            visibilityList.Add(MemberVisibility.Published);

            return visibilityList;
        }

        private void DecomposeElementTypeNodeAndRegister(XmlNode codeGroupNode,
                                                                                 string displayName,
                                                                                 string comment)
        {
            XmlNode elementsNode =
                 codeGroupNode.SelectSingleNode(ElementTypeCodeGroup.ElementTypesName);

            XmlNode visibilityNode =
                 codeGroupNode.SelectSingleNode(ElementTypeCodeGroup.VisibilitiesName);

            if ((elementsNode != null && elementsNode.ChildNodes != null)
                 && (visibilityNode != null && visibilityNode.ChildNodes != null))
            {
                List<LanguageElementType> elementTypes = new List<LanguageElementType>();
                foreach (XmlNode node in elementsNode.ChildNodes)
                {
                    int result;
                    if (int.TryParse(node.InnerText, out result) == true)
                        elementTypes.Add((LanguageElementType)result);
                }

                List<MemberVisibility> visibilties = new List<MemberVisibility>();
                foreach (XmlNode node in visibilityNode.ChildNodes)
                {
                    int result;
                    if (int.TryParse(node.InnerText, out result) == true)
                        visibilties.Add((MemberVisibility)result);
                }

                RegisterElementTypeCodeGroup(elementTypes, visibilties, displayName, comment);
            }
        }

        private DecoupledStorage GetStorage()
        {
            return CodeRush.Options.GetStorage(CatagoryName, PageName);
        }

        private void LoadCodeGroups()
        {
            bool loaded;

            groups.Clear();
            //Get groups from config
            //if none are retieved load the defaults
            using (DecoupledStorage storage = GetStorage())
            {
                string configXml = storage.ReadString(CatagoryName, ConfigKey);
                loaded = LoadFromXmlString(configXml);
            }

            if (loaded == false)
            {
                LoadDefaults();
                Save();
                LoadCodeGroups();
            }
        }

        private void LoadDefaults()
        {
            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.Enum),
                 CreateMemberVisibiltyListAll(),
                 "Enums",
                 "organizes Enum's into this group.");

            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.Const),
                 CreateMemberVisibiltyListAll(),
                 "Constants",
                 "Organizes constants into this group.");

            RegisterRegexCodeGroup(
                 @"^(\s+|)(public|protected|internal|^(\s+|)(private(?!(public|protected|internal))|(?!(public|protected|internal))))(\s|)static\sreadonly\s",
                 "Readonly",
                 "The regex matches on 'readonly' and organizes those items into this group.");

            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.Variable,
                                              LanguageElementType.InitializedVariable),
                 CreateMemberVisibiltyListAll(),
                 "Fields",
                 "Variables, initialized or not,  are organized into this group.");

            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.Event),
                 CreateMemberVisibiltyListAll(),
                 "Events",
                 "Organizes event declarations into this group.");

            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.Delegate),
                 CreateMemberVisibiltyListAll(),
                 "Delegates",
                 "Organizes delegates itno this group.");

            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.Struct),
                 CreateMemberVisibiltyListAll(),
                 "Structs");

            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.ConstructorInitializer),
                 CreateMemberVisibiltyListAll(),
                 "Constructors",
                 "Organizes constructors, static or not, into this group.");

            RegisterRegexCodeGroup(
                 @"^(\s+|)(public|protected|internal|^(\s+|)(private(?!(public|protected|internal))|(?!(public|protected|internal))))(\s|)static\s[a-zA-Z0-9]+\soperator\s",
                 "Operators",
                 "Finds and organizes operators.");

            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.Property),
                 CreateMemberVisibiltyListAll(),
                 "Properties",
                 "Organizes properties, public or private, into this group.");

            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.Method),
                 CreateMemberVisibiltyList(MemberVisibility.Public),
                 "Public Methods",
                 "Organizes public methods into this group.");

            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.Method),
                 CreateMemberVisibiltyList(MemberVisibility.Internal),
                 "Internal Methods",
                 "Organizes internal methods into this group.");

            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.Method),
                 CreateMemberVisibiltyList(MemberVisibility.Protected,
                                                    MemberVisibility.ProtectedInternal),
                 "Protected Methods",
                 "Organizes protected methods into this group.");

            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.Method),
                 CreateMemberVisibiltyList(MemberVisibility.Private),
                 "Private Methods",
                 "Organizes private methods into this group.");

            RegisterElementTypeCodeGroup(
                 CreateElementTypeList(LanguageElementType.Method),
                 CreateMemberVisibiltyList(MemberVisibility.Illegal,
                                                    MemberVisibility.Local,
                                                    MemberVisibility.Param,
                                                    MemberVisibility.Published),
                 "Methods",
                 "If method does not match one of the above groups it falls into here.");
        }

        private bool LoadFromXmlString(string configXml)
        {
            if (string.IsNullOrEmpty(configXml) == true)
                return false;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(configXml);

            XmlElement root = doc.DocumentElement;
            if (root == null)
                return false;
            if (root.ChildNodes == null || root.ChildNodes.Count == 0)
                return false;

            foreach (XmlNode node in root.ChildNodes)
            {
                ProcessCodeGroupNode(node);
            }

            return true;
        }

        private void ProcessCodeGroupNode(XmlNode node)
        {
            try
            {
                if (node.Name == CodeGroup.CodeGroupKey)
                {
                    XmlNode codeGroupTypeNode = node.SelectSingleNode(CodeGroup.CodeGroupTypeKey);
                    if (codeGroupTypeNode == null)
                        throw new ArgumentException(
                             "Current Configuration is out of date, please press the Reset button.");

                    XmlNode textNode = node.SelectSingleNode(CodeGroup.DisplayTextKey);
                    XmlNode commentNode = node.SelectSingleNode(CodeGroup.CommentKey);

                    string text = textNode != null ? textNode.InnerText : string.Empty;
                    string comment = commentNode != null ? commentNode.InnerText : string.Empty;

                    if (codeGroupTypeNode.InnerText == RegexCodeGroup.RegexKey)
                    {
                        RegisterRegexCodeGroup(node, text, comment);
                    }
                    else if (codeGroupTypeNode.InnerText == ElementTypeCodeGroup.ElementTypeName)
                    {
                        DecomposeElementTypeNodeAndRegister(node, text, comment);
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
        }

        private void RegisterElementTypeCodeGroup(List<LanguageElementType> elementTypes,
                                                                        List<MemberVisibility> visibilty,
                                                                        string displayName)
        {
            groups.Add(new ElementTypeCodeGroup(displayName, elementTypes, visibilty));
        }

        private void RegisterElementTypeCodeGroup(List<LanguageElementType> elementTypes,
                                                                        List<MemberVisibility> visibilty,
                                                                        string displayName,
                                                                        string comment)
        {
            groups.Add(new ElementTypeCodeGroup(displayName, elementTypes, visibilty, comment));
        }

        private void RegisterRegexCodeGroup(XmlNode codeGroupNode,
                                                                string displayName,
                                                                string comment)
        {
            XmlNode regexNode = codeGroupNode.SelectSingleNode(RegexCodeGroup.RegexKey);

            groups.Add(new RegexCodeGroup(displayName, regexNode.InnerText, comment));
        }

        private void RegisterRegexCodeGroup(string regex,
                                                                string displayName,
                                                                string comment)
        {
            groups.Add(new RegexCodeGroup(displayName, regex, comment));
        }

        private void RegisterRegexCodeGroup(string groupRegex, string displayName)
        {
            groups.Add(new RegexCodeGroup(displayName, groupRegex));
        }

        private string ToXml()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode rootNode = doc.CreateElement(ClassCleanerConfigElementName);
            doc.AppendChild(rootNode);
            foreach (CodeGroup group in groups)
            {
                group.AppendXml(doc, rootNode);
            }

            return doc.OuterXml;
        }

    }
}