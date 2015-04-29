using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Tynamix.ObjectFiller
{
    public class RandomListItem<T> : IRandomizerPlugin<T>
    {
        private readonly T[] _allAvailableValues;

        public RandomListItem(params T[] allAvailableValues)
        {
            if (allAvailableValues == null || allAvailableValues.Length == 0)
            {
                const string message = "List in RandomListItem ranomizer can not be empty!";
                Debug.WriteLine("ObjectFiller: " + message);
                throw new ArgumentException(message);
            }
            _allAvailableValues = allAvailableValues;
        }

        public RandomListItem(IEnumerable<T> allAvailableValues)
            : this(allAvailableValues.ToArray())
        {

        }

        public T GetValue()
        {
            int rndmListIndex = Random.Next(0, _allAvailableValues.Length);
            return _allAvailableValues[rndmListIndex];
        }
    }
}
