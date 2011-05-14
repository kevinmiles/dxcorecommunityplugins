Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Text.RegularExpressions

Namespace Helpers
    Namespace XPO
        Public Class Names
            Public Const XPCollection As String = "DevExpress.Xpo.XPCollection"
            Public Const PersistentBase As String = "DevExpress.Xpo.PersistentBase"
            Public Const PersistentAttribute As String = "DevExpress.Xpo.PersistentAttribute"
            Public Const PersistentAliasAttribute As String = "DevExpress.Xpo.PersistentAliasAttribute"
            Public Const NonPersistentAttribute As String = "DevExpress.Xpo.NonPersistentAttribute"
            Public Const FieldsClassName As String = "FieldsClass"
            Public Const PersistentClassFieldsPropertyName As String = "Fields"
            Public Const OperandProperty As String = "DevExpress.Data.Filtering.OperandProperty"
            Public Const AggregateOperand As String = "DevExpress.Data.Filtering.AggregateOperand"
            Public Const CriteriaOperator As String = "DevExpress.Data.Filtering.CriteriaOperator"
            Public Const AggregateSumEnum As String = "DevExpress.Data.Filtering.Aggregate.Sum"
            Public Const AggregateMinEnum As String = "DevExpress.Data.Filtering.Aggregate.Min"
            Public Const AggregateMaxEnum As String = "DevExpress.Data.Filtering.Aggregate.Max"
            Public Const AggregateExistsEnum As String = "DevExpress.Data.Filtering.Aggregate.Exists"
            Public Const AggregateCountEnum As String = "DevExpress.Data.Filtering.Aggregate.Avg"
            Private Const _CollectionFieldsClass As String = "CollectionFieldsClass"

            Public Shared ReadOnly Property PersistentClassFieldsVariableName As String
                Get
                    Return Settings.FieldsClassVariableName
                End Get
            End Property


            Public Shared ReadOnly Property CollectionFieldsClass() As String
                Get
                    'ISSUES, this won't resolve the collectionfieldsclass if it is moved out of the default project namespace :(
                    Dim Declaration As ITypeElement = CodeRush.Source.FindType(Names._CollectionFieldsClass)
                    If Declaration Is Nothing Then
                        CreateCollectionFieldsClass()
                        Return CodeRush.Source.ActiveProject.DefaultNamespace & "." & _CollectionFieldsClass
                    Else
                        Return Declaration.FullName
                    End If
                End Get
            End Property


            Private Shared Sub CreateCollectionFieldsClass()
                Dim BobClass As New ElementBuilder

                Dim newNS As [Namespace] = New [Namespace](CodeRush.Source.ActiveProject.DefaultNamespace)
                BobClass.AddNode(Nothing, newNS)
                Dim newClass As [Class] = BobClass.AddClass(newNS, Names._CollectionFieldsClass)
                newClass.Visibility = MemberVisibility.Public
                newClass.PrimaryAncestorType = New TypeReferenceExpression(Names.OperandProperty)

                'Dim constructor As Method = BobClass.AddConstructor(newClass)
                'constructor.Visibility = MemberVisibility.Public
                'BobClass.AddBaseConstructorInitializer(constructor, New ExpressionCollection())

                Dim constructorwithproperty As Method = BobClass.AddConstructor(newClass)
                constructorwithproperty.Visibility = MemberVisibility.Public

                Dim constructorArgument As String = "propertyName"

                BobClass.AddInParam(constructorwithproperty, CodeRush.Language.GetBaseTypeName("System.String"), _
                    constructorArgument)
                Dim ConstructorWithPropertyArguments = New ExpressionCollection
                ConstructorWithPropertyArguments.Add(BobClass.BuildElementReference(constructorArgument))
                BobClass.AddBaseConstructorInitializer(constructorwithproperty, ConstructorWithPropertyArguments)

                Dim SumCondition As Method = BobClass.AddMethod(newClass, Names.AggregateOperand, "Sum")
                SumCondition.Visibility = MemberVisibility.Public
                BobClass.AddInParam(SumCondition, Names.CriteriaOperator, "PropertyToSum")
                BobClass.AddInParam(SumCondition, Names.CriteriaOperator, "Condition")
                Dim SumConditionArgs As New ExpressionCollection()
                SumConditionArgs.Add(New ObjectCreationExpression(New TypeReferenceExpression(Names.OperandProperty), Common.GetExpressionCollection(BobClass.BuildElementReference("PropertyName"))))
                SumConditionArgs.Add(BobClass.BuildElementReference("PropertyToSum"))
                SumConditionArgs.Add(BobClass.BuildElementReference(Names.AggregateSumEnum))
                SumConditionArgs.Add(BobClass.BuildElementReference("Condition"))
                BobClass.AddReturn(SumCondition, New ObjectCreationExpression(New TypeReferenceExpression(Names.AggregateOperand), SumConditionArgs))

                Dim Sum As Method = BobClass.AddMethod(newClass, Names.AggregateOperand, "Sum")
                SumCondition.Visibility = MemberVisibility.Public
                BobClass.AddInParam(Sum, Names.CriteriaOperator, "PropertyToSum")
                Dim SumArgs As New ExpressionCollection()
                SumArgs.Add(New ObjectCreationExpression(New TypeReferenceExpression(Names.OperandProperty), Common.GetExpressionCollection(BobClass.BuildElementReference("PropertyName"))))
                SumArgs.Add(BobClass.BuildElementReference("PropertyToSum"))
                SumArgs.Add(BobClass.BuildElementReference(Names.AggregateSumEnum))
                SumArgs.Add(CodeRush.Language.GetNullReferenceExpression)
                BobClass.AddReturn(Sum, New ObjectCreationExpression(New TypeReferenceExpression(Names.AggregateOperand), SumArgs))

                Dim Count As Method = BobClass.AddMethod(newClass, Names.AggregateOperand, "Count")
                Count.Visibility = MemberVisibility.Public
                BobClass.AddInParam(Count, Names.CriteriaOperator, "PropertyToCount")
                Dim CountArgs As New ExpressionCollection()
                CountArgs.Add(New ObjectCreationExpression(New TypeReferenceExpression(Names.OperandProperty), Common.GetExpressionCollection(BobClass.BuildElementReference("PropertyName"))))
                CountArgs.Add(BobClass.BuildElementReference("PropertyToCount"))
                CountArgs.Add(BobClass.BuildElementReference(Names.AggregateSumEnum))
                CountArgs.Add(CodeRush.Language.GetNullReferenceExpression)
                BobClass.AddReturn(Count, New ObjectCreationExpression(New TypeReferenceExpression(Names.AggregateOperand), CountArgs))

                '                CodeRush.Language.GenerateExpressionCode(CodeRush.Language.GetNullReferenceExpression)
                Dim activeproject As ProjectElement = CodeRush.Source.ActiveProject
                Dim projectpath As String = System.IO.Path.GetDirectoryName(activeproject.FilePath)
                Dim newfilepath As String = System.IO.Path.Combine(projectpath, Names._CollectionFieldsClass & If(CodeRush.Language.IsBasic, ".vb", ".cs"))
                Dim newfilecode = BobClass.GenerateCode(CodeRush.Language.Active)
                System.IO.File.WriteAllText(newfilepath, newfilecode)
                CodeRush.Solution.AddFileToProject(activeproject.Name, newfilepath)
            End Sub
        End Class

        Public Module XPO
            Public Function IsPersistentClass(ByVal checkElement As IClassElement) As Boolean
                Return Helpers.Common.IsElementOfType(checkElement, "DevExpress.Xpo.PersistentBase")
            End Function

            Public Function IsXPCollection(ByVal TypeElement As IClassElement) As Boolean
                Return Common.IsElementOfType(TypeElement, Names.XPCollection)
            End Function

            Public Function IsPersistedMember(ByVal MemberElement As IMemberElement) As Boolean
                Dim MemberElementType As IHasType = TryCast(MemberElement, IHasType)
                If MemberElementType Is Nothing Then
                    Return False
                End If
                Dim Member As ITypeElement = Source.ResolveType(MemberElementType.Type)

                If Member Is Nothing Then
                    Return False
                End If

                If Member.Name = Helpers.XPO.Names.PersistentClassFieldsPropertyName _
                    Or Member.Name = Helpers.XPO.Names.PersistentClassFieldsVariableName Then

                    Return False
                End If

                For Each Attr As IAttributeElement In CType(MemberElement, IHasAttributes).Attributes

                    Dim AttrTypeElement As ITypeElement = TryCast(Attr.GetDeclaration, ITypeElement)
                    If AttrTypeElement IsNot Nothing Then
                        If Common.IsElementOfType(AttrTypeElement, Helpers.XPO.Names.PersistentAttribute) Then
                            Return True
                        ElseIf Common.IsElementOfType(AttrTypeElement, Helpers.XPO.Names.PersistentAliasAttribute) Then
                            Return True
                        ElseIf Common.IsElementOfType(AttrTypeElement, Helpers.XPO.Names.NonPersistentAttribute) And Not Settings.IncludeNonPersistent Then
                            Return False
                        End If
                    End If
                Next

                If MemberElement.Visibility = MemberVisibility.Public Then
                    Return True
                End If
                Return False
            End Function

            '                Namespace newNamespace = new Namespace("NamespaceName");

            'Class newClass = new Class("NewClassName");

            'newClass.IsStatic = true;

            'Method newMethod = new Method("TestMethod");

            'newMethod.MethodType = MethodTypeEnum.Void;

            'newClass.AddNode(newMethod);

            'newNamespace.AddNode(newClass);



            'ProjectElement activeProject = CodeRush.Source.ActiveProject;

            'string projectPath = Path.GetDirectoryName(activeProject.FilePath);

            'string newFilePath = Path.Combine(projectPath, "NewFileName.cs");

            'string newFileCode = CodeRush.Language.GenerateElement(newNamespace);

            'File.WriteAllText(newFilePath, newFileCode);

            'CodeRush.Solution.AddFileToProject(activeProject.Name, newFilePath);

        End Module

        Public Class PersistentClassElement
            Private _class As [Class]

            Public Sub New(ByVal ClassElement As [Class])
                _class = ClassElement
            End Sub

            Public ReadOnly Property Document() As TextDocument
                Get
                    Return _class.Document
                End Get
            End Property

            Public Sub ApplyQueuedChanges()
                If Document.QueuedEdits.Count > 0 Then
                    Document.ApplyQueuedEdits("XPO Update FieldsClass", True)
                    Document.RefreshViews()
                    Helpers.Common.ShowMessage("XPO FieldsClass Updated")
                End If
            End Sub

            Public Sub UpdateFieldsClass()
                If Helpers.XPO.IsPersistentClass(_class) Then
                    DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Caramel")
                    'Bob the CodeRush Builder ;)
                    Dim BobClass As ElementBuilder = New ElementBuilder
                    Dim BobProperty As ElementBuilder = New ElementBuilder
                    Dim BobVariable As ElementBuilder = New ElementBuilder

                    'Create the new FieldsClass class
                    Dim NewFieldsClass As [Class] = BobClass.AddClass(Nothing, Helpers.XPO.Names.FieldsClassName)
                    With NewFieldsClass
                        .Visibility = MemberVisibility.Public
                        .IsNew = True 'This seems to do the "Shadows" keyword
                        If Settings.IncludeInheritedMembers Then
                            .PrimaryAncestorType = New TypeReferenceExpression(String.Format("{0}.{1}", _class.GetBaseType.Name, Helpers.XPO.Names.FieldsClassName)) 'Couldn't find easier way, needs to "shadow" the persistentbase FieldsClass
                        Else
                            .PrimaryAncestorType = New TypeReferenceExpression(String.Format("{0}.{1}", Helpers.XPO.Names.PersistentBase, Helpers.XPO.Names.FieldsClassName))
                        End If

                    End With

                    Dim CommentFormat As String = Settings.CommentFormat
                    CommentFormat = CommentFormat.Replace("{computername}", My.Computer.Name)
                    CommentFormat = CommentFormat.Replace("{currentuser}", My.User.Name)
                    CommentFormat = CommentFormat.Replace("{dateshort}", Now.ToShortDateString)
                    CommentFormat = CommentFormat.Replace("{timeshort}", Now.ToShortTimeString)
                    CommentFormat = CommentFormat.Replace("{date:", "{0:")
                    Dim Comment As String
                    If CommentFormat.Contains("{0:") Then
                        Comment = String.Format(CommentFormat, Now)
                    Else
                        Comment = CommentFormat
                    End If
                    BobClass.AddComment(NewFieldsClass, Comment, CommentType.SingleLine) 'date format should use local culture, but then again Aussie way is best

                    Dim constructor As Method = BobClass.AddConstructor(NewFieldsClass)
                    constructor.Visibility = MemberVisibility.Public
                    BobClass.AddBaseConstructorInitializer(constructor, New ExpressionCollection())

                    Dim constructorwithproperty As Method = BobClass.AddConstructor(NewFieldsClass)
                    constructorwithproperty.Visibility = MemberVisibility.Public

                    Dim constructorArgument As String = "propertyName"

                    BobClass.AddInParam(constructorwithproperty, CodeRush.Language.GetBaseTypeName("System.String"), _
                        constructorArgument)
                    Dim ConstructorWithPropertyArguments = New ExpressionCollection
                    ConstructorWithPropertyArguments.Add(BobClass.BuildElementReference(constructorArgument))
                    BobClass.AddBaseConstructorInitializer(constructorwithproperty, ConstructorWithPropertyArguments)

                    AddMembersToFieldsClass(BobClass, NewFieldsClass, CType(_class, IClassElement).Members)

                    'Lapse in smartness, if I inherit from the Base PersistentClass.FieldsClass it will already contain the properties
                    'Will Keep Code here though as this does work, but is just duplicating.
                    'If Settings.IncludeInheritedMembers Then
                    '    'Cast to Class Element if not class (ie.LiteElement then we don't want to traverse it as PersistentBase's don't have any persisted properties)
                    '    Dim AncestorClass As [Class] = TryCast(_class.GetBaseType, [Class])
                    '    Do Until AncestorClass Is Nothing Or Not XPO.IsPersistentClass(AncestorClass)
                    '        AddMembersToFieldsClass(BobClass, NewFieldsClass, AncestorClass.AllProperties)
                    '        AddMembersToFieldsClass(BobClass, NewFieldsClass, AncestorClass.AllFields)
                    '        AncestorClass = AncestorClass.GetBaseType
                    '    Loop
                    'End If

                    Dim ExistingFieldsClass As [Class] = New ElementEnumerable(_class, GetType([Class]), False).OfType(Of [Class]).FirstOrDefault( _
                        Function(foundClass) foundClass.Name.ToLower = Names.FieldsClassName.ToLower _
                        )

                    If ExistingFieldsClass IsNot Nothing Then
                        Dim ClassRange As SourceRange = ExistingFieldsClass.Range

                        If Document.GetLine(ExistingFieldsClass.StartLine - 1).TrimStart(" ").StartsWith(CodeRush.Language.GetComment("").TrimEnd) Then
                            ClassRange.Set(New SourcePoint(ClassRange.Start.Line - 1, ClassRange.Start.Offset), ClassRange.End)
                        End If
                        Document.QueueReplace(ClassRange, BobClass.GenerateCode(Document.Language).TrimEnd)
                    Else
                        Document.QueueInsert(New SourcePoint(_class.EndLine, 1), BobClass.GenerateCode(Document.Language))
                    End If


                    Dim ExistingFieldsClassProperty As [Property] = New ElementEnumerable(_class, GetType([Property]), False).OfType(Of [Property]).FirstOrDefault( _
                        Function(foundProperty) foundProperty.Name.ToLower = Names.PersistentClassFieldsPropertyName.ToLower _
                        )

                    If ExistingFieldsClassProperty Is Nothing Or Not Settings.ReplaceClassOnly Then
                        Dim NewFieldsClassProperty As [Property] = BobProperty.AddProperty(Nothing, Names.FieldsClassName, Names.PersistentClassFieldsPropertyName)
                        Dim NewFieldsClassPropertyGet As [Get] = BobProperty.AddGetter(NewFieldsClassProperty)
                        NewFieldsClassProperty.IsNew = True
                        NewFieldsClassProperty.IsStatic = True
                        NewFieldsClassProperty.Visibility = MemberVisibility.Public
                        Dim NewFieldsClassPropertyGetIf As [If] = BobProperty.AddIf(NewFieldsClassPropertyGet, "ReferenceEquals(" & Names.PersistentClassFieldsVariableName & "," & CodeRush.Language.GenerateExpressionCode(CodeRush.Language.GetNullReferenceExpression) & ")")
                        BobProperty.AddAssignment(NewFieldsClassPropertyGetIf, Names.PersistentClassFieldsVariableName, BobProperty.BuildObjectCreationExpression(Names.FieldsClassName, New ExpressionCollection))
                        BobProperty.AddReturn(NewFieldsClassPropertyGet, Names.PersistentClassFieldsVariableName)

                        If ExistingFieldsClassProperty IsNot Nothing Then
                            Document.QueueReplace(ExistingFieldsClassProperty.Range, BobProperty.GenerateCode(Document.Language).TrimEnd)
                        Else
                            Document.QueueInsert(New SourcePoint(_class.EndLine, 1), BobProperty.GenerateCode(Document.Language))
                        End If
                    End If



                    Dim ExistingFieldsClassVariable As [Variable] = New ElementEnumerable(_class, GetType(Variable), False).OfType(Of [Variable]).FirstOrDefault( _
                        Function(foundVariable) foundVariable.Name.ToLower = Names.PersistentClassFieldsVariableName.ToLower _
                        )

                    If ExistingFieldsClassVariable Is Nothing Or Not Settings.ReplaceClassOnly Then
                        Dim NewFieldsClassVariable As Variable = BobVariable.AddVariable(Nothing, Names.FieldsClassName, Names.PersistentClassFieldsVariableName)
                        NewFieldsClassVariable.IsStatic = True
                        NewFieldsClassVariable.Visibility = MemberVisibility.Private
                        If ExistingFieldsClassVariable IsNot Nothing Then
                            Document.QueueReplace(ExistingFieldsClassVariable.Range, BobVariable.GenerateCode(Document.Language).TrimEnd)
                        Else
                            Document.QueueInsert(New SourcePoint(_class.EndLine, 1), BobVariable.GenerateCode(Document.Language))
                        End If
                    End If
                End If

            End Sub

            Private Sub AddMembersToFieldsClass(ByVal BobClass As ElementBuilder, ByVal NewFieldsClass As [Class], ByVal MembersList As IEnumerable, Optional ByVal PropertyPrefix As String = "", Optional ByVal FieldPrefix As String = "")
                For Each MemberElement As IElement In MembersList
                    If MemberElement.Name.ToLower = "fields" Then Continue For

                    Dim Member As IMemberElement = TryCast(MemberElement, IMemberElement)
                    If Member Is Nothing _
                        OrElse _
                            Not (Member.ElementType = LanguageElementType.Property Or Member.ElementType = LanguageElementType.Variable) Then
                        Continue For
                    End If

                    If Not XPO.IsPersistedMember(Member) Then Continue For


                    AddMemberToFieldsClass(BobClass, NewFieldsClass, MemberElement, PropertyPrefix, FieldPrefix)
                Next
            End Sub
            Private Sub AddMemberToFieldsClass(ByVal BobClass As ElementBuilder, ByVal NewFieldsClass As [Class], ByVal Member As IMemberElement, Optional ByVal PropertyPrefix As String = "", Optional ByVal FieldPrefix As String = "")
                Dim newPropertyType As String = ""
                Dim PropertyType As ITypeReferenceExpression = CType(Member, IHasType).Type

                Dim PropertyTypeElement = Source.ResolveType(PropertyType)
                newPropertyType = Names.OperandProperty
                If TypeOf PropertyTypeElement Is IClassElement Then
                    If XPO.IsPersistentClass(PropertyTypeElement) And Not XPO.IsXPCollection(PropertyTypeElement) Then
                        newPropertyType = PropertyTypeElement.FullName & "." & Names.FieldsClassName
                    ElseIf XPO.IsXPCollection(PropertyTypeElement) Then
                        If Settings.UseCollectionFieldsClass Then
                            newPropertyType = Names.CollectionFieldsClass
                        End If
                    End If
                ElseIf TypeOf PropertyTypeElement Is IStructElement And Not PropertyTypeElement.FullName.StartsWith("System.") Then
                    AddMembersToFieldsClass(BobClass, NewFieldsClass, CType(PropertyTypeElement, IStructElement).Members, Member.Name & "_", Member.Name)
                    'Exit Sub
                Else
                    newPropertyType = Names.OperandProperty
                End If

                If Settings.IncludeFieldConstants AndAlso newPropertyType = Names.OperandProperty Then
                    AddMemberConstantToFieldsClass(BobClass, NewFieldsClass, Member, PropertyPrefix, FieldPrefix)
                End If

                Dim newProperty As [Property] = BobClass.AddProperty(NewFieldsClass, newPropertyType, PropertyPrefix & Member.Name)
                Dim newPropertyGetter As [Get] = BobClass.AddGetter(newProperty)

                newProperty.Visibility = MemberVisibility.Public
                Dim ReturnArguments As New ExpressionCollection
                Dim GetNestedNameArguments As New ExpressionCollection
                GetNestedNameArguments.Add(BobClass.BuildPrimitiveFromObject(If(String.IsNullOrEmpty(FieldPrefix), "", FieldPrefix & ".") & Member.Name))
                ReturnArguments.Add(BobClass.BuildMethodCallExpression("GetNestedName", GetNestedNameArguments))
                BobClass.AddReturn(newPropertyGetter, BobClass.BuildObjectCreationExpression(newPropertyType, ReturnArguments))
            End Sub
            Private Sub AddMemberConstantToFieldsClass(ByVal BobClass As ElementBuilder, ByVal NewFieldsClass As [Class], ByVal Member As IMemberElement, Optional ByVal PropertyPrefix As String = "", Optional ByVal FieldPrefix As String = "")
                Dim newConst As New [Const]("String", PropertyPrefix & Member.Name & "FieldName", BobClass.BuildPrimitiveFromObject(If(String.IsNullOrEmpty(FieldPrefix), "", FieldPrefix & ".") & Member.Name))
                newConst.Visibility = MemberVisibility.Public
                BobClass.AddNode(NewFieldsClass, newConst)
            End Sub
        End Class
    End Namespace
End Namespace
