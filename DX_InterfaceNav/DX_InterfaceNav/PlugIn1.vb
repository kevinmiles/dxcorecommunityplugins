Option Infer On
Option Strict On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports DevExpress.Refactor.Core
Imports System.Runtime.CompilerServices

Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        'CreateNavigateToInterfaceFromClass()
        CreateNavigateToImplementor()
        CreateToImplementorFromInterfaceMethod()

        CreateNavigateToImplementorFromVarDeclaration()
        CreateNavigateToImplementorFromVarReference()

        CreateNavigateToImplementorFromMethodReference()

        CreateNavigateToImplementorFromClassImplementingInterface()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Private mElements As Dictionary(Of String, IElement)

#Region "NavigateToImplementorFromInterface"
    '   Interface Declaration -> Class(Of Interface)
    Public Sub CreateNavigateToImplementor()
        Dim NavigateToImplementor As New DevExpress.CodeRush.Library.NavigationProvider(components)
        CType(NavigateToImplementor, System.ComponentModel.ISupportInitialize).BeginInit()
        NavigateToImplementor.ProviderName = "NavigateToImplementorFromInterface" ' Should be Unique
        NavigateToImplementor.DisplayName = "To Implementor"
        AddHandler NavigateToImplementor.CheckAvailability, AddressOf NavigateToImplementor_CheckAvailability
        AddHandler NavigateToImplementor.Apply, AddressOf NavigateToImplementor_Apply
        CType(NavigateToImplementor, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub NavigateToImplementor_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        Dim [Interface] = TryCast(ea.CodeActive, [Interface])
        If [Interface] Is Nothing Then
            Exit Sub
        End If
        Dim Classes = CType(GetClassesImplementingInterface([Interface]), IEnumerable(Of IElement))
        Call PopulateMenuWithElements(ea, Classes)
        ea.Available = Classes.Any
    End Sub
    Private Sub NavigateToImplementor_Apply(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        JumpToFirstElementWithName(ea.SelectedSubMenuItem.Name)
    End Sub
#End Region
#Region "NavigateToImplementorFromInterfaceMethod"
    '   Interface Declaration.Member -> Class(Of Interface).Member
    Public Sub CreateToImplementorFromInterfaceMethod()
        Dim ToImplementorFromInterfaceMethod As New DevExpress.CodeRush.Library.NavigationProvider(components)
        CType(ToImplementorFromInterfaceMethod, System.ComponentModel.ISupportInitialize).BeginInit()
        ToImplementorFromInterfaceMethod.ProviderName = "ToImplementorFromInterfaceMethod" ' Should be Unique
        ToImplementorFromInterfaceMethod.DisplayName = "To Implementor"
        AddHandler ToImplementorFromInterfaceMethod.CheckAvailability, AddressOf ToImplementorFromInterfaceMethod_CheckAvailability
        AddHandler ToImplementorFromInterfaceMethod.Apply, AddressOf ToImplementorFromInterfaceMethod_Apply
        CType(ToImplementorFromInterfaceMethod, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub ToImplementorFromInterfaceMethod_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        Dim Method = TryCast(ea.CodeActive, Method)
        If Method Is Nothing Then
            Exit Sub
        End If
        If Not Method.Parent.ElementType = LanguageElementType.Interface Then
            Exit Sub
        End If
        Dim Members = GetMembersImplementingInterfaceMember(CType(Method.Parent, IInterfaceElement), Method)
        Call PopulateMenuWithElements(ea, Members)
        ea.Available = Members.Any
    End Sub

    'Private Function GetMembersImplementingInterfaceMethod(ByVal [Interface] As IInterfaceElement, ByVal Method As Method) As System.Collections.Generic.IEnumerable(Of IElement)
    '    Return CType(GetMembersImplementingInterfaceMethod([Interface], Method), IEnumerable(Of IElement))
    'End Function
    Private Function GetMembersImplementingInterfaceMember(ByVal [Interface] As IInterfaceElement, ByVal InterfaceMember As Member) As IEnumerable(Of IElement)
        Dim Types = GetClassesImplementingInterface([Interface])
        Dim Members = Types.SelectMany(Function(TheType) TheType.Members.OfType(Of Member)())
        Return Members.Where(Function(FoundMember) MemberImplementsMember(FoundMember, InterfaceMember)).Cast(Of IElement)()
    End Function
    Private Sub ToImplementorFromInterfaceMethod_Apply(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        JumpToFirstElementWithName(ea.SelectedSubMenuItem.Name)
    End Sub
#End Region
#Region "NavigateToImplementorFromVarDeclaration"
    '   Declaration of Variable(Of Interface) -> Class(Of Interface)
    Public Sub CreateNavigateToImplementorFromVarDeclaration()
        Dim NavigateToImplementorFromVarDeclaration As New DevExpress.CodeRush.Library.NavigationProvider(components)
        CType(NavigateToImplementorFromVarDeclaration, System.ComponentModel.ISupportInitialize).BeginInit()
        NavigateToImplementorFromVarDeclaration.ProviderName = "NavigateToImplementorFromVarDeclaration" ' Should be Unique
        NavigateToImplementorFromVarDeclaration.DisplayName = "To Implementor"
        AddHandler NavigateToImplementorFromVarDeclaration.CheckAvailability, AddressOf NavigateToImplementorFromVarDeclaration_CheckAvailability
        AddHandler NavigateToImplementorFromVarDeclaration.Apply, AddressOf NavigateToImplementorFromVarDeclaration_Apply
        CType(NavigateToImplementorFromVarDeclaration, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub NavigateToImplementorFromVarDeclaration_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ' Ensure Variable Declaration
        Dim Variable = TryCast(ea.CodeActive, Variable)
        If Variable Is Nothing Then
            Exit Sub
        End If
        Dim TheInterface = GetFirstInterfaceOfDeclaration(Variable)
        PopulateWithClassesImplementingInterfaces(TheInterface.ToList, ea)
    End Sub

    Private Sub NavigateToImplementorFromVarDeclaration_Apply(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        JumpToFirstElementWithName(ea.SelectedSubMenuItem.Name)
    End Sub

#End Region
#Region "NavigateToImplementorFromVarReference"
    Public Sub CreateNavigateToImplementorFromVarReference()
        Dim NavigateToImplementorFromVarReference As New DevExpress.CodeRush.Library.NavigationProvider(components)
        CType(NavigateToImplementorFromVarReference, System.ComponentModel.ISupportInitialize).BeginInit()
        NavigateToImplementorFromVarReference.ProviderName = "NavigateToImplementorFromVarReference" ' Should be Unique
        NavigateToImplementorFromVarReference.DisplayName = "To Implementor"
        AddHandler NavigateToImplementorFromVarReference.CheckAvailability, AddressOf NavigateToImplementorFromVarReference_CheckAvailability
        AddHandler NavigateToImplementorFromVarReference.Apply, AddressOf NavigateToImplementorFromVarReference_Apply
        CType(NavigateToImplementorFromVarReference, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub NavigateToImplementorFromVarReference_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        Dim Reference = TryCast(ea.CodeActive, ElementReferenceExpression)
        If Reference Is Nothing Then
            Exit Sub
        End If
        Dim Declaration = CType((New SourceTreeResolver()).GetDeclaration(Reference), IHasType)
        If Declaration Is Nothing Then
            Exit Sub
        End If
        If TypeOf Declaration Is IMethodElement OrElse TypeOf Declaration Is IPropertyElement Then
            ' Reference is to method Or Property
            Exit Sub
        End If

        Dim TheInterface = GetFirstInterfaceOfDeclaration(Declaration)
        PopulateWithClassesImplementingInterfaces(TheInterface.ToList, ea)
    End Sub
    Private Sub NavigateToImplementorFromVarReference_Apply(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        JumpToFirstElementWithName(ea.SelectedSubMenuItem.Name)
    End Sub
#End Region

#Region "NavigateToImplementorFromMethodReference"
    Public Sub CreateNavigateToImplementorFromMethodReference()
        Dim NavigateToImplementorFromMethodReference As New DevExpress.CodeRush.Library.NavigationProvider(components)
        CType(NavigateToImplementorFromMethodReference, System.ComponentModel.ISupportInitialize).BeginInit()
        NavigateToImplementorFromMethodReference.ProviderName = "NavigateToImplementorFromMethodReference" ' Should be Unique
        NavigateToImplementorFromMethodReference.DisplayName = "To Implementor"
        AddHandler NavigateToImplementorFromMethodReference.CheckAvailability, AddressOf NavigateToImplementorFromMethodReference_CheckAvailability
        AddHandler NavigateToImplementorFromMethodReference.Apply, AddressOf NavigateToImplementorFromMethodReference_Apply
        CType(NavigateToImplementorFromMethodReference, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub NavigateToImplementorFromMethodReference_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        Dim Member As Member
        If TypeOf ea.CodeActive.GetDeclaration Is Variable Then
            Exit Sub
        End If
        Dim MRE = TryCast(ea.CodeActive, MethodReferenceExpression)
        If MRE Is Nothing Then
            ' Ammend this later to account for ()less MRE represented by an ERE.
            ' This technically does translate correctly to an MRE
            Dim ERE = TryCast(ea.CodeActive, ElementReferenceExpression)
            If ERE Is Nothing Then
                Exit Sub
            End If
            'Translate ERE back to MRE
            Member = TryCast(ERE.GetDeclaration, Member)
        Else
            Member = TryCast(MRE.GetDeclaration, Member)
        End If
        If Member Is Nothing Then
            ' Couldn't get declaration of either ERE or MRE
            Exit Sub
        End If
        ' Have MRE. Need to work out Interface that method represents...
        ' ...then pass the interface and interface method to GetMembersImplementingInterfaceMethod and populate the list with that. 

        ' Get interfaces implemented by Class
        Dim MemberParent As TypeDeclaration = TryCast(Member.Parent, TypeDeclaration)
        If MemberParent Is Nothing Then
            Exit Sub
        End If
        Dim Interfaces = GetTypeInterfaces(MemberParent)
        Dim InterfaceMembers = GetInterfaceMemberMatchingMember(Interfaces, Member)
        Dim Methods As New List(Of IElement)
        For Each InterfaceMember In InterfaceMembers
            Dim [Interface] = CType(InterfaceMember.Parent, IInterfaceElement)
            Methods.AddRange(GetMembersImplementingInterfaceMember([Interface], InterfaceMember))
        Next
        PopulateMenuWithElements(ea, Methods)
        ea.Available = Methods.Any
    End Sub
    Private Shared Function GetTypeInterfaces(ByVal Type As ITypeElement) As IEnumerable(Of IInterfaceElement)
        Dim Interfaces As IEnumerable(Of IInterfaceElement) = Nothing
        If Type.ElementType = LanguageElementType.Class Then
            Interfaces = GetClassInterfaces(CType(Type, IClassElement))
        ElseIf Type.ElementType = LanguageElementType.Interface Then
            ' Find implementors of (Interface, InterfaceMember)
            Interfaces = CType(Type, IInterfaceElement).ToList
        End If
        Return Interfaces
    End Function
    Private Shared Function GetInterfaceMemberMatchingMember(ByVal Interfaces As IEnumerable(Of IInterfaceElement), ByVal Member As Member) As List(Of Member)
        ' Get InterfaceMethods that match The Passed Method
        Dim Results As New List(Of Member)
        For Each [Interface] As [Interface] In Interfaces
            For Each InterfaceMember As Member In [Interface].AllMethods
                If MemberImplementsMember(Member, InterfaceMember) Then
                    Results.Add(InterfaceMember)
                    Exit For
                End If
            Next
            For Each InterfaceMember As Member In [Interface].AllProperties
                If MemberImplementsMember(Member, InterfaceMember) Then
                    Results.Add(InterfaceMember)
                    Exit For
                End If
            Next
        Next
        Return Results
    End Function
    Private Shared Function GetClassInterfaces(ByVal [Class] As IClassElement) As IEnumerable(Of IInterfaceElement)
        Dim Results As New List(Of IInterfaceElement)
        Dim AncestorReferences = [Class].SecondaryAncestors.OfType(Of ITypeReferenceExpression)()
        For Each Reference In AncestorReferences
            Dim TypeDeclaration As ITypeElement = TryCast(Reference.GetDeclaration, ITypeElement)
            If TypeOf TypeDeclaration Is IInterfaceElement Then
                Results.Add(CType(TypeDeclaration, IInterfaceElement))
            End If
        Next
        'Results.AddRange(AncestorReferences.Cast(Of IInterfaceElement))
        Dim PrimaryAncestor = [Class].PrimaryAncestor

        If PrimaryAncestor IsNot Nothing AndAlso TypeOf PrimaryAncestor.GetDeclaration Is IInterfaceElement Then
            Results.Add(CType(PrimaryAncestor.GetDeclaration, IInterfaceElement))
        End If
        Return Results
        'Dim TypeReferences = SecondaryAncestors.Concat(
        'Return TypeReferences.Select(Function(i) CType(i.GetDeclaration, IInterfaceElement))


        'Return [Class].GetImplements.OfType(Of IInterfaceElement)()
    End Function
    Private Sub NavigateToImplementorFromMethodReference_Apply(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        JumpToFirstElementWithName(ea.SelectedSubMenuItem.Name)
    End Sub
#End Region

#Region "NavigateToImplementorFromClassImplementingInterface"
    Public Sub CreateNavigateToImplementorFromClassImplementingInterface()
        Dim NavigateToImplementorFromClassImplementingInterface As New DevExpress.CodeRush.Library.NavigationProvider(components)
        CType(NavigateToImplementorFromClassImplementingInterface, System.ComponentModel.ISupportInitialize).BeginInit()
        NavigateToImplementorFromClassImplementingInterface.ProviderName = "ToImplementor" ' Should be Unique
        NavigateToImplementorFromClassImplementingInterface.DisplayName = "To Implementor"
        AddHandler NavigateToImplementorFromClassImplementingInterface.CheckAvailability, AddressOf NavigateToImplementorFromClassImplementingInterface_CheckAvailability
        AddHandler NavigateToImplementorFromClassImplementingInterface.Apply, AddressOf NavigateToImplementorFromClassImplementingInterface_Apply
        CType(NavigateToImplementorFromClassImplementingInterface, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub NavigateToImplementorFromClassImplementingInterface_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        Dim [Class] = TryCast(ea.CodeActive, [Class])
        If [Class] Is Nothing Then
            Exit Sub
        End If
        Dim Interfaces = GetTypeInterfaces(CType([Class], TypeDeclaration))
        PopulateWithClassesImplementingInterfaces(Interfaces, ea)
    End Sub
    Private Sub PopulateWithClassesImplementingInterfaces(ByVal Interfaces As IEnumerable(Of IInterfaceElement), ByVal ea As CheckContentAvailabilityEventArgs)
        If Interfaces Is Nothing Then
            Exit Sub
        End If
        Dim ItemAdded As Boolean
        For Each [Interface] In Interfaces
            Dim Classes = CType(GetClassesImplementingInterface([Interface]), IEnumerable(Of IElement))
            mElements = New Dictionary(Of String, IElement)
            For Each Element In Classes
                Call AddItemToMenu(ea, mElements, Element)
                ItemAdded = True
            Next
        Next
        ea.Available = ItemAdded
    End Sub
    Private Sub NavigateToImplementorFromClassImplementingInterface_Apply(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        JumpToFirstElementWithName(ea.SelectedSubMenuItem.Name)
    End Sub
#End Region

#Region "Utility"
#Region "Menu Funcs"
    Private Sub PopulateMenuWithElements(ByVal ea As CheckContentAvailabilityEventArgs, ByVal Elements As IEnumerable(Of IElement))
        mElements = New Dictionary(Of String, IElement)
        If Not Elements.Any Then
            Return
        End If

        For Each Element In Elements
            Call AddItemToMenu(ea, mElements, Element)
        Next
    End Sub
    Private Sub AddItemToMenu(ByVal ea As CheckContentAvailabilityEventArgs, _
                              ByVal Dict As Dictionary(Of String, IElement), _
                              ByVal Element As IElement)
        Dim Key As String = Element.FullName
        Dict.Add(Key, Element)
        ea.AddSubMenuItem(Key, CType(Element, IElement).FullName)
    End Sub
#End Region
#Region "Get"
    Private Function GetFirstInterfaceOfDeclaration(ByVal Declaration As IHasType) As IInterfaceElement
        Dim DeclarationType = CType((New SourceTreeResolver()).GetDeclaration(Declaration.Type), ITypeElement)
        Return GetTypeInterfaces(DeclarationType).First
    End Function
    Private Function GetClassesImplementingInterface(ByVal [Interface] As IInterfaceElement) As IEnumerable(Of IClassElement)
        Dim Result As New List(Of IClassElement)
        Dim AllTypes = AllSolutionTypes()
        For Each Type In AllTypes
            If Type.DescendsFrom([Interface]) Then
                Result.Add(Type)
            End If
        Next
        Return Result
    End Function

#End Region

    Private Shared Function MemberImplementsMember(ByVal Member As IMemberElement, ByVal InterfaceMember As IMemberElement) As Boolean
        If (Member Is Nothing OrElse InterfaceMember Is Nothing) Then
            Return False
        End If

        Dim InterfaceType = TryCast(InterfaceMember.ParentType, IInterfaceElement)
        If (InterfaceType Is Nothing) Then
            Return False
        End If
        Dim MemberType = TryCast(Member.ParentType, ITypeElement)
        If (MemberType Is Nothing) Then
            Return False
        End If

        If (Not MemberType.DescendsFrom(InterfaceType)) AndAlso Not MemberType Is InterfaceType Then
            Return False
        End If
        If Not (TypeOf Member Is IWithParameters = TypeOf InterfaceMember Is IWithParameters) Then
            Return False
        End If
        If (Not SignatureHelper.SignaturesMatch(New SourceTreeResolver(), _
                                                CType(Member, IWithParameters), _
                                                CType(InterfaceMember, IWithParameters), True)) Then
            Return False
        End If
        Return True
    End Function

    Private Shared Function AllSolutionTypes() As IEnumerable(Of IClassElement)
        Dim Solution = CodeRush.Source.ActiveSolution
        Dim Projects = Solution.AllProjects.OfType(Of ProjectElement)()
        Return Projects.SelectMany(Function(Project) Project.AllTypes.OfType(Of IClassElement)())
    End Function

    Private Sub JumpToFirstElementWithName(ByVal ElementName As String)
        CodeRush.Navigation.Navigate(mElements(ElementName))
    End Sub
#End Region
End Class

Public Module SomeTypeExt
    <Extension()> _
    Public Function ToList(Of T)(ByVal Source As T) As IEnumerable(Of T)
        Dim X As New List(Of T)
        X.Add(Source)
        Return X
    End Function
End Module