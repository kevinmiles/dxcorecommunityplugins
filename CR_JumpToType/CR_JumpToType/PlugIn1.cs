using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_JumpToType
{
	public partial class PlugIn1 : StandardPlugIn
	{
		// I added this _ShowReferencedTypesInObjectBrowser field in case anyone wants to see how to show types in the Object Browser. 
		// Just set this field to true and any types declared in referenced assemblies will appear in the Object Browser instead of 
		// the "from metadata" document view.
		private bool _ShowReferencedTypesInObjectBrowser;

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

		#region GetGenericParameterList
		/// <summary>
		/// Returns a list of generic parameters to the specified IGenericElement implementer.
		/// </summary>
		private static string GetGenericParameterList(IGenericElement generic)
		{
			string genericParamList = string.Empty;
			for (int i = 0; i < generic.TypeParameters.Count; i++)
			{
				ITypeParameter p = generic.TypeParameters[i];
				genericParamList += p.Name + ",";
			}

			genericParamList = genericParamList.TrimEnd(',');
			return genericParamList;
		}
		#endregion
		#region GetSignaturePortion
		/// <summary>
		/// Gets the portion of a full signature based on the specified IElement.
		/// </summary>
		string GetSignaturePortion(IElement element)
		{
			if (element == null)
				return string.Empty;

			IGenericElement generic = element as IGenericElement;
			if (generic == null || generic.TypeParameters.Count == 0)		// Normal type...
				return element.Name;

			// It's a generic type...
			return string.Format("{0}<{1}>", element.Name, GetGenericParameterList(generic));
		}
		#endregion
		#region GetObjectBrowserCompatibleSignature
		/// <summary>
		/// Gets the full type name of the specified type reference. By the way, CodeRush has a GetSignature call, however it forms 
		/// Generic signatures like this: "Dictionary`2". Unfortunately the ObjectBrowser accepts signatures that look like this: 
		/// Dictionary &lt;TKey, TValue&gt;
		/// </summary>
		string GetObjectBrowserCompatibleSignature(IElement element)
		{
			string path = "";
			while (element != null)
			{
				string signaturePart = GetSignaturePortion(element);
				if (signaturePart != String.Empty)
					if (path == String.Empty)
						path = signaturePart;
					else
						path = String.Format("{0}.{1}", signaturePart, path);
				element = element.Parent;
			}
			return path;
		}
		#endregion

		#region BrowseObject(string fullTypeName)
		private static void BrowseObject(string fullTypeName)
		{
			CodeRush.Command.Execute("View.ObjectBrowserSearch", fullTypeName);
			CodeRush.Command.Execute("View.ObjectBrowserFilterToType");
		}
		#endregion
		#region BrowseObject(IElement element)
		private void BrowseObject(IElement element)
		{
			string fullTypeName = GetObjectBrowserCompatibleSignature(element);
			if (string.IsNullOrEmpty(fullTypeName))
				return;
			BrowseObject(fullTypeName);
		}
		#endregion
		#region GoToDefinition
		private static void GoToDefinition(IElement element)
		{
			CodeRush.Markers.Drop(MarkerStyle.Transient);
			IMarker ourTopMarker = CodeRush.Markers.Top;
			CodeRush.Caret.MoveTo(element.FirstNameRange.Start);
			CodeRush.Command.Execute("Edit.GoToDefinition");
			if (ourTopMarker != CodeRush.Markers.Top)
				CodeRush.Markers.Remove(CodeRush.Markers.Top);
		}
		#endregion
		
		private void navJumpToType_Apply(object sender, ApplyContentEventArgs ea)
		{
			LanguageElement activeElement = ea.CodeActive;
			if (activeElement == null)
				return;
			IElement declaration = activeElement.GetDeclaration();
			if (declaration == null)
				return;

			IHasType iHasType = declaration as IHasType;
			if (iHasType == null)
				return;

			ITypeReferenceExpression type = iHasType.Type;
			if (type == null)
				return;

			// Note to folks who watched this plug-in built during the webinar. At this point we were wondering why type.InReferencedAssembly 
			// was always false. The reason is that at the time we stepped through, the "type" variable represented a type reference located 
			// inside the active file (e.g., the "SolidBrush" of the declaration "SolidBrush orangeBrush..."). To see if the type is really 
			// in a referenced assembly, use the following two lines of code...

			ITypeElement iTypeDeclaration = type.GetDeclaration() as ITypeElement;
			if (iTypeDeclaration != null && iTypeDeclaration.InReferencedAssembly)
			{
				// Now we know the type is declared in a referenced assembly.
				if (_ShowReferencedTypesInObjectBrowser)
				{
					BrowseObject(iTypeDeclaration);
					return;
				}
			}

			GoToDefinition(type);
		}

		private void navJumpToType_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			LanguageElement activeElement = ea.CodeActive;
			if (activeElement == null)
				return;
			IElement declaration = activeElement.GetDeclaration();
			if (declaration == null)
				return;
			IHasType iHasType = declaration as IHasType;
			ea.Available = iHasType != null && iHasType.Type != null;
		}
	}
}