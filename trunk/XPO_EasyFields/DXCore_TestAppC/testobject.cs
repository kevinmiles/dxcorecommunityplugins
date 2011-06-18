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
        //Created/Updated: PC-DEV\Michael on PC-DEV at 11/06/2011 9:14 AM
        public new class FieldsClass : XPBaseObject.FieldsClass
        {
            public FieldsClass()
                : base()
            {
            }
            public FieldsClass(string propertyName)
                : base(propertyName)
            {
            }
            public const String IDFieldName = "ID";
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
    //Created/Updated: PC-DEV\Michael on PC-DEV at 11/06/2011 9:14 AM
    public new class FieldsClass : XPBaseObject.FieldsClass
    {
        public FieldsClass()
            : base()
        {
        }
        public FieldsClass(string propertyName)
            : base(propertyName)
        {
        }
        public const String Session3sFieldName = "Session3s";
        public DevExpress.Data.Filtering.OperandProperty Session3s
        {
            get
            {
                return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("Session3s"));
            }
        }
        public DXCore_TestAppC.SessionMB.FieldsClass Key_SessionMB
        {
            get
            {
                return new DXCore_TestAppC.SessionMB.FieldsClass(GetNestedName("Key.SessionMB"));
            }
        }
        public const String Key_SequenceFieldName = "Key.Sequence";
        public DevExpress.Data.Filtering.OperandProperty Key_Sequence
        {
            get
            {
                return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("Key.Sequence"));
            }
        }
        public const String KeyFieldName = "Key";
        public DevExpress.Data.Filtering.OperandProperty Key
        {
            get
            {
                return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("Key"));
            }
        }
        public const String sTestFieldName = "sTest";
        public DevExpress.Data.Filtering.OperandProperty sTest
        {
            get
            {
                return new DevExpress.Data.Filtering.OperandProperty(GetNestedName("sTest"));
            }
        }
    }
}

}
