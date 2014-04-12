using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ObjectFiller.FillerPlugins
{
    public class RandomListItem<T> : IRandomizerPlugin<T>
    {
        private readonly List<T> _allAvailableValues;
        private readonly Random _rndm;

        public RandomListItem(List<T> allAvailableValues)
        {
            if (allAvailableValues == null || allAvailableValues.Count == 0)
            {
                throw new ArgumentException("List in RandomListItem ranomizer can not be empty!");
            }
            _allAvailableValues = allAvailableValues;
            _rndm = new Random();
        }

        public T GetValue()
        {
            int rndmListIndex = _rndm.Next(0, _allAvailableValues.Count);
            return _allAvailableValues[rndmListIndex];
        }
    }
}
