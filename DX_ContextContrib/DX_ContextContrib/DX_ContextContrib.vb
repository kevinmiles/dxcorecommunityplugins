Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Text.RegularExpressions

Namespace DX_ContextContrib
    ''' <summary>
    ''' Summary description for DX_ContextContrib.
    ''' </summary>
    Public Class DX_ContextContrib
        Inherits StandardPlugIn
        Friend WithEvents ctxClipboardContainsMultiLineText As DevExpress.CodeRush.Extensions.ContextProvider
        Friend WithEvents ctxClipboardContainsLiteral As DevExpress.CodeRush.Extensions.ContextProvider
        Friend WithEvents ctxClipboardContainsRegEx As DevExpress.CodeRush.Extensions.ContextProvider
#Region "Standard Plugin Goodness :)"
#Region " private fields... "
        Private components As System.ComponentModel.IContainer
#End Region

        ' constructor...
#Region " DX_ContextContrib "
        ''' <summary>
        ''' Required for Windows.Forms Class Composition Designer support
        ''' </summary>
        Public Sub New()
            InitializeComponent()
        End Sub
#End Region

        ' CodeRush-generated code
#Region " InitializePlugIn "
        Public Overrides Sub InitializePlugIn()
            MyBase.InitializePlugIn()

            '
            ' TODO: Add your initialization code here.
            ''
        End Sub
#End Region
#Region " FinalizePlugIn "
        Public Overrides Sub FinalizePlugIn()
            '
            ' TODO: Add your finalization code here.
            '

            MyBase.FinalizePlugIn()
        End Sub
#End Region

#Region " Component Designer generated code "
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Friend WithEvents ctxSelectionHasBalancedParens As DevExpress.CodeRush.Extensions.ContextProvider
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim Parameter1 As DevExpress.CodeRush.Core.Parameter = New DevExpress.CodeRush.Core.Parameter(New DevExpress.CodeRush.Core.StringParameterType)
            Dim Parameter2 As DevExpress.CodeRush.Core.Parameter = New DevExpress.CodeRush.Core.Parameter(New DevExpress.CodeRush.Core.StringParameterType)
            Me.ctxClipboardContainsMultiLineText = New DevExpress.CodeRush.Extensions.ContextProvider(Me.components)
            Me.ctxClipboardContainsLiteral = New DevExpress.CodeRush.Extensions.ContextProvider(Me.components)
            Me.ctxClipboardContainsRegEx = New DevExpress.CodeRush.Extensions.ContextProvider(Me.components)
            Me.ctxSelectionHasBalancedParens = New DevExpress.CodeRush.Extensions.ContextProvider(Me.components)
            CType(Me.ctxClipboardContainsMultiLineText, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ctxClipboardContainsLiteral, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ctxClipboardContainsRegEx, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ctxSelectionHasBalancedParens, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'ctxClipboardContainsMultiLineText
            '
            Me.ctxClipboardContainsMultiLineText.Description = "True if the Clipboard text contains and CR or LF characters"
            Me.ctxClipboardContainsMultiLineText.DisplayName = "isMultiLine"
            Me.ctxClipboardContainsMultiLineText.ProviderName = "Editor\Clipboard\isMultiline"
            Me.ctxClipboardContainsMultiLineText.Register = True
            '
            'ctxClipboardContainsLiteral
            '
            Me.ctxClipboardContainsLiteral.Description = "True if the Clipboard contains the specified literal value."
            Me.ctxClipboardContainsLiteral.DisplayName = "Clipboard Contains (Literal)"
            Parameter1.DefaultValue = ""
            Parameter1.Description = "Text to Find"
            Parameter1.Name = "Text"
            Parameter1.Optional = False
            Me.ctxClipboardContainsLiteral.Parameters.Add(Parameter1)
            Me.ctxClipboardContainsLiteral.ProviderName = "Editor\Clipboard\Contains Literal"
            Me.ctxClipboardContainsLiteral.Register = True
            '
            'ctxClipboardContainsRegEx
            '
            Me.ctxClipboardContainsRegEx.Description = "True if text matching the provided RegEx is found within the Clipboard text."
            Me.ctxClipboardContainsRegEx.DisplayName = "Clipboard Contains (RegEx)"
            Parameter2.DefaultValue = ""
            Parameter2.Description = "Regular Expression used to match on text in the clipboard"
            Parameter2.Name = "RegEx"
            Parameter2.Optional = False
            Me.ctxClipboardContainsRegEx.Parameters.Add(Parameter2)
            Me.ctxClipboardContainsRegEx.ProviderName = "Editor\Clipboard\Contains RegEx"
            Me.ctxClipboardContainsRegEx.Register = True
            '
            'ctxSelectionHasBalancedParens
            '
            Me.ctxSelectionHasBalancedParens.Description = ""
            Me.ctxSelectionHasBalancedParens.DisplayName = "Selection has balanced Parens"
            Me.ctxSelectionHasBalancedParens.ProviderName = "Editor\Selection\Has Balanced Parens"
            Me.ctxSelectionHasBalancedParens.Register = True
            CType(Me.ctxClipboardContainsMultiLineText, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ctxClipboardContainsLiteral, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ctxClipboardContainsRegEx, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ctxSelectionHasBalancedParens, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
#End Region
#End Region

        Private Sub ctxClipboardContainsLiteral_ContextSatisfied(ByVal ea As DevExpress.CodeRush.Core.ContextSatisfiedEventArgs) Handles ctxClipboardContainsLiteral.ContextSatisfied
            ea.Satisfied = (CodeRush.Clipboard.AsText.IndexOf(ea.Parameters.GetString("Text")) > -1)
        End Sub

        Private Sub ctxClipboardContainsRegEx_ContextSatisfied(ByVal ea As DevExpress.CodeRush.Core.ContextSatisfiedEventArgs) Handles ctxClipboardContainsRegEx.ContextSatisfied
            Dim regex As New System.Text.RegularExpressions.Regex(ea.Parameters.GetString("RegEx"))
            ea.Satisfied = regex.Matches(CodeRush.Clipboard.AsText).Count > 0
        End Sub

        Private Sub ctxClipboardContainsMultiLineText_ContextSatisfied(ByVal ea As DevExpress.CodeRush.Core.ContextSatisfiedEventArgs) Handles ctxClipboardContainsMultiLineText.ContextSatisfied
            ea.Satisfied = CodeRush.Clipboard.AsText.IndexOfAny(Environment.NewLine.ToCharArray) > -1
        End Sub

        Private Sub ctxSelectionHasBalancedParens_ContextSatisfied(ByVal ea As DevExpress.CodeRush.Core.ContextSatisfiedEventArgs) Handles ctxSelectionHasBalancedParens.ContextSatisfied
            Dim LeftCount As Integer = Regex.Matches(CodeRush.Selection.Text, "\(").Count
            Dim RightCount As Integer = Regex.Matches(CodeRush.Selection.Text, "\)").Count
            ea.Satisfied = (LeftCount = RightCount)
        End Sub
    End Class
End Namespace
