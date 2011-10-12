using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_StringFormatter
{
	public partial class PlugIn1 : StandardPlugIn
	{
		// DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();

			//
			// TODO: Add your initialization code here.
			//
		}
		#endregion
		#region FinalizePlugIn
		public override void FinalizePlugIn()
		{
			//
			// TODO: Add your finalization code here.
			//

			base.FinalizePlugIn();
		}
		#endregion

		#region IsStringFormatCall
		private static bool IsStringFormatCall(MethodCallExpression methodCallExpression)
		{
			if (methodCallExpression == null)
				return false;

			MethodReferenceExpression formatCall = methodCallExpression.Qualifier as MethodReferenceExpression;
      if (formatCall == null)
        return false;
      ReferenceExpressionBase qualifier = formatCall.Qualifier as ReferenceExpressionBase;
      if (formatCall.Name == "Format")
        return qualifier.Name == "String" || qualifier.Name == CodeRush.Language.GetSimpleTypeName("System.String");
      else if (formatCall.Name == "AppendFormat")
      {
        ITypeElement qualifierDeclaration = qualifier.Resolve(ParserServices.SourceTreeResolver) as ITypeElement;
        return qualifierDeclaration != null && qualifierDeclaration.Is("System.Text.StringBuilder");
      }
      return false;
		}
		#endregion
		#region InFirstStringArgument
		private static bool InFirstStringArgument(LanguageElement element, int line, int offset)
		{
			Expression firstStringArgument = null;

			PrimitiveExpression primitiveExpression = element as PrimitiveExpression;
			if (primitiveExpression == null)
				return false;

			if (primitiveExpression.PrimitiveType != PrimitiveType.String)
				return false;

			MethodCallExpression methodCallExpression = primitiveExpression.Parent as MethodCallExpression;
			if (!IsStringFormatCall(methodCallExpression))
				return false;

			if (methodCallExpression.ArgumentsCount <= 0)
				return false;

			firstStringArgument = methodCallExpression.Arguments[0];
			if (firstStringArgument.Range.Contains(line, offset))
				return true;
			return false;
		}
		#endregion
		private bool InFormatItem(LanguageElement element, int line, int offset)
		{
			if (!InFirstStringArgument(element, line, offset))
				return false;

			FormatItems formatItems = GetFormatItems(element as PrimitiveExpression);
			return formatItems.GetFormatItemAtPos(line, offset) != null;
		}
		private void spFormatItems_CheckAvailability(object sender, CheckSearchAvailabilityEventArgs ea)
		{
			ea.Available = InFormatItem(ea.Element, ea.Caret.Line, ea.Caret.Offset);
		}

		private void spFormatItems_SearchReferences(object sender, SearchEventArgs ea)
		{
			int caretLine = ea.Caret.Line;
			int caretOffset = ea.Caret.Offset;

			if (!InFirstStringArgument(ea.Element, caretLine, caretOffset))
				return;

			FormatItems formatItems = GetFormatItems(ea.Element as PrimitiveExpression);
			if (formatItems.Count == 0)
				return;
			SourceFile sourceFile = formatItems.SourceFile;

			FormatItemPos formatItemPos = formatItems.GetFormatItemPosAtPos(caretLine, caretOffset);
			FormatItem formatItem = formatItemPos.Parent;
			if (formatItem == null)
				return;

			// Add each occurrence of this format item to the navigation range...
			foreach (FormatItemPos position in formatItem.Positions)
			{
				SourceRange sourceRange = position.GetSourceRange(caretLine);
				ea.AddRange(new FileSourceRange(sourceFile, sourceRange));
			}

			if (formatItem.Argument != null)
				ea.AddRange(new FileSourceRange(sourceFile, formatItem.Argument.Range));
		}

		#region GetFormatItems
		/// <summary>
		/// Parses the text in the specified PrimitiveExpression, collecting and returning a dictionary of FormatItems, indexed by the format item number.
		/// </summary>
		private FormatItems GetFormatItems(PrimitiveExpression primitiveExpression)
		{
			FormatItems formatItems = new FormatItems();
			formatItems.ParentMethodCall = primitiveExpression.Parent as MethodCallExpression;
			int argumentCount = formatItems.ParentMethodCall.ArgumentsCount;
			formatItems.PrimitiveExpression = primitiveExpression;
			if (primitiveExpression == null)
				return formatItems;
			string text = primitiveExpression.Name;
			bool lastCharWasOpenBrace = false;
			bool insideFormatItem = false;
			bool collectingFormatItemNumber = false;
			string numberStr = String.Empty;
			int lastOpenBraceOffset = 0;
			int length = 0;
			for (int i = 0; i < text.Length; i++)
			{
				char thisChar = text[i];
				if (thisChar == '{')
				{
					lastCharWasOpenBrace = !lastCharWasOpenBrace;
					lastOpenBraceOffset = i;
				}
				else if (thisChar == '}')
				{
					if (insideFormatItem)
					{
						insideFormatItem = false;
						if (numberStr != String.Empty)
						{
							int number = int.Parse(numberStr);
							const int INT_CountForBraceDelimeters = 2;
							int argumentIndex = number + 1;
							Expression argument = null;
							if (argumentIndex < argumentCount)
								argument = formatItems.ParentMethodCall.Arguments[argumentIndex];

							if (!formatItems.HasFormatItem(number))
								formatItems.AddFormatItem(number, argument);
							formatItems[number].AddPosition(lastOpenBraceOffset, length + INT_CountForBraceDelimeters);
						}
					}
				}
				else if (lastCharWasOpenBrace)
				{
					length = 0;
					lastCharWasOpenBrace = false;
					insideFormatItem = true;
					collectingFormatItemNumber = true;
					numberStr = String.Empty;
					if (char.IsDigit(thisChar))
						numberStr = thisChar.ToString();		// First digit...
				}
				else if (collectingFormatItemNumber)
				{
					if (char.IsDigit(thisChar))
						numberStr += thisChar.ToString();		// Subsequent digit...
					else
						collectingFormatItemNumber = false;
				}
				length++;
			}
			return formatItems;
		}
		#endregion

		#region ctxInFormatItem_ContextSatisfied
		private void ctxInFormatItem_ContextSatisfied(ContextSatisfiedEventArgs ea)
		{
			ea.Satisfied = InFormatItem(CodeRush.Source.Active, CodeRush.Caret.Line, CodeRush.Caret.Offset);
		}
		#endregion

		#region cpFormatItem_CheckAvailability
		private void cpFormatItem_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			ea.Available = InFormatItem(CodeRush.Source.Active, CodeRush.Caret.Line, CodeRush.Caret.Offset);
		}
		#endregion

		private FormatItemPos GetActivePosition(ApplyContentEventArgs ea)
		{
			int caretLine = ea.Caret.Line;
			int caretOffset = ea.Caret.Offset;

			if (!InFirstStringArgument(ea.Element, caretLine, caretOffset))
				return null;

			FormatItems formatItems = GetFormatItems(ea.Element as PrimitiveExpression);
			return formatItems.GetFormatItemPosAtPos(caretLine, caretOffset);
		}
		#region GetFormatItemDetails
		private static void GetFormatItemDetails(TextDocument textDocument, SourceRange formatItemRange, out string numberStr, out string alignment, out string format)
		{
			format = textDocument.GetText(formatItemRange);
			alignment = String.Empty;
			numberStr = String.Empty;
			if (format.Length >= 3)
			{
				if (format[format.Length - 1] == '}')
					format = format.Remove(format.Length - 1, 1);
				if (format[0] == '{')
					format = format.Remove(0, 1);
				while (format.Length > 0 && char.IsDigit(format[0]))
				{
					numberStr += format[0];
					format = format.Remove(0, 1);
				}
				if (format.StartsWith(","))
				{
					format = format.Remove(0, 1);
					if (format.StartsWith("-"))
					{
						format = format.Remove(0, 1);
						alignment = "-";
					}
					// Remove alignment text...
					while (format.Length > 0 && char.IsDigit(format[0]))
					{
						alignment += format[0];
						format = format.Remove(0, 1);
					}
				}
				if (format.StartsWith(":"))
					format = format.Remove(0, 1);
			}
		}
		#endregion
		private void cpFormatItem_Apply(object sender, ApplyContentEventArgs ea)
		{
			FormatItemPos activePosition;
			activePosition = GetActivePosition(ea);
			if (activePosition == null)
				return;

			FormatItems formatItems = activePosition.Parent.Parent;

			int line = formatItems.PrimitiveExpression.Range.Start.Line;
			SourceRange formatItemRange = activePosition.GetSourceRange(line);

			string number;
			string alignment;
			string format;
			GetFormatItemDetails(ea.TextDocument, formatItemRange, out number, out alignment, out format);

			using (FrmStringFormatter frmStringFormatter = new FrmStringFormatter())
			{
				if (number != String.Empty)
				{
					int argumentIndex = int.Parse(number) + 1;
					Expression argument = formatItems.ParentMethodCall.Arguments[argumentIndex];
					PrimitiveExpression primitiveExpression = argument as PrimitiveExpression;
					if (primitiveExpression != null)
					{
						// Use primitiveExpression's PrimitiveType property
						PrimitiveType primitiveType = primitiveExpression.PrimitiveType;
						if (primitiveType == PrimitiveType.Single || primitiveType == PrimitiveType.Decimal || primitiveType == PrimitiveType.Double)
							frmStringFormatter.FormatItemExpressionType = FormatItemExpressionType.DateTime;
						else if (primitiveType == PrimitiveType.Char || primitiveType == PrimitiveType.String)
							frmStringFormatter.FormatItemExpressionType = FormatItemExpressionType.String;
						else if (primitiveType == PrimitiveType.SByte || primitiveType == PrimitiveType.Byte || primitiveType == PrimitiveType.Int16 || primitiveType == PrimitiveType.Int32 || primitiveType == PrimitiveType.Int64 || primitiveType == PrimitiveType.UInt16 || primitiveType == PrimitiveType.UInt32 || primitiveType == PrimitiveType.UInt64)
							frmStringFormatter.FormatItemExpressionType = FormatItemExpressionType.Integer;
						else
							frmStringFormatter.FormatItemExpressionType = FormatItemExpressionType.Custom;
					}
					else
					{
						IElement resolve = argument.Resolve(ParserServices.SourceTreeResolver);
						if (resolve.Name == "DateTime")
							frmStringFormatter.FormatItemExpressionType = FormatItemExpressionType.DateTime;
						else if (resolve.Name == "Double" || resolve.Name == "Single")
							frmStringFormatter.FormatItemExpressionType = FormatItemExpressionType.Real;
						else if (resolve.Name == "String")
							frmStringFormatter.FormatItemExpressionType = FormatItemExpressionType.String;
						else if (resolve.Name == "Int32" || resolve.Name == "Int64" || resolve.Name == "Int16")
							frmStringFormatter.FormatItemExpressionType = FormatItemExpressionType.Integer;
						else
						{
							
						}
					}
				}

				frmStringFormatter.FormatString = format;
				frmStringFormatter.AlignmentString = alignment;
				if (frmStringFormatter.ShowDialog() == DialogResult.OK)
				{
					string newFormatItemCode = String.Format("{{{0}{1}:{2}}}", number, frmStringFormatter.AlignmentString, frmStringFormatter.FormatString);
					ea.TextDocument.Replace(formatItemRange, newFormatItemCode, "Format Item");
				}
			}
		}

		private void ipFormatItemIndexTooLarge_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
		{
			IEnumerable<IElement> enumerable = ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.PrimitiveExpression));
			foreach (IElement element in enumerable)
			{
				IPrimitiveExpression iPrimitiveExpression = element as IPrimitiveExpression;
				if (iPrimitiveExpression != null)
				{
					if (iPrimitiveExpression.PrimitiveType == PrimitiveType.String)
					{
						PrimitiveExpression primitiveExpression = LanguageElementRestorer.ConvertToLanguageElement(iPrimitiveExpression) as PrimitiveExpression;
						if (primitiveExpression != null)
						{
							MethodCallExpression methodCallExpression = primitiveExpression.Parent as MethodCallExpression;
							if (IsStringFormatCall(methodCallExpression))
							{
								FormatItems formatItems = GetFormatItems(primitiveExpression);
								if (formatItems.ParentMethodCall != null)
								{
									int argumentCount = formatItems.ParentMethodCall.ArgumentsCount;
									foreach (FormatItem formatItem in formatItems.Values)
									{
										int argumentIndex = formatItem.Id + 1;
										if (argumentIndex >= argumentCount)
										{
											int line = formatItems.PrimitiveExpression.Range.Start.Line;
											foreach (FormatItemPos position in formatItem.Positions)
												ea.AddError(position.GetSourceRange(line), ipFormatItemIndexTooLarge.DisplayName);
										}
									}
								}
							}
						}
					}
				}
			}
		}
	}
}