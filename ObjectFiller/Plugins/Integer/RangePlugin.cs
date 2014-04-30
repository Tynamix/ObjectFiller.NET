namespace Tynamix.ObjectFiller
{
    public class Range : IRandomizerPlugin<int>
    {
        private readonly int _min;
        private readonly int _max;

        public Range(int min, int max)
        {
            _min = min;
            _max = max;
        }

        public Range(int max)
            : this(0, max)
        {

        }

        public int GetValue()
        {
            return Random.Next(_min, _max);
        }
    }
}