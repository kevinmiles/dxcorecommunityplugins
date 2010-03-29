Public Class AggregateFieldsClass
    imports PersistentBase.FieldsClass

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal propertyName As String)
        MyBase.New(propertyName)
    End Sub

    Public Function Sum(ByVal expression As CriteriaOperator, ByVal condition As CriteriaOperator) As CriteriaOperator
        Return New AggregateOperand(PropertyName, CriteriaOperator.ToString(expression), Aggregate.Sum, condition)
    End Function
End Class
