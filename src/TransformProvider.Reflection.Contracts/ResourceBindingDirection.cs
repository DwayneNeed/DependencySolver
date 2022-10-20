using System;
using System.Collections.Generic;
using System.Text;

namespace TransformProvider.Reflection.Contracts
{
    [Flags]
    public enum ResourceBindingDirection
    {
        NotBound = 0,
        Input = 1,
        Output = 2
    }
}
