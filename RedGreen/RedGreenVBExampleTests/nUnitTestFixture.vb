Imports NUnit.Framework
''' <summary>
''' Class for nunit example tests
''' </summary>
''' <remarks>This class exists as a placeholder for VB coded nUnit Tests</remarks>
<TestFixture()> _
Public Class nUnitTestFixture
    <Test()> _
    Public Sub PassTest1()
        ' Test which does nothing and therefore passes
    End Sub
    <Test()> _
    Public Sub FailTest()
        ' Throw exception to cause test failure.
        Throw New ApplicationException("This test should Fail")
    End Sub
End Class