Public Enum Visibility
    [Public]
    [Protected]
    [Protected_internal]
    [Internal]
    [Private]
    [All]
End Enum

Public Class ExtendedNode
    Inherits ElementNode
    Public Sub New(ByVal name As String, ByVal type As String, ByVal vis As Visibility, Optional ByVal ID As String = "", Optional ByVal parentID As Object = Nothing, Optional ByVal blnAbstract As Boolean = False, Optional ByVal blnExtern As Boolean = False, Optional ByVal blnOverloads As Boolean = False, Optional ByVal blnOverrides As Boolean = False, Optional ByVal blnVirtual As Boolean = False)
        MyBase.New(name, type, ID, parentID)

        Me.Visible = vis
        Me.Abstract = blnAbstract
        Me.Extern = blnExtern
        Me.Overload = blnOverloads
        Me.Override = blnOverrides
        Me.Virtual = blnVirtual
    End Sub

    Private _visible As Visibility
    Public Property Visible() As Visibility
        Get
            Return _visible
        End Get
        Set(ByVal Value As Visibility)
            _visible = Value
        End Set
    End Property

    Private _abstract As Boolean
    Public Property Abstract() As Boolean
        Get
            Return _abstract
        End Get
        Set(ByVal Value As Boolean)
            _abstract = Value
        End Set
    End Property

    Private _extern As Boolean
    Public Property Extern() As Boolean
        Get
            Return _extern
        End Get
        Set(ByVal Value As Boolean)
            _extern = Value
        End Set
    End Property

    Private _overload As Boolean
    Public Property Overload() As Boolean
        Get
            Return _overload
        End Get
        Set(ByVal Value As Boolean)
            _overload = Value
        End Set
    End Property

    Private _override As Boolean
    Public Property Override() As Boolean
        Get
            Return _override
        End Get
        Set(ByVal Value As Boolean)
            _override = Value
        End Set
    End Property

    Private _virtual As Boolean
    Public Property Virtual() As Boolean
        Get
            Return _virtual
        End Get
        Set(ByVal Value As Boolean)
            _virtual = Value
        End Set
    End Property
    

    Public Overrides Function ToString() As String
        Dim specials As String = "("
        If Me.Abstract Then
            specials = specials & "Abstract, "
        End If
        If Me.Extern Then
            specials = specials & "Extern, "
        End If
        If Me.Overload Then
            specials = specials & "Overload, "
        End If
        If Me.Override Then
            specials = specials & "Override, "
        End If
        If Me.Virtual Then
            specials = specials & "Virtual, "
        End If
        If specials.EndsWith(", ") Then
            specials = specials.Substring(0, specials.Length - 2)
            Return Me.Visible.ToString() & " " & Me.Type.ToString() & " '" & Me.Name & "' " & specials & ")"
        Else
            Return Me.Visible.ToString() & " " & Me.Type.ToString() & " '" & Me.Name & "'"
        End If
    End Function
End Class
