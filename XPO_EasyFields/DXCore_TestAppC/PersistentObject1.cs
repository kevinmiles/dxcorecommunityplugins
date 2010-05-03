using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.Helpers;

namespace DXCore_TestAppC
{

    public class PersistentObject1 : XPObject
    {
        public PersistentObject1()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public PersistentObject1(Session session)
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
        private static FieldsClass _fields;
        public new static FieldsClass Fields
        {
            get
            {
                if (ReferenceEquals(_fields, null))
                    _fields = new FieldsClass();
                return _fields;
            }
        }
    } 
}