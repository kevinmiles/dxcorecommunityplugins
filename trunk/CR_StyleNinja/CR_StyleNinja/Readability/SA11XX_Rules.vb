Imports System.ComponentModel
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core
Imports System.Diagnostics.CodeAnalysis

Friend Module SA11XX_Rules
#Region "Utility"
    Private MemberOrType As LanguageElementType() = New LanguageElementType() {LanguageElementType.Method, _
                                                                               LanguageElementType.Class, _
                                                                               LanguageElementType.Struct, _
                                                                               LanguageElementType.Interface}
    Friend Function IsMemberOrType(ByVal CodeActive As LanguageElement) As Boolean
        Return MemberOrType.Contains(CodeActive.ElementType)
    End Function
    Friend Function HasExplicityVisibility(ByVal Element As AccessSpecifiedElement) As Boolean
        Return Element.VisibilityRange <> SourceRange.Empty
    End Function
#End Region

#Region "SA1409"
    Public Const Message_SA1124 As String = "SA1124 - Regions are bad"
    Public Sub Available_SA1124(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ea.Available = Qualifies_SA1124(ea.CodeActive)
    End Sub
    Public Function Qualifies_SA1124(ByVal Element As IElement) As Boolean
        Dim TheTry = TryCast(Region, [Try])
        If TheTry Is Nothing Then
            Return False
        End If
        Return TheTry.NodeCount = 0 AndAlso TheTry.NextSibling.NodeCount = 0
    End Function
    Public Sub Fix_SA1124(ByVal sender As Object, ByVal ea As ApplyContentEventArgs)
        ea.TextDocument.DeleteText(ea.CodeActive.GetFullBlockCutRange())
    End Sub
#End Region

    Private Function GetRegionsInRange(ByVal sourceFile As SourceFile, ByVal parent As LanguageElement, ByVal memberRange As SourceRange, ByRef newRegionsToAdd As StringCollection) As RegionDirectiveCollection


        Dim parentRange As SourceRange = parent.Range
        Dim result As RegionDirectiveCollection = New RegionDirectiveCollection()
        For Each regionDirective As RegionDirective In CollectRegions(sourceFile.Regions)
            Dim regionDirectiveRange As SourceRange = regionDirective.Range
            If (Not parentRange.Contains(regionDirectiveRange)) Then
                Continue For
            End If
            ' Region is inside the parent...
            Dim existingIndex As Integer = newRegionsToAdd.IndexOf(regionDirective.Name)
            If (existingIndex >= 0) Then ' Region already exists; no need to have it in the list of regions to add...
                newRegionsToAdd.RemoveAt(existingIndex)
            End If
            If (memberRange.Contains(regionDirectiveRange)) Then
                Continue For
            End If
            ' Region is a valid target.
            result.Add(regionDirective)
        Next
        Return result
    End Function
    Private Function CollectRegions(ByVal regions As RegionDirectiveCollection) As List(Of RegionDirective)

        Dim result = New List(Of RegionDirective)
        If (regions Is Nothing) Then
            Return result
        End If
        For Each regionDirective As RegionDirective In regions
            result.Add(regionDirective)
        Next

        Return result
    End Function




End Module

