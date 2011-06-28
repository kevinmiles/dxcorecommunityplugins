Option Infer On
Imports DevExpress.CodeRush.Core

Public Class PluginSettings
    Public Property ShouldLog() As Boolean
#Region "Construction"
    Public Shared Function LoadFromStorage(ByVal Storage As DecoupledStorage) As PluginSettings
        Return New PluginSettings() With {.ShouldLog = Storage.ReadBoolean("ExceptionHelper", "ShouldLog")}
    End Function
#End Region
    Public Sub SaveToStorage(ByVal Storage As DecoupledStorage)
        Storage.WriteBoolean("ExceptionHelper", "ShouldLog", ShouldLog)
    End Sub
End Class
