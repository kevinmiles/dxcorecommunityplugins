using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;

namespace RenameXamlNamespacePrefix
{
  public class TagPrefix
	{
		// private static methods...
		#region AtSourcePoint
		private static TagPrefix AtSourcePoint(LanguageElement activeElement, SourcePoint sourcePoint)
		{
			if (activeElement == null)
				return null;

			IEnumerable<TagPrefix> prefixes = TagPrefix.GetAll(activeElement);

			if (prefixes == null)
				return null;

			foreach (TagPrefix prefix in prefixes)
				if (prefix.Range.Contains(sourcePoint))
					return prefix;
			return null;
		}
		#endregion
		#region FromName
		private static TagPrefix FromName(string name, SourcePoint start)
		{
			int colonIndex = name.IndexOf(":");
			if (colonIndex <= 0)
				return null;

			SourceRange tagPrefixRange = SourceRange.Empty;
			string tagPrefixName = name.Substring(0, colonIndex);
			if (tagPrefixName == "xmlns")
			{
				string xmlnsName = name.Substring(colonIndex + 1);
				SourcePoint xmlnsNameStart = new SourcePoint(start.Line, start.Offset + tagPrefixName.Length + 1);
				SourcePoint end = new SourcePoint(xmlnsNameStart.Line, xmlnsNameStart.Offset + xmlnsName.Length);
				tagPrefixRange = new SourceRange(xmlnsNameStart, end);
				tagPrefixName = xmlnsName;
			}
			else
			{
				SourcePoint end = new SourcePoint(start.Line, start.Offset + tagPrefixName.Length);
				tagPrefixRange = new SourceRange(start, end);
			}

			return new TagPrefix() { Name = tagPrefixName, Range = tagPrefixRange };
		}
		#endregion
		#region FromAttribute
		private static IEnumerable<TagPrefix> FromAttribute(LanguageElement element)
		{
			HtmlAttribute htmlAttribute = element as HtmlAttribute;
			if (htmlAttribute == null)
				yield break;

			TagPrefix tagPrefix = FromName(htmlAttribute.Name, htmlAttribute.NameRange.Start);
			if (tagPrefix != null)
				yield return tagPrefix;
		}
		#endregion
		#region FromAttributeValue
		private static IEnumerable<TagPrefix> FromAttributeValue(LanguageElement element)
		{
			HtmlAttribute htmlAttribute = element as HtmlAttribute;
			if (htmlAttribute == null)
				yield break;

			TagPrefix tagPrefix = FromName(htmlAttribute.Value, htmlAttribute.ValueRange.Start);
			if (tagPrefix != null)
				yield return tagPrefix;
		}
		#endregion
		#region FromServerControl
		private static IEnumerable<TagPrefix> FromServerControl(LanguageElement element)
		{
			ServerControlElement serverControl = element as ServerControlElement;
			if (serverControl == null)
				yield break;

			if (!serverControl.TagPrefixRange.IsEmpty)
				yield return new TagPrefix() { Name = serverControl.TagPrefix, Range = serverControl.TagPrefixRange };

			if (!serverControl.CloseTagPrefixRange.IsEmpty)
				yield return new TagPrefix() { Name = serverControl.TagPrefix, Range = serverControl.CloseTagPrefixRange };
		}
		#endregion
		#region GetAll
		private static IEnumerable<TagPrefix> GetAll(LanguageElement node)
		{
			if (node == null)
				yield break;

			IEnumerable<TagPrefix> tagPrefixes = FromServerControl(node);
			foreach (TagPrefix tagPrefix in tagPrefixes)
				yield return tagPrefix;

			tagPrefixes = FromAttribute(node);
			foreach (TagPrefix tagPrefix in tagPrefixes)
				yield return tagPrefix;

			tagPrefixes = FromAttributeValue(node);
			foreach (TagPrefix tagPrefix in tagPrefixes)
				yield return tagPrefix;
		}
		#endregion

		// public static methods...
		#region AtCaret
		public static TagPrefix AtCaret(LanguageElement activeElement)
		{
			return AtSourcePoint(activeElement, CodeRush.Caret.SourcePoint);
		}
		#endregion
		#region FindMatching
		public static IEnumerable<TagPrefix> FindMatching(LanguageElement scope, TagPrefix tagPrefix)
		{
			if (tagPrefix == null)
				return null;
			List<TagPrefix> result = new List<TagPrefix>();
			ElementEnumerable e = new ElementEnumerable(scope, true);
			foreach (LanguageElement node in e)
			{
				IEnumerable<TagPrefix> nodeTagPrefixes = GetAll(node);
				if (nodeTagPrefixes != null)
				{
					foreach (TagPrefix nodeTagPrefix in nodeTagPrefixes)
						if (nodeTagPrefix.Name == tagPrefix.Name)
							result.Add(nodeTagPrefix);
				}
			}
			return result;
		}
		#endregion



    public string Name { get; set; }
    public SourceRange Range { get; set; }
  }
}