using System;
using System.Collections.Generic;
using System.Text;

namespace TransformProvider.Reflection.Contracts
{
    /// <summary>
    /// Applied to classes to indicate that it can be used as a transfrom.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TransformAttribute : Attribute
    {
        public TransformAttribute()
        {
        }
    }
}
