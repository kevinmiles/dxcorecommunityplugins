Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class PlugIn1

	'DXCore-generated code...
#Region " InitializePlugIn "
	Public Overrides Sub InitializePlugIn()
		MyBase.InitializePlugIn()
        Call CreateXMLAttributeAndValueSearcher()
    End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region

#Region "Utility"
    Private Function IsOnHTMLXMLAttribute(ByRef Attribute As HtmlAttribute) As Boolean
        Dim ActiveElement As LanguageElement = TryCast(CodeRush.Source.Active, HtmlAttribute)
        If ActiveElement Is Nothing Then
            Return False
        End If
        Dim FoundAttribute As Boolean = ActiveElement.ElementType = LanguageElementType.HtmlAttribute
        If FoundAttribute Then
            Attribute = ActiveElement
        End If
        Return FoundAttribute
    End Function
#End Region
#Region "Attribute Context"
    'Private Sub CreateOnHTMLXMLAttribute()
    '    Dim OnHTMLXMLAttribute = New DevExpress.CodeRush.Extensions.ContextProvider(Me.components)
    '    CType(OnHTMLXMLAttribute, System.ComponentModel.ISupportInitialize).BeginInit()
    '    OnHTMLXMLAttribute.Description = "OnHTMLXMLAttribute"
    '    OnHTMLXMLAttribute.ProviderName = "OnHTMLXMLAttribute" ' Needs to be Unique
    '    OnHTMLXMLAttribute.Register = True
    '    AddHandler OnHTMLXMLAttribute.ContextSatisfied, AddressOf OnHTMLXMLAttribute_ContextSatisfied
    '    CType(OnHTMLXMLAttribute, System.ComponentModel.ISupportInitialize).EndInit()
    'End Sub
    'Private Sub OnHTMLXMLAttribute_ContextSatisfied(ByVal ea As DevExpress.CodeRush.Core.ContextSatisfiedEventArgs)
    '    Dim FoundAttribute As HtmlAttribute = Nothing
    '    ea.Satisfied = IsOnHTMLXMLAttribute(FoundAttribute)
    'End Sub
#End Region
    Private Sub CreateXMLAttributeAndValueSearcher()
        Dim XMLAttributeAndValueSearcher = New DevExpress.CodeRush.Core.SearchProvider()
        CType(XMLAttributeAndValueSearcher, System.ComponentModel.ISupportInitialize).BeginInit()
        XMLAttributeAndValueSearcher.Description = "XMLAttributeAndValueSearcher"
        XMLAttributeAndValueSearcher.ProviderName = "XMLAttributeAndValueSearcher" ' Needs to be Unique
        XMLAttributeAndValueSearcher.Register = True
        XMLAttributeAndValueSearcher.UseForNavigation = True
        AddHandler XMLAttributeAndValueSearcher.SearchReferences, AddressOf XMLAttributeAndValueSearcher_SearchReferences
        AddHandler XMLAttributeAndValueSearcher.CheckAvailability, AddressOf XMLAttributeAndValueSearcher_CheckAvailability
        CType(XMLAttributeAndValueSearcher, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub XMLAttributeAndValueSearcher_CheckAvailability(ByVal sender As Object, ByVal ea As CheckSearchAvailabilityEventArgs)
        Dim FoundAttribute As HtmlAttribute = Nothing
        ea.Available = IsOnHTMLXMLAttribute(FoundAttribute)
    End Sub
    Private Sub XMLAttributeAndValueSearcher_SearchReferences(Sender As Object, ByVal ea As DevExpress.CodeRush.Core.SearchEventArgs)
        ' This event fires whenever the a Search is performed.
        Dim FoundAttribute As HtmlAttribute = Nothing
        If Not IsOnHTMLXMLAttribute(FoundAttribute) Then
            Exit Sub
        End If
        Dim HTMLTag As DevExpress.CodeRush.StructuralParser.HtmlElement = FoundAttribute.Parent
        If HTMLTag Is Nothing Then
            Exit Sub
        End If
        For Each Attribute In HTMLTag.Attributes.ToArray
            ea.AddRange(Attribute.FileNode, Attribute.NameRange)
            ea.AddRange(Attribute.FileNode, Attribute.ValueRange)
        Next
    End Sub

End Class
