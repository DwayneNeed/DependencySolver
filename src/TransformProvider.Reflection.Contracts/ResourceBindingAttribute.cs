using System;
using System.Collections.Generic;
using System.Text;

namespace TransformProvider.Reflection.Contracts
{
    /// <summary>
    /// Applied to properties on Transform classes
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ResourceBindingAttribute : Attribute
    {
        public ResourceBindingDirection BindingDirection { get; private set; }

        public ResourceBindingAttribute(ResourceBindingDirection bindingDirection)
        {
            this.BindingDirection = bindingDirection;
        }
    }
}
