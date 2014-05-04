using System;

// ReSharper disable CheckNamespace
// ReSharper disable RedundantCast ## intentional, allows a simple copy&paste of the classes
namespace Tynamix.ObjectFiller
{
	#region SequenceGeneratorSByte

	public class SequenceGeneratorSByte : IRandomizerPlugin<SByte>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 0 (zero) will be used as initial value.
		/// </summary>
		public SByte? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// For example, using From=1 and To=3 will generate a sequence like [1,2,3,1,2,3,1...]<para/>
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public SByte? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.
		/// For example, using From=1 and Step=2 will generate a sequence like [1,3,5,7,...]<para/>
		/// If this property is not set then a default value of 1 (one) will be used.
		/// </summary>
		public SByte? Step { get; set; }

		private SByte? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public SByte GetValue()
		{
			SByte nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (SByte) (LastValue.Value + Step.GetValueOrDefault(1));

			if (To != null)
			{
				if (((Step.GetValueOrDefault(1) > 0) && (nextValue > To)) ||
				    ((Step.GetValueOrDefault(1) < 0) && (nextValue < To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorInt16

	public class SequenceGeneratorInt16 : IRandomizerPlugin<Int16>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 0 (zero) will be used as initial value.
		/// </summary>
		public Int16? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// For example, using From=1 and To=3 will generate a sequence like [1,2,3,1,2,3,1...]<para/>
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public Int16? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.
		/// For example, using From=1 and Step=2 will generate a sequence like [1,3,5,7,...]<para/>
		/// If this property is not set then a default value of 1 (one) will be used.
		/// </summary>
		public Int16? Step { get; set; }

		private Int16? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public Int16 GetValue()
		{
			Int16 nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (Int16) (LastValue.Value + Step.GetValueOrDefault(1));

			if (To != null)
			{
				if (((Step.GetValueOrDefault(1) > 0) && (nextValue > To)) ||
				    ((Step.GetValueOrDefault(1) < 0) && (nextValue < To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorInt32

	public class SequenceGeneratorInt32 : IRandomizerPlugin<Int32>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 0 (zero) will be used as initial value.
		/// </summary>
		public Int32? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// For example, using From=1 and To=3 will generate a sequence like [1,2,3,1,2,3,1...]<para/>
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public Int32? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.
		/// For example, using From=1 and Step=2 will generate a sequence like [1,3,5,7,...]<para/>
		/// If this property is not set then a default value of 1 (one) will be used.
		/// </summary>
		public Int32? Step { get; set; }

		private Int32? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public Int32 GetValue()
		{
			Int32 nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (Int32) (LastValue.Value + Step.GetValueOrDefault(1));

			if (To != null)
			{
				if (((Step.GetValueOrDefault(1) > 0) && (nextValue > To)) ||
				    ((Step.GetValueOrDefault(1) < 0) && (nextValue < To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorInt64

	public class SequenceGeneratorInt64 : IRandomizerPlugin<Int64>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 0 (zero) will be used as initial value.
		/// </summary>
		public Int64? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// For example, using From=1 and To=3 will generate a sequence like [1,2,3,1,2,3,1...]<para/>
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public Int64? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.
		/// For example, using From=1 and Step=2 will generate a sequence like [1,3,5,7,...]<para/>
		/// If this property is not set then a default value of 1 (one) will be used.
		/// </summary>
		public Int64? Step { get; set; }

		private Int64? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public Int64 GetValue()
		{
			Int64 nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (Int64) (LastValue.Value + Step.GetValueOrDefault(1));

			if (To != null)
			{
				if (((Step.GetValueOrDefault(1) > 0) && (nextValue > To)) ||
				    ((Step.GetValueOrDefault(1) < 0) && (nextValue < To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorByte

	public class SequenceGeneratorByte : IRandomizerPlugin<Byte>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 0 (zero) will be used as initial value.
		/// </summary>
		public Byte? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// For example, using From=1 and To=3 will generate a sequence like [1,2,3,1,2,3,1...]<para/>
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public Byte? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.
		/// For example, using From=1 and Step=2 will generate a sequence like [1,3,5,7,...]<para/>
		/// If this property is not set then a default value of 1 (one) will be used.
		/// </summary>
		public Byte? Step { get; set; }

		private Byte? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public Byte GetValue()
		{
			Byte nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (Byte)(LastValue.Value + Step.GetValueOrDefault(1));

			if (To != null)
			{
				if (((Step.GetValueOrDefault(1) > 0) && (nextValue > To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorChar

	public class SequenceGeneratorChar : IRandomizerPlugin<Char>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 0 (zero) will be used as initial value.
		/// </summary>
		public Char? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// For example, using From=1 and To=3 will generate a sequence like [1,2,3,1,2,3,1...]<para/>
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public Char? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.
		/// For example, using From=1 and Step=2 will generate a sequence like [1,3,5,7,...]<para/>
		/// If this property is not set then a default value of 1 (one) will be used.
		/// </summary>
		public Char? Step { get; set; }

		private Char? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public Char GetValue()
		{
			Char nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (Char)(LastValue.Value + Step.GetValueOrDefault((Char)1));

			if (To != null)
			{
				if (((Step.GetValueOrDefault((Char)1) > 0) && (nextValue > To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorUInt16

	public class SequenceGeneratorUInt16 : IRandomizerPlugin<UInt16>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 0 (zero) will be used as initial value.
		/// </summary>
		public UInt16? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// For example, using From=1 and To=3 will generate a sequence like [1,2,3,1,2,3,1...]<para/>
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public UInt16? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.
		/// For example, using From=1 and Step=2 will generate a sequence like [1,3,5,7,...]<para/>
		/// If this property is not set then a default value of 1 (one) will be used.
		/// </summary>
		public UInt16? Step { get; set; }

		private UInt16? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public UInt16 GetValue()
		{
			UInt16 nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (UInt16) (LastValue.Value + Step.GetValueOrDefault(1));

			if (To != null)
			{
				if (((Step.GetValueOrDefault(1) > 0) && (nextValue > To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorUInt32

	public class SequenceGeneratorUInt32 : IRandomizerPlugin<UInt32>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 0 (zero) will be used as initial value.
		/// </summary>
		public UInt32? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// For example, using From=1 and To=3 will generate a sequence like [1,2,3,1,2,3,1...]<para/>
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public UInt32? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.
		/// For example, using From=1 and Step=2 will generate a sequence like [1,3,5,7,...]<para/>
		/// If this property is not set then a default value of 1 (one) will be used.
		/// </summary>
		public UInt32? Step { get; set; }

		private UInt32? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public UInt32 GetValue()
		{
			UInt32 nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (UInt32) (LastValue.Value + Step.GetValueOrDefault(1));

			if (To != null)
			{
				if (((Step.GetValueOrDefault(1) > 0) && (nextValue > To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorUInt64

	public class SequenceGeneratorUInt64 : IRandomizerPlugin<UInt64>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 0 (zero) will be used as initial value.
		/// </summary>
		public UInt64? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// For example, using From=1 and To=3 will generate a sequence like [1,2,3,1,2,3,1...]<para/>
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public UInt64? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.
		/// For example, using From=1 and Step=2 will generate a sequence like [1,3,5,7,...]<para/>
		/// If this property is not set then a default value of 1 (one) will be used.
		/// </summary>
		public UInt64? Step { get; set; }

		private UInt64? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public UInt64 GetValue()
		{
			UInt64 nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (UInt64) (LastValue.Value + Step.GetValueOrDefault(1));

			if (To != null)
			{
				if (((Step.GetValueOrDefault(1) > 0) && (nextValue > To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorSingle

	public class SequenceGeneratorSingle : IRandomizerPlugin<Single>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 0 (zero) will be used as initial value.
		/// </summary>
		public Single? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// For example, using From=1 and To=3 will generate a sequence like [1,2,3,1,2,3,1...]<para/>
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public Single? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.
		/// For example, using From=1 and Step=2 will generate a sequence like [1,3,5,7,...]<para/>
		/// If this property is not set then a default value of 1 (one) will be used.
		/// </summary>
		public Single? Step { get; set; }

		private Single? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public Single GetValue()
		{
			Single nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (Single)(LastValue.Value + Step.GetValueOrDefault(1));

			if (To != null)
			{
				if (((Step.GetValueOrDefault(1) > 0) && (nextValue > To)) ||
					((Step.GetValueOrDefault(1) < 0) && (nextValue < To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorDouble

	public class SequenceGeneratorDouble : IRandomizerPlugin<Double>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 0 (zero) will be used as initial value.
		/// </summary>
		public Double? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// For example, using From=1 and To=3 will generate a sequence like [1,2,3,1,2,3,1...]<para/>
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public Double? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.
		/// For example, using From=1 and Step=2 will generate a sequence like [1,3,5,7,...]<para/>
		/// If this property is not set then a default value of 1 (one) will be used.
		/// </summary>
		public Double? Step { get; set; }

		private Double? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public Double GetValue()
		{
			Double nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (Double)(LastValue.Value + Step.GetValueOrDefault(1));

			if (To != null)
			{
				if (((Step.GetValueOrDefault(1) > 0) && (nextValue > To)) ||
					((Step.GetValueOrDefault(1) < 0) && (nextValue < To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorDecimal

	public class SequenceGeneratorDecimal : IRandomizerPlugin<Decimal>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 0 (zero) will be used as initial value.
		/// </summary>
		public Decimal? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// For example, using From=1 and To=3 will generate a sequence like [1,2,3,1,2,3,1...]<para/>
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public Decimal? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.
		/// For example, using From=1 and Step=2 will generate a sequence like [1,3,5,7,...]<para/>
		/// If this property is not set then a default value of 1 (one) will be used.
		/// </summary>
		public Decimal? Step { get; set; }

		private Decimal? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public Decimal GetValue()
		{
			Decimal nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (Decimal)(LastValue.Value + Step.GetValueOrDefault(1));

			if (To != null)
			{
				if (((Step.GetValueOrDefault(1) > 0) && (nextValue > To)) ||
					((Step.GetValueOrDefault(1) < 0) && (nextValue < To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorBoolean

	public class SequenceGeneratorBoolean : IRandomizerPlugin<Boolean>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then 'false' will be used as initial value.
		/// </summary>
		public Boolean? From { get; set; }

		private Boolean? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public Boolean GetValue()
		{
			Boolean nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: !LastValue.Value;

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion

	#region SequenceGeneratorDateTime

	public class SequenceGeneratorDateTime : IRandomizerPlugin<DateTime>
	{
		/// <summary>
		/// The initial value at which a sequence should start.<para/>
		/// If this property is not set then the default date will be used as initial value.
		/// </summary>
		public DateTime? From { get; set; }

		/// <summary>
		/// The max value until which the sequence should continue before it will wrap around.
		/// If this property is not set then the sequence will not wrap, unless it reaches the limit of your datatype.
		/// </summary>
		public DateTime? To { get; set; }

		/// <summary>
		/// The step value which sould be used when generating the sequence.<para/>
		/// If this property is not set then a default value of 1 (one) day will be used.
		/// </summary>
		public TimeSpan? Step { get; set; }

		private DateTime? LastValue { get; set; }

		/// <summary>
		/// Gets the next value of the sequence.
		/// </summary>
		public DateTime GetValue()
		{
			DateTime nextValue = (LastValue == null)
				? From.GetValueOrDefault()
				: (DateTime)(LastValue.Value + Step.GetValueOrDefault(TimeSpan.FromDays(1)));

			if (To != null)
			{
				if (((Step.GetValueOrDefault(TimeSpan.FromDays(1)).TotalMilliseconds > 0) && (nextValue > To)) ||
					((Step.GetValueOrDefault(TimeSpan.FromDays(1)).TotalMilliseconds < 0) && (nextValue < To)))
				{
					nextValue = From.GetValueOrDefault();
				}
			}

			LastValue = nextValue;
			return nextValue;
		}
	}

	#endregion
}