Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Namespace CR_EventHandlerCheckTC
    Public Class ListItem
        Private _theElement As IElement

        ''' <summary>
        ''' Initializes a new instance of the ListItem class.
        ''' </summary>
        ''' <param name="theElement"></param>
        ''' <param name="calledDirectly"></param>
        ''' <param name="createdWithAddHandler"></param>
        Public Sub New(ByVal theElement As Method, ByVal calledDirectly As Boolean, ByVal createdWithAddHandler As Boolean)
            _theElement = theElement
            _calledDirectly = calledDirectly
            _createdWithAddHandler = createdWithAddHandler
            FullFileName = theElement.FileNode.FilePath
            FileName = theElement.FileNode.Name
        End Sub

        Private _fileName As String
        Public Property FileName() As String
            Get
                Return _fileName
            End Get
            Set(ByVal Value As String)
                _fileName = Value
            End Set
        End Property
        

        Public Property theElement() As Method
            Get
                Return _theElement
            End Get
            Set(ByVal Value As Method)
                _theElement = Value
            End Set
        End Property

        Private _calledDirectly As Boolean
        Public Property CalledDirectly() As Boolean
            Get
                Return _calledDirectly
            End Get
            Set(ByVal Value As Boolean)
                _calledDirectly = Value
            End Set
        End Property
        Private _createdWithAddHandler As Boolean
        Public Property CreatedWithAddHandler() As Boolean
            Get
                Return _createdWithAddHandler
            End Get
            Set(ByVal Value As Boolean)
                _createdWithAddHandler = Value
            End Set
        End Property

        Private _fullFileName As String
        Public Property FullFileName() As String
            Get
                Return _fullFileName
            End Get
            Set(ByVal Value As String)
                _fullFileName = Value
            End Set
        End Property


        Public Overrides Function ToString() As String
            Dim fullString As String = theElement.Name & " in file " & FileName
            If CalledDirectly Then
                fullString += " " & "(DirectCall)"
            End If
            If CreatedWithAddHandler Then
                fullString += " " & "(AddHandler)"
            End If
            Return fullString
        End Function
    End Class
End Namespace
