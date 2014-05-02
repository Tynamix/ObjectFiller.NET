namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// Class wraps the <see cref="System.Random"/> class. 
    /// This is to have a static instance of the random class
    /// </summary>
    internal static class Random
    {
        private static readonly System.Random Rnd;

        static Random()
        {

            Rnd = new System.Random();
        }

        internal static int Next()
        {
            return Rnd.Next();
        }

        internal static int Next(int maxValue)
        {
            return Rnd.Next(maxValue);
        }

        internal static int Next(int minValue, int maxValue)
        {
            return Rnd.Next(minValue, maxValue);
        }

        internal static double NextDouble()
        {
            return Rnd.NextDouble();
        }

        internal static void NextByte(byte[] buffer)
        {
            Rnd.NextBytes(buffer);
        }

    }
}