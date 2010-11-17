Imports System.Linq
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices


Public Structure ParsedSection
#Region "Fields"
    Private Const CHAR_ID As String = "#"
    Private Const CHAR_CLASS As String = "."
    Private Const CHAR_Multiplicity As String = "*"

    Private mBase As String
    Private mID As String
    Private mClass As String
    Private mAttributes As String
    Private mMultiplicity As Integer
    Private mTemplate As Template
#End Region

#Region "Utils"
    Private Function AddSpaceIfNotEmpty(ByVal Value As String) As String
        Return If(Value = String.Empty, "", " ") & Value
    End Function
#End Region
#Region "Constructors"
    Public Sub New(ByVal Section As String)
        Dim Quote = GetQuote()
        ' Extract Multiplicity
        Dim MultiplicityParts = Section.Split(CHAR_Multiplicity)
        mMultiplicity = If(MultiplicityParts.Count > 1, MultiplicityParts(1), 1)
        mBase = MultiplicityParts(0)

        ' Extract ID
        Dim IDParts = mBase.Split(CHAR_ID)
        mID = If(IDParts.Count > 1, IDParts(1), "")
        mBase = IDParts(0)
        ' Extract Class
        Dim ClassParts = mBase.Split(CHAR_CLASS)
        Dim DotPosition As Integer = mBase.IndexOf(CHAR_CLASS)
        mClass = If(ClassParts.Count > 1, mBase.Substring(DotPosition).Replace(CHAR_CLASS, " "), "") ' ClassParts(1), "")
        mClass = mClass.TrimStart
        mBase = ClassParts(0)

        ' Extract Attributes
        If mBase.Contains("[") Then
            mAttributes = mBase.Contents("["c, "]"c)
            mBase = mBase.Replace(mAttributes, "").Replace("[]", "")
        End If
        mAttributes = AddSpaceIfNotEmpty(mAttributes)

        ' ID -> mAttributes
        If mID <> String.Empty Then
            mAttributes = String.Format(" id={1}{0}{1}", mID, Quote) & mAttributes
        End If

        ' Class -> mAttributes
        If mClass <> String.Empty Then
            mAttributes = String.Format(" class={1}{0}{1}", mClass, Quote) & mAttributes
        End If

    End Sub
    Private Function GetQuote() As String
        Return """"
    End Function
    Public Sub New(ByVal Section As String, ByVal Multiplicity As Integer)
        mBase = Section
        mMultiplicity = Multiplicity
    End Sub
#End Region
#Region "Simple Properties"
    Public ReadOnly Property Base() As String
        Get
            Return mBase
        End Get
    End Property
    Public ReadOnly Property Attributes() As String
        Get
            Return mAttributes
        End Get
    End Property
    Public ReadOnly Property Multiplicity() As Integer
        Get
            Return mMultiplicity
        End Get
    End Property
#End Region
    Public Sub LocateTemplate()
        If mTemplate Is Nothing Then
            mTemplate = GetFirstTemplateWithName(Base)
        End If
    End Sub
    Public ReadOnly Property Template() As Template
        Get
            Return mTemplate
        End Get
    End Property
    Public ReadOnly Property GetAttributeWithIteration(ByVal Iteration As Integer) As String
        Get
            Dim Attributes As String = mAttributes
            For Count = 5 To 1 Step -1
                Attributes = Attributes.Replace(New String("$", Count), Iteration.ToString("D" & Count))
            Next
            Return Attributes
        End Get
    End Property
End Structure
