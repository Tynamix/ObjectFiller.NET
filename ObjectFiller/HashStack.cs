using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tynamix.ObjectFiller
{
    /// <summary>
    /// A stack-like collection that uses a HashSet under the covers, so elements also need to be unique.
    /// </summary>
    /// <remarks>
    /// I decided to follow the HashSet more closely than the stack, 
    /// which is why Push returns a boolean. I still think throwing an exception makes more
    /// sense, but if HashSet does it people should be already familiar with the paradigm.
    /// 
    /// Pop however follows the standard stack implementation, of course.
    /// </remarks>
    public class HashStack<T> : IEnumerable<T>
    {
        private readonly HashSet<T> _set;
        private readonly Stack<T> _stack;

        public HashStack() : this(EqualityComparer<T>.Default)
        {
        }

        public HashStack(IEqualityComparer<T> comparer)
        {
            _set = new HashSet<T>(comparer);
            _stack = new Stack<T>();
        }

        /// <summary>
        /// Adds the specified element to the HashStack.
        /// </summary>
        /// <returns>True if the element is added; false if the element is already present.</returns>
        public bool Push(T item)
        {
            var added = _set.Add(item);
            if (added) _stack.Push(item);
            return added;
        }

        /// <summary>
        /// Removes and returns the last added element.
        /// </summary>
        public T Pop()
        {
            var last = _stack.Pop();
            _set.Remove(last);
            return last;
        }

        public void Clear()
        {
            _set.Clear();
            _stack.Clear();
        }

        public bool Contains(T item)
        {
            return _set.Contains(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _stack.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}