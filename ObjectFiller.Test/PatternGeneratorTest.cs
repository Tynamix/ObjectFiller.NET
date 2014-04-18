using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectFiller;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Tynamix.ObjectFiller;
using Tynamix.ObjectFiller.Plugins;

namespace ObjectFiller.Test
{
	[TestClass]
	public class PatternGeneratorTest
	{
		[TestMethod]
		public void Must_be_able_to_handle_private_setters()
		{
			var filler = new ObjectFiller<ClassWithPrivateStuff>();
			var obj = filler.Fill();

			Assert.AreNotEqual(0, obj.WithPrivateSetter, "Must be able to set even a private setter");
			Assert.AreEqual(123, obj.WithoutSetter, "Cannot set that... must get default value");
		}

		[TestMethod]
		public void Must_be_able_to_handle_inheritance_and_sealed()
		{
			var filler = new ObjectFiller<InheritedClass>();
			var obj = filler.Fill();

			Assert.AreNotEqual(0, obj.NormalNumber);
			Assert.AreNotEqual(0, obj.OverrideNormalNumber);
			Assert.AreNotEqual(0, obj.SealedOverrideNormalNumber);
		}

		[TestMethod, Ignore]
		public void Must_be_able_to_handle_arrays()
		{
			var filler = new ObjectFiller<WithArrays>();
			filler.Setup()
				.RegisterInterface<int[],int[]>();
				//.SetupFor<int[]>();
			var obj = filler.Fill();

			Assert.IsNotNull(obj.Ints);
			Assert.IsNotNull(obj.Strings);
			Assert.IsNotNull(obj.Interfaces);
		}

		[TestMethod]
		public void StringPatternGenerator_A()
		{
			HashSet<char> chars = new HashSet<char>();

			var sut = new PatternGenerator("{A}");
			for (int n = 0; n < 10000; n++)
			{
				var s = sut.GetValue();
				Assert.IsTrue(Regex.IsMatch(s, "^[A-Z]$"));
				chars.Add(s[0]);
			}

			Assert.AreEqual(26, chars.Count, "Should have all a..z");
		}

		[TestMethod]
		public void StringPatternGenerator_A_fixed_len()
		{
			var sut = new PatternGenerator("x{A:3}x");
			for (int n = 0; n < 10000; n++)
			{
				var s = sut.GetValue();
				Assert.IsTrue(Regex.IsMatch(s, "^x[A-Z]{3}x$"));
			}
		}

		[TestMethod]
		public void StringPatternGenerator_A_random_len()
		{
			var sut = new PatternGenerator("x{A:3-6}x");
			for (int n = 0; n < 10000; n++)
			{
				var s = sut.GetValue();
				Assert.IsTrue(Regex.IsMatch(s, "^x[A-Z]{3,6}x$"));
			}
		}

		[TestMethod]
		public void StringPatternGenerator_a()
		{
			HashSet<char> chars = new HashSet<char>();

			var sut = new PatternGenerator("{a}");
			for (int n = 0; n < 10000; n++)
			{
				var s = sut.GetValue();
				Assert.IsTrue(s.Length == 1);
				Assert.IsTrue(Regex.IsMatch(s, "^[a-z]$"));
				chars.Add(s[0]);
			}

			Assert.AreEqual(26, chars.Count, "Should have all a..z");
		}

		[TestMethod]
		public void StringPatternGenerator_aaa()
		{
			var sut = new PatternGenerator("xcccx");
			for (int n = 0; n < 10000; n++)
			{
				var s = sut.GetValue();
				Assert.IsTrue(s.Length == 5);
				Assert.IsTrue(Regex.IsMatch(s, "^x[a-z]{3}x$"));
			}
		}

		[TestMethod]
		public void StringPatternGenerator_N()
		{
			HashSet<char> chars = new HashSet<char>();

			var sut = new PatternGenerator("{N}");
			for (int n = 0; n < 10000; n++)
			{
				var s = sut.GetValue();
				Assert.IsTrue(s.Length == 1);
				Assert.IsTrue(Regex.IsMatch(s, "^[0-9]$"));
				chars.Add(s[0]);
			}

			Assert.AreEqual(10, chars.Count, "Should have all 0-9");
		}

		[TestMethod]
		public void StringPatternGenerator_X()
		{
			HashSet<char> chars = new HashSet<char>();

			var sut = new PatternGenerator("{X}");
			for (int n = 0; n < 10000; n++)
			{
				var s = sut.GetValue();
				Assert.IsTrue(s.Length == 1);
				Assert.IsTrue(Regex.IsMatch(s, "^[0-9A-F]$"));
				chars.Add(s[0]);
			}

			Assert.AreEqual(16, chars.Count, "Should have all 0-9 A-F");
		}

		[TestMethod]
		public void StringPatternGenerator_C_simple()
		{
			var sut = new PatternGenerator("{C}");
			Assert.AreEqual("1", sut.GetValue());
			Assert.AreEqual("2", sut.GetValue());
			Assert.AreEqual("3", sut.GetValue());
		}

		[TestMethod]
		public void StringPatternGenerator_C_with_StartValue()
		{
			var sut = new PatternGenerator("{C:33}");
			Assert.AreEqual("33", sut.GetValue());
			Assert.AreEqual("34", sut.GetValue());
			Assert.AreEqual("35", sut.GetValue());
		}

		[TestMethod]
		public void StringPatternGenerator_C_with_StartValue_with_Increment()
		{
			var sut = new PatternGenerator("{C:33,3}");
			Assert.AreEqual("33", sut.GetValue());
			Assert.AreEqual("36", sut.GetValue());
			Assert.AreEqual("39", sut.GetValue());
		}

		[TestMethod]
		public void StringPatternGenerator_C_combination()
		{
			var sut = new PatternGenerator("_{C}_{C:+11}_{C:110,10}_");
			Assert.AreEqual("_1_11_110_", sut.GetValue());
			Assert.AreEqual("_2_12_120_", sut.GetValue());
			Assert.AreEqual("_3_13_130_", sut.GetValue());
			Assert.AreEqual("_4_14_140_", sut.GetValue());
		}

		[TestMethod]
		public void StringPatternGenerator_C_startvalue_negative_value()
		{
			var sut = new PatternGenerator("{C:-3}");
			Assert.AreEqual("-3", sut.GetValue());
			Assert.AreEqual("-2", sut.GetValue());
			Assert.AreEqual("-1", sut.GetValue());
			Assert.AreEqual("0", sut.GetValue());
			Assert.AreEqual("1", sut.GetValue());
			Assert.AreEqual("2", sut.GetValue());
			Assert.AreEqual("3", sut.GetValue());
		}

		[TestMethod]
		public void StringPatternGenerator_C__startvalue_negative__positive_increment()
		{
			var sut = new PatternGenerator("{C:-3,+2}");
			Assert.AreEqual("-3", sut.GetValue());
			Assert.AreEqual("-1", sut.GetValue());
			Assert.AreEqual("1", sut.GetValue());
			Assert.AreEqual("3", sut.GetValue());
		}

		[TestMethod]
		public void StringPatternGenerator_C__startvalue_negative__negative_increment()
		{
			var sut = new PatternGenerator("{C:-3,-2}");
			Assert.AreEqual("-3", sut.GetValue());
			Assert.AreEqual("-5", sut.GetValue());
			Assert.AreEqual("-7", sut.GetValue());
		}

	}

	public sealed class ClassWithPrivateStuff
	{
		public int WithPrivateSetter { get; private set; }
		public int WithoutSetter { get { return 123; } }
	}

	public class BaseClass
	{
		public int NormalNumber { get; set; }
		public virtual int OverrideNormalNumber { get; set; }
		public virtual int SealedOverrideNormalNumber { get; set; }
	}

	public class InheritedClass : BaseClass
	{
		public override int OverrideNormalNumber { get; set; }
		public override sealed int SealedOverrideNormalNumber { get; set; }
	}

	public class WithArrays
	{
		public int[] Ints { get; set; }
		public string[] Strings { get; set; }
		public IDisposable[] Interfaces { get; set; }
	}
}
