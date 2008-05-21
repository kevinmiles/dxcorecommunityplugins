Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Collections
Imports System.Diagnostics
Imports System.Reflection
Imports System.Threading
Imports System.IO

Namespace CR_EnhancedForEach
    ''' <summary>
    ''' Summary description for CR_EnhancedForEachPlugIn.
    ''' </summary>
    Public Class CR_EnhancedForEachPlugIn
        Inherits StandardPlugIn
        Private Const strIEnum As String = "System.Collections.IEnumerable"
        Private Const strGenericIEnum As String = "System.Collections.Generic.IEnumerable"
        Friend WithEvents ForEachAction As DevExpress.CodeRush.Core.Action
        Friend WithEvents ForEachInit As DevExpress.CodeRush.Extensions.StringProvider
        Friend WithEvents ForEachVar As DevExpress.CodeRush.Extensions.StringProvider
        Friend WithEvents ForEachVarType As DevExpress.CodeRush.Extensions.StringProvider
        Friend WithEvents ForEachVarItemName As DevExpress.CodeRush.Extensions.StringProvider
        Private projHash As New Hashtable
        Private shared _Cache As new Hashtable
        Dim isOldVS As Boolean= CodeRush.ApplicationObject.Version.StartsWith("7")

#Region " private fields... "
        Private components As System.ComponentModel.IContainer
#End Region

        ' constructor...
#Region " CR_EnhancedForEachPlugIn "
        Public Sub New()
            ''' <summary>
            ''' Required for Windows.Forms Class Composition Designer support
            ''' </summary>
            InitializeComponent()
        End Sub
#End Region

        ' CodeRush-generated code
#Region " InitializePlugIn "
        Public Overrides Sub InitializePlugIn()
            MyBase.InitializePlugIn()

            '
            ' TODO: Add your initialization code here.
            ''
        End Sub
#End Region
#Region " FinalizePlugIn "
        Public Overrides Sub FinalizePlugIn()
            '
            ' TODO: Add your finalization code here.
            '

            MyBase.FinalizePlugIn()
        End Sub
#End Region

#Region " Component Designer generated code "
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CR_EnhancedForEachPlugIn))
            Me.ForEachAction = New DevExpress.CodeRush.Core.Action(Me.components)
            Me.ForEachInit = New DevExpress.CodeRush.Extensions.StringProvider(Me.components)
            Me.ForEachVar = New DevExpress.CodeRush.Extensions.StringProvider(Me.components)
            Me.ForEachVarType = New DevExpress.CodeRush.Extensions.StringProvider(Me.components)
            Me.ForEachVarItemName = New DevExpress.CodeRush.Extensions.StringProvider(Me.components)
            CType(Me.ForEachAction, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ForEachInit, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ForEachVar, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ForEachVarType, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ForEachVarItemName, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'ForEachAction
            '
            Me.ForEachAction.ActionName = "EnhancedForEach"
            Me.ForEachAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
            Me.ForEachAction.Image = CType(resources.GetObject("ForEachAction.Image"), System.Drawing.Bitmap)
            Me.ForEachAction.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
            '
            'ForEachInit
            '
            Me.ForEachInit.Description = ""
            Me.ForEachInit.DisplayName = "ForEachInit"
            Me.ForEachInit.ProviderName = "ForEachInit"
            Me.ForEachInit.Register = True
            '
            'ForEachVar
            '
            Me.ForEachVar.Description = ""
            Me.ForEachVar.DisplayName = "ForEachVarName"
            Me.ForEachVar.ProviderName = "ForEachVarName"
            Me.ForEachVar.Register = True
            '
            'ForEachVarType
            '
            Me.ForEachVarType.Description = ""
            Me.ForEachVarType.DisplayName = "ForEachVarType"
            Me.ForEachVarType.ProviderName = "ForEachVarType"
            Me.ForEachVarType.Register = True
            '
            'ForEachVarItemName
            '
            Me.ForEachVarItemName.Description = ""
            Me.ForEachVarItemName.DisplayName = "ForEachVarItemName"
            Me.ForEachVarItemName.ProviderName = "ForEachVarItemName"
            Me.ForEachVarItemName.Register = True
            '
            'CR_EnhancedForEachPlugIn
            '
            CType(Me.ForEachAction, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ForEachInit, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ForEachVar, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ForEachVarType, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ForEachVarItemName, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
#End Region

        Private _EnumVars As New ArrayList
        Private theClass As [Class]
        Private theStruct As struct
        Private _parentClassOrStructName As String
        Private activeLe As LanguageElement
        Private _isStatic As Boolean

        Private Sub FillArray(scope as string)
            _EnumVars.Clear()
            debug.WriteLine(CodeRush.Documents.ActiveTextDocument.ParseState.ToString)
            If Not CodeRush.Documents.ActiveTextDocument Is Nothing andalso (CodeRush.Documents.ActiveTextDocument.ParseState=DocumentParseState.OutOfSyncDueToCommittedChanges orelse CodeRush.Documents.ActiveTextDocument.ParseState=DocumentParseState.OutOfSyncDueToUncommittedTextChanges) Then
                CodeRush.Documents.ActiveTextDocument.ParseIfTextChanged()
            End If

            activeLe = coderush.Source.GetNodeBefore(New SourcePoint(coderush.Caret.Line, coderush.Caret.Offset))
            If activeLe Is Nothing Then
                theclass=CodeRush.Source.ActiveClass
                thestruct=CodeRush.Source.ActiveStruct
            Else
                theclass = activeLe.GetClass 
                thestruct=activeLe.GetStruct
            End If
            If (Not activeLe Is Nothing AndAlso activeLe.InsideStruct) OrElse (activeLe Is Nothing AndAlso not theStruct Is Nothing) Then
                If Not activeLe Is Nothing Then
                    _parentClassOrStructName = activele.GetStruct.Name    
                Else
                    _parentClassOrStructName=theStruct.Name
                End If
                
            Else
                _parentClassOrStructName = theclass.Name
            End If
            _isstatic= False 
            dim themethod as Method=coderush.Source.ActiveMethod
            Dim theProperty As [Property] = CodeRush.Source.ActiveProperty
            If Not themethod Is Nothing Then
                _isStatic=themethod.IsStatic
            End If
            If Not theProperty Is Nothing Then
                _isStatic=theProperty.IsStatic
            End If
            'FillVarsForMainClass()
            If scope.ToLower="class" Then
                If Not theClass Is Nothing Then
                    FillVarsForClass(theClass)
                ElseIf not theStruct Is Nothing Then
                    FillVarsForClass(theStruct)
                End If
            End If
            FillVarsLocal()
            AddClipBoardItem()
            _EnumVars.Add(New EnumerableElement(" New", "System.Object", False))
            If _EnumVars.Count > 0 Then
                _EnumVars.Sort()
            End If
        End Sub
        'Private Sub FillVarsForMainClass()
        '    If Not CodeRush.Documents.ActiveTextDocument Is Nothing Then
        '        CodeRush.Documents.ActiveTextDocument.ParseIfTextChanged
        '    End If
        '    dim le as LanguageElement=coderush.Source.GetNodeBefore(new SourcePoint(coderush.Caret.Line,coderush.Caret.Offset))
        '    If le Is Nothing Then
        '        Return
        '    End If
        '    dim m as Method
        '    dim p as [Property]
        '    m=le.GetMethod
        '    If not m is nothing Then
        '        le=m.PreviousSibling
        '    Else
        '        p=le.GetProperty
        '        If Not p Is Nothing Then
        '            le=p.PreviousSibling
        '        End If
        '    End If
        '    do while not typeof le is [Class] andalso not le is nothing
        '        If typeof le is Variable Then
        '            CheckAndAddVar(le,false)
        '        End If
        '        le=le.PreviousSibling
        '    Loop
        'End Sub

        Private Sub FillVarsForClass(ByVal theElement As LanguageElement, Optional ByVal isParent As Boolean = False)
            Dim ienum As IEnumerator
            dim theclass as [Class]
            dim theStruct as struct
            If typeof theElement is [Class] Then
                theclass=directcast(theElement,[Class])
                ienum= theclass.AllVariables.GetEnumerator
            elseif typeof theElement is struct Then
                thestruct=directcast(theElement,struct)
                ienum= thestruct.AllVariables.GetEnumerator
            End If
             
            While ienum.MoveNext
                Dim var As Variable = DirectCast(ienum.Current, Variable)
                If (Not var.Visibility = MemberVisibility.Local AndAlso Not var.Visibility = MemberVisibility.Param) Then
                    If Not isParent OrElse Not var.Visibility = MemberVisibility.Private Then
                        'If Not var.GetClass Is Nothing AndAlso ((var.InsideStruct AndAlso var.GetStruct.Name = _parentClassOrStructName) OrElse Not var.InsideStruct) Then
                        If not(var.InsideMethod orelse var.InsideProperty) Then
                            If (not var.GetClass is nothing andalso var.GetClass.Name=_parentClassOrStructName)  Then
                                 If not _isStatic orelse var.IsStatic  Then
                                    CheckAndAddVar(var, False)
                                End If
                            End If
                            'If Not var.GetClass Is Nothing AndAlso ((var.InsideStruct AndAlso var.GetStruct.Name = _parentClassOrStructName) OrElse Not var.InsideStruct) Then
                               
                            'End If
                        End If
                    End If

                End If
                Dim tst As String
            End While
            'If Not theclass Is Nothing Then
            '    If Not theclass.PrimaryAncestorType Is Nothing Then
            '        If TypeOf theclass.PrimaryAncestorType.GetDeclaration Is [Class] Then
            '            Dim parentclass As [Class] = DirectCast(theclass.PrimaryAncestorType.GetDeclaration, [Class])
            '            FillVarsForClass(parentclass, True)
            '        End If
            '    End If
            'ElseIf Not theStruct Is Nothing Then
            '    If Not theStruct.PrimaryAncestorType Is Nothing Then
            '        if  TypeOf theStruct.PrimaryAncestorType.GetDeclaration Is struct Then
            '            Dim parentstruct As struct = DirectCast(theStruct.PrimaryAncestorType.GetDeclaration, struct)
            '            FillVarsForClass(parentstruct, True)
            '        End If
            '    End If
            'end if
               
        End Sub

        Private Sub ForEachAction_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles ForEachAction.Execute
            FillArray("class")
        End Sub

        Private Sub AddClipBoardItem()
            If clipboardEnumerableElement Is Nothing Then
                Return
            End If
            For Each item As EnumerableElement In _EnumVars
                If item.ToString = clipboardEnumerableElement.ToString Then
                    Return
                End If
            Next
            _EnumVars.Add(clipboardEnumerableElement)
        End Sub
        Private Sub FillVarsLocal()
            Dim activenode As LanguageElement = CodeRush.Source.GetNodeBefore(CodeRush.Caret.SourcePoint)
            If activenode Is Nothing Then
                Return
            End If
            If activenode.GetMethod Is Nothing AndAlso activenode.GetProperty Is Nothing Then
                Return
            End If
            Do While Not TypeOf activenode Is Method AndAlso Not TypeOf activenode Is [Property] AndAlso Not activenode Is Nothing
                If TypeOf activenode Is Variable OrElse TypeOf activenode Is Param Then
                    Debug.WriteLine(activenode.Name)
                    CheckAndAddVar(activenode, True)
                End If
                activenode = activenode.PreviousNode
            Loop
        End Sub
        Private Sub CheckAndAddVar(ByVal var As LanguageElement, ByVal local As Boolean)
            debug.WriteLine(var.Name)
            Dim typeElement As IElement
            If CanDoForEach(var,typeElement) Then
                'Dim leType As LanguageElement = GetTypeLanguageElement(var)
                'Dim leType As LanguageElement = GetTypeLanguageElement(var)
                If typeElement Is Nothing Then
                    typeElement = ResolveType(var)
                End If
                 
                If Not typeElement Is Nothing AndAlso Not typeElement.Name.ToLower = "string" Then
                    _EnumVars.Add(New EnumerableElement(var.Name,var, nothing, local))
                End If
            End If
        End Sub
        Private Shared Function GetTypeLanguageElement(ByVal le As LanguageElement) As LanguageElement
            Dim leType As LanguageElement
            If typeof le is Variable Then
                    Dim var As Variable
                var = DirectCast(le, Variable)
                leType = var.MemberTypeReference
                If Not leType Is Nothing Then
                    Return leType
                End If
                leType = var.DetailNodes(0)
                If Not TypeOf leType Is ITypeReferenceExpression Then
                    If DirectCast(var.DetailNodes(0), LanguageElement).NodeCount > 0 AndAlso TypeOf DirectCast(var.DetailNodes(0), LanguageElement).Nodes(0) Is ITypeReferenceExpression Then
                        leType = DirectCast(var.DetailNodes(0), LanguageElement).Nodes(0)
                    End If
                End If
            elseif typeof le is Param then
                Dim prm As param
                prm = DirectCast(le, param)
                
                leType = prm.MemberTypeReference
                If Not leType Is Nothing Then
                    Return leType
                End If
                leType = prm.DetailNodes(0)
                If Not TypeOf leType Is ITypeReferenceExpression Then
                    If DirectCast(prm.DetailNodes(0), LanguageElement).NodeCount > 0 AndAlso TypeOf DirectCast(prm.DetailNodes(0), LanguageElement).Nodes(0) Is ITypeReferenceExpression Then
                        leType = DirectCast(prm.DetailNodes(0), LanguageElement).Nodes(0)
                    End If
                End If
            End If
            
            Return leType
        End Function
        Private  Function CanDoForEach(ByVal le As LanguageElement, byref leTypeDecl As IElement) As Boolean
            'Dim letypeIElement As IElement=ResolveType(le)
            
            'If Not letypeIElement Is Nothing Then
            '    If _Cache.ContainsKey(letypeIElement.FullName) Then
            '        Return _cache(letypeIElement.FullName)
            '    End If
            'End If
            
            If TypeOf le Is Variable orelse typeof le is Param Then 'AndAlso Not TypeOf le Is ImplicitVariable Then
                Dim leType As LanguageElement
                If typeof le is Variable Then
                    leType = DirectCast(le, Variable).MemberTypeReference
                Else
                    letype= DirectCast(le, Param).MemberTypeReference
                End If
                If letype.Name.ToLower = "string" OrElse letype.Name.ToLower = "system.string" Then
                    Return False 
                End If
                leTypeDecl = leType.GetDeclaration
                If Not leTypeDecl Is Nothing Then
                    If _Cache.ContainsKey(leTypeDecl.FullName) Then
                        Return _cache(leTypeDecl.FullName)
                    End If
                End If
                If not typeof leTypeDecl is IClassElement andalso not  typeof leTypeDecl is IInterfaceElement Then
                    If Not leTypeDecl Is Nothing andalso leTypeDecl.InReferencedAssembly Then
                        If not _Cache.ContainsKey(leTypeDecl.FullName) Then
                                _Cache.Add(leTypeDecl.FullName, False)
                        End If
                    End If
                    Return False
                End If
                If ImplementsIEnumerable(leTypeDecl) Then
                   If Not leTypeDecl Is Nothing andalso leTypeDecl.InReferencedAssembly Then
                        If not _Cache.ContainsKey(leTypeDecl.FullName) Then
                            _Cache.Add(leTypeDecl.FullName, True)
                        End If
                    End If
                    Return True
                End If
                'End If
            'ElseIf TypeOf le Is Param Then 'AndAlso Not TypeOf le Is ImplicitVariable Then
            '    Dim leType As LanguageElement = DirectCast(le, Param).MemberTypeReference
            '    If letype.Name.ToLower = "string" OrElse letype.Name.ToLower = "system.string" Then
            '        Return False
            '    End If
            '    leTypeDecl = leType.GetDeclaration
            '    If Not leTypeDecl Is Nothing  Then
            '        If _Cache.ContainsKey(leTypeDecl.FullName) Then
            '            Return _cache(leTypeDecl.FullName)
            '        End If
            '    End If
            '    If not typeof leTypeDecl is IClassElement andalso not  typeof leTypeDecl is IInterfaceElement Then
            '        If Not leTypeDecl Is Nothing andalso leTypeDecl.InReferencedAssembly Then
            '            If not _Cache.ContainsKey(leTypeDecl.FullName) Then
            '                    _Cache.Add(leTypeDecl.FullName, False)
            '            End If
            '        End If
            '        Return False
            '    End If
            '    If ImplementsIEnumerable(leTypeDecl) Then
            '       If Not leTypeDecl Is Nothing andalso leTypeDecl.InReferencedAssembly Then
            '            If not _Cache.ContainsKey(leTypeDecl.FullName) Then
            '                _Cache.Add(leTypeDecl.FullName, True)
            '            End If
            '        End If
            '        Return True
            '    End If

            '    'If ImplementsIEnumerable(leTypeDecl) then 'OrElse (TypeOf letype Is TypeReferenceExpression AndAlso DirectCast(letype, TypeReferenceExpression).TypeReferenceType.tostring = TypeReferenceType.Array.ToString) OrElse DirectCast(le, Param).MemberType.EndsWith("()") OrElse DirectCast(le, Param).MemberType.EndsWith("[]") Then
            '    '    Return True
            '    'End If
            End If
        End Function
        Private Sub AddToCache(ByVal strName As String, ByVal isEnumerable As Boolean)
            If not _Cache.ContainsKey(strname) Then
                _Cache.Add(strname,isEnumerable)
            End If
        End Sub
        Private  function ImplementsIEnumerable(ByVal cls As IElement) as Boolean
            'Return True
            Dim impl As ITypeReferenceExpressionCollection
            If _Cache.ContainsKey(cls.FullName) Then
                    Return _cache(cls.FullName)
            End If
            If typeof cls is IClassElement Then
                impl = directcast(cls,IClassElement).SecondaryAncestors
            elseif typeof cls is IInterfaceElement
                If directcast(cls,IInterfaceElement).fullName=strIEnum orelse directcast(cls,IInterfaceElement).fullName=strGenericIEnum Then
                    If cls.InReferencedAssembly Then
                        addtocache(cls.FullName,True)
                    End If
                    Return True
                End If
                impl=directcast(cls,IInterfaceElement).SecondaryAncestors
                'strImplements.Add(directcast(cls,IInterfaceElement).fullName)
            else
                If cls.InReferencedAssembly Then
                    AddToCache(cls.FullName, False)
                End If
                Return False
            End If
            
            For Each interf As ITypeReferenceExpression In impl
                If _Cache.ContainsKey(interf.FullName) Then
                    dim blnRes as Boolean= _cache(interf.FullName)
                    If cls.InReferencedAssembly Then
                        addtocache(cls.FullName,blnRes)
                    End If
                End If
                dim decl as IElement = interf.GetDeclaration
                If  decl Is Nothing Then
                     If interf.FullName=strIEnum orelse interf.fullName=strGenericIEnum Then
                        If cls.InReferencedAssembly Then
                            addtocache(cls.FullName,True)
                        End If
                        Return True
                    End If
                Else
                    Dim lInterfGetDeclarationFullName As String = decl.FullName
                    If lInterfGetDeclarationFullName = strIEnum OrElse lInterfGetDeclarationFullName = strGenericIEnum Then
                        If cls.InReferencedAssembly Then
                            addtocache(cls.FullName,True)
                        End If
                        Return True
                    End If
                End If
            Next
            Dim baseType As ITypeReferenceExpression
            If typeof cls is IClassElement Then
                baseType = directcast(cls,IClassElement).PrimaryAncestor
            elseif typeof cls is IInterfaceElement
                baseType=directcast(cls,IInterfaceElement).PrimaryAncestor
            End If
            If Not baseType Is Nothing Then
                Dim base As IClassElement = baseType.GetDeclaration
                If Not base Is Nothing Then
                    Return ImplementsIEnumerable(base)
                End If
            End If
            If cls.InReferencedAssembly Then
                AddToCache(cls.FullName, False)
            End If
        End function

        Private _NameOnClipboard As String
        Private _TypeOnClipboard As String
        Private _ReturnTypeOnClipboard As String
        Private clipboardEnumerableElement As EnumerableElement

        Private Function GetDetailsCount(ByVal le As LanguageElement) As Integer
            Dim counter As Integer=0
            For Each leDetails As Languageelement In le.DetailNodes
                If leDetails.Range.Start.Line>le.NameRange.Start.Line orelse ((leDetails.Range.Start.Line=le.NameRange.Start.Line) andalso (leDetails.Range.Start.offset>le.NameRange.Start.offset)) Then
                    counter+=1
                End If
            Next
            Return counter
        End Function

        Private Function GetReturnTypeForGeneric(ByVal le As LanguageElement) As String
            Dim details As Integer = GetDetailsCount(le)
            If details = 1 Then
                Dim refEl As LanguageElement = DirectCast(le.DetailNodes(le.DetailNodeCount - 1), LanguageElement)
                Dim strName As String=le.Document.GetText(refEl.Range.Start.Line,refEl.Range.Start.Offset,refEl.Range.End.Line,refEl.Range.End.Offset)
                Return strName
                              
            ElseIf details = 2 Then
                Return "KeyValuePair" & le.Document.GetText(le.NameRange.End.Line, le.NameRange.End.Offset, le.Range.End.Line, le.Range.End.Offset)

            End If
            If TypeOf le Is IClassElement Then
                If Not le.NextNode Is Nothing AndAlso Not le.NextNode.NextNode Is Nothing Then
                    If le.NextNode.NextNode.NextNode Is Nothing Then
                        Return le.NextNode.NextNode.Name
                    Else
                        Return le.NextNode.NextNode.NextNode.Name
                    End If
                End If
            End If
        End Function
        Private Sub CR_EnhancedForEachPlugIn_ClipboardChanged(ByVal ea As DevExpress.CodeRush.Core.ClipboardChangedEventArgs) Handles MyBase.ClipboardChanged
            _NameOnClipboard = ""
            _TypeOnClipboard = ""
            _ReturnTypeOnClipboard = ""

            clipboardEnumerableElement = Nothing
            If ea.Details.Type Is Nothing Then
                Return
            End If
            Dim blnLocal As Boolean
            Dim le As LanguageElement = Nothing
            If ea.Details.HasSource Then
                If ea.Details.HasDocument AndAlso ea.Details.HasSource Then
                    If TypeOf ea.Details.Document Is TextDocument Then
                        Dim doc As TextDocument = DirectCast(ea.Details.Document, TextDocument)
                        doc.ParseIfTextChanged()
                        le = doc.GetNodeAt(ea.Details.Source.AnchorLine, ea.Details.Source.AnchorColumn)
                    End If
                End If
            End If
            If Not ea.Details.Type Is Nothing AndAlso CodeRush.Source.Implements(ea.Details.Type.FullName, strIEnum) Then
                _NameOnClipboard = ea.Details.Text
                _TypeOnClipboard = ea.Details.Type.FullName
                'If ea.Details.Type.FullName.IndexOf("System.Collections.Generic") > -1 Then
                '    _ReturnTypeOnClipboard = GetReturnTypeForGeneric(le)
                'Else
                '    _ReturnTypeOnClipboard = GetReturnType(ea.Details.Type)
                'End If

                'elseif Not ea.Details.Type Is Nothing AndAlso  CodeRush.Source.Implements(ea.Details.Type.FullName,strGenericIEnum) Then
                '    _NameOnClipboard = ea.Details.Text
                '    _TypeOnClipboard = ea.Details.Type.FullName
                '    _ReturnTypeOnClipboard = GetReturnTypeForGeneric(le)
            Else
                If ea.Details.HasDocument AndAlso ea.Details.HasSource AndAlso Not ea.Details.Type Is Nothing Then
                    dim leTypeDecl As IElement
                    If CanDoForEach(le,leTypeDecl) Then
                        _NameOnClipboard = ea.Details.Text
                        _TypeOnClipboard = ea.Details.Type.FullName
                        'If CodeRush.Source.Implements(ea.Details.Type.FullName, strIEnum) Then
                        '    _ReturnTypeOnClipboard = GetReturnType(ea.Details.Type)
                        'Else
                        '    _ReturnTypeOnClipboard = _TypeOnClipboard
                        'End If
                    End If

                End If
            End If
            Dim strClipboardreturnType As String = CodeRush.Strings.[Get]("EnumerableType", _TypeOnClipboard)
            If _NameOnClipboard <> "" Then
                If TypeOf le Is Variable Then
                    blnLocal = DirectCast(le, Variable).Visibility = MemberVisibility.Local
                Else
                    blnLocal = False
                End If
                clipboardEnumerableElement = New EnumerableElement(_NameOnClipboard,strClipboardreturnType,blnlocal)
            End If
            'MessageBox.Show(_ReturnTypeOnClipboard)
        End Sub

        Private Function GetTypeFromAsm(ByVal typename As String, ByVal asm As System.Reflection.Assembly) As Type
            Dim types As Type() = asm.GetTypes()
            For Each lType As Type In types
                'Debug.WriteLine(lType.FullName.Replace("+", "."))
                If lType.FullName.Replace("+", ".") = typename Then
                    Return lType
                End If
            Next
            Return Nothing
        End Function
        Private Function GetReturnTypeByName(ByVal typename As String) As String

            Dim theNodelist As NodeList = CodeRush.Source.ActiveProject.AssemblyReferences
            If CodeRush.ApplicationObject.Version.StartsWith("7") Then
                theNodelist.Add(New AssemblyReference("mscorlib.dll"))
            End If
            For Each leRef As AssemblyReference In theNodelist
                Dim theTypeName As String
                Dim theTypeNameToLower As String

                If Not leRef.IsProjectReference Then
                    Dim asm As System.Reflection.Assembly
                    Try
                        If leRef.FilePath <> "" Then
                            If leRef.FilePath = "mscorlib.dll" Then
                                asm = System.Reflection.Assembly.Load(leRef.FilePath)
                            Else
                                asm = System.Reflection.Assembly.LoadFrom(leRef.FilePath)
                            End If
                            If Not asm Is Nothing Then
                                Dim theType As Type
                                theType = GetTypeFromAsm(typename, asm)
                                If Not theType Is Nothing Then
                                    Dim pi As PropertyInfo()
                                    pi = theType.GetProperties
                                    For Each item As PropertyInfo In pi
                                        If item.Name.ToLower = "item" Then
                                            Return CodeRush.Language.GetSimpleTypeName(item.PropertyType.FullName)
                                        End If
                                    Next
                                End If
                            End If
                        End If
                    Catch
                        'ignore
                    Finally
                        asm = Nothing
                    End Try
                Else

                End If
            Next
            Return "System.Object"
        End Function

'Private function GetElementType( byval element As Ielement)  as string
'    Dim typeElement As IElement = ResolveType(element) 
'            If typeof typeElement is TypeReferenceExpression Then
'                Return directcast(typeElement,TypeReferenceExpression).FullSignature
'            End If
'    Dim type As ITypeElement = DirectCast(typeElement, ITypeElement) 
'    Dim typeRef As ITypeReferenceExpression = Nothing 
'    Dim members As IMemberElementCollection = type.Members 
'    For Each member As IMemberElement In members 
'                If typeof member is IPropertyElement Then
'                    Dim [property] As IPropertyElement =  directcast(member, IPropertyElement) 
'                    If not [property] Is Nothing AndAlso [property].IsIndexed Then 
'                        typeRef = [property].Type 
'                        Exit For 
'                    End If 
'                End If
'    Next 
'    Dim elementType As ITypeElement = Nothing 
'    If not typeRef Is Nothing Then 
'        elementType = DirectCast(ParserServices.SourceTreeResolver.Resolve(typeRef), ITypeElement) 
'    End If 
'            If elementType Is Nothing Then
'                 Return "System.Object"
'            Else
'                return elementType.FullName
'            End If
   
'End function
        Private Function GetReturnType(ByVal typeElement As IElement) As String
          If typeElement Is Nothing Then
                Return "System.Object"
            End If
            'If  typeof typeElement is TypeReferenceExpression andalso directcast(typeElement,TypeReferenceExpression).IsGeneric Then
            '                Return GetReturnTypeForGeneric(typeElement.ToLanguageElement)
            '    end if  
            Return GetElementType(typeElement)
        End Function

        Dim frmSelect As New SelectForm

        Private Sub ForEachInit_GetString(ByVal ea As DevExpress.CodeRush.Core.GetStringEventArgs) Handles ForEachInit.GetString
            If ea.Parameters.All<>"" Then
                FillArray(ea.Parameters.all)
            Else
                FillArray("method")
            End If
            
            If _EnumVars.Count = 1 Then
                frmSelect.SelectedElement = _EnumVars(0)
                Return
            ElseIf _EnumVars.Count = 0 Then
                frmSelect.SelectedElement = New EnumerableElement("item", False)
                Return
            End If
            frmSelect.LoadItems(_EnumVars)
            Dim posLeft As Integer = CodeRush.TextViews.Active.ScreenBounds.Left + CodeRush.TextViews.Active.GetPointFromLineAndColumn(CodeRush.Caret.Line, CodeRush.Caret.Offset).X
            Dim posTop As Integer = CodeRush.TextViews.Active.ScreenBounds.Top + CodeRush.TextViews.Active.GetPointFromLineAndColumn(CodeRush.Caret.Line, CodeRush.Caret.Offset).Y + CodeRush.TextViews.Active.LineHeight
            Dim formWidth As Integer = 0
            For Each item As EnumerableElement In _EnumVars
                frmSelect.lblSize.Text = item.ToString
                If frmSelect.lblSize.Width > formWidth Then
                    formWidth = frmSelect.lblSize.Width
                End If
            Next
            formWidth+=20
            dim screenHeight as Integer=screen.FromPoint(new System.Drawing.Point(posleft,1)).workingArea.Height
            If posleft + formWidth> screen.FromPoint(new System.Drawing.Point(posleft,1)).workingArea.Width Then
                posleft=screen.FromPoint(new System.Drawing.Point(posleft,1)).workingArea.Width-formWidth    
            End If
            If Not clipboardEnumerableElement Is Nothing Then
                frmSelect.ClipBoardElement = clipboardEnumerableElement
            End If
            frmSelect.Show()
            frmSelect.Left = posLeft
            frmSelect.Top = posTop
            frmSelect.Width = formWidth + 5
            If frmSelect.Width < 160 Then
                frmSelect.Width = 160
            End If
            frmSelect.Hide()
            frmSelect.Height = ((_EnumVars.Count) * 16) + 28
            If frmSelect.Height>screenHeight/4 Then
                frmSelect.Height=screenHeight/4
            End If
            If Not Me.clipboardEnumerableElement Is Nothing Then
                frmSelect.ClipBoardElement = Me.clipboardEnumerableElement
            End If
            If frmselect.Top + frmselect.Height>screenHeight Then
                If frmselect.Top-frmselect.Height<0 Then
                    frmselect.Top=0
                Else
                    frmselect.Top=(frmselect.Top-frmselect.Height)-CodeRush.TextViews.Active.LineHeight
                End If
                
            End If
            'frmSelect.ShowDialog()
            frmSelect.DialogResult = DialogResult.Abort
            frmSelect.TopMost = True
            frmSelect.Show()
            frmSelect.lstItems.Focus()
            Do While frmSelect.DialogResult <> DialogResult.OK AndAlso frmSelect.DialogResult <> DialogResult.Cancel
                System.Threading.Thread.Sleep(10)
                Application.DoEvents()
            Loop
            If frmSelect.DialogResult = DialogResult.Cancel Then
                Throw New ApplicationException("Canceled by user")
            End If
        End Sub

        Private Sub ForEachVar_GetString(ByVal ea As DevExpress.CodeRush.Core.GetStringEventArgs) Handles ForEachVar.GetString
             If frmSelect.SelectedElement.Name = " New" Then
                ea.Value = "collection"
            Else
                ea.Value = frmSelect.SelectedElement.Name
            End If
            ''For Each item As EnumerableElement In _EnumVars
            ''    ea.Value &= item.ToString() & " "
            ''Next
            'If frmSelect.SelectedElement.Name = " New" Then
            '    ea.Value = "collection"
            'Else
            '    ea.Value = frmSelect.SelectedElement.Name
            'End If

        End Sub

        Private Sub ForEachVarType_GetString(ByVal ea As DevExpress.CodeRush.Core.GetStringEventArgs) Handles ForEachVarType.GetString
            Dim rettype As String
            If frmSelect.SelectedElement.ReturnTypeName = "" Then
                If frmSelect.SelectedElement Is Nothing Then
                    rettype = "System.Object"
                Else
                    rettype=GetReturnType(frmSelect.SelectedElement.Element)
                    frmSelect.SelectedElement.ReturnTypeName=rettype
                End If
            Else
                rettype=frmSelect.SelectedElement.ReturnTypeName
            End If
            ea.Value = CodeRush.Language.GetSimpleName( rettype)
           'Dim rettype As String
           ' If frmSelect.SelectedElement.ReturnTypeName = "" Then
           '     If frmSelect.SelectedElement.ReturnType Is Nothing Then
           '         frmSelect.SelectedElement.ReturnType=ResolveType(frmSelect.SelectedElement.Element)
           '     End If
           '     'Dim declElement As IElement = resolvetype(frmSelect.SelectedElement.Element)
           '     Dim declElement As IElement = resolvetype(frmSelect.SelectedElement.Element) 'DirectCast(frmSelect.SelectedElement.ReturnType, ITypeReferenceExpression).GetDeclaration
           '     If TypeOf declElement Is IClassElement andalso directcast(declElement,IClassElement).IsGeneric andalso typeof frmSelect.SelectedElement.element is Variable Then
           '         rettype=GetReturnTypeForGeneric(directcast(frmSelect.SelectedElement.element,Variable).MemberTypeReference)
           '     Else
           '         rettype = GetReturnType(declElement)
           '     End If
           '     frmSelect.SelectedElement.ReturnTypeName=rettype
           ' Else
           '     rettype=frmSelect.SelectedElement.ReturnTypeName
           ' End If
           ' ea.Value = CodeRush.Language.GetSimpleName( rettype)
        End Sub

        Private Sub ForEachVarItemName_GetString(ByVal ea As DevExpress.CodeRush.Core.GetStringEventArgs) Handles ForEachVarItemName.GetString
            Dim rettype As String
            rettype=frmSelect.SelectedElement.ReturnTypeName
            If frmSelect.SelectedElement.ReturnTypeName = "" Then
                If frmSelect.SelectedElement Is Nothing Then
                    ea.Value = "lObject"
                    Return
                End If
                rettype=GetReturnType(frmSelect.SelectedElement.Element)
                    frmSelect.SelectedElement.ReturnTypeName = rettype
            End If
            If frmSelect.SelectedElement.ReturnTypeName = "System.Object" Then
                ea.Value = "item"
            Else
                dim strRettype as string=CodeRush.Language.GetSimpleName( rettype)
                If strRettype.IndexOf("(")>-1    Then
                    ea.value = "l" &  strRettype.Substring(0,strRettype.IndexOf("("))
                ElseIf strRettype.IndexOf("<")>-1  then
                     ea.value = "l" & strRettype.Substring(0,strRettype.IndexOf("<"))
                Else
                    ea.value = "l" & strRettype
                End If
            End If
            'Dim rettype As String
            'rettype=frmSelect.SelectedElement.ReturnTypeName
            'If frmSelect.SelectedElement.ReturnTypeName = "" Then
            '    If frmSelect.SelectedElement Is Nothing Then
            '        ea.Value = "lObject"
            '        Return
            '    End If
                
            '    Dim declElement As IElement = resolvetype(frmSelect.SelectedElement.Element) 'DirectCast(frmSelect.SelectedElement.ReturnType, ITypeReferenceExpression).GetDeclaration
            '    If TypeOf declElement Is IClassElement andalso directcast(declElement,IClassElement).IsGeneric Then
            '        'rettype=GetReturnTypeForGeneric(frmSelect.SelectedElement.ReturnType)
            '        rettype=GetReturnTypeForGeneric(directcast(frmSelect.SelectedElement.Element,Variable).MemberTypeReference)
            '    Else
            '        rettype = GetReturnType(declElement)
            '    End If
                
            '        frmSelect.SelectedElement.ReturnTypeName = rettype
                
            'End If
            'If frmSelect.SelectedElement.ReturnTypeName = "System.Object" Then
            '    ea.Value = "item"
            'Else
            '    dim strRettype as string=CodeRush.Language.GetSimpleName( rettype)
            '    If strRettype.IndexOf("(")>-1    Then
            '        ea.value = "l" &  strRettype.Substring(0,strRettype.IndexOf("("))
            '    ElseIf strRettype.IndexOf("<")>-1  then
            '         ea.value = "l" & strRettype.Substring(0,strRettype.IndexOf("<"))
            '    Else
            '        ea.value = "l" & strRettype
            '    End If
            'End If
        End Sub

      Private _AsmChecked As new Hashtable

        Private Sub BuildCache()
            Try
                Dim projNodes As NodeList = _Sol.ProjectElements
                For Each proj As ProjectElement In projNodes
                    Dim theNodelist As NodeList = proj.AssemblyReferences
                    If isOldVS Then
                        theNodelist.Add(New AssemblyReference("mscorlib.dll"))
                    End If
                    For Each leRef As AssemblyReference In theNodelist
                        Dim LeRefFilePath As String = leRef.FilePath
                        Dim LeRefIsProjectReference As Boolean = leRef.IsProjectReference
                        CheckAndAddReference(LeRefFilePath, LeRefIsProjectReference)

                    Next
                Next
            Catch ex As Exception
                
            End Try
            'MessageBox.Show(_cache.Count)
        End Sub

        Private Sub CheckAndAddReference(ByVal LeRefFilePath As String, ByVal LeRefIsProjectReference As Boolean)
            If Not _AsmChecked.ContainsKey(LeRefFilePath) Then
                _AsmChecked.Add(LeRefFilePath, LeRefFilePath)
                If Not LeRefIsProjectReference AndAlso Not LeRefFilePath = "" Then
                    Dim asm As System.Reflection.Assembly
                    If LeRefFilePath = "mscorlib.dll" Then
                        asm = System.Reflection.Assembly.Load(LeRefFilePath)
                    Else
                        Dim fs As New FileStream(LeRefFilePath, FileMode.Open)
                        Dim b(fs.Length) As Byte
                        fs.Read(b, 0, fs.Length)
                        asm = System.Reflection.Assembly.Load(b)
                        'asm = System.Reflection.Assembly.LoadFrom(LeRefFilePath)
                    End If
                    If Not asm Is Nothing Then
                        Application.DoEvents()
                        Dim mytypes As Type() = asm.GetTypes
                        Application.DoEvents()
                        For Each lType As Type In mytypes
                            If Not _Cache.ContainsKey(lType.FullName) Then
                                Application.DoEvents()
                                Dim iType As Type() = lType.GetInterfaces
                                Application.DoEvents()
                                For Each liType As Type In iType
                                    If liType.FullName = strIEnum Then
                                        _Cache.Add(lType.FullName, True)
                                        Exit For
                                    End If
                                Next
                                If Not _Cache.ContainsKey(lType.FullName) Then
                                    _Cache.Add(lType.FullName, False)
                                End If
                            End If
                        Next
                    End If
                End If
            End If
        End Sub
        Private Sub CR_EnhancedForEachPlugIn_ProjectAdded(ByVal project As EnvDTE.Project) Handles MyBase.ProjectAdded 
                dim workThread As Thread
                If Not _sol is Nothing Then
                'If not projHash.ContainsKey(ea.document.ProjectElement.FullName) Then
                    workThread=New Thread(AddressOf BuildCache)
                    workThread.Priority=ThreadPriority.Lowest
                    workThread.Start()
                'End If
            End If
        End Sub

            Private Sub CR_EnhancedForEachPlugIn_ReferenceAdded(ByVal ea As DevExpress.CodeRush.Core.ReferenceEventArgs) Handles mybase.ReferenceAdded
            For Each item As LanguageElement In _Sol.ProjectElements
                If typeof item is ProjectElement Then
                    dim refs As NodeList= DirectCast(item, ProjectElement).AssemblyReferences()
                    For Each ref As AssemblyReference In refs
                        If Not ref.IsProjectReference AndAlso ref.FilePath.ToLower = ea.Reference.Path.ToLower Then
                            CheckAndAddReference(ref.FilePath, True)            
                        End If
                    Next
                End If
            Next
                
        End Sub

        Dim _Sol As SolutionElement
            Private Sub CR_EnhancedForEachPlugIn_SolutionOpened() Handles mybase.SolutionOpened
                _Sol=CodeRush.Source.ActiveSolution
                dim workThread As Thread
                If Not _sol is Nothing Then
                'If not projHash.ContainsKey(ea.document.ProjectElement.FullName) Then
                'debug.WriteLine(date.Now.ToShortTimeString)
                dim frmStatus as New frmBuildCache
                frmstatus.Show
                frmStatus.Refresh
                application.DoEvents
                BuildCache
                frmstatus.Close
                'debug.WriteLine(date.Now.ToShortTimeString)
                    'workThread=New Thread(AddressOf BuildCache)
                    'workThread.Priority=ThreadPriority.Lowest
                    'workThread.Start()
                'End If
            End If

        End Sub
        
        Private Function ResolveType(ByVal active As IElement) As IElement 
    Dim resolver As ISourceTreeResolver = ParserServices.SourceTreeResolver 
    If TypeOf active Is IMemberElement Then 
        Return resolver.ResolveElementType(active) 
    End If 

    Dim expression As IExpression '= TryCast(active, IExpression) 
            If typeof active is IExpression Then
                expression=directcast(active, IExpression)
            End If
             
    If not expression is Nothing Then 
        Return resolver.ResolveExpression(expression) 
    End If 

    Return Nothing 
End Function 

        Private Function GetHardCodedType(ByVal element As IElement, ByVal propertyName As String) As String
            Dim type As ITypeElement = DirectCast(ResolveType(element), ITypeElement)
            Dim typeName As String = type.FullName
            If typeName Is Nothing Then
                Return [String].Empty
            End If
            If typeName = "System.Collections.Hashtable" Then
                If propertyName = "Keys" Then
                    Return "DictionaryEntry"
                End If
            ElseIf typeName = "System.Collections.Generic.IDictionary`2" _
            OrElse typeName = "System.Collections.Generic.Dictionary`2" Then
                Dim declaration As IElement = CodeRush.Source.GetDeclaration(element)
                If Not declaration Is Nothing AndAlso (TypeOf declaration Is IHasType) Then
                    Dim typeReference As ITypeReferenceExpression = DirectCast(declaration, IHasType).Type
                    If typeReference Is Nothing Then
                        Return String.Empty
                    End If
                    If TypeOf typeReference Is IGenericExpression Then
                        Dim keyValuePairRef As New TypeReferenceExpression("KeyValuePair")
                        keyValuePairRef.TypeArguments.AddRange(DirectCast(typeReference, IGenericExpression).TypeArguments)
                        Return CodeRush.Language.GenerateExpressionCode(keyValuePairRef)
                    End If
                End If
            End If
            Return [String].Empty
        End Function

Private Function GetGenericType(ByVal element As LanguageElement) As String 
    Dim typeReference As ITypeReferenceExpression = Nothing 
    If TypeOf element Is ITypeReferenceExpression Then 
        typeReference = DirectCast(element, ITypeReferenceExpression) 
    Else 
        Dim declaration As IElement = CodeRush.Source.GetDeclaration(element) 
        If not declaration Is Nothing AndAlso (TypeOf declaration Is IHasType) Then 
            typeReference = DirectCast(declaration, IHasType).Type 
        End If 
    End If 
    If typeReference Is Nothing Then 
        Return String.Empty 
    End If 
    
    Dim arguments As ITypeReferenceExpressionCollection = DirectCast(typeReference, IGenericExpression).TypeArguments 
    If not arguments Is Nothing AndAlso arguments.Count > 0 Then 
        Dim arg As ITypeReferenceExpression = DirectCast(arguments(0), ITypeReferenceExpression) 
        Dim typeRefExp As TypeReferenceExpression = SourceModelUtils.CreateTypeReferenceExpression(arg) 
        Return CodeRush.Language.GenerateExpressionCode(typeRefExp) 
    End If 
    Return String.Empty 
End Function 

Private Function GetIndexedPropertyName(ByVal type As ITypeElement) As String 
    Dim members As IMemberElementCollection = type.Members 
    Dim propertyName As String = [String].Empty 
    For Each member As IMemberElement In members 
        Dim [property] As IPropertyElement '= TryCast(member, IPropertyElement) 
        If typeof member is IPropertyElement Then
            [property]=directcast(member,IPropertyElement)
        End If
        If not [property] Is Nothing AndAlso [property].IsIndexed Then 
            propertyName = [property].Name 
            Exit For 
        End If 
    Next 
    Return propertyName 
End Function 

Private function GetElementType( element As LanguageElement) As string 
    Dim typeElement As IElement = ResolveType(element) 
            If typeof typeElement is TypeReferenceExpression Then
                Return directcast(typeElement,TypeReferenceExpression).FullSignature
            End If
    If Not (TypeOf typeElement Is ITypeElement) Then 
        Return  "System.Object"
    End If 
    
    Dim type As ITypeElement = DirectCast(typeElement, ITypeElement) 
    
    If type.IsGeneric Then 
        Dim result As String = String.Empty 
        Dim propertyName As String = GetIndexedPropertyName(type) 
        If Not propertyName Is Nothing andalso propertyName<>"" Then
            result = GetHardCodedType(element, propertyName) 
        If Not result Is Nothing andalso result<>"" Then
                Return  result
            End If 
        End If 
        result = GetGenericType(element) 
        If Not result Is Nothing andalso result<>"" Then
            Return result
        End If 
    Else 
        return CodeRush.Strings.[Get]("EnumerableType", type.FullName)
    End If 
            Return ""
End function 
    End Class

End Namespace