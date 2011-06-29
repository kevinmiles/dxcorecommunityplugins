using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_Dispos_o_matic
{
	public partial class PlugIn1 : StandardPlugIn
	{
		private TypeDeclaration _ActiveClass;
    private string _Code;
		private TextDocument _TextDocument;
    private SourcePoint _InsertionPoint;
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

		private void cpImplementIDisposable_Apply(object sender, ApplyContentEventArgs ea)
		{
			_ActiveClass = ea.ClassInterfaceOrStruct as TypeDeclaration;
			if (_ActiveClass == null)
				return;
			_TextDocument = ea.TextDocument;

			ElementBuilder elementBuilder = new ElementBuilder();
			Method disposeMethod = elementBuilder.AddMethod(null, null, "Dispose");
			// If implicit interface implementation is supported by the language?
			disposeMethod.Visibility = MemberVisibility.Public;
			Expression newCollection = new ElementReferenceExpression("IDisposable.Dispose");
			disposeMethod.AddImplementsExpression(newCollection);
			elementBuilder.AddMethodCall(disposeMethod, "Dispose", new string[] { "true" });
			string thisReference = CodeRush.Language.GenerateElement(new ThisReferenceExpression());
			elementBuilder.AddMethodCall(disposeMethod, "GC.SuppressFinalize", new string[] { thisReference });


			Method virtualDisposeMethod = elementBuilder.AddMethod(null, null, "Dispose");
			virtualDisposeMethod.Visibility = MemberVisibility.Protected;
			virtualDisposeMethod.IsVirtual = true;
			string typeName = CodeRush.Language.GetSimpleTypeName("System.Boolean");
			virtualDisposeMethod.AddParameter(new Param(typeName, "disposing"));

			If ifDisposingBlock = new If();
			ifDisposingBlock.Expression = new ElementReferenceExpression("disposing");
			virtualDisposeMethod.AddNode(ifDisposingBlock);

			foreach (BaseVariable field in _ActiveClass.AllFields)
			{
				if (field.Is("System.IDisposable"))
				{
					If ifFieldIsAssignedBlock = new If();
					ifFieldIsAssignedBlock.Expression = CodeRush.Language.GetNullCheck(field.Name).Invert();
					ifDisposingBlock.AddNode(ifFieldIsAssignedBlock);
					elementBuilder.AddMethodCall(ifFieldIsAssignedBlock, field.Name + CodeRush.Language.MemberAccessOperator + "Dispose");
					elementBuilder.AddAssignment(ifFieldIsAssignedBlock, field.Name, CodeRush.Language.GetNullReferenceExpression());
				}
			}

			_Code = elementBuilder.GenerateCode(_TextDocument.Language);

			if (_ActiveClass.FirstChild == null)
			{
				SourcePoint startPt = SourcePoint.Empty;
				CodeRush.Language.GetCodeBlockStart(_ActiveClass, ref startPt);
				_InsertionPoint = startPt;
				GenerateCode();
			}
			else
			{
				targetPicker1.Code = _Code;
				LanguageElement target = _ActiveClass.FirstChild;
				foreach (Method method in _ActiveClass.AllMethods)
				{
					target = method;
					break;
				}
				targetPicker1.Start(ea.TextView, target, InsertCode.UsePicker);
			}
		}

		private void GenerateCode()
		{
			using (_TextDocument.NewCompoundAction("Implement IDisposable"))
			{
				CodeRush.Source.ImplementInterface(_ActiveClass, new Interface("IDisposable"));
				SourceRange insertedRange = _TextDocument.InsertText(_InsertionPoint, _Code);
				insertedRange.End.Line++;
				_TextDocument.Format(insertedRange);
			}
		}
		private void cpImplementIDisposable_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			Class activeClass = ea.ClassInterfaceOrStruct as Class;
			if (activeClass == null)
				return;
			if (ea.Caret.Line == activeClass.Range.Start.Line)
			{
				ITypeElement[] baseTypes = activeClass.GetBaseTypes();
				foreach (ITypeElement item in baseTypes)
					if (item.Name == "IDisposable")
						return;
				ea.Available = true;
			}
		}

		private void targetPicker1_TargetSelected(object sender, TargetSelectedEventArgs ea)
		{
			_InsertionPoint = ea.Location.SourcePoint;
			GenerateCode();
		}
	}
}