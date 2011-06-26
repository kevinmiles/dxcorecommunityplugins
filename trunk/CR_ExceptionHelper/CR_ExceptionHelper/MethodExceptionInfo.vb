Option Infer On
Imports System.Xml.XPath
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System
Imports System.Xml.Linq


''' <summary>
''' Stores information about one method.
''' </summary>
''' <remarks></remarks>
Friend Class MethodExceptionInfo

#Region " Private Members "

    Private ReadOnly m_MethodDescriptor As String
    Private m_NameRanges As SourceRangeCollection
    Private m_ExceptionInfos As ChainedList(Of ExceptionInfo)

#End Region

#Region " Shared Members "

    'TODO: add the ability to refresh the cache, or remove it again (which is possible).
    Private Shared m_DiscoveredMethods As New Dictionary(Of String, MethodExceptionInfo)

#End Region

#Region " Constructors "

    ''' <summary>
    ''' Standard Constructor
    ''' </summary>
    ''' <param name="methodDescriptor">The method call, in the format of Namespace.Type.Method(Params)</param>
    ''' <param name="assemblyReference">A DXCore assembly reference.</param>
    ''' <param name="nameRange">A SourceRange indicating the location of the method call reference in the code.</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal methodDescriptor As String, ByVal assemblyReference As AssemblyReference, ByVal nameRange As SourceRange)
        'Store field values
        m_MethodDescriptor = methodDescriptor
        NameRanges.Add(nameRange)
        FindExceptions(assemblyReference)
    End Sub

    Public Sub New(ByVal methodDescriptor As String, ByVal docComment As XmlDocComment, ByVal nameRange As SourceRange)
        m_MethodDescriptor = methodDescriptor
        NameRanges.Add(nameRange)
        FindExceptions(docComment)
    End Sub

#End Region


#Region " Shared Factory Methods "

    ''' <summary>
    ''' Clears the cache of previously discovered methodsexceptioninfos
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub Clear()
        m_DiscoveredMethods.Clear()
    End Sub

    ''' <summary>
    ''' Generates a MethodInfo instance based on the provided language element
    ''' </summary>
    ''' <param name="element">A language element which possibly represents a method call</param>
    ''' <returns>A MethodExceptionInfo instance, or null</returns>
    ''' <remarks></remarks>
    Public Shared Function Create(ByVal element As LanguageElement) As MethodExceptionInfo
        If Not TypeOf element Is IMethodCallExpression _
           AndAlso Not TypeOf element Is IMethodCallStatement Then
            Return Nothing
        End If

        Dim MethodDeclaration = TryCast(ParserServices.SourceModelService.GetDeclaration(element), IMethodElement)
        If Not MethodDeclaration.InReferencedAssembly Then
            ' Call is to sourcecode rather than a library.
            Return Nothing
        End If

        Dim MethodDescriptorString = GetMethodDescriptor(MethodDeclaration)
        If MethodDescriptorString = Nothing Then
            ' Couldn't build Method Descriptor
            Return Nothing
        End If
        Dim methodInfo As MethodExceptionInfo = Nothing
        If m_DiscoveredMethods.TryGetValue(MethodDescriptorString, methodInfo) Then
            methodInfo.NameRanges.Add(element.NameRange)    ' Store this location for later when we paint references contributing to this exception.
        Else
            'locate the assemblyreference
            Dim assemblyModel As MetaDataAssemblyModel = TryCast(MethodDeclaration.AssemblyModel, MetaDataAssemblyModel)
            Dim assemblyRef As AssemblyReference = Nothing
            If assemblyModel IsNot Nothing Then
                assemblyRef = assemblyModel.Assembly
            End If

            If assemblyRef IsNot Nothing Then
                methodInfo = New MethodExceptionInfo(MethodDescriptorString, assemblyRef, element.NameRange)
                m_DiscoveredMethods.Add(methodInfo.MethodDescriptor, methodInfo)
            Else
                Dim codeEl As CodeElement = TryCast(MethodDeclaration, CodeElement)
                If codeEl IsNot Nothing Then
                    methodInfo = New MethodExceptionInfo(MethodDescriptorString, codeEl.DocComment, element.NameRange)
                End If
            End If

        End If

        Return methodInfo

    End Function


    ''' <summary>
    ''' Generates the methodDescriptor string for a method
    ''' </summary>
    ''' <param name="methodDeclaration">The method to generated the descriptor for</param>
    ''' <returns>The methodDescriptor string</returns>
    ''' <remarks></remarks>
    Private Shared Function GetMethodDescriptor(ByVal methodDeclaration As IMethodElement) As String

        Dim sb As New System.Text.StringBuilder(64)
        Dim parameter As IParameterElement
        Dim iCount As Int32 = 0
        Dim blnIsGeneric As Boolean = False
        Dim genericParameterNames() As String = Nothing

        If Not methodDeclaration Is Nothing Then

            Dim genericElem As IMethodElement = TryCast(methodDeclaration.GenericTemplate, IMethodElement)
            If genericElem IsNot Nothing Then
                methodDeclaration = genericElem
                blnIsGeneric = True
                ReDim genericParameterNames(methodDeclaration.TypeParameters.Count - 1)
                For i As Int32 = 0 To methodDeclaration.TypeParameters.Count - 1
                    genericParameterNames(i) = methodDeclaration.TypeParameters(i).Name
                Next
            End If

            sb.Append(methodDeclaration.FullName)
            If blnIsGeneric Then sb.Replace("`", "``")

            If methodDeclaration.Parameters.Count > 0 Then
                sb.Append("(")

                For Each parameter In methodDeclaration.Parameters

                    Dim paramName As String

                    If Not parameter.Type Is Nothing Then
                        If iCount > 0 Then
                            sb.Append(",")
                        End If

                        If parameter.Type.IsArrayType Then
                            paramName = ParserServices.LanguageService.GetFullTypeName(parameter.Type.BaseType.Name)
                            If blnIsGeneric Then paramName = "``" & Array.IndexOf(genericParameterNames, paramName)
                            sb.Append(paramName & "[]")
                        Else
                            paramName = ParserServices.LanguageService.GetFullTypeName(parameter.Type.Name)
                            If blnIsGeneric Then paramName = "``" & Array.IndexOf(genericParameterNames, paramName)
                            sb.Append(paramName)
                        End If

                        iCount += 1
                    End If
                Next

                sb.Append(")")

            End If

        End If

        Return sb.ToString

    End Function

#End Region

#Region " Public Properties "

    ''' <summary>
    ''' Method name string accessor
    ''' </summary>
    ''' <returns>The method name string</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MethodDescriptor() As String
        Get
            Return m_MethodDescriptor
        End Get
    End Property

    ''' <summary>
    ''' Returns a SourceRange indicating the location of the method call reference in the code.
    ''' </summary>
    Public ReadOnly Property NameRanges() As SourceRangeCollection
        Get
            If m_NameRanges Is Nothing Then
                m_NameRanges = New SourceRangeCollection()
            End If
            Return m_NameRanges
        End Get
    End Property

    ''' <summary>
    ''' Exception list accessor
    ''' </summary>
    ''' <returns>The set of exceptions that this method can throw</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Exceptions() As ReadOnlyChainedList(Of ExceptionInfo)
        Get
            Return m_ExceptionInfos
        End Get
    End Property

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Analyses the exception entries in the assembly's xml file
    ''' </summary>
    ''' <param name="assemblyReference">The assembly to analyse</param>
    ''' <remarks></remarks>
    Private Sub FindExceptions(ByVal assemblyReference As AssemblyReference)

        If assemblyReference Is Nothing Then Return

        m_ExceptionInfos = New ChainedList(Of ExceptionInfo)()

        Dim fileinfo As IO.FileInfo = Nothing

        If assemblyReference.FilePath.StartsWith("file:") Then
            fileinfo = New IO.FileInfo((New Uri(assemblyReference.FilePath)).AbsolutePath)
        Else
            fileinfo = New IO.FileInfo(assemblyReference.FilePath)
        End If

        Dim SubDir As String = If(fileinfo.Directory.GetDirectories("en").Count = 0, "en\", "")
        fileinfo = New IO.FileInfo(fileinfo.DirectoryName & "\" & SubDir & fileinfo.Name.Substring(0, fileinfo.Name.Length - fileinfo.Extension.Length) & ".xml")

        If Not fileinfo.Exists Then Return

        Dim XMLDoc = XElement.Parse(My.Computer.FileSystem.ReadAllText(fileinfo.FullName))
        Dim Member = (From item In XMLDoc.<members>.<member> _
                                 Where item.@name = "M:" & m_MethodDescriptor _
                                 Select item).FirstOrDefault
        Dim Exceptions = Member.<exception>
        For Each Ex As XElement In Exceptions
            m_ExceptionInfos.Add(New ExceptionInfo(Ex.@cref.Substring(2), Ex.Value))
        Next
    End Sub

    ''' <summary>
    ''' Analyses the exception entries in the provided xml comment block 
    ''' </summary>
    ''' <param name="comment">The comment to analyse</param>
    ''' <remarks></remarks>
    Private Sub FindExceptions(ByVal comment As XmlDocComment)

        m_ExceptionInfos = New ChainedList(Of ExceptionInfo)()

        If comment Is Nothing Then Return

        Dim strComment As String = comment.ToString

        Dim lines() As String = strComment.Split(New String() {vbCrLf, vbCr, vbLf}, StringSplitOptions.RemoveEmptyEntries)

        For i As Int32 = 0 To lines.Length - 1
            lines(i) = lines(i).TrimStart(New Char() {ChrW(9), ChrW(32), "'"c})
        Next

        strComment = "<member name=""M:" & m_MethodDescriptor & """>" & String.Join(vbCrLf, lines) & "</member>"

        Using reader As System.Xml.XmlReader = New System.Xml.XmlTextReader(strComment, System.Xml.XmlNodeType.Element, Nothing)
            If reader.ReadToDescendant("exception") Then
                Do
                    Dim exName As String = reader.GetAttribute("cref")
                    Dim content As String
                    If exName.StartsWith("T:") Then
                        exName = exName.Substring(2)
                    Else
                        exName = CodeRush.Source.GetDeclaration(comment, exName).FullName
                    End If
                    content = reader.ReadInnerXml
                    m_ExceptionInfos.Add(New ExceptionInfo(exName, content))
                Loop While reader.ReadToNextSibling("exception")
            End If
        End Using

    End Sub


    Private Sub FindExceptions(ByVal reader As System.Xml.XmlReader)
        reader.ReadToFollowing("member")
        Dim strName As String = "M:" & m_MethodDescriptor
        While reader.GetAttribute("name") <> strName
            reader.Skip()
            reader.Read()
        End While
        If reader.ReadToDescendant("exception") Then
            Do
                Dim exName As String = reader.GetAttribute("cref")
                Dim content As String
                If exName.StartsWith("T:") Then exName = exName.Substring(2)
                content = reader.ReadInnerXml
                m_ExceptionInfos.Add(New ExceptionInfo(exName, content))
            Loop While reader.ReadToNextSibling("exception")
        End If
    End Sub


#End Region

End Class
