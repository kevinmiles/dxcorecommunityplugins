namespace DXCore_TestAppC
{
    namespace SomeReallyFuckingRandomNamespace
    {
        public class CollectionFieldsClass : DevExpress.Data.Filtering.OperandProperty
        {
            public CollectionFieldsClass(string propertyName)
                : base(propertyName)
            {
            }
            public DevExpress.Data.Filtering.AggregateOperand Sum(DevExpress.Data.Filtering.CriteriaOperator PropertyToSum, DevExpress.Data.Filtering.CriteriaOperator Condition)
            {
                return new DevExpress.Data.Filtering.AggregateOperand(new DevExpress.Data.Filtering.OperandProperty(PropertyName), PropertyToSum, DevExpress.Data.Filtering.Aggregate.Sum, Condition);
            }
            DevExpress.Data.Filtering.AggregateOperand Sum(DevExpress.Data.Filtering.CriteriaOperator PropertyToSum)
            {
                return new DevExpress.Data.Filtering.AggregateOperand(new DevExpress.Data.Filtering.OperandProperty(PropertyName), PropertyToSum, DevExpress.Data.Filtering.Aggregate.Sum, null);
            }
            public DevExpress.Data.Filtering.AggregateOperand Count(DevExpress.Data.Filtering.CriteriaOperator PropertyToCount)
            {
                return new DevExpress.Data.Filtering.AggregateOperand(new DevExpress.Data.Filtering.OperandProperty(PropertyName), PropertyToCount, DevExpress.Data.Filtering.Aggregate.Sum, null);
            }
        }
    }
}
