Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core
Imports System.Windows.Forms
Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.PlugInCore
Imports System.Linq
Imports System.Linq.Expressions


Public Module ListViewExt
    <Extension()> _
    Public Sub ClearCheckedItems(ByVal TheListview As ListView)
        TheListview.CheckedItems.Cast(Of ListViewItem)() _
                            .Where(Function(item) item.Checked = False)
    End Sub
End Module
