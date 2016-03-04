namespace Tynamix.ObjectFiller
{
    using System;

    public class FloatRange : IRandomizerPlugin<float>, IRandomizerPlugin<float?>
    {
        private readonly float _minValue;
        private readonly float _maxValue;

        /// <summary>
        /// Use to define just a max value for the double randomizer. Min value will be 0!
        /// </summary>
        /// <param name="maxValue">Maximum double value</param>
        public FloatRange(float maxValue)
            : this(0, maxValue)
        {

        }

        /// <summary>
        /// Use to define a min and max double value for the randomizer
        /// </summary>
        /// <param name="minValue">Min value</param>
        /// <param name="maxValue">Max value</param>
        public FloatRange(float minValue, float maxValue)
        {
            this._minValue = minValue;
            this._maxValue = maxValue;
        }

        /// <summary>
        /// Use this to generate a double value between double.MinValue and double.MaxValue
        /// </summary>
        public FloatRange()
            : this(float.MinValue, float.MaxValue)
        {

        }

        public float GetValue()
        {
            var value = Random.NextDouble();
            
            return Convert.ToSingle(value) * (this._maxValue - this._minValue) + this._minValue;
        }

        float? IRandomizerPlugin<float?>.GetValue()
        {
            return this.GetValue();
        }
    }
}