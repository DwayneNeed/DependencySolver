using System;
using System.Collections.Generic;
using System.Text;

namespace TransformGraph.Contracts
{
    public interface IResourcePredicateSet<TResource> : IPredicateSet
        where TResource : class
    {
        /// <summary>
        /// Checks if this predicate set matches the input predicates for a transform.
        /// </summary>
        /// <param name="predicateSet">The input predicates for a transform.</param>
        /// <returns>
        /// Whether or not this predicate set matches the input predicates for a transform.
        /// </returns>
        bool Matches(ITransformInputPredicateSet predicateSet);

        /// <summary>
        /// Applies the output predicates for a transform to this predicate set to produce a
        /// new predicate set.
        /// </summary>
        /// <param name="predicateSet">The output predicates for a transform.</param>
        /// <returns>
        /// A new predicate set that combines the output predicates of a transform with this
        /// predicate set.
        /// </returns>
        IResourcePredicateSet<TResource> Apply(ITransformOutputPredicateSet predicateSet);

        /// <summary>
        /// Verifies that the actual state of a resource instance matches the expectations
        /// expressed in this predicate set.
        /// </summary>
        /// <param name="resource">The resource instance to verify.</param>
        void VerifyPredicates(TResource resource);
    }
}
