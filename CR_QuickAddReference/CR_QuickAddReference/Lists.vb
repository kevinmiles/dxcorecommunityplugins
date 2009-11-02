Imports System.Collections.Generic
Public Module Lists
    Friend Lists As New List(Of ReferenceList)
    Public Sub InitialiseLists()
        Lists.Clear()
        Lists.Add(New ReferenceList("Solution", AddressOf DefaultReferences.GetSolutionReferences)) ' Only Defaults. 
        Lists.Add(New PersistableReferenceList("Recent", AddressOf DefaultReferences.GetNoReferences)) ' There are no Default "Recent" References.
        Lists.Add(New PersistableReferenceList("Common", AddressOf DefaultReferences.DefaultReferencesCommon)) ' Default Common References
        Lists.Add(New PersistableReferenceList("Win", AddressOf DefaultReferences.DefaultReferencesWin)) ' Default Win References
        Lists.Add(New PersistableReferenceList("Web", AddressOf DefaultReferences.DefaultReferencesWeb)) ' Default Web References  
        Lists.Add(New PersistableReferenceList("MVC", AddressOf DefaultReferences.DefaultReferencesMVC)) ' Default MVC References  
        Lists.Add(New PersistableReferenceList("DXCore", AddressOf DefaultReferences.DefaultReferencesDXCore)) ' Default DXCore References
    End Sub
    Public Function GetList(ByVal Name As String) As ReferenceList
        Return (From list In Lists Where list.Name = Name).First
    End Function
    Public Function GetCustomLists() As IEnumerable(Of ReferenceList)
        Return From List In Lists Where TypeOf List Is PersistableReferenceList
    End Function
    Public Function Recent() As PersistableReferenceList
        Return CType(GetList("Recent"), PersistableReferenceList)
    End Function

End Module
