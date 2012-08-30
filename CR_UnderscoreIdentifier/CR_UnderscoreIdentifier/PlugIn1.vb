Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.Refactor
Imports DevExpress.CodeRush.Core.Replacement

Public Class PlugIn1
#Region "Generated Code + Registrations"
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        RegisterUnderscoreIdentifier()
    End Sub
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Public Sub RegisterUnderscoreIdentifier()
        Dim UnderscoreIdentifier As New DevExpress.Refactor.Core.RefactoringProvider(components)
        CType(UnderscoreIdentifier, System.ComponentModel.ISupportInitialize).BeginInit()
        UnderscoreIdentifier.ProviderName = "UnderscoreIdentifier" ' Should be Unique
        UnderscoreIdentifier.DisplayName = "Underscore Identifier"
        AddHandler UnderscoreIdentifier.CheckAvailability, AddressOf UnderscoreIdentifier_CheckAvailability
        AddHandler UnderscoreIdentifier.Apply, AddressOf UnderscoreIdentifier_Execute
        CType(UnderscoreIdentifier, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub UnderscoreIdentifier_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        Dim RenameProvider = CodeRush.Refactoring.Get("Rename")
        If RenameProvider Is Nothing Then
            Return
        End If
        If CodeRush.Source.Active.Name.Contains("_") Then
            Return
        End If
        ea.Available = RenameProvider.IsAvailable
    End Sub
    Private Sub UnderscoreIdentifier_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        CodeRush.Source.ParseIfNeeded()
        Dim References = CodeRush.Refactoring.FindAllReferences(ea.Element.Solution, ea.Element.GetDeclaration)
        Dim NewMethodName As String = GetUnderscoredName(CodeRush.Source.Active.Name)
        Dim FileChanges As New FileChangeCollection
        For Each Ref As IElement In References
            For index = 0 To Ref.NameRanges.Count - 1
                FileChanges.Add(New FileChange(TryCast(Ref.Files(index), SourceFile).FilePath, Ref.NameRanges(index), NewMethodName))
            Next
        Next
        FileChanges.Add(New FileChange(CodeRush.Source.ActiveSourceFile.FilePath, ea.Element.GetDeclaration.ToLanguageElement.NameRange, NewMethodName))
        CodeRush.File.ApplyChanges(FileChanges)
    End Sub
    Function GetUnderscoredName(ByVal Name As String) As String
        Dim UPPERCASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim StartedWithUpperCase = UPPERCASE.Contains(Name.Substring(0, 1))
        For Each [Char] In UPPERCASE
            Name = Name.Replace([Char], "_" & [Char])
        Next
        If StartedWithUpperCase Then
            Name = Name.Substring(1)
        End If
        Return Name
    End Function

End Class
