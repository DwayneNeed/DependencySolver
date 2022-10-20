using System;
using System.Collections.Generic;
using System.Text;

namespace TransformProvider.Reflection.Contracts
{
    /// <summary>
    /// Applied to classes to indicate that it can be used as a resource.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ResourceAttribute : Attribute
    {
        public ResourceAttribute()
        {
        }
    }
}
