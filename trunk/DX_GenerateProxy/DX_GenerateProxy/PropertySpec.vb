Imports DevExpress.CodeRush.StructuralParser


Public Structure PropertySpec
    Private mName As String
    Private mMemberType As String
    Private mVisibility As MemberVisibility
    Private mIsStatic As Boolean
    Private mHasGetter As Boolean
    Private mHasSetter As Boolean
    ''' <summary>
    ''' Summary for AddPropertyToClassArgs
    ''' </summary>
    Public Sub New(ByVal Name As String, ByVal MemberType As String, ByVal Visibility As MemberVisibility, ByVal IsStatic As Boolean, ByVal HasGetter As Boolean, ByVal HasSetter As Boolean)
        mName = Name
        mMemberType = MemberType
        mVisibility = Visibility
        mIsStatic = IsStatic
        mHasGetter = HasGetter
        mHasSetter = HasSetter
    End Sub
    Public ReadOnly Property Name() As String
        Get
            Return mName
        End Get
    End Property
    Public ReadOnly Property MemberType() As String
        Get
            Return mMemberType
        End Get
    End Property
    Public ReadOnly Property Visibility() As MemberVisibility
        Get
            Return mVisibility
        End Get
    End Property
    Public ReadOnly Property IsStatic() As Boolean
        Get
            Return mIsStatic
        End Get
    End Property
    Public ReadOnly Property HasGetter() As Boolean
        Get
            Return mHasGetter
        End Get
    End Property
    Public ReadOnly Property HasSetter() As Boolean
        Get
            Return mHasSetter
        End Get
    End Property
End Structure