Option Strict On
Option Infer On
Imports EnvDTE
Imports System.Drawing
Imports DevExpress.CodeRush.Core

Public Module Defaults
    Public Function SelectedForeColor() As Color
        Dim Properties = CodeRush.ApplicationObject.Properties("FontsAndColors", "TextEditor")
        Dim FontsAndColorsItems = CType(Properties.Item("FontsAndColorsItems").Object,  _
                                        FontsAndColorsItems)
        Dim TextColors = FontsAndColorsItems.Item("Selected Text")
        Return ColorTranslator.FromOle(Convert.ToInt32(TextColors.Foreground))
    End Function
    Public Function SelectedBackColor() As Color
        Dim Properties = CodeRush.ApplicationObject.Properties("FontsAndColors", "TextEditor")
        Dim FontsAndColorsItems = CType(Properties.Item("FontsAndColorsItems").Object,  _
                                        FontsAndColorsItems)
        Dim TextColors = FontsAndColorsItems.Item("Selected Text")
        Return ColorTranslator.FromOle(Convert.ToInt32(TextColors.Background))
    End Function
End Module
