Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Net
Namespace ExampleCS
    Public Interface MyInterface
        Sub MySub()
        Function MyFunc() As String
        ReadOnly Property MyProp() As String
    End Interface
    Public Class Tester
        Public Sub IdealTests1()
            Dim MyVar As MyInterface
            Dim Value1 As MyInterface = MyVar
            Dim Value2 As String = MyVar.MyProp()
            Dim Value3 As String = MyVar.MyFunc()
            Dim Value4 As String = MyVar.MyProp
            Dim Value5 As String = MyVar.MyFunc
            MyVar.MySub()
        End Sub
        Public Sub IdealTests2()
            Dim MyVar As MyImplementor
            Dim Value1 As MyInterface = MyVar
            Dim Value2 As String = MyVar.MyProp()
            Dim Value3 As String = MyVar.MyFunc()
            Dim Value4 As String = MyVar.MyProp
            Dim Value5 As String = MyVar.MyFunc 'Fail
            MyVar.MySub()
        End Sub
        Public Sub RequiredTests1()
            Dim MyVar As MyInterface = New MyImplementor()
            Dim Value1 As MyInterface = MyVar
            Dim Value2 As String = MyVar.MyProp
            Dim Value3 As String = MyVar.MyFunc()
            MyVar.MySub()
        End Sub
        Public Sub RequiredTests2()
            Dim MyVar As MyImplementor = New MyImplementor()
            Dim Value1 As MyInterface = MyVar
            Dim Value2 As String = MyVar.MyProp
            Dim Value3 As String = MyVar.MyFunc()
            MyVar.MySub()
        End Sub
    End Class
    Public Class MyImplementor
        Implements MyInterface
        Public Sub MySub() Implements ExampleCS.MyInterface.MySub
        End Sub
        Public Function MyFunc() As String Implements ExampleCS.MyInterface.MyFunc
            Throw New NotImplementedException()
        End Function
        Public ReadOnly Property MyProp() As String Implements ExampleCS.MyInterface.MyProp
            Get
            End Get
        End Property
    End Class
    Public Class MyImplementor2
        Implements MyInterface
        Sub MySub() Implements MyInterface.MySub
        End Sub
        Function MyFunc() As String Implements MyInterface.MyFunc
            Throw New NotImplementedException()
        End Function
        ReadOnly Property MyProp() As String Implements MyInterface.MyProp
            Get
            End Get
        End Property
    End Class
End Namespace
