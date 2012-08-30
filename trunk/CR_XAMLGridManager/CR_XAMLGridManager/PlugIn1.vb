Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports System.Linq
Public Class PlugIn1

    'DXCore-generated code...
#Region "Generated Code + Registrations"
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        RegisterAddXAMLGridColumn()
        RegisterAddXAMLGridRow()
        RegisterInsertXAMLGridColumn()
        RegisterInsertXAMLGridRow()
    End Sub
    Public Overrides Sub FinalizePlugIn()
        MyBase.FinalizePlugIn()
    End Sub
#End Region

#Region "Utility"
    Private Function HasGridColumnIndex(ByVal Sibling As SP.HtmlElement) As Boolean
        Return Sibling.Attributes("Grid.Column") IsNot Nothing
    End Function
    Private Sub IncreaseGridColumnIndex(ByVal Sibling As SP.HtmlElement)
        IncreaseIntAttributeValue(Sibling, "Grid.Column", 1)
    End Sub
    Private Sub IncreaseGridRowIndex(ByVal Sibling As SP.HtmlElement)
        IncreaseIntAttributeValue(Sibling, "Grid.Row", 1)
    End Sub
    Private Shared Sub IncreaseIntAttributeValue(ByVal Sibling As SP.HtmlElement, ByVal AttributeName As String, ByVal DefaultValue As Integer)
        If Sibling.Attributes(AttributeName) Is Nothing Then
            Sibling.AddAttribute(AttributeName, DefaultValue)
        Else
            Sibling.Attributes(AttributeName).Value = CInt(Sibling.Attributes(AttributeName).Value) + 1
        End If
    End Sub
    Private Function InXAMLGrid(Language As String, StartingElement As LanguageElement) As Boolean
        If Language <> "XAML" Then
            Return False
        End If
        Dim Grid = GetParentTag(StartingElement, "Grid")
        If Grid Is Nothing Then
            Return False
        End If
        Return True
    End Function
    Private Function GetParentTag(ByVal StartPoint As LanguageElement, ByVal TagName As String) As SP.HtmlElement
        Dim CurrentElement = TryCast(StartPoint, SP.HtmlElement)
        Do Until CurrentElement Is Nothing
            ' If Grid found, return Grid
            If CurrentElement.Name = TagName Then
                Return CurrentElement
            End If
            ' Try again with parent of current element. 
            CurrentElement = TryCast(CurrentElement.GetParent(LanguageElementType.HtmlElement), SP.HtmlElement)
        Loop
        Return CurrentElement
    End Function
    Public Function EnsureChildTag(ByVal ParentTag As SP.HtmlElement, ByVal ChildTagName As String) As SP.HtmlElement
        Dim ChildTag = (From tags In ParentTag.Nodes Where tags.name = ChildTagName).FirstOrDefault()
        If ChildTag Is Nothing Then
            AddChildTag(ParentTag, ChildTagName)
            ChildTag = (From tags In ParentTag.Nodes Where tags.name = ChildTagName).FirstOrDefault()
        End If
        Return ChildTag
    End Function
    Shared Function AddChildTag(ByVal ParentTag As SP.HtmlElement, ByVal ChildTagName As String) As SP.HtmlElement
        Dim NewTag As SP.HtmlElement = New SP.HtmlElement() With {.Name = ChildTagName}
        ParentTag.AddNode(NewTag)
        Return NewTag
    End Function
    Function InsertBeforeTag(ByVal ExistingColumn As SP.HtmlElement, ByVal TagName As String) As SP.HtmlElement
        Dim NewTag As SP.HtmlElement = New SP.HtmlElement() With {.Name = TagName}
        ExistingColumn.Parent.InsertNode(ExistingColumn.Parent.Nodes.IndexOf(ExistingColumn), NewTag)
        Return NewTag
    End Function
    Private Sub ReRenderGrid(ByVal Grid As SP.HtmlElement, ByVal StepDescription As String)
        Dim Newcode As String = CodeRush.CodeMod.GenerateCode(Grid, False)
        CodeRush.Documents.ActiveTextDocument.Replace(Grid.Range, Newcode, StepDescription)
    End Sub
#End Region

#Region "CodeProvider - Add Grid Column"
    Public Sub RegisterAddXAMLGridColumn()
        Dim AddXAMLGridColumn As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(AddXAMLGridColumn, System.ComponentModel.ISupportInitialize).BeginInit()
        AddXAMLGridColumn.ProviderName = "AddXAMLGridColumn" ' Should be Unique
        AddXAMLGridColumn.DisplayName = "Add Column"
        AddHandler AddXAMLGridColumn.CheckAvailability, AddressOf AddXAMLGridColumn_CheckAvailability
        AddHandler AddXAMLGridColumn.Apply, AddressOf AddXAMLGridColumn_Execute
        CType(AddXAMLGridColumn, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub AddXAMLGridColumn_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ea.Availability = InXAMLGrid(ea.ActiveLanguage, ea.Element)
    End Sub
    Private Sub AddXAMLGridColumn_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        ' This method is executed when the system executes your Code 
        Dim Grid As SP.HtmlElement = GetParentTag(ea.Element, "Grid")
        Dim ColumnDefsTag = EnsureChildTag(Grid, "Grid.ColumnDefinitions")
        Dim ColumnTag = AddChildTag(ColumnDefsTag, "ColumnDefinition")
        ' Rerendering the entire grid is probably overkill, but it works :)
        ReRenderGrid(Grid, "Added Column")
    End Sub
#End Region
#Region "CodeProvider - Add Grid Row"
    Public Sub RegisterAddXAMLGridRow()
        Dim AddXAMLGridRow As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(AddXAMLGridRow, System.ComponentModel.ISupportInitialize).BeginInit()
        AddXAMLGridRow.ProviderName = "AddXAMLGridRow" ' Should be Unique
        AddXAMLGridRow.DisplayName = "Add Row"
        AddHandler AddXAMLGridRow.CheckAvailability, AddressOf AddXAMLGridRow_CheckAvailability
        AddHandler AddXAMLGridRow.Apply, AddressOf AddXAMLGridRow_Execute
        CType(AddXAMLGridRow, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub AddXAMLGridRow_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ea.Availability = InXAMLGrid(ea.ActiveLanguage, ea.Element)
    End Sub
    Private Sub AddXAMLGridRow_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        ' This method is executed when the system executes your Code 
        Dim Grid As SP.HtmlElement = GetParentTag(ea.Element, "Grid")
        Dim ColumnDefsTag = EnsureChildTag(Grid, "Grid.RowDefinitions")
        Dim ColumnTag = AddChildTag(ColumnDefsTag, "RowDefinition")
        ' Rerendering the entire grid is probably overkill, but it works :)
        ReRenderGrid(Grid, "Added Row")

    End Sub
#End Region

#Region "CodeProvider - Insert Grid Column"
    Public Sub RegisterInsertXAMLGridColumn()
        Dim InsertXAMLGridColumn As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(InsertXAMLGridColumn, System.ComponentModel.ISupportInitialize).BeginInit()
        InsertXAMLGridColumn.ProviderName = "InsertXAMLGridColumn" ' Should be Unique
        InsertXAMLGridColumn.DisplayName = "Insert Column"
        AddHandler InsertXAMLGridColumn.CheckAvailability, AddressOf InsertXAMLGridColumn_CheckAvailability
        AddHandler InsertXAMLGridColumn.Apply, AddressOf InsertXAMLGridColumn_Execute
        CType(InsertXAMLGridColumn, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub InsertXAMLGridColumn_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ea.Available = IsOnViableColumn(ea.Element)
    End Sub
    Function IsOnViableColumn(StartingElement As LanguageElement) As Boolean
        Dim ExistingColumn As SP.HtmlElement = GetParentTag(StartingElement, "ColumnDefinition")
        If ExistingColumn Is Nothing Then
            Return False
        End If
        Dim ColumnDefsTag = ExistingColumn.Parent
        If ColumnDefsTag.Name <> "Grid.ColumnDefinitions" Then
            Return False
        End If
        Return True
    End Function
    Private Sub InsertXAMLGridColumn_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)

        Dim Grid As SP.HtmlElement = GetParentTag(ea.Element, "Grid")
        Dim ExistingColumn As SP.HtmlElement = GetParentTag(ea.Element, "ColumnDefinition")
        Dim ColumnDefsTag = ExistingColumn.Parent
        ' Create new column and Insert definition ahead of this one.
        Dim NewColumn = InsertBeforeTag(ExistingColumn, "ColumnDefinition")
        ' Identify index of this new column.
        Dim Index As Integer = ColumnDefsTag.Nodes.IndexOf(NewColumn)

        ' Add 1 to grid.Column of all controls which indicate this index or higher.
        For Each Sibling As SP.HtmlElement In ColumnDefsTag.Parent.Nodes
            Dim ColumnAttribute As XmlAttribute = Sibling.GetAttribute("Grid.Column")
            If ColumnAttribute IsNot Nothing AndAlso ColumnAttribute.Value >= Index Then
                Call IncreaseGridColumnIndex(Sibling)
            End If
        Next
        ' Rerendering the entire grid is probably overkill, but it works :)
        ReRenderGrid(Grid, "Inserted Column")
    End Sub
#End Region
#Region "CodeProvider - Insert Grid Row"
    Public Sub RegisterInsertXAMLGridRow()
        Dim RegisterInsertXAMLGridRow As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(RegisterInsertXAMLGridRow, System.ComponentModel.ISupportInitialize).BeginInit()
        RegisterInsertXAMLGridRow.ProviderName = "InsertXAMLGridRow" ' Should be Unique
        RegisterInsertXAMLGridRow.DisplayName = "Insert Row"
        AddHandler RegisterInsertXAMLGridRow.CheckAvailability, AddressOf RegisterInsertXAMLGridRow_CheckAvailability
        AddHandler RegisterInsertXAMLGridRow.Apply, AddressOf RegisterInsertXAMLGridRow_Execute
        CType(RegisterInsertXAMLGridRow, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub RegisterInsertXAMLGridRow_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ea.Available = IsOnViableRow(ea.Element)
    End Sub
    Function IsOnViableRow(StartingElement As LanguageElement) As Boolean
        Dim ExistingColumn As SP.HtmlElement = GetParentTag(StartingElement, "RowDefinition")
        If ExistingColumn Is Nothing Then
            Return False
        End If
        Dim ColumnDefsTag = ExistingColumn.Parent
        If ColumnDefsTag.Name <> "Grid.RowDefinitions" Then
            Return False
        End If
        Return True
    End Function
    Private Sub RegisterInsertXAMLGridRow_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)

        Dim Grid As SP.HtmlElement = GetParentTag(ea.Element, "Grid")
        Dim ExistingRow As SP.HtmlElement = GetParentTag(ea.Element, "RowDefinition")
        Dim RowDefsTag = ExistingRow.Parent
        ' Create new Row and Insert definition ahead of this one.
        Dim NewRow = InsertBeforeTag(ExistingRow, "RowDefinition")
        ' Identify index of this new Row.
        Dim Index As Integer = RowDefsTag.Nodes.IndexOf(NewRow)

        ' Add 1 to grid.Row of all controls which indicate this index or higher.
        For Each Sibling As SP.HtmlElement In RowDefsTag.Parent.Nodes
            Dim RowAttribute As XmlAttribute = Sibling.GetAttribute("Grid.Row")
            If RowAttribute IsNot Nothing AndAlso RowAttribute.Value >= Index Then
                Call IncreaseGridRowIndex(Sibling)
            End If
        Next
        ' Rerendering the entire grid is probably overkill, but it works :)
        ReRenderGrid(Grid, "Inserted Row")
    End Sub
#End Region





End Class
