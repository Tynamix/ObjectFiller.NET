using System;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// The double range plugin
    /// </summary>
    public class DoubleRange : IRandomizerPlugin<double>, IRandomizerPlugin<double?>, IRandomizerPlugin<decimal>, IRandomizerPlugin<decimal?>
    {
        /// <summary>
        /// The min value.
        /// </summary>
        private readonly double minValue;

        /// <summary>
        /// The max value.
        /// </summary>
        private readonly double maxValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleRange"/> class. 
        /// Use to define just a max value for the double randomizer. Min value will be 0!
        /// </summary>
        /// <param name="maxValue">
        /// Maximum double value
        /// </param>
        public DoubleRange(double maxValue)
            : this(0, maxValue)
        {

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleRange"/> class. 
        /// Use to define a min and max double value for the randomizer
        /// </summary>
        /// <param name="minValue">
        /// Min value
        /// </param>
        /// <param name="maxValue">
        /// Max value
        /// </param>
        public DoubleRange(double minValue, double maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleRange"/> class. 
        /// Use this to generate a double value between double.MinValue and double.MaxValue
        /// </summary>
        public DoubleRange()
            : this(-999999999, 999999999)
        {

        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        public double GetValue()
        {
            return Random.NextDouble() * Math.Abs(this.maxValue - this.minValue) + this.minValue;
        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        double? IRandomizerPlugin<double?>.GetValue()
        {
            return this.GetValue();
        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        decimal IRandomizerPlugin<decimal>.GetValue()
        {
            return (decimal)GetValue();
        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        decimal? IRandomizerPlugin<decimal?>.GetValue()
        {
            return (decimal)GetValue();
        }
    }
}
