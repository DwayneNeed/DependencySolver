﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TransformProvider.Reflection.Contracts
{
    /// <summary>
    /// Applied to Resource properties on a Transform to describe the required
    /// predicate matches.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class InputPredicateMatchAttribute : Attribute
    {
        public string Name { get; private set; }

        public bool Value { get; private set; }

        public InputPredicateMatchAttribute(string name, bool value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}
