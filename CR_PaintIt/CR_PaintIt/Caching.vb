Imports DevExpress.CodeRush.Core
Public Module Caching
    Public Function GetCacheFromDocument(ByVal Document As Document, ByVal CacheID As String) As PaintOptionsCache
        Return CType(Document.Storage.GetObject(CacheID), PaintOptionsCache)
    End Function
    Public Function GetCacheFromDocument(ByVal Document As Document, ByVal CacheID As String, ByVal DefaultCache As PaintOptionsCache) As PaintOptionsCache
        Dim Cache As PaintOptionsCache = CType(Document.Storage.GetObject(CacheID), PaintOptionsCache)
        If Cache Is Nothing Then
            Cache = DefaultCache
        End If
        Return Cache
    End Function
    Public Sub SaveCacheToDocument(ByVal Document As Document, ByVal CacheToStore As PaintOptionsCache, ByVal CacheID As String)
        Document.Storage.AttachObject(CacheID, CacheToStore)
    End Sub
End Module
