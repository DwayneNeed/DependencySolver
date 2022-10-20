using System;
using System.Collections.Generic;
using System.Text;

namespace TransformProvider.Reflection
{
    public interface ITypeProvider
    {
        IEnumerable<Type> Types { get; }
    }
}
