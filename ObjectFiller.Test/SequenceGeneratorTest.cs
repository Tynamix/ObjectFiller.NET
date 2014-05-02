using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tynamix.ObjectFiller.Plugins;
// ReSharper disable RedundantCast

namespace ObjectFiller.Test
{
	[TestClass]
	public class SequenceGeneratorTest
	{
		[TestMethod]
		public void SequenceGeneratorSByte__Should_work()
		{
			var generator = new SequenceGeneratorSByte();
			Assert.AreEqual((SByte)0, generator.GetValue());
			Assert.AreEqual((SByte)1, generator.GetValue());
			Assert.AreEqual((SByte)2, generator.GetValue());

			generator = new SequenceGeneratorSByte { From = 3 };
			Assert.AreEqual((SByte)3, generator.GetValue());
			Assert.AreEqual((SByte)4, generator.GetValue());
			Assert.AreEqual((SByte)5, generator.GetValue());

			generator = new SequenceGeneratorSByte { From = 3, Step = 3 };
			Assert.AreEqual((SByte)3, generator.GetValue());
			Assert.AreEqual((SByte)6, generator.GetValue());
			Assert.AreEqual((SByte)9, generator.GetValue());

			generator = new SequenceGeneratorSByte { From = 3, Step = -3 };
			Assert.AreEqual((SByte)3, generator.GetValue());
			Assert.AreEqual((SByte)0, generator.GetValue());
			Assert.AreEqual((SByte)(-3), generator.GetValue());

			generator = new SequenceGeneratorSByte {From = SByte.MaxValue - 1};
			Assert.AreEqual((SByte)(SByte.MaxValue - 1), generator.GetValue());
			Assert.AreEqual((SByte)(SByte.MaxValue - 0), generator.GetValue());
			Assert.AreEqual((SByte)(SByte.MinValue + 0), generator.GetValue());
			Assert.AreEqual((SByte)(SByte.MinValue + 1), generator.GetValue());
		}

		[TestMethod]
		public void SequenceGeneratorInt16__Should_work()
		{
			var generator = new SequenceGeneratorInt16();
			Assert.AreEqual((Int16)0, generator.GetValue());
			Assert.AreEqual((Int16)1, generator.GetValue());
			Assert.AreEqual((Int16)2, generator.GetValue());

			generator = new SequenceGeneratorInt16 { From = 3 };
			Assert.AreEqual((Int16)3, generator.GetValue());
			Assert.AreEqual((Int16)4, generator.GetValue());
			Assert.AreEqual((Int16)5, generator.GetValue());

			generator = new SequenceGeneratorInt16 { From = 3, Step = 3 };
			Assert.AreEqual((Int16)3, generator.GetValue());
			Assert.AreEqual((Int16)6, generator.GetValue());
			Assert.AreEqual((Int16)9, generator.GetValue());

			generator = new SequenceGeneratorInt16 { From = 3, Step = -3 };
			Assert.AreEqual((Int16)3, generator.GetValue());
			Assert.AreEqual((Int16)0, generator.GetValue());
			Assert.AreEqual((Int16)(-3), generator.GetValue());

			generator = new SequenceGeneratorInt16 {From = Int16.MaxValue - 1};
			Assert.AreEqual((Int16)(Int16.MaxValue - 1), generator.GetValue());
			Assert.AreEqual((Int16)(Int16.MaxValue - 0), generator.GetValue());
			Assert.AreEqual((Int16)(Int16.MinValue + 0), generator.GetValue());
			Assert.AreEqual((Int16)(Int16.MinValue + 1), generator.GetValue());
		}

		[TestMethod]
		public void SequenceGeneratorInt32__Should_work()
		{
			var generator = new SequenceGeneratorInt32();
			Assert.AreEqual((Int32)0, generator.GetValue());
			Assert.AreEqual((Int32)1, generator.GetValue());
			Assert.AreEqual((Int32)2, generator.GetValue());

			generator = new SequenceGeneratorInt32 { From = 3 };
			Assert.AreEqual((Int32)3, generator.GetValue());
			Assert.AreEqual((Int32)4, generator.GetValue());
			Assert.AreEqual((Int32)5, generator.GetValue());

			generator = new SequenceGeneratorInt32 { From = 3, Step = 3 };
			Assert.AreEqual((Int32)3, generator.GetValue());
			Assert.AreEqual((Int32)6, generator.GetValue());
			Assert.AreEqual((Int32)9, generator.GetValue());

			generator = new SequenceGeneratorInt32 { From = 3, Step = -3 };
			Assert.AreEqual((Int32)3, generator.GetValue());
			Assert.AreEqual((Int32)0, generator.GetValue());
			Assert.AreEqual((Int32)(-3), generator.GetValue());

			generator = new SequenceGeneratorInt32 {From = Int32.MaxValue - 1};
			Assert.AreEqual((Int32)(Int32.MaxValue - 1), generator.GetValue());
			Assert.AreEqual((Int32)(Int32.MaxValue - 0), generator.GetValue());
			Assert.AreEqual((Int32)(Int32.MinValue + 0), generator.GetValue());
			Assert.AreEqual((Int32)(Int32.MinValue + 1), generator.GetValue());
		}

		[TestMethod]
		public void SequenceGeneratorInt64__Should_work()
		{
			var generator = new SequenceGeneratorInt64();
			Assert.AreEqual((Int64)0, generator.GetValue());
			Assert.AreEqual((Int64)1, generator.GetValue());
			Assert.AreEqual((Int64)2, generator.GetValue());

			generator = new SequenceGeneratorInt64 { From = 3 };
			Assert.AreEqual((Int64)3, generator.GetValue());
			Assert.AreEqual((Int64)4, generator.GetValue());
			Assert.AreEqual((Int64)5, generator.GetValue());

			generator = new SequenceGeneratorInt64 { From = 3, Step = 3 };
			Assert.AreEqual((Int64)3, generator.GetValue());
			Assert.AreEqual((Int64)6, generator.GetValue());
			Assert.AreEqual((Int64)9, generator.GetValue());

			generator = new SequenceGeneratorInt64 { From = 3, Step = -3 };
			Assert.AreEqual((Int64)3, generator.GetValue());
			Assert.AreEqual((Int64)0, generator.GetValue());
			Assert.AreEqual((Int64)(-3), generator.GetValue());

			generator = new SequenceGeneratorInt64 {From = Int64.MaxValue - 1};
			Assert.AreEqual((Int64)(Int64.MaxValue - 1), generator.GetValue());
			Assert.AreEqual((Int64)(Int64.MaxValue - 0), generator.GetValue());
			Assert.AreEqual((Int64)(Int64.MinValue + 0), generator.GetValue());
			Assert.AreEqual((Int64)(Int64.MinValue + 1), generator.GetValue());
		}

		[TestMethod]
		public void SequenceGeneratorUInt16__Should_work()
		{
			var generator = new SequenceGeneratorUInt16();
			Assert.AreEqual((UInt16)0, generator.GetValue());
			Assert.AreEqual((UInt16)1, generator.GetValue());
			Assert.AreEqual((UInt16)2, generator.GetValue());

			generator = new SequenceGeneratorUInt16 { From = 3 };
			Assert.AreEqual((UInt16)3, generator.GetValue());
			Assert.AreEqual((UInt16)4, generator.GetValue());
			Assert.AreEqual((UInt16)5, generator.GetValue());

			generator = new SequenceGeneratorUInt16 { From = 3, Step = 3 };
			Assert.AreEqual((UInt16)3, generator.GetValue());
			Assert.AreEqual((UInt16)6, generator.GetValue());
			Assert.AreEqual((UInt16)9, generator.GetValue());

			generator = new SequenceGeneratorUInt16 { From = UInt16.MaxValue - 1 };
			Assert.AreEqual((UInt16)(UInt16.MaxValue - 1), generator.GetValue());
			Assert.AreEqual((UInt16)(UInt16.MaxValue - 0), generator.GetValue());
			Assert.AreEqual((UInt16)(UInt16.MinValue + 0), generator.GetValue());
			Assert.AreEqual((UInt16)(UInt16.MinValue + 1), generator.GetValue());
		}

		[TestMethod]
		public void SequenceGeneratorByte__Should_work()
		{
			var generator = new SequenceGeneratorByte();
			Assert.AreEqual((Byte)0, generator.GetValue());
			Assert.AreEqual((Byte)1, generator.GetValue());
			Assert.AreEqual((Byte)2, generator.GetValue());

			generator = new SequenceGeneratorByte { From = 3 };
			Assert.AreEqual((Byte)3, generator.GetValue());
			Assert.AreEqual((Byte)4, generator.GetValue());
			Assert.AreEqual((Byte)5, generator.GetValue());

			generator = new SequenceGeneratorByte { From = 3, Step = 3 };
			Assert.AreEqual((Byte)3, generator.GetValue());
			Assert.AreEqual((Byte)6, generator.GetValue());
			Assert.AreEqual((Byte)9, generator.GetValue());

			generator = new SequenceGeneratorByte { From = Byte.MaxValue - 1 };
			Assert.AreEqual((Byte)(Byte.MaxValue - 1), generator.GetValue());
			Assert.AreEqual((Byte)(Byte.MaxValue - 0), generator.GetValue());
			Assert.AreEqual((Byte)(Byte.MinValue + 0), generator.GetValue());
			Assert.AreEqual((Byte)(Byte.MinValue + 1), generator.GetValue());
		}

		[TestMethod]
		public void SequenceGeneratorUInt32__Should_work()
		{
			var generator = new SequenceGeneratorUInt32();
			Assert.AreEqual((UInt32)0, generator.GetValue());
			Assert.AreEqual((UInt32)1, generator.GetValue());
			Assert.AreEqual((UInt32)2, generator.GetValue());

			generator = new SequenceGeneratorUInt32 { From = 3 };
			Assert.AreEqual((UInt32)3, generator.GetValue());
			Assert.AreEqual((UInt32)4, generator.GetValue());
			Assert.AreEqual((UInt32)5, generator.GetValue());

			generator = new SequenceGeneratorUInt32 { From = 3, Step = 3 };
			Assert.AreEqual((UInt32)3, generator.GetValue());
			Assert.AreEqual((UInt32)6, generator.GetValue());
			Assert.AreEqual((UInt32)9, generator.GetValue());

			generator = new SequenceGeneratorUInt32 { From = UInt32.MaxValue - 1 };
			Assert.AreEqual((UInt32)(UInt32.MaxValue - 1), generator.GetValue());
			Assert.AreEqual((UInt32)(UInt32.MaxValue - 0), generator.GetValue());
			Assert.AreEqual((UInt32)(UInt32.MinValue + 0), generator.GetValue());
			Assert.AreEqual((UInt32)(UInt32.MinValue + 1), generator.GetValue());
		}

		[TestMethod]
		public void SequenceGeneratorUInt64__Should_work()
		{
			var generator = new SequenceGeneratorUInt64();
			Assert.AreEqual((UInt64)0, generator.GetValue());
			Assert.AreEqual((UInt64)1, generator.GetValue());
			Assert.AreEqual((UInt64)2, generator.GetValue());

			generator = new SequenceGeneratorUInt64 { From = 3 };
			Assert.AreEqual((UInt64)3, generator.GetValue());
			Assert.AreEqual((UInt64)4, generator.GetValue());
			Assert.AreEqual((UInt64)5, generator.GetValue());

			generator = new SequenceGeneratorUInt64 { From = 3, Step = 3 };
			Assert.AreEqual((UInt64)3, generator.GetValue());
			Assert.AreEqual((UInt64)6, generator.GetValue());
			Assert.AreEqual((UInt64)9, generator.GetValue());

			generator = new SequenceGeneratorUInt64 { From = UInt64.MaxValue - 1 };
			Assert.AreEqual((UInt64)(UInt64.MaxValue - 1), generator.GetValue());
			Assert.AreEqual((UInt64)(UInt64.MaxValue - 0), generator.GetValue());
			Assert.AreEqual((UInt64)(UInt64.MinValue + 0), generator.GetValue());
			Assert.AreEqual((UInt64)(UInt64.MinValue + 1), generator.GetValue());
		}

		[TestMethod]
		public void SequenceGeneratorChar__Should_work()
		{
			var generator = new SequenceGeneratorChar();
			Assert.AreEqual((Char)0, generator.GetValue());
			Assert.AreEqual((Char)1, generator.GetValue());
			Assert.AreEqual((Char)2, generator.GetValue());

			generator = new SequenceGeneratorChar { From = 'A' };
			Assert.AreEqual((Char)'A', generator.GetValue());
			Assert.AreEqual((Char)'B', generator.GetValue());
			Assert.AreEqual((Char)'C', generator.GetValue());

			generator = new SequenceGeneratorChar { From = 'A', Step = (Char)3 };
			Assert.AreEqual((Char)'A', generator.GetValue());
			Assert.AreEqual((Char)'D', generator.GetValue());
			Assert.AreEqual((Char)'G', generator.GetValue());
		}

		[TestMethod]
		public void SequenceGeneratorSingle__Should_work()
		{
			var generator = new SequenceGeneratorSingle();
			Assert.AreEqual((Single)0, generator.GetValue());
			Assert.AreEqual((Single)1, generator.GetValue());
			Assert.AreEqual((Single)2, generator.GetValue());

			generator = new SequenceGeneratorSingle { From = 3 };
			Assert.AreEqual((Single)3, generator.GetValue());
			Assert.AreEqual((Single)4, generator.GetValue());
			Assert.AreEqual((Single)5, generator.GetValue());

			generator = new SequenceGeneratorSingle { From = 3, Step = 3 };
			Assert.AreEqual((Single)3, generator.GetValue());
			Assert.AreEqual((Single)6, generator.GetValue());
			Assert.AreEqual((Single)9, generator.GetValue());

			generator = new SequenceGeneratorSingle { From = 3, Step = -3 };
			Assert.AreEqual((Single)3, generator.GetValue());
			Assert.AreEqual((Single)0, generator.GetValue());
			Assert.AreEqual((Single)(-3), generator.GetValue());

			generator = new SequenceGeneratorSingle { From = Single.MaxValue - 1 };
			Assert.AreEqual((Single)(Single.MaxValue - 1), generator.GetValue());
			Assert.AreEqual((Single)(Single.MaxValue - 0), generator.GetValue());
		}

		[TestMethod]
		public void SequenceGeneratorDouble__Should_work()
		{
			var generator = new SequenceGeneratorDouble();
			Assert.AreEqual((Double)0, generator.GetValue());
			Assert.AreEqual((Double)1, generator.GetValue());
			Assert.AreEqual((Double)2, generator.GetValue());

			generator = new SequenceGeneratorDouble { From = 3 };
			Assert.AreEqual((Double)3, generator.GetValue());
			Assert.AreEqual((Double)4, generator.GetValue());
			Assert.AreEqual((Double)5, generator.GetValue());

			generator = new SequenceGeneratorDouble { From = 3, Step = 3 };
			Assert.AreEqual((Double)3, generator.GetValue());
			Assert.AreEqual((Double)6, generator.GetValue());
			Assert.AreEqual((Double)9, generator.GetValue());

			generator = new SequenceGeneratorDouble { From = 3, Step = -3 };
			Assert.AreEqual((Double)3, generator.GetValue());
			Assert.AreEqual((Double)0, generator.GetValue());
			Assert.AreEqual((Double)(-3), generator.GetValue());

			generator = new SequenceGeneratorDouble { From = Double.MaxValue - 1 };
			Assert.AreEqual((Double)(Double.MaxValue - 1), generator.GetValue());
			Assert.AreEqual((Double)(Double.MaxValue - 0), generator.GetValue());
		}

		[TestMethod]
		public void SequenceGeneratorDateTime__Should_work()
		{
			var generator = new SequenceGeneratorDateTime();
			Assert.AreEqual(new DateTime().AddDays(0), generator.GetValue());
			Assert.AreEqual(new DateTime().AddDays(1), generator.GetValue());
			Assert.AreEqual(new DateTime().AddDays(2), generator.GetValue());

			var date = DateTime.Now.Date;
			generator = new SequenceGeneratorDateTime { From = date };
			Assert.AreEqual(date.AddDays(0), generator.GetValue());
			Assert.AreEqual(date.AddDays(1), generator.GetValue());
			Assert.AreEqual(date.AddDays(2), generator.GetValue());

			generator = new SequenceGeneratorDateTime { From = date, Step = TimeSpan.FromDays(3) };
			Assert.AreEqual(date.AddDays(0), generator.GetValue());
			Assert.AreEqual(date.AddDays(3), generator.GetValue());
			Assert.AreEqual(date.AddDays(6), generator.GetValue());

			generator = new SequenceGeneratorDateTime { From = date, Step = TimeSpan.FromDays(-3) };
			Assert.AreEqual(date.AddDays(0), generator.GetValue());
			Assert.AreEqual(date.AddDays(-3), generator.GetValue());
			Assert.AreEqual(date.AddDays(-6), generator.GetValue());
		}

	}
}
