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
        If ea.CodeActive.ElementType = LanguageElementType.Class Then
            ea.Available = True
        End If
        ' Change this to return true, only when your Code should be available.
    End Sub
    Private Function FindElement(ByVal elementType As LanguageElementType, ByVal elementName As String) As LanguageElement
        Dim Searcher As ElementEnumerable
        Dim element As IEnumerator
        Searcher = New ElementEnumerable(CodeRush.Source.ActiveClass, elementType, True)
        element = Searcher.GetEnumerator
        element.Reset()
        While element.MoveNext
            Dim FoundElement As LanguageElement = TryCast(element.Current, LanguageElement)
            If FoundElement IsNot Nothing AndAlso FoundElement.Name.ToLower = elementName.ToLower Then
                Return element.Current() 'Found it, keep a reference so we can "override" it later
                Exit While
            End If
        End While
        Return Nothing
    End Function
    Private Sub XPOSimplifier_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        ' This method is executed when the system executes your Code 

        If CodeRush.Source.ActiveClass IsNot Nothing Then
            'Bob the CodeRush Builder ;)
            Dim BobClass As ElementBuilder = ea.NewElementBuilder
            Dim BobProperty As ElementBuilder = ea.NewElementBuilder
            Dim BobVariable As ElementBuilder = ea.NewElementBuilder

            Dim nameClass As String = "FieldsClass"
            Dim nameVariable As String = "_fields"
            Dim nameProperty As String = "Fields"

            'TODO: Need to work out how to ensure there is firstly an Import of the DevExpress.Xpo and DevExpress.Data.Filtering namespace



            Dim ExistingFieldsClass As [Class] = FindElement(LanguageElementType.Class, nameClass)

            'Check whether we already have the FieldsClass declared


            'Create the new FieldsClass class
            Dim NewFieldsClass As [Class] = BobClass.AddClass(Nothing, nameClass)
            NewFieldsClass.Visibility = MemberVisibility.Public
            NewFieldsClass.IsNew = True 'This seems to do the "Shadows" keyword
            NewFieldsClass.PrimaryAncestorType = New TypeReferenceExpression(CodeRush.Source.ActiveClass.PrimaryAncestorType.Name & "." & nameClass) 'Couldn't find easier way, needs to "shadow" the persistentbase FieldsClass
            Dim CommentStart As String = "Created/Updated:"
            BobClass.AddComment(NewFieldsClass, CommentStart & " " & Now.ToString("ddd dd-MMM-yyyy HH:mm:ss"), CommentType.SingleLine)
            Dim constructor As Method = BobClass.AddConstructor(NewFieldsClass)
            constructor.Visibility = MemberVisibility.Public
            BobClass.AddMethodCall(constructor, "MyBase.New") 'Need to work out how to make this language agnostic

            Dim constructorwithproperty As Method = BobClass.AddConstructor(NewFieldsClass)
            constructorwithproperty.Visibility = MemberVisibility.Public
            Dim constructorArgument As String = "propertyName"
            BobClass.AddInParam(constructorwithproperty, CodeRush.Language.GetBaseTypeName("System.String"), constructorArgument)
            BobClass.AddMethodCall(constructorwithproperty, "MyBase.New", New String() {constructorArgument}) 'Need to work out how to make this language agnostic

            'Go through all the possible persistent members of the class (Variables and Properties)
            Dim Searcher As ElementEnumerable
            Dim element As IEnumerator
            Searcher = New ElementEnumerable(CodeRush.Source.ActiveClass, New LanguageElementType() {LanguageElementType.Property, LanguageElementType.Variable})
            element = Searcher.GetEnumerator
            element.Reset()
            While element.MoveNext
                Dim FoundMember As MemberWithParameters = TryCast(element.Current, MemberWithParameters)
                If FoundMember IsNot Nothing Then
                    If IsPersistentMember(FoundMember) Then

                        'TODO: Make this more clever, if the FoundMember type is a reference type that has a FieldsClass defined then return FieldsClass not OperandProperty
                        Dim newProperty As [Property] = BobClass.AddProperty(NewFieldsClass, "DevExpress.Data.Filtering.OperandProperty", FoundMember.Name)
                        Dim newPropertyGetter As [Get] = BobClass.AddGetter(newProperty)
                        newProperty.IsReadOnly = True
                        newProperty.Visibility = MemberVisibility.Public
                        BobClass.AddReturn(newPropertyGetter, "New DevExpress.Data.Filtering.OperandProperty(GetNestedName(""" & FoundMember.Name & """))")
                    End If
                End If
            End While
            If ExistingFieldsClass IsNot Nothing Then
                Dim ClassRange As SourceRange = ExistingFieldsClass.Range
                If CodeRush.Documents.ActiveTextDocument.GetLine(ExistingFieldsClass.StartLine - 1).TrimStart(" ").StartsWith(CodeRush.Language.GetComment(CommentStart).TrimEnd) Then
                    ClassRange.Set(New SourcePoint(ClassRange.Start.Line - 1, ClassRange.Start.Offset), ClassRange.End)
                End If
                CodeRush.Documents.ActiveTextDocument.QueueReplace(ClassRange, BobClass.GenerateCode.TrimEnd())
            Else
                CodeRush.Documents.ActiveTextDocument.InsertText(CodeRush.Source.ActiveClass.EndLine, 1, BobClass.GenerateCode)
            End If

            Dim ExistingFieldsClassProperty As [Property] = FindElement(LanguageElementType.Property, nameProperty)
            Dim NewFieldsClassProperty As [Property] = BobProperty.AddProperty(Nothing, nameClass, nameProperty)
            Dim NewFieldsClassPropertyGet As [Get] = BobProperty.AddGetter(NewFieldsClassProperty)
            NewFieldsClassProperty.IsNew = True
            NewFieldsClassProperty.IsStatic = True
            NewFieldsClassProperty.IsReadOnly = True
            NewFieldsClassProperty.Visibility = MemberVisibility.Public
            Dim NewFieldsClassPropertyGetIf As [If] = BobProperty.AddIf(NewFieldsClassPropertyGet, "ReferenceEquals(" & nameVariable & ",Nothing)")
            BobProperty.AddAssignment(NewFieldsClassPropertyGetIf, nameVariable, "New " & nameClass & "()")
            BobProperty.AddReturn(NewFieldsClassPropertyGet, nameVariable)
            If ExistingFieldsClassProperty IsNot Nothing Then
                CodeRush.Documents.ActiveTextDocument.QueueReplace(ExistingFieldsClassProperty.Range, BobProperty.GenerateCode.TrimEnd)
            Else
                CodeRush.Documents.ActiveTextDocument.InsertText(CodeRush.Source.ActiveClass.EndLine, 1, BobProperty.GenerateCode)
            End If


            Dim ExistingFieldsClassVariable As Variable = FindElement(LanguageElementType.Variable, nameVariable)
            Dim NewFieldsClassVariable As Variable = BobVariable.AddVariable(Nothing, nameClass, nameVariable)
            NewFieldsClassVariable.IsStatic = True
            NewFieldsClassVariable.Visibility = MemberVisibility.Private
            If ExistingFieldsClassVariable IsNot Nothing Then
                CodeRush.Documents.ActiveTextDocument.QueueReplace(ExistingFieldsClassVariable.Range, BobVariable.GenerateCode.TrimEnd)
            Else
                CodeRush.Documents.ActiveTextDocument.InsertText(CodeRush.Source.ActiveClass.EndLine, 1, BobVariable.GenerateCode)
            End If

            If CodeRush.Documents.ActiveTextDocument.QueuedEdits.Count > 0 Then
                CodeRush.Documents.ActiveTextDocument.ApplyQueuedEdits("XPO Update FieldsClass")
            End If

            'TODO: make a new property



        End If
    End Sub
    Private Function IsPersistentMember(ByVal foundProperty As MemberWithParameters) As Boolean

        For Each Attr As IElement In foundProperty.Attributes

            If Attr.Name.ToLower = "nonpersistent" Then
                Return False
            ElseIf Attr.Name.ToLower = "persistent" Then
                Return True
            ElseIf Attr.Name.ToLower = "persistentalias" Then
                Return True
            End If
        Next
        If foundProperty.Visibility = MemberVisibility.Public Then
            Return True
        End If
        Return False
    End Function


End Class
