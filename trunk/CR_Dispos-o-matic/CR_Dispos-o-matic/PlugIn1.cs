using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_Dispos_o_matic
{
	public partial class PlugIn1 : StandardPlugIn
	{
		private TypeDeclaration _ActiveClass;
		private string _CodeForNewIfDisposingBlock;
		private SourceRange _OldIfDisposingBlockRange;
    private string _DisposableImplementationCode;
		private TextDocument _TextDocument;
		private SourcePoint _InsertionPoint;
		// DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();

			// Tie the code provider to the code issue:
			cpImplementIDisposable.CodeIssueMessage = ipClassShouldImplementIDisposable.DisplayName;
			cpDisposeFields.CodeIssueMessage = ipFieldsShouldBeDisposed.DisplayName;
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

		private static PrimitiveExpression GetBooleanLiteral(bool booleanValue)
		{
			PrimitiveExpression primitive = new PrimitiveExpression("");
			primitive.PrimitiveType = PrimitiveType.Boolean;
			primitive.PrimitiveValue = booleanValue;
			return primitive;
		}

		#region AddDisposeImplementer
		private static void AddDisposeImplementer(ElementBuilder elementBuilder)
		{
			Method disposeMethod = elementBuilder.AddMethod(null, null, "Dispose");
			// If implicit interface implementation is supported by the language?
			disposeMethod.Visibility = MemberVisibility.Public;
			Expression newCollection = new ElementReferenceExpression("IDisposable.Dispose");
			disposeMethod.AddImplementsExpression(newCollection);
			PrimitiveExpression booleanTrue = GetBooleanLiteral(true);
			ExpressionCollection argumentsCollection = new ExpressionCollection();
			argumentsCollection.Add(booleanTrue);
			elementBuilder.AddMethodCall(disposeMethod, "Dispose", argumentsCollection, null);
			string thisReference = CodeRush.Language.GenerateElement(new ThisReferenceExpression());
			elementBuilder.AddMethodCall(disposeMethod, "GC.SuppressFinalize", new string[] { thisReference });
		}
		#endregion
		#region AddFieldDisposeBlock
		private static void AddFieldDisposeBlock(ElementBuilder elementBuilder, If parentIfDisposingBlock, BaseVariable field)
		{
			If ifFieldIsAssignedBlock = new If();
			ifFieldIsAssignedBlock.Expression = CodeRush.Language.GetNullCheck(field.Name).Invert();
			elementBuilder.AddMethodCall(ifFieldIsAssignedBlock, field.Name + CodeRush.Language.MemberAccessOperator + "Dispose");
			elementBuilder.AddAssignment(ifFieldIsAssignedBlock, field.Name, CodeRush.Language.GetNullReferenceExpression());
			parentIfDisposingBlock.AddNode(ifFieldIsAssignedBlock);
		}
		#endregion
		#region AddVirtualDisposeMethod
		private void AddVirtualDisposeMethod(ElementBuilder elementBuilder)
		{
			Method virtualDisposeMethod = elementBuilder.AddMethod(null, null, "Dispose");
			virtualDisposeMethod.Visibility = MemberVisibility.Protected;
			virtualDisposeMethod.IsVirtual = true;
			string typeName = CodeRush.Language.GetSimpleTypeName("System.Boolean");
			virtualDisposeMethod.AddParameter(new Param(typeName, "disposing"));

			If ifDisposingBlock = new If();
			ifDisposingBlock.Expression = new ElementReferenceExpression("disposing");
			virtualDisposeMethod.AddNode(ifDisposingBlock);

			// Dispose fields individually...
			foreach (BaseVariable field in _ActiveClass.AllFields)
				if (field.Is("System.IDisposable"))
					AddFieldDisposeBlock(elementBuilder, ifDisposingBlock, field);
		}
		#endregion
		#region cpImplementIDisposable_Apply
		private void cpImplementIDisposable_Apply(object sender, ApplyContentEventArgs ea)
		{
			_ActiveClass = ea.ClassInterfaceOrStruct as TypeDeclaration;
			if (_ActiveClass == null)
				return;
			_TextDocument = ea.TextDocument;

			ElementBuilder elementBuilder = new ElementBuilder();

			AddDisposeImplementer(elementBuilder);
			AddVirtualDisposeMethod(elementBuilder);

			_DisposableImplementationCode = elementBuilder.GenerateCode(_TextDocument.Language);

			if (_ActiveClass.FirstChild == null)
			{
				SourcePoint startPt = SourcePoint.Empty;
				CodeRush.Language.GetCodeBlockStart(_ActiveClass, ref startPt);
				_InsertionPoint = startPt;
				GenerateIDisposableImplementationCode();
			}
			else
			{
				targetPicker1.Code = _DisposableImplementationCode;
				LanguageElement target = _ActiveClass.FirstChild;
				foreach (Method method in _ActiveClass.AllMethods)
				{
					target = method;
					break;
				}
				targetPicker1.Start(ea.TextView, target, InsertCode.UsePicker);
			}
		}
		#endregion

		#region GenerateIDisposableImplementationCode
		private void GenerateIDisposableImplementationCode()
		{
			using (_TextDocument.NewCompoundAction("Implement IDisposable"))
			{
				
				SourceRange insertedRange = _TextDocument.InsertText(_InsertionPoint, _DisposableImplementationCode);
				insertedRange.End.Line++;
				_TextDocument.Format(insertedRange);
        CodeRush.Source.ImplementInterface(_ActiveClass, new Interface("IDisposable"));
        CodeRush.Source.DeclareNamespaceReference("System");
			}
		}
		#endregion
		private void cpImplementIDisposable_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			Class activeClass = ea.ClassInterfaceOrStruct as Class;
			if (activeClass == null || activeClass.IsStatic)
				return;
			if (ea.Caret.Line == activeClass.Range.Start.Line)
				ea.Available = !AlreadyImplementsIDisposable(activeClass);
		}

		#region targetPicker1_TargetSelected
		private void targetPicker1_TargetSelected(object sender, TargetSelectedEventArgs ea)
		{
			_InsertionPoint = ea.Location.SourcePoint;
			GenerateIDisposableImplementationCode();
		}
		#endregion

		private static IList<BaseVariable> GetDisposableFieldsThatHaveNotBeenDisposed(ISourceFile scope, IClassElement iClassElement, out IIfStatement parentIfDisposing)
		{
			parentIfDisposing = null;
			IList<BaseVariable> disposableFields = new List<BaseVariable>();

			foreach (IElement child in iClassElement.AllChildren)
			{
        if (child.FirstFile == null)
          continue;

        // fix for partial classes
        if (child.FirstFile.Name != scope.Name)
          continue;

				IBaseVariable iBaseVariable = child as IBaseVariable;
				if (iBaseVariable != null)
				{
					BaseVariable baseVariable = LanguageElementRestorer.ConvertToLanguageElement(iBaseVariable) as BaseVariable;
					if (baseVariable == null)
						continue;

					if (baseVariable.Is("System.IDisposable"))
						disposableFields.Add(baseVariable);
				}
				else if (parentIfDisposing == null)
				{
					IMethodElement iMethodElement = child as IMethodElement;
					if (iMethodElement != null && iMethodElement.Name == "Dispose" && iMethodElement.IsVirtual && iMethodElement.Parameters.Count == 1)
					{
						foreach (IElement potentialStatement in iMethodElement.AllChildren)
						{
							IIfStatement iIfStatement = potentialStatement as IIfStatement;
							if (iIfStatement != null && iIfStatement.Condition != null && iIfStatement.Condition.Name == iMethodElement.Parameters[0].Name)
							{
								// We have found the "if (disposing)" block of code!
								parentIfDisposing = iIfStatement;
								break;
							}
						}
					}
				}
			}
			// Now we should have disposableFields and parentIfDisposing properly filled out.
			if (disposableFields.Count > 0 && parentIfDisposing != null)
			{
				foreach (IElement potentialChildIf in parentIfDisposing.AllChildren)
				{
					IIfStatement fieldNotNullStatement = potentialChildIf as IIfStatement;
					if (fieldNotNullStatement != null)
					{
						IRelationalOperationExpression iRelationalOperationExpression = fieldNotNullStatement.Condition as IRelationalOperationExpression;
						if (iRelationalOperationExpression != null)
						{
							foreach (BaseVariable disposableField in disposableFields)
							{
								if (disposableField.Name == iRelationalOperationExpression.LeftSide.Name || disposableField.Name == iRelationalOperationExpression.RightSide.Name)
								{
									// Reasonably safe to assume we have proper dispose code. HOWEVER... you could continue to check for a Dispose call in the children.
									disposableFields.Remove(disposableField);		// Why remove it? Because it is disposed!
									break;
								}
							}

						}
					}
				}
			}
			return disposableFields;
		}
		private void ipFieldsShouldBeDisposed_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
		{
			IEnumerable<IElement> enumerable = ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.Class));
			foreach (IElement element in enumerable)
			{
				IClassElement iClassElement = element as IClassElement;
				if (iClassElement == null)
					continue;

				if (!AlreadyImplementsIDisposable(iClassElement))
					continue;

				// We DO implement IDisposable! Let's make sure all the fields are disposed....

				IIfStatement parentIfDisposing;
        IList<BaseVariable> disposableFields = GetDisposableFieldsThatHaveNotBeenDisposed(ea.Scope as ISourceFile, iClassElement, out parentIfDisposing);
				if (disposableFields.Count > 0)
					foreach (BaseVariable disposableField in disposableFields)
						ea.AddWarning(disposableField.NameRange, ipFieldsShouldBeDisposed.DisplayName);
			}
		}

		#region AlreadyImplementsIDisposable
		private static bool AlreadyImplementsIDisposable(IClassElement iClassElement)
		{
			ITypeElement[] baseTypes = iClassElement.GetBaseTypes();
			foreach (ITypeElement item in baseTypes)
				if (item.Is("System.IDisposable"))
					return true;
			return false;
		}
		#endregion
		private void ipClassShouldImplementIDisposable_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
		{
			IEnumerable<IElement> enumerable = ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.Class));
			foreach (IElement element in enumerable)
			{
				IClassElement iClassElement = element as IClassElement;
				if (iClassElement == null)
					continue;

				if (AlreadyImplementsIDisposable(iClassElement))
					continue;

				// We do NOT implement IDisposable! Let's see if any of the fields implement IDisposable....

				foreach (IElement child in iClassElement.AllChildren)
				{
					IBaseVariable iBaseVariable = child as IBaseVariable;
					if (iBaseVariable == null)
						continue;

					BaseVariable baseVariable = LanguageElementRestorer.ConvertToLanguageElement(iBaseVariable) as BaseVariable;
					if (baseVariable == null)
						continue;

					if (baseVariable.Is("System.IDisposable"))
					{
						// Holy cow! We are available!
						ea.AddWarning(iClassElement.FirstNameRange, ipClassShouldImplementIDisposable.DisplayName);
						break;
					}
				}
			}
		}

		private void cpDisposeFields_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			IClassElement iClassElement = ea.ClassInterfaceOrStruct as IClassElement;
			if (iClassElement == null)
				return;

			if (!AlreadyImplementsIDisposable(iClassElement))
				return;

			// We DO implement IDisposable! Let's make sure all the fields are disposed....

			IIfStatement parentIfDisposing;
			IList<BaseVariable> disposableFields = GetDisposableFieldsThatHaveNotBeenDisposed(ea.ClassInterfaceOrStruct.GetSourceFile(), iClassElement, out parentIfDisposing);
			if (disposableFields.Count > 0 && parentIfDisposing != null)
			{
				If ifParent = LanguageElementRestorer.ConvertToLanguageElement(parentIfDisposing) as If;
				if (ifParent != null)
				{
					ea.Available = true;
					ElementBuilder elementBuilder = new ElementBuilder();
					If newIfStatement = ifParent.Clone() as If;
					elementBuilder.AddNode(null, newIfStatement);
					foreach (BaseVariable disposableField in disposableFields)
						AddFieldDisposeBlock(elementBuilder, newIfStatement, disposableField);
					_CodeForNewIfDisposingBlock = elementBuilder.GenerateCode();
					_OldIfDisposingBlockRange = ifParent.Range;
				}
			}
		}

		private void cpDisposeFields_Apply(object sender, ApplyContentEventArgs ea)
		{
			ea.TextDocument.QueueDelete(_OldIfDisposingBlockRange);
			ea.TextDocument.QueueInsert(_OldIfDisposingBlockRange.Top, _CodeForNewIfDisposingBlock);
			ea.TextDocument.ApplyQueuedEdits("Dispose Fields", true);
		}

		private void cpDisposeFields_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
		{
			ea.AddCodePreview(_OldIfDisposingBlockRange.Top, _CodeForNewIfDisposingBlock);
			ea.AddStrikethrough(_OldIfDisposingBlockRange);
		}
	}
}