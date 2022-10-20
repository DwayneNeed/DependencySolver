using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using TransformGraph.Contracts;

namespace TransformProvider.Reflection
{
    public class PredicateSet : IPredicateSet, IEquatable<PredicateSet>
    {
        private SortedDictionary<string, bool> predicates = new SortedDictionary<string, bool>();

        private int hashcode;

        /// <summary>
        /// Creates an new TransformInputPredicateSet instance.
        /// </summary>
        /// <param name="predicates">The predicates.</param>
        /// <remaraks>
        /// A PredicateSet is immutable, so all of the predicates must be passed to the
        /// constructor.
        /// </remaraks>
        protected PredicateSet(IEnumerable<KeyValuePair<string,bool>> predicates)
        {
            // A null predicate sequence is allowed, it just means empty.
            if (predicates != null)
            {
                foreach (var kvp in predicates)
                {
                    if (string.IsNullOrWhiteSpace(kvp.Key))
                    {
                        throw new ArgumentException(
                            "sequence contains an invalid predicate name", 
                            nameof(predicates));
                    }

                    if (this.predicates.ContainsKey(kvp.Key))
                    {
                        throw new ArgumentException(
                            "sequence contains a duplicate predicate name",
                            nameof(predicates));
                    }

                    this.predicates.Add(kvp.Key, kvp.Value);
                }
            }

            // Use the sorted dictionary order to calculate the hash code.
            HashCode hashcode = new HashCode();
            foreach (var kvp in this.predicates)
            {
                hashcode.Add(kvp.Key);
                hashcode.Add(kvp.Value);
            }
            this.hashcode = hashcode.ToHashCode();
        }

        /// <summary>
        /// Get the value of a specific predicate in this predicate set.
        /// </summary>
        /// <param name="predicate">The predicate to get the value of.</param>
        /// <returns>The value of the predicate.</returns>
        /// <exception cref="ArgumentNullException">
        /// A null predicate was specified
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// The specified predicate is not part of this predicate set.
        /// </exception>
        public bool this[string predicate]
        {
            get
            {
                if (predicate == null)
                {
                    throw new ArgumentNullException(nameof(predicate), "predicate cannot be null");
                }

                return this.predicates[predicate];
            }
        }

        /// <summary>
        /// Enumerate all of the predicates in this predicate set.
        /// </summary>
        public IEnumerable<KeyValuePair<string, bool>> Predicates
        {
            get
            {
                // We stream back each element, rather than just returning the internal dictionary
                // so that it can't be modified.
                foreach (var kvp in this.predicates)
                {
                    yield return kvp;
                }
            }
        }

        /// <summary>
        /// Returns whether or not the specified predicate is in the set.
        /// </summary>
        /// <param name="predicate">The predicate to check.</param>
        /// <returns>Whether or not the specified predicate is in the set.</returns>
        public bool Contains(string predicate)
        {
            if (predicate == null)
            {
                return false;
            }

            return this.predicates.ContainsKey(predicate);
        }

        public override bool Equals(object obj)
        {
            // never equal to null or another type
            if (!(obj is PredicateSet)) return false;

            return Equals(obj as PredicateSet);
        }

        public bool Equals([AllowNull] PredicateSet other)
        {
            // This instance is obviously never equal to null.
            if (other is null) return false;

            // This instance is obviously equal to the exact same object.
            if (Object.ReferenceEquals(this, other)) return true;

            // If the hashcodes are different, then they are obviously not equal.
            if (this.hashcode != other.hashcode) return false;

            // Even if the hashcodes are the same, we have to double check that the predicates
            // are the same for the (rare) case of hash collision.
            // Because we use a sorted dictionary, we can use sequence equality.
            return this.predicates.SequenceEqual(other.predicates);
        }

        public static bool operator ==(PredicateSet predicateSet1, PredicateSet predicateSet2)
        {
            // I guess we can handle (null == null)
            if (predicateSet1 is null)
            {
                return predicateSet2 is null;
            }
            else
            {
                // We know predicateSet1 is not null
                return predicateSet1.Equals(predicateSet2);
            }
        }

        public static bool operator !=(PredicateSet predicateSet1, PredicateSet predicateSet2)
        {
            return !(predicateSet1 == predicateSet2);
        }

        public override int GetHashCode() => this.hashcode;
    }
}
