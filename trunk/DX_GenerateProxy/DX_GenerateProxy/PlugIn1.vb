Option Strict On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.Serialization
Imports System.Runtime.CompilerServices

Public Class PlugIn1
    Private mProxyField As Variable
    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        CreateGenerateProxyClass()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

#Region "Magic Plugin Hookup Code"
    Public Sub CreateGenerateProxyClass()
        Dim GenerateProxyClass As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(GenerateProxyClass, System.ComponentModel.ISupportInitialize).BeginInit()
        GenerateProxyClass.ProviderName = "GenerateProxyClass" ' Should be Unique
        GenerateProxyClass.DisplayName = "Generate Proxy Class"
        AddHandler GenerateProxyClass.CheckAvailability, AddressOf GenerateProxyClass_CheckAvailability
        AddHandler GenerateProxyClass.Apply, AddressOf GenerateProxyClass_Execute
        CType(GenerateProxyClass, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
#End Region

#Region "Start Here"
    Private Sub GenerateProxyClass_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        Dim ClassElement As IClassElement = GetClassToProxy(ea.CodeActive)
        ea.Available = ClassElement IsNot Nothing
    End Sub

    Private Sub GenerateProxyClass_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        Dim ClassElement As IClassElement = GetClassToProxy(ea.CodeActive)
        Try
            Dim Proxy As [Class] = GenerateProxyClass(ClassElement)
            EmitProxyClassInDocument(Proxy, ea.TextDocument.Range.End)
        Catch ex As Exception

        End Try
    End Sub
#End Region
#Region "Utility"
    Private Sub CopyParameters(Of T As {IElement, IWithParameters})(ByVal Source As T, ByVal DestParameters As LanguageElementCollection)
        If Source.Parameters Is Nothing Then
            Exit Sub
        End If
        For Each SourceParam As IParameterElement In Source.Parameters
            Dim SourceParamName As String = SourceParam.Name
            If SourceParamName.ToLower = Source.Name.ToLower Then
                SourceParamName = SourceParamName & "_Dup"
            End If
            Dim newParam = New Param(SourceParamName)
            newParam.IsOptional = SourceParam.IsOptional
            newParam.MemberTypeReference = ExtractType(SourceParam.Type)
            DestParameters.Add(newParam)
        Next
    End Sub
    Private Function ExtractType(ByVal Type As ITypeReferenceExpression) As TypeReferenceExpression
        If Type Is Nothing Then
            Return Nothing
        End If
        Return SourceModelUtils.CreateTypeReferenceExpression(Type)
    End Function
    Private Function Proxificate(Of T As {IHasType, IMemberElement})(ByVal Member As T) As String
        Dim Qualifier = If(Member.IsStatic, Member.Type.Name, mProxyField.Name)
        Return String.Format("{0}.{1}", Qualifier, Member.Name)
    End Function
#End Region

    Private Function GenerateProxyClass(ByVal Element As IClassElement) As [Class]
        Dim Proxy As New [Class](String.Format("{0}_Proxy", Element.Name))
        Proxy.Visibility = Element.Visibility
        Call GenerateClassConstructor(Element, Proxy)
        For Each Member As IMemberElement In Element.Members
            If Member.Visibility <> MemberVisibility.Public Then
                Continue For
            End If
            Select Case Member.ElementType
                Case LanguageElementType.Property : AddPropertyToClass(TryCast(Member, IPropertyElement), Proxy)
                Case LanguageElementType.Method : AddMethodToClass(TryCast(Member, IMethodElement), Proxy)
                Case LanguageElementType.Variable : AddFieldToClassAsProperty(TryCast(Member, IFieldElement), Proxy)
            End Select
        Next
        Return Proxy
    End Function
    Private Sub EmitProxyClassInDocument(ByVal Proxy As [Class], ByVal InsertionPoint As SourcePoint)
        Dim TheCode = vbCrLf & CodeRush.CodeMod.GenerateCode(Proxy)
        Dim Doc = CodeRush.Documents.ActiveTextDocument
        Using Action = Doc.NewCompoundAction("Generate Proxy Class")
            Dim Range = Doc.InsertText(InsertionPoint, TheCode)
            Doc.Format(Range)
        End Using
    End Sub
    Private Sub GenerateClassConstructor(ByVal Source As IClassElement, ByVal Proxy As [Class])
        Dim Builder As New ElementBuilder
        mProxyField = Builder.AddVariable(Proxy, Source.Name, "mProxiedClass")
        Dim Constructor = Builder.AddConstructor(Proxy)
        Constructor.Visibility = MemberVisibility.Public
        Constructor.Parameters.Add(New Param(Source.Name, "ProxiedClass"))
        Builder.AddAssignment(Constructor, _
                              New PrimitiveExpression("mProxiedClass"), _
                              New PrimitiveExpression("ProxiedClass"))
    End Sub
    Private Shared Function GetClassToProxy(ByVal CodeActive As LanguageElement) As IClassElement
        Dim TypeElement = TryCast(CodeActive, IClassElement)
        If TypeElement Is Nothing Then
            Dim TypeReference = TryCast(CodeActive, ITypeReferenceExpression)
            If TypeReference IsNot Nothing Then
                TypeElement = TryCast(TypeReference.GetDeclaration, IClassElement)
            End If
        End If
        Return TypeElement
    End Function

#Region "Build Wrapper Method"
    Private Sub AddMethodToClass(ByVal Source As IMethodElement, ByVal Proxy As [Class])
        If Source.IsClassOperator Then
            Exit Sub
        End If
        If Source.MethodType = MethodTypeEnum.Constructor Then
            Exit Sub
        End If
        'Dim Dest As Method = TryCast(Source.Clone(), Method)
        Dim Dest = CopyBasicMethod(Source, Proxy)
        Call CopyParameters(Source, Dest.Parameters)
        Call GenerateCallToProxyMethod(Source, Dest)
    End Sub
    Private Function CopyBasicMethod(ByVal Source As IMethodElement, ByVal NewParentClass As [Class]) As Method
        Dim Builder As New ElementBuilder
        Dim SourceTypeName = String.Empty
        If Source.Type Is Nothing Then
            SourceTypeName = String.Empty
        ElseIf Source.Type.IsArrayType Then
            SourceTypeName = Source.Type.BaseType.Name
        Else
            SourceTypeName = Source.Type.Name
        End If
        Dim Dest As Method = Builder.AddMethod(NewParentClass, SourceTypeName, Source.Name)
        Dest.MethodType = Source.MethodType
        Dest.Visibility = Source.Visibility
        Dest.IsStatic = Source.IsStatic
        Dest.MemberTypeReference = ExtractType(Source.Type)
        Dest.IsOverride = Source.IsOverride
        Dest.IsOverloads = IsOverloads(Source)
        If Source.IsGeneric Then
            Dim types As TypeParameterCollection = New TypeParameterCollection()
            types.Add(New TypeParameter("T"))
            Dim modifier As GenericModifier = New GenericModifier(types)
            Dest.SetGenericModifier(modifier)
        End If
        Return Dest
    End Function

    Private Function IsOverloads(ByVal Source As IMethodElement) As Boolean
        Return Source.MethodType <> MethodTypeEnum.Constructor _
            AndAlso Source.ParentType.FindMembers(Source.Name).Count > 1
    End Function
    Private Sub GenerateCallToProxyMethod(ByVal Source As IMethodElement, ByVal Dest As Method)
        Dim Builder As New ElementBuilder()
        Dim Arguments As New List(Of String)()
        For Each SourceParam As IParameterElement In Source.Parameters
            Dim SourceParamName As String = SourceParam.Name
            If SourceParamName.ToLower = Source.Name.ToLower Then
                SourceParamName = SourceParamName & "_Dup"
            End If
            Arguments.Add(SourceParamName)
        Next
        Dim NameToCall As String = If(Source.IsStatic, Source.ParentType.Name & "." & Source.Name, Proxificate(Source))
        Dim MethodCall As MethodCall = Builder.BuildMethodCall(NameToCall, Arguments.ToArray)
        If Source.MethodType = MethodTypeEnum.Void Then
            Dest.AddNode(MethodCall)
        ElseIf Source.MethodType = MethodTypeEnum.Function Then
            Dest.AddNode(Builder.BuildReturn(MethodCall))
        End If
    End Sub

#End Region

#Region "Build Wrapper Property"
    Private Sub AddFieldToClassAsProperty(ByVal Source As IFieldElement, ByVal Proxy As [Class])
        Dim DestProperty As [Property] = BuildBasicProperty(Source, Proxy)

        Dim FieldReferenceExpression As ElementReferenceExpression = GetProxificatedFieldReference(Source)
        Dim DestGetter = AddExpressionGetter(DestProperty, FieldReferenceExpression)

        If Not Source.IsReadOnly Then
            AddExpressionSetter(DestProperty, FieldReferenceExpression)
        End If
    End Sub

    Private Sub AddPropertyToClass(ByVal Source As IPropertyElement, ByVal Proxy As [Class])
        Dim DestProperty As [Property] = BuildBasicProperty(Source, Proxy)
        Call CopyParameters(Source, DestProperty.Parameters)

        Dim PropertyReferenceExpression = GetProxificatedPropertyReference(Source)
        Dim SourceGetter As IMethodElement = Source.GetMethod
        If SourceGetter IsNot Nothing _
            AndAlso SourceGetter.Visibility = MemberVisibility.Public Then
            Dim DestGetter = AddExpressionGetter(DestProperty, PropertyReferenceExpression)
            CopyParameters(SourceGetter, DestGetter.Parameters)
        End If
        Dim SourceSetter As IMethodElement = Source.SetMethod
        If SourceSetter IsNot Nothing _
            AndAlso SourceSetter.Visibility = MemberVisibility.Public Then
            Dim DestSetter = AddExpressionSetter(DestProperty, PropertyReferenceExpression)
            DestSetter.Parameters = New LanguageElementCollection
            CopyParameters(SourceSetter, DestSetter.Parameters)
        End If
    End Sub
    Private Function AddExpressionGetter(ByVal DestProperty As [Property], ByVal ReferenceExpression As Expression) As [Get]
        Dim Builder As New ElementBuilder()
        Dim DestGetter As [Get] = Builder.AddGetter(DestProperty)
        DestGetter.AddNode(New [Return](ReferenceExpression))
        Return DestGetter
    End Function
    Private Function AddExpressionSetter(ByVal DestProperty As [Property], ByVal ReferenceExpression As Expression) As [Set]
        Dim Builder As New ElementBuilder()
        Dim DestSetter As [Set] = Builder.AddSetter(DestProperty)
        Builder.AddAssignment(DestSetter, ReferenceExpression, New PrimitiveExpression("value"))
        Return DestSetter
    End Function

    Private Function BuildBasicProperty(Of T As {IMemberElement, IHasType})(ByVal Source As T, ByVal ParentClass As [Class]) As [Property]
        Dim Builder As New ElementBuilder
        Dim PropertyName As String = Source.Name
        Dim DestProperty As [Property] = Builder.AddProperty(ParentClass, Source.Type.Name, PropertyName)
        DestProperty.Visibility = Source.Visibility
        DestProperty.IsStatic = Source.IsStatic
        Return DestProperty
    End Function
    'Private Function BuildBasicProperty(ByVal Source As IPropertyElement, ByVal ParentClass As [Class]) As [Property]
    '    Dim Builder As New ElementBuilder
    '    Dim PropertyName As String = Source.Name
    '    Dim DestProperty As [Property] = Builder.AddProperty(ParentClass, Source.Type.Name, PropertyName)
    '    DestProperty.Visibility = Source.Visibility
    '    DestProperty.IsStatic = Source.IsStatic
    '    Return DestProperty
    'End Function
    'Private Sub BuildGetterIntoProperty(ByVal DestProperty As [Property], ByVal Source As IPropertyElement, ByVal MethodCallExpression As MethodCallExpression)
    '    Dim SourceGetter As IMethodElement = Source.GetMethod
    '    If SourceGetter IsNot Nothing Then
    '        Dim Builder As New ElementBuilder
    '        Dim DestGetter As [Get] = Builder.AddGetter(DestProperty)
    '        CopyParameters(SourceGetter, DestGetter.Parameters)

    '        DestGetter.AddNode(New [Return](MethodCallExpression))
    '    End If
    'End Sub
    'Private Sub BuildSetterIntoProperty(ByVal DestProperty As [Property], ByVal Source As IPropertyElement, ByVal MethodCallExpression As MethodCallExpression)
    '    Dim SourceSetter As IMethodElement = Source.SetMethod
    '    If SourceSetter IsNot Nothing Then
    '        Dim Builder As New ElementBuilder
    '        Dim DestSetter As [Set] = Builder.AddSetter(DestProperty)
    '        CopyParameters(SourceSetter, DestSetter.Parameters)
    '        Builder.AddAssignment(DestSetter, _
    '                              MethodCallExpression, _
    '                              New PrimitiveExpression("value"))
    '    End If
    'End Sub

    Private Function GetProxificatedPropertyReference(ByVal Source As IPropertyElement) As MethodCallExpression
        Dim Builder As New ElementBuilder
        Dim MethodToCall As String = Proxificate(Source)
        Dim MethodCallExpression = Builder.BuildMethodCallExpression(MethodToCall)
        MethodCallExpression.Arguments.Clear()
        For Each SourceParam As IParameterElement In Source.Parameters
            MethodCallExpression.Arguments.Add(New PrimitiveExpression(SourceParam.Name))
        Next
        Return MethodCallExpression
    End Function
    Private Function GetProxificatedFieldReference(ByVal Source As IFieldElement) As ElementReferenceExpression
        Dim Builder As New ElementBuilder
        Dim FieldToReference As String = Proxificate(Source)
        Return Builder.BuildElementReference(FieldToReference)
    End Function
    Dim X As String
    Private Function MethodName() As String
        Return X
    End Function
#End Region
End Class
