using System.Collections;
using System.Collections.Generic;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// The collectionizer can be used to create <see cref="List{T}"/> based on a <see cref="IRandomizerPlugin{T}"/>.
    /// </summary>
    /// <typeparam name="T">Typeparameter of of the target List</typeparam>
    /// <typeparam name="TRandomizer">Plugin which will be used to create the List</typeparam>
    public class Collectionizer<T, TRandomizer> : IRandomizerPlugin<List<T>>, IRandomizerPlugin<T[]>
#if !NETSTANDARD1_0
        , IRandomizerPlugin<ArrayList>
#endif
        where TRandomizer : IRandomizerPlugin<T>, new()
    {
        private readonly IRandomizerPlugin<T> randomizerToUse;
        private readonly int minCount;
        private readonly int maxCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Collectionizer{T,TRandomizer}"/> class.
        /// </summary>
        public Collectionizer()
            : this(new TRandomizer())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Collectionizer{T,TRandomizer}"/> class.
        /// </summary>
        /// <param name="minCount">Min count of list items</param>
        public Collectionizer(uint minCount)
            : this(new TRandomizer(), minCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Collectionizer{T,TRandomizer}"/> class.
        /// </summary>
        /// <param name="minCount">Min count of list items</param>
        /// <param name="maxCount">Max count of list items</param>
        public Collectionizer(uint minCount, uint maxCount)
            : this(new TRandomizer(), minCount, maxCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Collectionizer{T,TRandomizer}"/> class.
        /// </summary>
        /// <param name="randomizerToUse">The randomizer which will be used. Use this if you want
        /// to specifiy how the randomizer to use should work</param>
        public Collectionizer(TRandomizer randomizerToUse)
            : this(randomizerToUse, 10)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Collectionizer{T,TRandomizer}"/> class.
        /// </summary>
        /// <param name="randomizerToUse">The randomizer which will be used. Use this if you want
        /// to specifiy how the randomizer to use should work</param>
        /// <param name="minCount">Min count of list items</param>
        public Collectionizer(TRandomizer randomizerToUse, uint minCount)
            : this(randomizerToUse, minCount, 50)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Collectionizer{T,TRandomizer}"/> class.
        /// </summary>
        /// <param name="randomizerToUse">The randomizer which will be used. Use this if you want
        /// to specifiy how the randomizer to use should work</param>
        /// <param name="minCount">Min count of list items</param>
        /// <param name="maxCount">Max count of list items</param>
        public Collectionizer(TRandomizer randomizerToUse, uint minCount, uint maxCount)
        {
            if (randomizerToUse == null)
            {
                randomizerToUse = new TRandomizer();
            }

            if (minCount > maxCount)
            {
                var temp = maxCount;
                maxCount = minCount;
                minCount = temp;
            }

            this.randomizerToUse = randomizerToUse;
            this.minCount = (int)minCount;
            this.maxCount = (int)maxCount;
        }


        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        public List<T> GetValue()
        {
            var count = Randomizer<int>.Create(new IntRange(this.minCount, this.maxCount));
            List<T> result = new List<T>();

            for (int i = 0; i < count; i++)
            {
                result.Add(this.randomizerToUse.GetValue());
            }

            return result;
        }

        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        T[] IRandomizerPlugin<T[]>.GetValue()
        {
            return this.GetValue().ToArray();
        }

#if !NETSTANDARD1_0
        /// <summary>
        /// Gets random data for type <see cref="T"/>
        /// </summary>
        /// <returns>Random data for type <see cref="T"/></returns>
        ArrayList IRandomizerPlugin<ArrayList>.GetValue()
        {
            ArrayList arrayList = new ArrayList();
            arrayList.AddRange(this.GetValue());

            return arrayList;
        }
#endif

    }

}
