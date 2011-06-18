using System;
using DevExpress.Xpo.Helpers;

namespace DXCore_TestAppC
{
    
    public class PersistentObject1 : DevExpress.Xpo.XPObject
    {
        public PersistentObject1()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public PersistentObject1(DevExpress.Xpo.Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        public System.Collections.Generic.IList<PersistentObject1> IListPropertyOfPersistentObject1
        {
            get
            {
                return null;
            }
        }
            


        private byte[] fTestByteArray;
        public byte[] TestByteArray
        {
            get
            {
                return fTestByteArray;
            }
            set
            {
                SetPropertyValue("TestByteArray", ref fTestByteArray, value);
            }
        }

        private string _PersistentProperty;
        public string PersistentProperty
        {
            get
            {
                var myvar = new FieldsClass();
                return _PersistentProperty;  
            }
            set
            {
                SetPropertyValue("PersistentProperty", ref _PersistentProperty, value);
            }
        }

        private string _NonPersistentProperty; 
        [DevExpress.Xpo.NonPersistent()]
        public string NonPersistentProperty
        {
            get { return _NonPersistentProperty; }
            set
            {
                _NonPersistentProperty = value;
            }
        } 

        private PersistentObject1 _PersistentReferenceProperty;
        public PersistentObject1 PersistentReferenceProperty
        {
            get
            {
                return _PersistentReferenceProperty;
            }
            set
            {
                SetPropertyValue("PersistentReferenceProperty", ref _PersistentReferenceProperty, value);
            }
        }
        private static FieldsClass _Fields;
        public new static FieldsClass Fields
        {
            get
            {
                if (ReferenceEquals(_Fields, null))
                    _Fields = new FieldsClass();
                return _Fields;
            }
        }
        //Created/Updated: PC-DEV\Michael on PC-DEV at 18/06/2011 12:30 PM
        public new class FieldsClass : DevExpress.Xpo.XPObject.FieldsClass
        {
            public FieldsClass()
                : base()
            {
            }
            public FieldsClass(string propertyName)
                : base(propertyName)
            {
            }
            public const String IListPropertyOfPersistentObject1FieldName = "IListPropertyOfPersistentObject1";
            public DevExpress.Data.Filtering.OperandProperty IListPropertyOfPersistentObject1
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("IListPropertyOfPersistentObject1"));
                }
            }
            public const String TestByteArrayFieldName = "TestByteArray";
            public DevExpress.Data.Filtering.OperandProperty TestByteArray
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("TestByteArray"));
                }
            }
            public const String PersistentPropertyFieldName = "PersistentProperty";
            public DevExpress.Data.Filtering.OperandProperty PersistentProperty
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("PersistentProperty"));
                }
            }
            public const String NonPersistentPropertyFieldName = "NonPersistentProperty";
            public DevExpress.Data.Filtering.OperandProperty NonPersistentProperty
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("NonPersistentProperty"));
                }
            }
            public DXCore_TestAppC.PersistentObject1.FieldsClass PersistentReferenceProperty
            {
                get
                {
                    return new DXCore_TestAppC.PersistentObject1.FieldsClass(GetNestedName("PersistentReferenceProperty"));
                }
            }
        }
    } 
}