using System;
using Xunit;
using Tynamix.ObjectFiller;
// ReSharper disable RedundantCast

namespace ObjectFiller.Test
{
    public class SequenceGeneratorTest
    {
        [Fact]
        public void SequenceGeneratorSByte__Should_work()
        {
            var generator = new SequenceGeneratorSByte();
            Assert.Equal((SByte)0, generator.GetValue());
            Assert.Equal((SByte)1, generator.GetValue());
            Assert.Equal((SByte)2, generator.GetValue());

            generator = new SequenceGeneratorSByte { From = 3 };
            Assert.Equal((SByte)3, generator.GetValue());
            Assert.Equal((SByte)4, generator.GetValue());
            Assert.Equal((SByte)5, generator.GetValue());

            generator = new SequenceGeneratorSByte { From = 3, Step = 3 };
            Assert.Equal((SByte)3, generator.GetValue());
            Assert.Equal((SByte)6, generator.GetValue());
            Assert.Equal((SByte)9, generator.GetValue());

            generator = new SequenceGeneratorSByte { From = 3, Step = -3 };
            Assert.Equal((SByte)3, generator.GetValue());
            Assert.Equal((SByte)0, generator.GetValue());
            Assert.Equal((SByte)(-3), generator.GetValue());

            generator = new SequenceGeneratorSByte { From = SByte.MaxValue - 1 };
            Assert.Equal((SByte)(SByte.MaxValue - 1), generator.GetValue());
            Assert.Equal((SByte)(SByte.MaxValue - 0), generator.GetValue());
            Assert.Equal((SByte)(SByte.MinValue + 0), generator.GetValue());
            Assert.Equal((SByte)(SByte.MinValue + 1), generator.GetValue());
        }

        [Fact]
        public void SequenceGeneratorInt16__Should_work()
        {
            var generator = new SequenceGeneratorInt16();
            Assert.Equal((Int16)0, generator.GetValue());
            Assert.Equal((Int16)1, generator.GetValue());
            Assert.Equal((Int16)2, generator.GetValue());

            generator = new SequenceGeneratorInt16 { From = 3 };
            Assert.Equal((Int16)3, generator.GetValue());
            Assert.Equal((Int16)4, generator.GetValue());
            Assert.Equal((Int16)5, generator.GetValue());

            generator = new SequenceGeneratorInt16 { From = 3, Step = 3 };
            Assert.Equal((Int16)3, generator.GetValue());
            Assert.Equal((Int16)6, generator.GetValue());
            Assert.Equal((Int16)9, generator.GetValue());

            generator = new SequenceGeneratorInt16 { From = 3, Step = -3 };
            Assert.Equal((Int16)3, generator.GetValue());
            Assert.Equal((Int16)0, generator.GetValue());
            Assert.Equal((Int16)(-3), generator.GetValue());

            generator = new SequenceGeneratorInt16 { From = Int16.MaxValue - 1 };
            Assert.Equal((Int16)(Int16.MaxValue - 1), generator.GetValue());
            Assert.Equal((Int16)(Int16.MaxValue - 0), generator.GetValue());
            Assert.Equal((Int16)(Int16.MinValue + 0), generator.GetValue());
            Assert.Equal((Int16)(Int16.MinValue + 1), generator.GetValue());
        }

        [Fact]
        public void SequenceGeneratorInt32__Should_work()
        {
            var generator = new SequenceGeneratorInt32();
            Assert.Equal((Int32)0, generator.GetValue());
            Assert.Equal((Int32)1, generator.GetValue());
            Assert.Equal((Int32)2, generator.GetValue());

            generator = new SequenceGeneratorInt32 { From = 3 };
            Assert.Equal((Int32)3, generator.GetValue());
            Assert.Equal((Int32)4, generator.GetValue());
            Assert.Equal((Int32)5, generator.GetValue());

            generator = new SequenceGeneratorInt32 { From = 3, Step = 3 };
            Assert.Equal((Int32)3, generator.GetValue());
            Assert.Equal((Int32)6, generator.GetValue());
            Assert.Equal((Int32)9, generator.GetValue());

            generator = new SequenceGeneratorInt32 { From = 3, Step = -3 };
            Assert.Equal((Int32)3, generator.GetValue());
            Assert.Equal((Int32)0, generator.GetValue());
            Assert.Equal((Int32)(-3), generator.GetValue());

            generator = new SequenceGeneratorInt32 { From = Int32.MaxValue - 1 };
            Assert.Equal((Int32)(Int32.MaxValue - 1), generator.GetValue());
            Assert.Equal((Int32)(Int32.MaxValue - 0), generator.GetValue());
            Assert.Equal((Int32)(Int32.MinValue + 0), generator.GetValue());
            Assert.Equal((Int32)(Int32.MinValue + 1), generator.GetValue());
        }

        [Fact]
        public void SequenceGeneratorInt64__Should_work()
        {
            var generator = new SequenceGeneratorInt64();
            Assert.Equal((Int64)0, generator.GetValue());
            Assert.Equal((Int64)1, generator.GetValue());
            Assert.Equal((Int64)2, generator.GetValue());

            generator = new SequenceGeneratorInt64 { From = 3 };
            Assert.Equal((Int64)3, generator.GetValue());
            Assert.Equal((Int64)4, generator.GetValue());
            Assert.Equal((Int64)5, generator.GetValue());

            generator = new SequenceGeneratorInt64 { From = 3, Step = 3 };
            Assert.Equal((Int64)3, generator.GetValue());
            Assert.Equal((Int64)6, generator.GetValue());
            Assert.Equal((Int64)9, generator.GetValue());

            generator = new SequenceGeneratorInt64 { From = 3, Step = -3 };
            Assert.Equal((Int64)3, generator.GetValue());
            Assert.Equal((Int64)0, generator.GetValue());
            Assert.Equal((Int64)(-3), generator.GetValue());

            generator = new SequenceGeneratorInt64 { From = Int64.MaxValue - 1 };
            Assert.Equal((Int64)(Int64.MaxValue - 1), generator.GetValue());
            Assert.Equal((Int64)(Int64.MaxValue - 0), generator.GetValue());
            Assert.Equal((Int64)(Int64.MinValue + 0), generator.GetValue());
            Assert.Equal((Int64)(Int64.MinValue + 1), generator.GetValue());
        }

        [Fact]
        public void SequenceGeneratorUInt16__Should_work()
        {
            var generator = new SequenceGeneratorUInt16();
            Assert.Equal((UInt16)0, generator.GetValue());
            Assert.Equal((UInt16)1, generator.GetValue());
            Assert.Equal((UInt16)2, generator.GetValue());

            generator = new SequenceGeneratorUInt16 { From = 3 };
            Assert.Equal((UInt16)3, generator.GetValue());
            Assert.Equal((UInt16)4, generator.GetValue());
            Assert.Equal((UInt16)5, generator.GetValue());

            generator = new SequenceGeneratorUInt16 { From = 3, Step = 3 };
            Assert.Equal((UInt16)3, generator.GetValue());
            Assert.Equal((UInt16)6, generator.GetValue());
            Assert.Equal((UInt16)9, generator.GetValue());

            generator = new SequenceGeneratorUInt16 { From = UInt16.MaxValue - 1 };
            Assert.Equal((UInt16)(UInt16.MaxValue - 1), generator.GetValue());
            Assert.Equal((UInt16)(UInt16.MaxValue - 0), generator.GetValue());
            Assert.Equal((UInt16)(UInt16.MinValue + 0), generator.GetValue());
            Assert.Equal((UInt16)(UInt16.MinValue + 1), generator.GetValue());
        }

        [Fact]
        public void SequenceGeneratorByte__Should_work()
        {
            var generator = new SequenceGeneratorByte();
            Assert.Equal((Byte)0, generator.GetValue());
            Assert.Equal((Byte)1, generator.GetValue());
            Assert.Equal((Byte)2, generator.GetValue());

            generator = new SequenceGeneratorByte { From = 3 };
            Assert.Equal((Byte)3, generator.GetValue());
            Assert.Equal((Byte)4, generator.GetValue());
            Assert.Equal((Byte)5, generator.GetValue());

            generator = new SequenceGeneratorByte { From = 3, Step = 3 };
            Assert.Equal((Byte)3, generator.GetValue());
            Assert.Equal((Byte)6, generator.GetValue());
            Assert.Equal((Byte)9, generator.GetValue());

            generator = new SequenceGeneratorByte { From = Byte.MaxValue - 1 };
            Assert.Equal((Byte)(Byte.MaxValue - 1), generator.GetValue());
            Assert.Equal((Byte)(Byte.MaxValue - 0), generator.GetValue());
            Assert.Equal((Byte)(Byte.MinValue + 0), generator.GetValue());
            Assert.Equal((Byte)(Byte.MinValue + 1), generator.GetValue());
        }

        [Fact]
        public void SequenceGeneratorUInt32__Should_work()
        {
            var generator = new SequenceGeneratorUInt32();
            Assert.Equal((UInt32)0, generator.GetValue());
            Assert.Equal((UInt32)1, generator.GetValue());
            Assert.Equal((UInt32)2, generator.GetValue());

            generator = new SequenceGeneratorUInt32 { From = 3 };
            Assert.Equal((UInt32)3, generator.GetValue());
            Assert.Equal((UInt32)4, generator.GetValue());
            Assert.Equal((UInt32)5, generator.GetValue());

            generator = new SequenceGeneratorUInt32 { From = 3, Step = 3 };
            Assert.Equal((UInt32)3, generator.GetValue());
            Assert.Equal((UInt32)6, generator.GetValue());
            Assert.Equal((UInt32)9, generator.GetValue());

            generator = new SequenceGeneratorUInt32 { From = UInt32.MaxValue - 1 };
            Assert.Equal((UInt32)(UInt32.MaxValue - 1), generator.GetValue());
            Assert.Equal((UInt32)(UInt32.MaxValue - 0), generator.GetValue());
            Assert.Equal((UInt32)(UInt32.MinValue + 0), generator.GetValue());
            Assert.Equal((UInt32)(UInt32.MinValue + 1), generator.GetValue());
        }

        [Fact]
        public void SequenceGeneratorUInt64__Should_work()
        {
            var generator = new SequenceGeneratorUInt64();
            Assert.Equal((UInt64)0, generator.GetValue());
            Assert.Equal((UInt64)1, generator.GetValue());
            Assert.Equal((UInt64)2, generator.GetValue());

            generator = new SequenceGeneratorUInt64 { From = 3 };
            Assert.Equal((UInt64)3, generator.GetValue());
            Assert.Equal((UInt64)4, generator.GetValue());
            Assert.Equal((UInt64)5, generator.GetValue());

            generator = new SequenceGeneratorUInt64 { From = 3, Step = 3 };
            Assert.Equal((UInt64)3, generator.GetValue());
            Assert.Equal((UInt64)6, generator.GetValue());
            Assert.Equal((UInt64)9, generator.GetValue());

            generator = new SequenceGeneratorUInt64 { From = UInt64.MaxValue - 1 };
            Assert.Equal((UInt64)(UInt64.MaxValue - 1), generator.GetValue());
            Assert.Equal((UInt64)(UInt64.MaxValue - 0), generator.GetValue());
            Assert.Equal((UInt64)(UInt64.MinValue + 0), generator.GetValue());
            Assert.Equal((UInt64)(UInt64.MinValue + 1), generator.GetValue());
        }

        [Fact]
        public void SequenceGeneratorChar__Should_work()
        {
            var generator = new SequenceGeneratorChar();
            Assert.Equal((Char)0, generator.GetValue());
            Assert.Equal((Char)1, generator.GetValue());
            Assert.Equal((Char)2, generator.GetValue());

            generator = new SequenceGeneratorChar { From = 'A' };
            Assert.Equal((Char)'A', generator.GetValue());
            Assert.Equal((Char)'B', generator.GetValue());
            Assert.Equal((Char)'C', generator.GetValue());

            generator = new SequenceGeneratorChar { From = 'A', Step = (Char)3 };
            Assert.Equal((Char)'A', generator.GetValue());
            Assert.Equal((Char)'D', generator.GetValue());
            Assert.Equal((Char)'G', generator.GetValue());
        }

        [Fact]
        public void SequenceGeneratorSingle__Should_work()
        {
            var generator = new SequenceGeneratorSingle();
            Assert.Equal((Single)0, generator.GetValue());
            Assert.Equal((Single)1, generator.GetValue());
            Assert.Equal((Single)2, generator.GetValue());

            generator = new SequenceGeneratorSingle { From = 3 };
            Assert.Equal((Single)3, generator.GetValue());
            Assert.Equal((Single)4, generator.GetValue());
            Assert.Equal((Single)5, generator.GetValue());

            generator = new SequenceGeneratorSingle { From = 3, Step = 3 };
            Assert.Equal((Single)3, generator.GetValue());
            Assert.Equal((Single)6, generator.GetValue());
            Assert.Equal((Single)9, generator.GetValue());

            generator = new SequenceGeneratorSingle { From = 3, Step = -3 };
            Assert.Equal((Single)3, generator.GetValue());
            Assert.Equal((Single)0, generator.GetValue());
            Assert.Equal((Single)(-3), generator.GetValue());

            generator = new SequenceGeneratorSingle { From = Single.MaxValue - 1 };
            Assert.Equal((Single)(Single.MaxValue - 1), generator.GetValue());
            Assert.Equal((Single)(Single.MaxValue - 0), generator.GetValue());
        }

        [Fact]
        public void SequenceGeneratorDouble__Should_work()
        {
            var generator = new SequenceGeneratorDouble();
            Assert.Equal((Double)0, generator.GetValue());
            Assert.Equal((Double)1, generator.GetValue());
            Assert.Equal((Double)2, generator.GetValue());

            generator = new SequenceGeneratorDouble { From = 3 };
            Assert.Equal((Double)3, generator.GetValue());
            Assert.Equal((Double)4, generator.GetValue());
            Assert.Equal((Double)5, generator.GetValue());

            generator = new SequenceGeneratorDouble { From = 3, Step = 3 };
            Assert.Equal((Double)3, generator.GetValue());
            Assert.Equal((Double)6, generator.GetValue());
            Assert.Equal((Double)9, generator.GetValue());

            generator = new SequenceGeneratorDouble { From = 3, Step = -3 };
            Assert.Equal((Double)3, generator.GetValue());
            Assert.Equal((Double)0, generator.GetValue());
            Assert.Equal((Double)(-3), generator.GetValue());

            generator = new SequenceGeneratorDouble { From = Double.MaxValue - 1 };
            Assert.Equal((Double)(Double.MaxValue - 1), generator.GetValue());
            Assert.Equal((Double)(Double.MaxValue - 0), generator.GetValue());
        }

        [Fact]
        public void SequenceGeneratorDateTime__Should_work()
        {
            var generator = new SequenceGeneratorDateTime();
            Assert.Equal(new DateTime().AddDays(0), generator.GetValue());
            Assert.Equal(new DateTime().AddDays(1), generator.GetValue());
            Assert.Equal(new DateTime().AddDays(2), generator.GetValue());

            var date = DateTime.Now.Date;
            generator = new SequenceGeneratorDateTime { From = date };
            Assert.Equal(date.AddDays(0), generator.GetValue());
            Assert.Equal(date.AddDays(1), generator.GetValue());
            Assert.Equal(date.AddDays(2), generator.GetValue());

            generator = new SequenceGeneratorDateTime { From = date, Step = TimeSpan.FromDays(3) };
            Assert.Equal(date.AddDays(0), generator.GetValue());
            Assert.Equal(date.AddDays(3), generator.GetValue());
            Assert.Equal(date.AddDays(6), generator.GetValue());

            generator = new SequenceGeneratorDateTime { From = date, Step = TimeSpan.FromDays(-3) };
            Assert.Equal(date.AddDays(0), generator.GetValue());
            Assert.Equal(date.AddDays(-3), generator.GetValue());
            Assert.Equal(date.AddDays(-6), generator.GetValue());
        }

    }
}
