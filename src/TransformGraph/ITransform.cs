using System;
using System.Collections.Generic;
using System.Text;

namespace TransformGraph.Contracts
{
    public interface ITransform
    {
        /// <summary>
        /// The resource bindings for this transform.
        /// </summary>
        IEnumerable<ITransformResourceBinding> ResourceBindings { get; }
    }
}
