// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HashStack.cs" company="Tynamix">
//   © by GothicX
// </copyright>
// <summary>
//   A stack-like collection that uses a HashSet under the covers, so elements also need to be unique.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tynamix.ObjectFiller
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A stack-like collection that uses a HashSet under the covers, so elements also need to be unique.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the  Hashstack
    /// </typeparam>
    /// <remarks>
    /// I decided to follow the HashSet more closely than the stack, 
    /// which is why Push returns a boolean. I still think throwing an exception makes more
    /// sense, but if HashSet does it people should be already familiar with the paradigm.
    /// Pop however follows the standard stack implementation, of course.
    /// </remarks>
    internal class HashStack<T> : IEnumerable<T>
    {
        /// <summary>
        /// The _set.
        /// </summary>
        private readonly HashSet<T> set;

        private readonly Stack<T> stack;

        /// <summary>
        /// Initializes a new instance of the <see cref="HashStack{T}"/> class.
        /// </summary>
        internal HashStack()
            : this(EqualityComparer<T>.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashStack{T}"/> class.
        /// </summary>
        /// <param name="comparer">
        /// The comparer.
        /// </param>
        internal HashStack(IEqualityComparer<T> comparer)
        {
            this.set = new HashSet<T>(comparer);
            this.stack = new Stack<T>();
        }

        /// <summary>
        /// Adds the specified element to the HashStack.
        /// </summary>
        /// <returns>True if the element is added; false if the element is already present.</returns>
        internal bool Push(T item)
        {
            var added = this.set.Add(item);
            if (added)
            {
                this.stack.Push(item);
            }

            return added;
        }

        /// <summary>
        /// Removes and returns the last added element.
        /// </summary>
        /// <returns>
        /// The item of type <see cref="T"/>
        /// </returns>
        internal T Pop()
        {
            var last = this.stack.Pop();
            this.set.Remove(last);
            return last;
        }

        /// <summary>
        /// Clears the hash stack
        /// </summary>
        internal void Clear()
        {
            this.set.Clear();
            this.stack.Clear();
        }

        /// <summary>
        /// Checks if the <see cref="HashStack{T}"/> contains the <see cref="item"/>
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// True if the <see cref="HashStack{T}"/> contains the item
        /// </returns>
        internal bool Contains(T item)
        {
            return this.set.Contains(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator()
        {
            return this.stack.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}