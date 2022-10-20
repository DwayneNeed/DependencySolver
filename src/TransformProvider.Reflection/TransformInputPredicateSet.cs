using System;
using System.Collections.Generic;
using System.Text;
using TransformGraph.Contracts;

namespace TransformProvider.Reflection
{
    public class TransformInputPredicateSet : PredicateSet, ITransformInputPredicateSet
    {
        /// <summary>
        /// Creates an new TransformInputPredicateSet instance.
        /// </summary>
        /// <param name="predicates">The predicates.</param>
        /// <remarks>
        /// A TransformInputPredicateSet is immutable, so all of the predicates must be passed to
        /// the constructor.
        /// </remarks>
        protected TransformInputPredicateSet(IEnumerable<KeyValuePair<string, bool>> predicates) :
            base(predicates)
        {
        }
    }
}
