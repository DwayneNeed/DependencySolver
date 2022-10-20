using System;
using System.Collections.Generic;
using System.Text;

namespace TransformGraph.Contracts
{
    public interface ITransformProvider
    {
        /// <summary>
        /// The transforms provided by this provider.
        /// </summary>
        IEnumerable<ITransform> Transforms { get; }
    }
}
