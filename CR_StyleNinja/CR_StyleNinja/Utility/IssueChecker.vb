Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Enum SourceTypeEnum
    Unknown
    Field
    Local
    LocalOrField
    Method
    Param
    Variable
    Member
    MainElement
    [Interface]
End Enum
Public Class Checker
    Private mIssueMessage As String
    Private mQualifier As QualifiesDelegate
    Private mSourceType As SourceTypeEnum
    Private mExitStrategy As Func(Of Boolean)
    Public Delegate Function QualifiesDelegate(ByVal Element As IElement) As Boolean
    Public Sub New(ByVal SourceType As SourceTypeEnum, ByVal Qualifier As QualifiesDelegate, ByVal IssueMessage As String, Optional ByVal ExitStrategy As Func(Of Boolean) = Nothing)
        mExitStrategy = ExitStrategy
        mSourceType = SourceType
        mIssueMessage = IssueMessage
        mQualifier = Qualifier
    End Sub
    Public Shared Function ParamChecker(ByVal Qualifier As QualifiesDelegate, ByVal IssueMessage As String, Optional ByVal ExitStrategy As Func(Of Boolean) = Nothing) As Checker
        Return New Checker(SourceTypeEnum.Param, Qualifier, IssueMessage, ExitStrategy)
    End Function
    Public Shared Function LocalChecker(ByVal Qualifier As QualifiesDelegate, ByVal IssueMessage As String, Optional ByVal ExitStrategy As Func(Of Boolean) = Nothing) As Checker
        Return New Checker(SourceTypeEnum.Local, Qualifier, IssueMessage, ExitStrategy)
    End Function
    Public Shared Function InterfaceChecker(ByVal Qualifier As QualifiesDelegate, ByVal IssueMessage As String, Optional ByVal ExitStrategy As Func(Of Boolean) = Nothing) As Checker
        Return New Checker(SourceTypeEnum.Interface, Qualifier, IssueMessage, ExitStrategy)
    End Function
    Public Shared Function MainElementChecker(ByVal Qualifier As QualifiesDelegate, ByVal IssueMessage As String, Optional ByVal ExitStrategy As Func(Of Boolean) = Nothing) As Checker
        Return New Checker(SourceTypeEnum.MainElement, Qualifier, IssueMessage, ExitStrategy)
    End Function
    Public Shared Function VariableChecker(ByVal Qualifier As QualifiesDelegate, ByVal IssueMessage As String, Optional ByVal ExitStrategy As Func(Of Boolean) = Nothing) As Checker
        Return New Checker(SourceTypeEnum.Variable, Qualifier, IssueMessage, ExitStrategy)
    End Function
    Public Shared Function FieldChecker(ByVal Qualifier As QualifiesDelegate, ByVal IssueMessage As String, Optional ByVal ExitStrategy As Func(Of Boolean) = Nothing) As Checker
        Return New Checker(SourceTypeEnum.Field, Qualifier, IssueMessage, ExitStrategy)
    End Function
    Public Sub CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If mExitStrategy IsNot Nothing AndAlso mExitStrategy.Invoke() Then
            Exit Sub
        End If
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From e In GetFinder(ea.Scope) Where mQualifier.Invoke(e)
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, mIssueMessage)
            Next
        End If
    End Sub
    Private Function GetFinder(ByVal Scope As IElement) As IEnumerable(Of LanguageElement)
        Select Case mSourceType
            Case SourceTypeEnum.Field
                Return Fields(Scope.ToLE)
            Case SourceTypeEnum.Local
                Return FieldsAndLocals(Scope.ToLE)
            Case SourceTypeEnum.LocalOrField
                Return FieldsAndLocals(Scope.ToLE)
            Case SourceTypeEnum.Variable
                Return Variables(Scope.ToLE)
            Case SourceTypeEnum.Param
                Return Params(Scope.ToLE)
            Case SourceTypeEnum.Member
                Return Members(Scope.ToLE)
            Case SourceTypeEnum.Method
                Return Methods(Scope.ToLE)
            Case SourceTypeEnum.MainElement
                Return MainElements(Scope.ToLE)
            Case SourceTypeEnum.Interface
                Return Interfaces(Scope.ToLE)
            Case Else
                Return Elements(Scope.ToLE)
        End Select

    End Function
End Class