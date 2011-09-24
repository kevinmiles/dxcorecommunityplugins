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
        Call RegisterCautiousChange()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Public Sub RegisterCautiousChange()
        ' -------------------------------------------------------------
        ' I recreated this project to bring the name into line in the simplest way.
        ' The content of this file is the same with the exception of this method.
        ' This method is designed to replace the Action object on the design surface.
        ' It is called once from the InitializePlugIn() routine (above)
        ' -------------------------------------------------------------


        Dim CautiousChange As New DevExpress.CodeRush.Core.Action(components)
        CType(CautiousChange, System.ComponentModel.ISupportInitialize).BeginInit()
        CautiousChange.ActionName = "CautiousChange"
        CautiousChange.RegisterInCR = True
        AddHandler CautiousChange.Execute, AddressOf CautiousChange_Execute
        CType(CautiousChange, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub

#Region "Constants"
    Const RevisionHeader As String = "----------amended"
    Const RevisionDivider As String = "-----amendment"
    Const RevisionEnding As String = "----------end amendment"
#End Region

    Private Sub CautiousChange_Execute(ByVal ea As ExecuteEventArgs)
        Dim ActiveTextDocument As TextDocument = CodeRush.Documents.ActiveTextDocument
        If ActiveTextDocument Is Nothing Then
            Exit Sub
        End If

        Dim EndComment As LanguageElement = Nothing
        Dim CaretOnEndComment As Boolean = OnEndComment(EndComment)
        If CodeRush.Selection.Exists AndAlso Not CaretOnEndComment Then
            ExecuteCautiousChange(ActiveTextDocument)
        ElseIf CaretOnEndComment Then
            ExecuteRemoveCautiousChange(ActiveTextDocument, EndComment)
        End If
    End Sub

    Private Sub ExecuteCautiousChange(ByVal ActiveTextDocument As TextDocument)
        Dim RevisionDate As String = Now.ToString(" yyyyMMdd HHmmss")
        Dim ActiveView As TextView = ActiveTextDocument.ActiveView
        Using UndoUnit = ActiveTextDocument.NewCompoundAction("Cautious Change")

            ' Gather code to duplicate
            ActiveView.Selection.ExtendToWholeLines()
            Dim activeSelectionRange As SourceRange = ActiveView.Selection.Range
            Dim textToDuplicate As String = ActiveTextDocument.GetText(activeSelectionRange)
            Dim commentedLines() As String = Microsoft.VisualBasic.Split(textToDuplicate, Environment.NewLine)
            Dim CommentedCode As String = String.Empty

            For i As Integer = 0 To commentedLines.Length - 2 'minus 2 to remove the trailing CRLF (after the selection was expanded to the full lines)
                CommentedCode += CodeRush.Language.GetComment(commentedLines(i))
            Next

            ' Build Replacement Parts
            Dim HeaderText As String = CodeRush.Language.GetComment(RevisionHeader + RevisionDate) _
                                     + CodeRush.Language.GetComment("Job No: «Caret»«FieldStart»xxxxxx«FieldEnd»«BlockAnchor»")
            Dim DividerText As String = CodeRush.Language.GetComment(RevisionDivider)
            Dim WhitespaceEnd As Integer = textToDuplicate.IndexOf(textToDuplicate.TrimStart()(0))
            Dim TextToEdit As String = textToDuplicate.Substring(0, WhitespaceEnd) _
                                       + "«FinalTarget»" + textToDuplicate.Substring(WhitespaceEnd)
            Dim FooterText As String = CodeRush.Language.GetComment(RevisionEnding)

            ' Build Replacement TextToDuplicate
            Dim ReplacementText As String = HeaderText _
                                         + CommentedCode _
                                         + DividerText _
                                         + TextToEdit _
                                         + FooterText
            'Clear Original Text
            ActiveTextDocument.SetText(activeSelectionRange, "")
            'Emit Replacement Text
            ActiveTextDocument.ExpandText(activeSelectionRange.Start, ReplacementText)
        End Using
    End Sub

    Private Sub ExecuteRemoveCautiousChange(ByVal ActiveTextDocument As TextDocument, ByVal EndComment As LanguageElement)
        Using UndoUnit = ActiveTextDocument.NewCompoundAction("Remove Cautious Change")
            ' Search upward from EndComment for Divider
            Dim Divider As LanguageElement = EndComment
            Do
                Divider = Divider.PreviousSibling
            Loop While Divider IsNot Nothing AndAlso Not isDivider(Divider)
            If Divider Is Nothing Then
                ' Divider not found. Exit
                Exit Sub
            End If

            ' Search upward from Divider for Header
            Dim Header As LanguageElement = Divider
            Do
                Header = Header.PreviousSibling
            Loop While Divider IsNot Nothing AndAlso Not isHeader(Header)
            If Header Is Nothing Then
                ' Divider not found. Exit
                Exit Sub
            End If

            ' Now we have References to Header, Divider and EndComment

            ' Build the HeaderRange
            Dim StartPoint As SourcePoint = New SourcePoint(Header.Range.Start.Line, 1)
            Dim EndPoint As SourcePoint = New SourcePoint(Divider.Range.End.Line + 1, 0)
            Dim HeaderRange As SourceRange = New SourceRange(StartPoint, EndPoint)


            Dim FooterRange As SourceRange = EndComment.GetFullBlockCutRange

            CodeRush.Documents.ActiveTextDocument.Replace(FooterRange, "", "")
            CodeRush.Documents.ActiveTextDocument.Replace(HeaderRange, "", "")
        End Using
    End Sub

    Private Function OnEndComment(ByRef EndComment As LanguageElement) As Boolean
        EndComment = CodeRush.Source.ActiveComment
        Return EndComment IsNot Nothing AndAlso EndComment.Name.StartsWith(RevisionEnding)
    End Function

    Private Function isDivider(ByVal Candidate As LanguageElement) As Boolean
        If Candidate.ElementType <> LanguageElementType.Comment Then
            Return False
        End If
        If Candidate.Name <> RevisionDivider Then
            Return False
        End If
        Return True
    End Function

    Private Function isHeader(ByVal Candidate As LanguageElement) As Boolean
        If Candidate.ElementType <> LanguageElementType.Comment Then
            Return False
        End If
        If Not Candidate.Name.StartsWith(RevisionHeader) Then
            Return False
        End If
        Return True
    End Function

End Class
