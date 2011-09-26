// <copyright file="SA1121TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using MyBool = System.Boolean;
    using MyByte = System.Byte;
    using MyChar = System.Char;
    using MyDecimal = System.Decimal;
    using MyDouble = System.Double;
    using MyInt16 = System.Int16;
    using MyInt32 = System.Int32;
    using MyInt64 = System.Int64;
    using MyObject = System.Object;
    using MySByte = System.SByte;
    using MySingle = System.Single;
    using MyString = System.String;
    using MySystem = System;
    using MyUInt16 = System.UInt16;
    using MyUInt32 = System.UInt32;
    using MyUInt64 = System.UInt64;

#pragma warning disable 169

    /// <summary>
    /// Test code for SA1121 rule - always use type alias.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1121 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "*", Justification = "This is about SA1121 rule.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "*", Justification = "This is about SA1121 rule.")]
    public class SA1121TestCode
    {
        private Boolean boolean1;
        private System.Boolean boolean2;
        private global::System.Boolean boolean3;
        private MySystem.Boolean boolean4;
        private MyBool boolean5;
        private bool bool6;
        
        private Byte byte1;
        private System.Byte byte2;
        private global::System.Byte byte3;
        private MySystem.Byte byte4;
        private MyByte byte5;
        private byte byte6;
        
        private Char char1;
        private System.Char char2;
        private global::System.Char char3;
        private MySystem.Char char4;
        private MyChar char5;
        private char char6;
        
        private Decimal decimal1;
        private System.Decimal decimal2;
        private global::System.Decimal decimal3;
        private MySystem.Decimal decimal4;
        private MyDecimal decimal5;
        private decimal decimal6;
        
        private Double double1;
        private System.Double double2;
        private global::System.Double double3;
        private MySystem.Double double4;
        private MyDouble double5;
        private double double6;
        
        private Int16 short1;
        private System.Int16 short2;
        private global::System.Int16 short3;
        private MySystem.Int16 short4;
        private MyInt16 short5;
        private short short6;
        
        private Int32 int1;
        private System.Int32 int2;
        private global::System.Int32 int3;
        private MySystem.Int32 int4;
        private MyInt32 int5;
        private int int6;
        
        private Int64 long1;
        private System.Int64 long2;
        private global::System.Int64 long3;
        private MySystem.Int64 long4;
        private MyInt64 long5;
        private long long6;
        
        private Object object1;
        private System.Object object2;
        private global::System.Object object3;
        private MySystem.Object object4;
        private MyObject object5;
        private object object6;
        
        private SByte sbyte1;
        private System.SByte sbyte2;
        private global::System.SByte sbyte3;
        private MySystem.SByte sbyte4;
        private MySByte sbyte5;
        private sbyte sbyte6;
        
        private Single float1;
        private System.Single float2;
        private global::System.Single float3;
        private MySystem.Single float4;
        private MySingle float5;
        private float float6;
        
        private String string1;
        private System.String string2;
        private global::System.String string3;
        private MySystem.String string4;
        private MyString string5;
        private string string6;
        
        private UInt16 ushort1;
        private System.UInt16 ushort2;
        private global::System.UInt16 ushort3;
        private MySystem.UInt16 ushort4;
        private MyUInt16 ushort5;
        private ushort ushort6;
        
        private UInt32 uint1;
        private System.UInt32 uint2;
        private global::System.UInt32 uint3;
        private MySystem.UInt32 uint4;
        private MyUInt32 uint5;
        private uint uint6;
        
        private UInt64 ulong1;
        private System.UInt64 ulong2;
        private global::System.UInt64 ulong3;
        private MySystem.UInt64 ulong4;
        private MyUInt64 ulong5;
        private ulong ulong6;
    }
}
