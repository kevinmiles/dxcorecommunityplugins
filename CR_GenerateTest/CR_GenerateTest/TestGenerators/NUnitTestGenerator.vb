Option Infer On
Public Class NUnitTestGenerator
    Inherits BaseTestGenerator
    Public Sub New()
        MyBase.New("NUnit.Framework.dll", "NUnit.Framework", "TestFixture", "Test")
    End Sub
End Class