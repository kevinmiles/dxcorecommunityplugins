Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class XPOSimplifier

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
        CreateXPOSimplifier()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    ' Please ensure the following line is not missing from your plugin's InitializeComponent
    ' components = New System.ComponentModel.Container()
    Public Sub CreateXPOSimplifier()
        Dim XPOSimplifier As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(XPOSimplifier, System.ComponentModel.ISupportInitialize).BeginInit()
        XPOSimplifier.ProviderName = "XPOSimplifier" ' Should be Unique
        XPOSimplifier.DisplayName = "Manual XPO Simplified Criteria Plugin"
        AddHandler XPOSimplifier.CheckAvailability, AddressOf XPOSimplifier_CheckAvailability
        AddHandler XPOSimplifier.Apply, AddressOf XPOSimplifier_Execute
        CType(XPOSimplifier, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub XPOSimplifier_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ' This method is executed when the system checks the availability of your Code.
        If ea.CodeActive.ElementType = LanguageElementType.Property Then
            ea.Available = True
        End If
        ' Change this to return true, only when your Code should be available.
    End Sub
    Private Sub XPOSimplifier_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        ' This method is executed when the system executes your Code 
        If CodeRush.Source.ActiveClass IsNot Nothing Then
            Dim FieldsClass As [Class] = Nothing
            Dim FieldProperty As [Property] = Nothing
            Dim Searcher As ElementEnumerable
            Dim element As IEnumerator

            Searcher = New ElementEnumerable(CodeRush.Source.ActiveClass, GetType([Class]), True)
            element = Searcher.GetEnumerator
            element.Reset()
            While element.MoveNext
                Dim FoundClass As [Class] = TryCast(element.Current, [Class])
                If FoundClass IsNot Nothing AndAlso FoundClass.Name = "FieldsClass" Then
                    FieldsClass = element.Current
                    Exit While
                End If
            End While

            If FieldsClass Is Nothing Then
                'TODO: Workout how to create a class ;)
                'Need to create the Shared Class
                'then assign it to the FieldClass variable
            End If

            Searcher = New ElementEnumerable(CodeRush.Source.ActiveClass, GetType([Property]), True)
            element = Searcher.GetEnumerator
            element.Reset()
            While element.MoveNext
                Dim FoundProperty As [Property] = TryCast(element.Current, [Property])
                If FoundProperty IsNot Nothing AndAlso FoundProperty.Name = CodeRush.Source.ActiveProperty.Name Then
                    FieldProperty = element.Current
                    Exit While
                End If
            End While

            If FieldProperty IsNot Nothing Then
                Dim builder As ElementBuilder = ea.NewElementBuilder
                Dim newPropertyGetter As New [Get]()
                Dim newProperty As [Property] = builder.AddProperty(Nothing, "OperandProperty", CodeRush.Source.ActiveProperty.Name, newPropertyGetter, Nothing)
                newProperty.IsReadOnly = True
                builder.AddReturn(newPropertyGetter, "not sure")

                'TODO: workout how to blow away the property
                'set the fieldproperty back to nothing 
            End If


            'TODO: make a new property



        End If
    End Sub


End Class
