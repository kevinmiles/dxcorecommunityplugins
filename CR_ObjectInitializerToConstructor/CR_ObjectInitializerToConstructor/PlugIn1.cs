using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core.Replacement;

namespace CR_ObjectInitializerToConstructor
{
	public partial class PlugIn1 : StandardPlugIn
	{
		private static ObjectInitializerExpression _InitializerExpression;
    private string _NewCall;
		private SourceRange _DeleteRange;
		private FileChangeCollection _Changes;
		private static ObjectCreationExpression _ObjectCreationExpression;
    private static ITypeElement _TypeElement;
		private bool _WaitingForViewActivate;
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

		private void targetPicker1_TargetSelected(object sender, TargetSelectedEventArgs ea)
		{
			ElementBuilder elementBuilder = new ElementBuilder();

			IElementCollection existingDefaultConstructors = ParserServices.SourceTreeResolver.FindConstructors(_TypeElement, new ExpressionCollection());
			bool missingDefaultConstructor = existingDefaultConstructors == null || existingDefaultConstructors.Count == 0;
			if (missingDefaultConstructor)
			{
				Method defaultConstructor = elementBuilder.BuildConstructor(null);
				defaultConstructor.Visibility = MemberVisibility.Public;
				defaultConstructor.Name = _TypeElement.Name;
				elementBuilder.AddNode(null, defaultConstructor);
			}
			Method constructor = elementBuilder.BuildConstructor(null);
			constructor.Visibility = MemberVisibility.Public;
			constructor.Name = _TypeElement.Name;
			elementBuilder.AddNode(null, constructor);

			foreach (Expression initializer in _InitializerExpression.Initializers)
			{
				MemberInitializerExpression memberInitializerExpression = initializer as MemberInitializerExpression;
				if (memberInitializerExpression == null)
					continue;

				// Using the FormatParamName StringProvider...
				string parameterName = CodeRush.Strings.Get("FormatParamName", memberInitializerExpression.Name);
				IElement resolve = memberInitializerExpression.Resolve(ParserServices.SourceTreeResolver);
				if (resolve != null)
				{
					Param param = new Param(resolve.Name, parameterName);
					constructor.Parameters.Add(param);

					Assignment assignment = new Assignment();
					ElementReferenceExpression leftSide = new ElementReferenceExpression(memberInitializerExpression.Name);
					if (CodeRush.Language.IdentifiersMatch(memberInitializerExpression.Name, parameterName))
					{
						string selfQualifier = CodeRush.Language.GenerateElement(new ThisReferenceExpression());
						leftSide = new ElementReferenceExpression(selfQualifier + "." + memberInitializerExpression.Name);
					}
					assignment.LeftSide = leftSide;
					assignment.Expression = new ElementReferenceExpression(parameterName);
					constructor.AddNode(assignment);
				}
			}
			string newConstructorCode = elementBuilder.GenerateCode();
			// Use FileChange for multi-file changes...
			FileChange newConstructor = new FileChange();
			newConstructor.Path = _TypeElement.FirstFile.Name;
			newConstructor.Range = new SourceRange(ea.Location.SourcePoint, ea.Location.SourcePoint);
			newConstructor.Text = newConstructorCode;
			_Changes.Add(newConstructor);

			DevExpress.DXCore.TextBuffers.ICompoundAction action = CodeRush.TextBuffers.NewMultiFileCompoundAction("Create Constructor from Initializer");
			try
			{
				CodeRush.File.ApplyChanges(_Changes, true, false);
			}
			finally
			{
				action.Close();
				_Changes.Clear();
			}
		}

		private static bool IsInsideInitializerToTypeWithSource(CheckContentAvailabilityEventArgs ea)
		{
			_TypeElement = null;
			_ObjectCreationExpression = ea.Element as ObjectCreationExpression;
			if (_ObjectCreationExpression == null)
			{
				_ObjectCreationExpression = ea.Element.GetParent(LanguageElementType.ObjectCreationExpression) as ObjectCreationExpression;
				if (_ObjectCreationExpression == null)
					return false;
			}
			_InitializerExpression = _ObjectCreationExpression.ObjectInitializer;
			if (_InitializerExpression == null)
				return false;
			_TypeElement = _ObjectCreationExpression.ObjectType.Resolve(ParserServices.SourceTreeResolver) as ITypeElement;
			if (_TypeElement == null)
				return false;

			return !_TypeElement.InReferencedAssembly;
		}

		private static bool HasMatchingConstructors(ObjectInitializerExpression initializer, ITypeElement typeElement)
		{
			IElementCollection foundConstructors = ParserServices.SourceTreeResolver.FindConstructors(typeElement, initializer.Initializers);
			return foundConstructors != null && foundConstructors.Count > 0;
		}

		private void cdeCreateConstructorCall_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			if (!IsInsideInitializerToTypeWithSource(ea))
				return;

			if (!HasMatchingConstructors(_ObjectCreationExpression.ObjectInitializer, _TypeElement))
			{
				ea.Available = true;
				_NewCall = GetNewConstructorCall(_ObjectCreationExpression, _TypeElement);
				_DeleteRange = _ObjectCreationExpression.Range;
			}
		}

		private void StartTargetPicker(TextDocument textDocument)
		{
			TextView activeView = textDocument.ActiveView;
			if (activeView == null)
			{
				if (textDocument.FirstView != null)
				{
					textDocument.Activate();
					activeView = textDocument.FirstView;
					activeView.Activate();
				}
			}

			// IElement -- Liteweight elements for representing source code, including referenced assemblies.
			// LanguageElement -- Heavier, bigger elements
			// We can convert from liteweight to heavy using LanguageElementRestorer.ConvertToLanguageElement();

			Class typeElement = LanguageElementRestorer.ConvertToLanguageElement(_TypeElement) as Class;
			if (typeElement == null)
			{
				// We just opened the file, and we need to get a new resolve on the _TypeElement.
				_TypeElement = _ObjectCreationExpression.ObjectType.Resolve(ParserServices.SourceTreeResolver) as ITypeElement;
				typeElement = LanguageElementRestorer.ConvertToLanguageElement(_TypeElement) as Class;
				if (typeElement == null)
					return;
			}
			targetPicker1.Start(activeView, typeElement.FirstChild, InsertCode.UsePicker, null);
		}

		private void cdeCreateConstructorCall_Apply(object sender, ApplyContentEventArgs ea)
		{
			FileChange newFileChange = new FileChange();
			newFileChange.Path = CodeRush.Documents.ActiveTextDocument.FileNode.Name;
			newFileChange.Range = _DeleteRange;
			newFileChange.Text = _NewCall;
			CodeRush.Markers.Drop(_DeleteRange.Start, MarkerStyle.Transient);
			if (_Changes == null)
				_Changes = new FileChangeCollection();
			else
				_Changes.Clear();
			try
			{
				_Changes.Add(newFileChange);
			}
			catch (Exception ex)
			{

			}

			TextDocument textDocument = CodeRush.Documents.Get(_TypeElement.FirstFile.Name) as TextDocument;
			if (textDocument == null)
			{
				_WaitingForViewActivate = true;
				textDocument = CodeRush.File.Activate(_TypeElement.FirstFile.Name) as TextDocument;
				return;
			}

			StartTargetPicker(textDocument);
		}

		private string GetNewConstructorCall(ObjectCreationExpression objectCreationWithInitializer, ITypeElement type)
		{
			string result = String.Empty;
			if (type == null || objectCreationWithInitializer == null || objectCreationWithInitializer.ObjectInitializer == null)
				return result;

			ExpressionCollection arguments = objectCreationWithInitializer.ObjectInitializer.Initializers;
			ExpressionCollection newArgs = new ExpressionCollection();
			foreach (Expression argument in arguments)
			{
				MemberInitializerExpression memberInitializerExpression = argument as MemberInitializerExpression;
				if (memberInitializerExpression == null)
					continue;

				newArgs.Add(memberInitializerExpression.Value);
			}

			ObjectCreationExpression newObjectCreationExpression = new ObjectCreationExpression(new TypeReferenceExpression(type.Name), newArgs);
			if (newObjectCreationExpression != null)
				result = CodeRush.Language.GenerateElement(newObjectCreationExpression);
			return result;
		}

		private void refConvertToConstructorCall_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			if (!IsInsideInitializerToTypeWithSource(ea))
				return;

			if (HasMatchingConstructors(_ObjectCreationExpression.ObjectInitializer, _TypeElement))
			{
				ea.Available = true;
				_NewCall = GetNewConstructorCall(_ObjectCreationExpression, _TypeElement);
				_DeleteRange = _ObjectCreationExpression.Range;
			}
		}

		private void refConvertToConstructorCall_Apply(object sender, ApplyContentEventArgs ea)
		{
			ea.TextDocument.Replace(_DeleteRange, _NewCall, "Convert Initializer to Constructor");
		}

		private void refConvertToConstructorCall_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
		{
			ea.AddCodePreview(_DeleteRange.Start, _NewCall);
			ea.AddStrikethrough(_DeleteRange);
		}

		private void cdeCreateConstructorCall_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
		{
			ea.AddCodePreview(_DeleteRange.Start, _NewCall);
			ea.AddStrikethrough(_DeleteRange);				
		}

		private void PlugIn1_TextViewActivated(TextViewEventArgs ea)
		{
			if (_WaitingForViewActivate)
			{
				_WaitingForViewActivate = false;
				TextDocument activeTextDocument = CodeRush.Documents.ActiveTextDocument;
				if (activeTextDocument != null)
					StartTargetPicker(activeTextDocument);
			}
		}
	}
}