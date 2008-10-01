Imports System.ComponentModel
Imports System.Drawing
Imports System.Environment
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class ImplementBaseConstructors

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Private Sub CODE_ImplementBaseConstructors_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs) Handles CODE_ImplementBaseConstructors.CheckAvailability
        ea.Available = TypeOf ea.Element Is [Class]
    End Sub
    Private Sub CODE_ImplementBaseConstructors_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles CODE_ImplementBaseConstructors.Apply
        ' Assume based on Availability that ea.Element is a a Class
        Dim ThisClass As [Class] = CType(ea.Element, [Class])
        Dim ParentClass As IClassElement = CType(ThisClass.PrimaryAncestorType.GetDeclaration, IClassElement)
        Dim OldConstructors As New List(Of IMethodElement)
        ' Gather Old Constructors
        For Each Member As IMemberElement In ParentClass.Members
            If TypeOf Member Is IMethodElement Then
                Dim Method As IMethodElement = CType(Member, IMethodElement)
                If Method.IsConstructor Then
                    OldConstructors.Add(Method)
                End If
            End If
        Next
        ' Generate New Constructors
        Dim NewConstructors As New List(Of Method)
        For Each Constructor As IMethodElement In OldConstructors
            Dim NewConstructor As Method = BaseInitialisedConstructorOf(ThisClass.Name, Constructor)
            ThisClass.AddNode(NewConstructor) ' Add to tree for Context
            NewConstructors.Add(NewConstructor)
        Next
        Call NewConstructors.Reverse()
        ' Render New Constructors
        Dim Insertpoint As SourcePoint = ThisClass.BlockCodeRange.Start
        For Each Constructor As Method In NewConstructors
            ea.TextDocument.InsertText(Insertpoint, NewLine & CodeRush.Language.GenerateElement(Constructor))
            'Insertpoint = Constructor.BlockRange.End.OffsetPoint(1, -1 * Constructor.BlockRange.End.Offset + 1)
        Next
    End Sub
    Private MyBuilder As New ElementBuilder
    Private Function BaseInitialisedConstructorOf(ByVal NewConstructorClassName As String, ByVal ParentClassConstructor As IMethodElement) As Method
        Dim NewConstructor As Method = MyBuilder.BuildConstructor(NewConstructorClassName)
        For Each Param As IParameterElement In ParentClassConstructor.Parameters
            Dim SafeParamName As String = CodeRush.Language.GetIdentifierFromKeyword(Param.Name)
            NewConstructor.Parameters.Add(New Param(Param.Type.Name, SafeParamName))
        Next
        Dim Arguments As ExpressionCollection = ArgumentsFromParams(NewConstructor.Parameters)
        NewConstructor.AddNode(MyBuilder.BuildBaseConstructorInitializer(Arguments))
        Return NewConstructor
    End Function
    Private Function ArgumentsFromParams(ByVal Parameters As LanguageElementCollection) As ExpressionCollection
        Dim Arguments As New ExpressionCollection
        For Each Param As IParameterElement In Parameters
            Arguments.Add(New PrimitiveExpression(Param.Name))
        Next
        Return Arguments
    End Function

End Class
