Option Strict On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports EnvDTE
Imports System.IO

Class ReferenceColors
    Inherits SmartTagColors


    Public Overrides ReadOnly Property Dark() As System.Drawing.Color
        Get
            Return Color.FromArgb(&HFB, &H3B, &H8A)
        End Get
    End Property

    Public Overrides ReadOnly Property Light() As System.Drawing.Color
        Get
            Return Color.FromArgb(&HFD, &H95, &HC0)
        End Get
    End Property
End Class

