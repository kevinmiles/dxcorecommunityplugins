using System;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.Helpers;
using DevExpress.XtraEditors.DXErrorProvider;
using ShepherdOaks.Business.Interfaces;

namespace ShepherdOaks.Persistency
{
    [Persistent("BusinessObjects")]
    public class BusinessObject : XPCustomObject, IBusinessObject, IDXDataErrorInfo
    {
        public BusinessObject() : base() { }
        public BusinessObject(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            CreatedDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }

        #region Relationships

        private User _ownerUser;
        [Association(RelationshipNames.UserToBusinessObjects),Persistent("OwnerUserId")]
        public User OwnerUser
        {
            get { return _ownerUser; }
            set { SetPropertyValue("OwnerUser", ref _ownerUser, value); }
        }
        private User _createdBy;
        [Association(RelationshipNames.UserToCreatedObjects), Persistent("CreatedByUserId")]
        public User CreatedBy
        {
            get { return _createdBy; }
            set { SetPropertyValue("CreatedBy", ref _createdBy, value); }
        }

        private User _lastModifiedBy;
        [Association(RelationshipNames.UserToLastModifiedObjects), Persistent("LastModifiedByUserId")]
        public User LastModifiedBy
        {
            get { return _lastModifiedBy; }
            set { SetPropertyValue("LastModifiedBy", ref _lastModifiedBy, value); }
        }

        [Association(RelationshipNames.BusinessObjectsToDocuments, UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Document> Documents
        {
            get { return GetCollection<Document>("Documents"); }
        }

        [Association(RelationshipNames.BusinessObjectToPictures, UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Picture> Pictures
        {
            get { return GetCollection<Picture>("Pictures"); }
        }

        [Association(RelationshipNames.BusinessObjectsToGroups, UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Group> Groups
        {
            get { return GetCollection<Group>("Groups"); }
        }

        [Association(RelationshipNames.BusinessObjectsToNotes, UseAssociationNameAsIntermediateTableName = true)]
        public XPCollection<Note> Notes
        {
            get { return GetCollection<Note>("Notes"); }
        }
        #endregion

        private Guid _oid; 
        [Key(true)]
        public Guid Oid
        {
            get { return _oid; }
            set { SetPropertyValue("Oid", ref _oid, value); }
        }
        private DateTime _createdDate;
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { SetPropertyValue("CreatedDate", ref _createdDate, value); }
        }

        private DateTime _lastModifiedDate;
        public DateTime LastModifiedDate
        {
            get { return _lastModifiedDate; }
            set { SetPropertyValue("LastModifiedDate", ref _lastModifiedDate, value); }
        }

        private bool _isSystem;
        public bool IsSystem
        {
            get { return _isSystem; }
            set { SetPropertyValue("IsSystem", ref _isSystem, value); }
        }

        private Accessability _accessability;
        public virtual Accessability Accessability
        {
            get { return Accessability.Private; }
            set { SetPropertyValue("Accessability", ref _accessability, value); }
             
        }


        #region IBusinessObject Members

        IUser IBusinessObject.OwnerUser
        {
            get { return OwnerUser as IUser; }
            set { OwnerUser = value as User; }
        }

        IUser IBusinessObject.CreatedBy
        {
            get { return CreatedBy as IUser; }
            set { CreatedBy = value as User; }
        }

        IUser IBusinessObject.LastModifiedBy
        {
            get { return LastModifiedBy as IUser; }
            set { LastModifiedBy = value as User; }
        }

        IList<IDocument> IBusinessObject.Documents
        {
            get { return new ListMorpher<IDocument, Document>(this.Documents); }
        }

        IList<IPicture> IBusinessObject.Pictures
        {
            get { return new ListMorpher<IPicture, Picture>(this.Pictures); }
        }

        IList<IGroup> IBusinessObject.Groups
        {
            get { return new ListMorpher<IGroup, Group>(this.Groups); }
        }

        IList<INote> IBusinessObject.Notes
        {
            get { return new ListMorpher<INote, Note>(this.Notes); }
        }

        #endregion

        #region IDXDataErrorInfo Members

        public virtual void GetError(ErrorInfo info){}

        public virtual void GetPropertyError(string propertyName, ErrorInfo info)
        {
            //throw new NotImplementedException();
        }

        #endregion
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
        //Created/Updated: Mon 29-Mar-2010 20:57:39
        public new class FieldsClass : XPCustomObject.FieldsClass
        {
            public FieldsClass()
                : base()
            {
            }
            public FieldsClass(string propertyName)
                : base(propertyName)
            {
            }
            public DevExpress.Data.Filtering.OperandProperty OwnerUser
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("OwnerUser"));
                }
            }
            public DevExpress.Data.Filtering.OperandProperty CreatedBy
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("CreatedBy"));
                }
            }
            public DevExpress.Data.Filtering.OperandProperty LastModifiedBy
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("LastModifiedBy"));
                }
            }
            public DevExpress.Data.Filtering.OperandProperty Documents
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("Documents"));
                }
            }
            public DevExpress.Data.Filtering.OperandProperty Pictures
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("Pictures"));
                }
            }
            public DevExpress.Data.Filtering.OperandProperty Groups
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("Groups"));
                }
            }
            public DevExpress.Data.Filtering.OperandProperty Notes
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("Notes"));
                }
            }
            public DevExpress.Data.Filtering.OperandProperty Oid
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("Oid"));
                }
            }
            public DevExpress.Data.Filtering.OperandProperty CreatedDate
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("CreatedDate"));
                }
            }
            public DevExpress.Data.Filtering.OperandProperty LastModifiedDate
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("LastModifiedDate"));
                }
            }
            public DevExpress.Data.Filtering.OperandProperty IsSystem
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("IsSystem"));
                }
            }
            public DevExpress.Data.Filtering.OperandProperty Accessability
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("Accessability"));
                }
            }
        }
	}
}