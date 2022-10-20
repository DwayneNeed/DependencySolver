using System;
using System.Collections.Generic;
using System.Reflection;
using TransformGraph.Contracts;
using TransformProvider.Reflection.Contracts;

namespace TransformProvider.Reflection
{
    public class TransformProvider : ITransformProvider
    {
        private ITypeProvider typeProvider;

        public TransformProvider(ITypeProvider typeProvider)
        {
            this.typeProvider = typeProvider;
        }

        public IEnumerable<ITransform> Transforms
        {
            get
            {
                return null;
            }
        }
    }
}
