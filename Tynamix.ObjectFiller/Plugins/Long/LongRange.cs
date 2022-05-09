namespace Tynamix.ObjectFiller
{
	using System;

	/// <summary>
	/// The 64-integer range plugin
	/// </summary>
	public class LongRange : IRandomizerPlugin<long>, IRandomizerPlugin<long?>
	{
		/// <summary>
		/// The min value
		/// </summary>
		private readonly long min;

		/// <summary>
		/// The max value
		/// </summary>
		private readonly long max;

		/// <summary>
		/// Initializes a new instance of the <see cref="LongRange"/> class.
		/// </summary>
		/// <param name="min">
		/// The min value
		/// </param>
		/// <param name="max">
		/// The max value
		/// </param>
		public LongRange(long min, long max)
		{
			this.min = min;
			this.max = max;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LongRange"/> class.
		/// </summary>
		/// <param name="max">
		/// The max.
		/// </param>
		public LongRange(long max)
			: this(long.MinValue, max)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LongRange"/> class.
		/// </summary>
		public LongRange()
			: this(long.MinValue, long.MaxValue)
		{
		}

		/// <summary>
		/// Gets random data for type <see cref="long"/>
		/// </summary>
		/// <returns>Random data for type <see cref="long"/></returns>
		public long GetValue()
		{
			return Random.NextLong(this.min, this.max);
		}


		/// <summary>
		/// Gets random data for type <see cref="Nullable{Int64}"/>
		/// </summary>
		/// <returns>Random data for type <see cref="Nullable{Int64}"/></returns>
		long? IRandomizerPlugin<long?>.GetValue()
		{
			return this.GetValue();
		}
	}
}