using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace DXCore_TestAppC
{

    public class SessionMB : XPBaseObject
    {
        private int _key;
        [Key, Persistent]
        private int ID
        {
            get { return _key; }
            set { _key = value; }
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
        //Created/Updated: PC-ALF\PC-ALF\Michael 4/05/2010 2:11 AM
        public new class FieldsClass : DevExpress.Xpo.XPBaseObject.FieldsClass
        {
            public FieldsClass()
                : base()
            {
            }
            public FieldsClass(string propertyName)
                : base(propertyName)
            {
            }
            public DevExpress.Data.Filtering.OperandProperty ID
            {
                get
                {
                    return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("ID"));
                }
            }
        }
    }

  public struct CaptureKey
{
        [Association( "Session_Captures" ), Persistent( "IdSession" )]
        public SessionMB SessionMB;

        [Persistent( "Sequence" )]
        public int Sequence;

        public CaptureKey( SessionMB session, int sequence )
        {
            SessionMB = session;
            Sequence = sequence;
        }

}

public class Capture : XPBaseObject
{

    [Association("Capture-Sessions")]
    public XPCollection<SessionMB> Session3s
    {
        get
        {
            return GetCollection<SessionMB>("Sessions");
        }
    }
    private CaptureKey _key;
    [Key, Persistent]
    private CaptureKey Key
    {
        get { return _key; }
        set { _key = value; }
    }

    private string _STest;
    public string sTest
    { 
        get
        {
            return _STest;
        }
        set
        {
            SetPropertyValue("sTest", ref _STest, value);
        }
    }


    //Created/Updated: PC-ALF\PC-ALF\Michael 4/05/2010 2:14 AM
    public new class FieldsClass : DevExpress.Xpo.XPBaseObject.FieldsClass
    {
        public FieldsClass()
            : base()
        {
        }
        public FieldsClass(string propertyName)
            : base(propertyName)
        {
        }
        public DXCore_TestAppC.CollectionFieldsClass Session3s
        {
            get
            {
                return new DXCore_TestAppC.CollectionFieldsClass(GetNestedName("Session3s"));
            }
        }
        public DXCore_TestAppC.SessionMB.FieldsClass Key_SessionMB
        {
            get
            {
                return new DXCore_TestAppC.SessionMB.FieldsClass(GetNestedName("SessionMB"));
            }
        }
        public DevExpress.Data.Filtering.OperandProperty Key_Sequence
        {
            get
            {
                return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("Sequence"));
            }
        }
        public DevExpress.Data.Filtering.OperandProperty sTest
        {
            get
            {
                return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("sTest"));
            }
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
