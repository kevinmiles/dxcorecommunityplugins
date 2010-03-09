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
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using Parser = DevExpress.CodeRush.StructuralParser;
using ENV = EnvDTE;

namespace CR_ClassCleaner
{
	public class ClassOrganizer
	{
		private List<CodeGroup> codeGroups = new List<CodeGroup>();

		private StringBuilder codeToInsert = new StringBuilder();

		private List<string> deletedRegions = new List<string>();

		private TextDocument textDocument;

		private TypeDeclaration typeDeclaration;

		public void Organize(
			 TextDocument txtDoc, LanguageElement activeType, bool addRegions)
		{
			typeDeclaration = activeType as TypeDeclaration;
			if (typeDeclaration == null)
				return;

			if (txtDoc == null)
				return;

			textDocument = txtDoc;

			SourcePoint cursorPosition = CodeRush.Caret.SourcePoint;

			try
			{
				Class currentClass = typeDeclaration.GetClass();
				if (currentClass == null || currentClass.Document == null)
					return;

				textDocument.Lock();
				InitializeDictionary();
				//Clear out list of regions that have been deleted
				deletedRegions.Clear();
				//clean out the string builder
				codeToInsert.Remove(0, codeToInsert.Length);

				ReadCodeElements(currentClass);
				BuildCode(addRegions);

				ApplyEdits();
			}
			catch (Exception ex)
			{
				Utilities.HandleException(ex);
			}
			finally
			{
				if (cursorPosition != SourcePoint.Empty)
					CodeRush.Caret.MoveTo(cursorPosition);

				textDocument.Unlock();
				textDocument.Format();
			}
		}

		private void ApplyEdits()
		{
			if (textDocument != null)
			{
				textDocument.QueueInsert(GetClassStartPoint(), codeToInsert.ToString());
				textDocument.ApplyQueuedEdits("Organize");

				Utilities.DeleteWhitespace(textDocument);
			}
		}

		private void BuildCode(bool addRegions)
		{
			foreach (CodeGroup group in ClassCleanerConfig.Current.Groups)
			{
				string groupCode = group.Write(addRegions);
				if (string.IsNullOrEmpty(groupCode) == false)
					codeToInsert.Append(groupCode);
			}
		}

		private SourcePoint GetClassStartPoint()
		{
			SourcePoint start = typeDeclaration.BlockCodeRange.Start;

			return textDocument.GetStartEmptyLinePoint(start);
		}

		private CodeGroup GetCodeGroup(LanguageElement element)
		{
			foreach (CodeGroup group in ClassCleanerConfig.Current.Groups)
			{
				if (group.IsMatch(element))
					return group;
			}

			return null;
		}

		/// <summary
		/// Populate Dictionary with empty lists
		/// </summary>
		private void InitializeDictionary()
		{
			foreach (CodeGroup group in ClassCleanerConfig.Current.Groups)
			{
				group.Clear();
				codeGroups.Add(group);
			}
		}

		private bool IsAttribute(LanguageElement element)
		{
			return element.ElementType == LanguageElementType.AttributeSection;
		}

		private bool IsComment(LanguageElement element)
		{
			return (element.ElementType == LanguageElementType.Comment ||
					  element.ElementType == LanguageElementType.XmlDocComment);
		}

		private void ReadCodeElements(Class currentClass)
		{
			foreach (object node in currentClass.Nodes)
			{
				LanguageElement element = node as LanguageElement;
				if (element != null &&
					 IsComment(element) == false &&
					 IsAttribute(element) == false)
				{
					RemoveParentRegionIfExists(element);

					bool saved = SaveCodeBlock(element);

					if (saved == true)
					{
						SourceRange fullRange =
							 element.GetFullBlockRange(BlockElements.AllSupportElements);
						textDocument.QueueDelete(fullRange);
					}
				}
			}
		}

		private void RemoveParentRegionIfExists(LanguageElement element)
		{
			if (element.ParentRegion != null)
			{
				RegionDirective region = element.ParentRegion;

				if (deletedRegions.Contains(region.Name) == false)
				{
					//Add 10 to just make sure all whitespace is deleted
					int startEndPoint = region.StartOffset + region.Name.Length + 10;
					SourceRange startRange =
						 new SourceRange(region.StartLine, 1, region.StartLine, startEndPoint);
					textDocument.QueueDelete(startRange);
					//Add 10 to just make sure all whitespace is deleted
					//Include name just in case name was a comment at the end
					int endEndPoint = region.EndOffset + region.Name.Length + 10;
					SourceRange endRange =
						 new SourceRange(region.EndLine, 1, region.EndLine, endEndPoint);
					textDocument.QueueDelete(endRange);

					deletedRegions.Add(region.Name);
				}
			}
		}

		private bool SaveCodeBlock(LanguageElement element)
		{
			bool saved = false;

			CodeGroup group = GetCodeGroup(element);

			if (group != null)
			{
				saved = group.Save(element);
			}

			return saved;
		}
	}
}