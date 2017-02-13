using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tynamix.ObjectFiller;

namespace ObjectFiller.Test
{
    [TestClass]
    public class DefaultDatatypeMappingsTest
    {
        [TestMethod]
        public void Ensure_that_double_does_not_return_infinity()
        {
            var filler = new Filler<MyClass>();
            var myClass = filler.Create();
            Assert.IsFalse(double.IsInfinity(myClass._double));

            Assert.IsFalse(float.IsInfinity(myClass._float));
        }

        [TestMethod]
        public void Ensure_that_each_primitive_datatype_is_mapped_by_default()
        {
            var filler = new Filler<MyClass>();
            var myClasses = filler.Create(100).ToArray();
            foreach (var myClass in myClasses)
            {
                Assert.AreNotEqual(default(Guid), myClass._Guid);
                Assert.AreNotEqual(default(decimal), myClass._Decimal);
            }
        }

        public class MyClass
        {
            public bool _bool { get; set; }
            public byte _byte { get; set; }
            public char _char { get; set; }
            public Int16 _i16 { get; set; }
            public Int32 _i32 { get; set; }
            public Int64 _i64 { get; set; }
            public UInt16 _u16 { get; set; }
            public UInt32 _u32 { get; set; }
            public UInt64 _u64 { get; set; }
            public float _float { get; set; }
            public double _double { get; set; }
            public decimal _Decimal { get; set; }
            public IntPtr _IntPtr { get; set; }
            public DateTime _DateTime { get; set; }
            public TimeSpan _TimeSpan { get; set; }
            public Guid _Guid { get; set; }
            public string _String { get; set; }

            public bool? _n_bool { get; set; }
            public byte? _n_byte { get; set; }
            public char? _n_char { get; set; }
            public Int16? _n_i16 { get; set; }
            public Int32? _n_i32 { get; set; }
            public Int64? _n_i64 { get; set; }
            public UInt16? _n_u16 { get; set; }
            public UInt32? _n_u32 { get; set; }
            public UInt64? _n_u64 { get; set; }
            public float? _n_float { get; set; }
            public double? _n_double { get; set; }
            public decimal? _n_Decimal { get; set; }
            public IntPtr? _n_IntPtr { get; set; }
            public DateTime? _n_DateTime { get; set; }
            public TimeSpan? _n_TimeSpan { get; set; }
            public Guid? _n_Guid { get; set; }
        }
    }
}
