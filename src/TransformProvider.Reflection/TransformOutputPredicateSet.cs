using System;
using System.Collections.Generic;
using System.Text;
using TransformGraph.Contracts;

namespace TransformProvider.Reflection
{
    public class TransformOutputPredicateSet : PredicateSet, ITransformOutputPredicateSet
    {
        /// <summary>
        /// Creates an new TransformOutputPredicateSet instance.
        /// </summary>
        /// <param name="predicates">The predicates.</param>
        /// <remarks>
        /// A TransformOutputPredicateSet is immutable, so all of the predicates must be passed to
        /// the constructor.
        /// </remarks>
        protected TransformOutputPredicateSet(IEnumerable<KeyValuePair<string, bool>> predicates) :
            base(predicates)
        {
        }
    }
}
