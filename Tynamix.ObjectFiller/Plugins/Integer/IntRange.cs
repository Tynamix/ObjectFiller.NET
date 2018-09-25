namespace Tynamix.ObjectFiller
{
    using System;

    /// <summary>
    /// The integer range plugin
    /// </summary>
    public class IntRange : IRandomizerPlugin<int>, IRandomizerPlugin<int?>
    {
        /// <summary>
        /// The min value
        /// </summary>
        private readonly int min;

        /// <summary>
        /// The max value
        /// </summary>
        private readonly int max;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntRange"/> class.
        /// </summary>
        /// <param name="min">
        /// The min value
        /// </param>
        /// <param name="max">
        /// The max value
        /// </param>
        public IntRange(int min, int max)
        {
            this.min = min;
            this.max = max;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntRange"/> class.
        /// </summary>
        /// <param name="max">
        /// The max.
        /// </param>
        public IntRange(int max)
            : this(int.MinValue, max)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IntRange"/> class.
        /// </summary>
        public IntRange()
            : this(int.MinValue, int.MaxValue)
        {
        }

        /// <summary>
        /// Gets random data for type <see cref="int"/>
        /// </summary>
        /// <returns>Random data for type <see cref="int"/></returns>
        public int GetValue()
        {
            return Random.Next(this.min, this.max);
        }


        /// <summary>
        /// Gets random data for type <see cref="Nullable{Int32}"/>
        /// </summary>
        /// <returns>Random data for type <see cref="Nullable{Int32}"/></returns>
        int? IRandomizerPlugin<int?>.GetValue()
        {
            return this.GetValue();
        }
    }
}
