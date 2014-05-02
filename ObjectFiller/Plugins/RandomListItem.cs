using System;
using System.Collections.Generic;

namespace Tynamix.ObjectFiller
{
    public class RandomListItem<T> : IRandomizerPlugin<T>
    {
        private readonly List<T> _allAvailableValues;

        public RandomListItem(List<T> allAvailableValues)
        {
            if (allAvailableValues == null || allAvailableValues.Count == 0)
            {
                throw new ArgumentException("List in RandomListItem ranomizer can not be empty!");
            }
            _allAvailableValues = allAvailableValues;

        }

        public T GetValue()
        {
            int rndmListIndex = Random.Next(0, _allAvailableValues.Count);
            return _allAvailableValues[rndmListIndex];
        }
    }
}
