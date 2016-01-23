namespace Tynamix.ObjectFiller
{
    public class IntRange : IRandomizerPlugin<int>, IRandomizerPlugin<int?>
    {
        private readonly int _min;
        private readonly int _max;

        public IntRange(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public IntRange(int max)
            : this(0, max)
        {

        }

        public int GetValue()
        {
            return Random.Next(_min, _max);
        }

        int? IRandomizerPlugin<int?>.GetValue()
        {
            return GetValue();
        }
    }
}
