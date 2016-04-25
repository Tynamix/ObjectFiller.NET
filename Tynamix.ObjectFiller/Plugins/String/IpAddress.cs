namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// Generates an IP address
    /// </summary>
    public class IpAddress : IRandomizerPlugin<string>
    {
        /// <summary>
        /// The maximum of the first IP part (max 255)
        /// </summary>
        private int firstIpPartMax;

        /// <summary>
        /// The maximum of the second IP part (max 255)
        /// </summary>
        private int secondIpPartMax;

        /// <summary>
        /// The maximum of the third IP part (max 255)
        /// </summary>
        private int thirdIpPartMax;

        /// <summary>
        /// The maximum of the last IP part (max 255)
        /// </summary>
        private int lastIpPartMax;

        /// <summary>
        /// Initializes a new instance of the <see cref="IpAddress"/> class.
        /// </summary>
        public IpAddress()
            : this(255, 255, 255, 255)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IpAddress"/> class.
        /// </summary>
        /// <param name="firstIpPartMax">
        /// The maximum of the first IP part (max 255)
        /// </param>
        public IpAddress(uint firstIpPartMax)
            : this(firstIpPartMax, 255, 255, 255)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IpAddress"/> class.
        /// </summary>
        /// <param name="firstIpPartMax">
        /// The maximum of the first IP part (max 255)
        /// </param>
        /// <param name="secondIpPartMax">
        /// The maximum of the second IP part (max 255)
        /// </param>
        public IpAddress(
             uint firstIpPartMax,
             uint secondIpPartMax)
            : this(firstIpPartMax, secondIpPartMax, 255, 255)

        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IpAddress"/> class.
        /// </summary>
        /// <param name="firstIpPartMax">
        /// The maximum of the first IP part (max 255)
        /// </param>
        /// <param name="secondIpPartMax">
        /// The maximum of the second IP part (max 255)
        /// </param>
        /// <param name="thirdIpPartMax">
        /// The maximum of the third IP part (max 255)
        /// </param>
        public IpAddress(
            uint firstIpPartMax,
            uint secondIpPartMax,
            uint thirdIpPartMax)
            : this(firstIpPartMax, secondIpPartMax, thirdIpPartMax, 255)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IpAddress"/> class.
        /// </summary>
        /// <param name="firstIpPartMax">
        /// The maximum of the first IP part (max 255)
        /// </param>
        /// <param name="secondIpPartMax">
        /// The maximum of the second IP part (max 255)
        /// </param>
        /// <param name="thirdIpPartMax">
        /// The maximum of the third IP part (max 255)
        /// </param>
        /// <param name="lastIpPartMax">
        /// The maximum of the last IP part (max 255)
        /// </param>
        public IpAddress(
            uint firstIpPartMax,
            uint secondIpPartMax,
            uint thirdIpPartMax,
            uint lastIpPartMax)
        {
            this.firstIpPartMax = (int)(firstIpPartMax > 255 ? 255 : firstIpPartMax);
            this.secondIpPartMax = (int)(secondIpPartMax > 255 ? 255 : secondIpPartMax);
            this.thirdIpPartMax = (int)(thirdIpPartMax > 255 ? 255 : thirdIpPartMax);
            this.lastIpPartMax = (int)(lastIpPartMax > 255 ? 255 : lastIpPartMax);
        }

        /// <summary>
        /// Gets random IP address
        /// </summary>
        /// <returns>An random IP address</returns>
        public string GetValue()
        {
            int firstSegment = Randomizer<int>.Create(new IntRange(1, this.firstIpPartMax));
            int secondSegment = Randomizer<int>.Create(new IntRange(1, this.secondIpPartMax));
            int thirdSegment = Randomizer<int>.Create(new IntRange(1, this.thirdIpPartMax));
            int lastSegment = Randomizer<int>.Create(new IntRange(1, this.lastIpPartMax));

            return string.Format("{0}.{1}.{2}.{3}", firstSegment, secondSegment, thirdSegment, lastSegment);
        }
    }
}
