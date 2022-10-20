using System;
using System.Collections.Generic;
using System.Text;

namespace TransformGraph.Contracts
{
    public interface IPredicateSet
    {
        /// <summary>
        /// Enumerate all of the predicates in this predicate set.
        /// </summary>
        IEnumerable<KeyValuePair<string, bool>> Predicates { get; }

        /// <summary>
        /// Get the value of a specific predicate in this predicate set.
        /// </summary>
        /// <param name="predicate">The predicate to get the value of.</param>
        /// <returns>The value of the predicate.</returns>
        /// <exception cref="IndexOutOfRangeException">
        /// The specified predicate is not part of this predicate set.
        /// </exception>
        bool this[string predicate] { get; }
    }
}
