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
        ea.Available = ea.CodeActive.ElementType = LanguageElementType.Class
    End Sub
    Private Sub GenerateProxyClass_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        Dim SourceClass As [Class] = TryCast(ea.CodeActive, [Class])
        Dim Proxy As [Class] = GenerateProxyClass(SourceClass)
        EmitProxyClassInDocument(Proxy, SourceClass.Range.End)
    End Sub
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
                Case LanguageElementType.Property : AddPropertyToClass(TryCast(Member, [Property]).ToPropSpec(), Proxy)
                Case LanguageElementType.Method : AddMethodToClass(TryCast(Member, [Method]), Proxy)
                Case LanguageElementType.Variable : AddPropertyToClass(TryCast(Member, Variable).ToPropSpec(), Proxy)
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
    Private Sub AddPropertyToClass(ByVal PropertySpec As PropertySpec, _
                                   ByVal Proxy As [Class])
        Dim Builder As New ElementBuilder
        Dim PropertyName As String = PropertySpec.Name
        Dim Dest As [Property] = Builder.AddProperty(Proxy, PropertySpec.MemberType, PropertyName)
        Dest.Visibility = PropertySpec.Visibility
        Dest.IsStatic = PropertySpec.IsStatic
        If PropertySpec.HasGetter Then
            Dim Getter = Builder.AddGetter(Dest)
            Dim ERE = New ElementReferenceExpression(Proxificate(PropertyName))
            Getter.AddNode(New [Return](ERE))
        End If
        If PropertySpec.HasSetter Then
            Dim Setter = Builder.AddSetter(Dest)
            Builder.AddAssignment(Setter, _
                                  New PrimitiveExpression(Proxificate(PropertyName)), _
                                  New PrimitiveExpression("value"))
        End If
    End Sub
    Private Sub AddMethodToClass(ByVal Source As [Method], ByVal Proxy As [Class])
        Dim Builder As New ElementBuilder
        Dim Dest As Method = Builder.AddMethod(Proxy, Source.MemberType, Source.Name)
        Dest.Visibility = Source.Visibility
        Dest.IsStatic = Source.IsStatic
        Dim Arguments As New List(Of String)
        For Each SourceParam As Param In Source.Parameters
            Dest.Parameters.Add(SourceParam)
            Arguments.Add(SourceParam.Name)
        Next
        Dim MethodCall = Builder.BuildMethodCall(Proxificate(Source.Name), Arguments.ToArray)
        If Source.MethodType = MethodTypeEnum.Void Then
            Dest.AddNode(MethodCall)
        ElseIf Source.MethodType = MethodTypeEnum.Function Then
            Dest.AddNode(Builder.BuildReturn(MethodCall))
        End If
    End Sub
    Private Function Proxificate(ByVal Name As String) As String
        Return String.Format("{0}.{1}", mProxyField.Name, Name)
    End Function

End Class