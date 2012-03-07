

Public Class PluginSettings
    Public Const DEFAULT_TestNamespaceSuffix As String = "_Tests"
    Public Const DEFAULT_TestClassSuffix As String = "_Tests"
    Private mTestNamespaceSuffix As String = DEFAULT_TestNamespaceSuffix
    Private mTestClassSuffix As String = DEFAULT_TestClassSuffix
    Public Property TestClassSuffix() As String
        Get
            Return mTestClassSuffix
        End Get
        Set(ByVal value As String)
            mTestClassSuffix = value
        End Set
    End Property
    Public Property TestNamespaceSuffix() As String
        Get
            Return mTestNamespaceSuffix
        End Get
        Set(ByVal value As String)
            mTestNamespaceSuffix = value
        End Set
    End Property
End Class
