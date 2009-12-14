Option Strict On

Public Class PersistableReferenceList
    Inherits ReferenceList
    Private mPersistableReferences As New List(Of Reference)
    Public Sub New(ByVal Name As String, ByVal DefaultFunction As Func(Of List(Of Reference)))
        MyBase.New(Name, DefaultFunction)
    End Sub
    Public Overrides Function References() As IEnumerable(Of Reference)
        ' Saved list if available, else default
        Load()
        Return If(mPersistableReferences.Count > 0, mPersistableReferences, MyBase.References())
    End Function
    Public Sub Add(ByVal Reference As Reference)
        mPersistableReferences.Add(Reference)
        Save()
    End Sub
    Public Sub Load()
        Dim mPersistedReferenceStrings = OptionsQuickAddReference.Storage.ReadStrings(OptionsQuickAddReference.SECTION_QUICKADD, mName)
        mPersistableReferences.Clear()
        mPersistableReferences.AddRange(mPersistedReferenceStrings.Select(Function(R) New Reference(R)).Distinct())
    End Sub
    Public Sub Save()
        Dim ReferenceStrings As String() = References.Select(Function(r) r.FullName).Distinct().ToArray()
        Call OptionsQuickAddReference.Storage.WriteStrings(OptionsQuickAddReference.SECTION_QUICKADD, mName, ReferenceStrings)
    End Sub

End Class
