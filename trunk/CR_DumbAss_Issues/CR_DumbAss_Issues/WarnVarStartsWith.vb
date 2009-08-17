Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports System.Collections.Generic

Public Class WarnVarStartsWith
    Private mBadToStartWith As String
    Public Sub New(ByVal BadToStartWith As String)
        mBadToStartWith = BadToStartWith
    End Sub
    Public Function StartsWith(ByVal Element As LanguageElement) As Boolean
        Return Element.Name.StartsWith(mBadToStartWith)
    End Function
End Class
