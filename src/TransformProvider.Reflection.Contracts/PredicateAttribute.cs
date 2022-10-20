using System;
using System.Collections.Generic;
using System.Text;

namespace TransformProvider.Reflection.Contracts
{
    /// <summary>
    /// Applied to properties on a resource to indicate the property can be used as a predicate.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PredicateAttribute : Attribute
    {
        public PredicateAttribute()
        {
        }
    }
}
