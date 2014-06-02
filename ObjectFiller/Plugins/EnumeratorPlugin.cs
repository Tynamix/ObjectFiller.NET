using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Tynamix.ObjectFiller
{
    internal class EnumeratorPlugin<T> : IRandomizerPlugin<T>
    {
        private readonly IEnumerable<T> _enumerable;
        private IEnumerator<T> _enumerator;


        public EnumeratorPlugin(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
        }

        public T GetValue()
        {
            // First time?
            if (_enumerator == null)
            {
                _enumerator = _enumerable.GetEnumerator();
            }

            // Advance, try to recover if we hit end-of-enumeration
            var hasNext = _enumerator.MoveNext();
            if (!hasNext)
            {
                _enumerator = _enumerable.GetEnumerator();
                hasNext = _enumerator.MoveNext();

                if (!hasNext)
                {
	                string message = "Unable to get next value from enumeration " + _enumerable;
					Debug.WriteLine("ObjectFiller: " + message);
					throw new Exception(message);
                }
            }

            return _enumerator.Current;
        }
    }
}
