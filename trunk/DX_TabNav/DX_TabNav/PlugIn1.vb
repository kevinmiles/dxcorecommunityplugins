Option Strict On
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
        CreateTabNav()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Public Sub CreateTabNav()
        Dim TabNav As New DevExpress.CodeRush.Core.Action(components)
        CType(TabNav, System.ComponentModel.ISupportInitialize).BeginInit()
        TabNav.ActionName = "TabNav"
        TabNav.ButtonText = "Tab to End" ' Used if button is placed on a menu.
        TabNav.RegisterInCR = True
        AddHandler TabNav.CheckAvailability, AddressOf TabNav_CheckAvailability
        AddHandler TabNav.Execute, AddressOf TabNav_Execute
        CType(TabNav, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub TabNav_CheckAvailability(ByVal ea As CheckActionAvailabilityEventArgs)
        If Not CodeRush.Language.Active = "Basic" Then
            Exit Sub
        End If
        Dim Block = TryCast(CodeRush.Source.Active, IHasBlock)
        If Block Is Nothing Then
            Exit Sub
        End If
        Dim Element = TryCast(CodeRush.Source.Active, IElement)
        If OnElementName(Element) Then
            Exit Sub
        End If
        If OnElementType(Element) Then
            Exit Sub
        End If
        ea.Available = True
    End Sub
    Private Sub TabNav_Execute(ByVal ea As ExecuteEventArgs)
        Dim Element = TryCast(CodeRush.Source.Active, IElement)
        Dim Line = If(CodeRush.Caret.Line = Element.FirstRange.Start.Line,
                      Element.FirstRange.End.Line,
                      Element.FirstRange.Start.Line)
        CodeRush.Caret.MoveTo(Line, PosOfFirstNonSpaceChar(Line))
    End Sub
    Private Shared Function PosOfFirstNonSpaceChar(ByVal Line As Integer) As Integer
        Dim TheLine As String = CodeRush.Documents.ActiveTextDocument.GetLine(Line)
        If TheLine.Trim = String.Empty Then
            Return 1
        End If
        For Pos As Integer = 0 To TheLine.Length Step 1
            If TheLine.Substring(Pos, 1) <> " " Then
                Return Pos + 1
            End If
        Next
        Return 1
    End Function
    Private Shared Function OnElementName(ByVal Element As IElement) As Boolean
        Return Element.Name <> String.Empty _
            AndAlso Element.FirstNameRange.Contains(CodeRush.Caret.SourcePoint)
    End Function
    Private Shared Function OnElementType(ByVal Element As IElement) As Boolean
        Dim TypedElement = TryCast(Element, IHasType)
        If TypedElement Is Nothing OrElse TypedElement.Type Is Nothing Then
            Return False
        End If
        Return TypedElement.Type.FirstRange.Contains(CodeRush.Caret.SourcePoint)
    End Function
End Class
