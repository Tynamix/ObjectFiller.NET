using System;

namespace Tynamix.ObjectFiller
{
    public class DoubleRange : IRandomizerPlugin<double>, IRandomizerPlugin<double?>,IRandomizerPlugin<decimal>, IRandomizerPlugin<decimal?>
    {
        private readonly double _minValue;
        private readonly double _maxValue;

        /// <summary>
        /// Use to define just a max value for the double randomizer. Min value will be 0!
        /// </summary>
        /// <param name="maxValue">Maximum double value</param>
        public DoubleRange(double maxValue)
            : this(0, maxValue)
        {

        }


        /// <summary>
        /// Use to define a min and max double value for the randomizer
        /// </summary>
        /// <param name="minValue">Min value</param>
        /// <param name="maxValue">Max value</param>
        public DoubleRange(double minValue, double maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
        }

        /// <summary>
        /// Use this to generate a double value between double.MinValue and double.MaxValue
        /// </summary>
        public DoubleRange()
            : this(double.MinValue, double.MaxValue)
        {

        }

        public double GetValue()
        {
            return Random.NextDouble() * (_maxValue - _minValue) + _minValue;
        }

        double? IRandomizerPlugin<double?>.GetValue()
        {
            return GetValue();
        }

        decimal IRandomizerPlugin<decimal>.GetValue()
        {
            return (decimal) GetValue();
        }

        decimal? IRandomizerPlugin<decimal?>.GetValue()
        {
            return (decimal)GetValue();
        }
    }
}
