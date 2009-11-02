Imports System.Windows.Forms
Imports System.Runtime.CompilerServices
Imports System.Linq


Public Module ListViewExt
    <Extension()> _
    Public Sub ClearCheckedItems(ByVal TheListview As ListView)
        TheListview.CheckedItems.Cast(Of ListViewItem)() _
                            .Where(Function(item) item.Checked = False)
    End Sub
End Module
