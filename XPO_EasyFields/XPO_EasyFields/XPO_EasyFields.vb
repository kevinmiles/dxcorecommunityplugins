Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class XPO_EasyFields

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
        CreateXPO_EasyFields()
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
    Dim NormalFeedbackColor As Color = CodeRush.Hints.Settings.FeedbackFillColor
    Dim NormalFeedbackBorderColor As Color = CodeRush.Hints.Settings.FeedbackBorderColor
    ' Please ensure the following line is not missing from your plugin's InitializeComponent
    ' components = New System.ComponentModel.Container()
    Public Sub CreateXPO_EasyFields()
        Dim XPOSimplifier As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(XPOSimplifier, System.ComponentModel.ISupportInitialize).BeginInit()
        XPOSimplifier.ProviderName = "XPO_EasyFields" ' Should be Unique
        XPOSimplifier.DisplayName = "Update XPO FieldsClass"
        AddHandler XPOSimplifier.CheckAvailability, AddressOf XPOSimplifier_CheckAvailability
        AddHandler XPOSimplifier.Apply, AddressOf XPOSimplifier_Execute
        CType(XPOSimplifier, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub XPOSimplifier_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ' This method is executed when the system checks the availability of your Code.
        If TypeOf ea.CodeActive Is ITypeElement AndAlso DXCoreXPOHelper.XPOElement.Check("DevExpress.Xpo.PersistentBase", ea.CodeActive) Then
            ea.Available = True
            Return
        End If
        ea.Available = False
        ' Change this to return true, only when your Code should be available.
    End Sub

    Private Sub XPOSimplifier_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        ' This method is executed when the system executes your Code 

        Try
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
                BobClass.AddComment(NewFieldsClass, CommentStart & " " & Now.ToString("ddd dd-MMM-yyyy HH:mm:ss"), CommentType.SingleLine) 'date format should use local culture, but then again Aussie way is best

                Dim constructor As Method = BobClass.AddConstructor(NewFieldsClass)
                constructor.Visibility = MemberVisibility.Public
                BobClass.AddBaseConstructorInitializer(constructor, New ExpressionCollection())

                Dim constructorwithproperty As Method = BobClass.AddConstructor(NewFieldsClass)
                constructorwithproperty.Visibility = MemberVisibility.Public
                Dim constructorArgument As String = "propertyName"
                Dim inParam As Param = BobClass.AddInParam(constructorwithproperty, CodeRush.Language.GetBaseTypeName("System.String"), constructorArgument)
                Dim rah = New ExpressionCollection
                rah.Add(BobClass.BuildElementReference(constructorArgument))
                BobClass.AddBaseConstructorInitializer(constructorwithproperty, rah) 'Need to work out how to make this language agnostic




                'Go through all the possible persistent members of the class (Variables and Properties)
                Dim Searcher As ElementEnumerable
                Dim element As IEnumerator
                Searcher = New ElementEnumerable(CodeRush.Source.ActiveClass, New LanguageElementType() {LanguageElementType.Property, LanguageElementType.Variable})
                element = Searcher.GetEnumerator
                element.Reset()
                While element.MoveNext
                    Dim FoundMember As MemberWithParameters = TryCast(element.Current, MemberWithParameters)
                    If FoundMember IsNot Nothing Then
                        If DXCoreXPOHelper.IsPersistentMember(FoundMember) Then

                            Dim XPOType As New DXCoreXPOHelper.XPOElement(FoundMember.MemberTypeReference.GetDeclaration)
                            Dim newPropertyType As String
                            If XPOType.IsPersistentClass And Not XPOType.IsXPCollection Then
                                newPropertyType = XPOType.Element.FullName & ".FieldsClass"
                            Else
                                newPropertyType = "DevExpress.Data.Filtering.OperandProperty"
                            End If

                            Dim newProperty As [Property] = BobClass.AddProperty(NewFieldsClass, newPropertyType, FoundMember.Name)
                            Dim newPropertyGetter As [Get] = BobClass.AddGetter(newProperty)
                            newProperty.IsReadOnly = True
                            newProperty.Visibility = MemberVisibility.Public
                            Dim ReturnArguments As New ExpressionCollection
                            Dim GetNestedNameArguments As New ExpressionCollection
                            GetNestedNameArguments.Add(BobClass.BuildPrimitiveFromObject(FoundMember.Name))
                            'GetNestedName("Test")
                            ReturnArguments.Add(BobClass.BuildMethodCallExpression("GetNestedName", GetNestedNameArguments))
                            'Dim returnExpression = BobClass.BuildObjectCreationExpression(newPropertyType, ReturnArguments)
                            'Dim ReturnExpression = BobClass.BuildExpressionConstructorInitializer(BobClass.va(BobClass.BuildElementReference(newPropertyType), ReturnArguments)
                            BobClass.AddReturn(newPropertyGetter, BobClass.BuildObjectCreationExpression(newPropertyType, ReturnArguments))
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
                Dim NewFieldsClassPropertyGetIf As [If] = BobProperty.AddIf(NewFieldsClassPropertyGet, "ReferenceEquals(" & nameVariable & "," & CodeRush.Language.GenerateExpressionCode(CodeRush.Language.GetNullReferenceExpression) & ")")
                BobProperty.AddAssignment(NewFieldsClassPropertyGetIf, nameVariable, BobProperty.BuildObjectCreationExpression(nameClass, New ExpressionCollection))
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
                    CodeRush.Hints.Settings.FeedbackFillColor = NormalFeedbackColor
                    CodeRush.Hints.Settings.FeedbackBorderColor = NormalFeedbackBorderColor
                    BigFeedback1.Text = "XPO FieldsClass Updated"
                    BigFeedback1.Show()
                    Return
                End If
            End If
            BigFeedback1.Text = "Nothing to do"
            BigFeedback1.Show()
        Catch ex As Exception
            CodeRush.Hints.Settings.FeedbackFillColor = Color.Red
            CodeRush.Hints.Settings.FeedbackBorderColor = Color.DarkRed
            BigFeedback1.Text = "XPO FieldsClass Failed"
        Finally

        End Try

    End Sub


    Private Function FindElement(ByVal elementType As LanguageElementType, ByVal elementName As String) As LanguageElement
        Dim Searcher As ElementEnumerable
        Dim element As IEnumerator
        Searcher = New ElementEnumerable(CodeRush.Source.ActiveClass, elementType)
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
End Class
