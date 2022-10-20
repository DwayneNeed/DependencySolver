using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TransformProvider.Reflection
{
    public class AssemblyTypeProvider : ITypeProvider
    {
        private Assembly assembly;

        public AssemblyTypeProvider(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public IEnumerable<Type> Types
        {
            get
            {
                foreach(var type in this.assembly.GetTypes())
                {
                    yield return type;

                    // Extract nested types and stream out the results.
                    foreach(var nestedType in GetNestedTypes(type))
                    {
                        yield return nestedType;
                    }
                }
            }
        }

        private IEnumerable<Type> GetNestedTypes(Type type)
        {
            foreach (var nestedType in type.GetNestedTypes())
            {
                yield return nestedType;

                // Make a recursive call, and stream out the results.
                foreach(var nestedNestedType in GetNestedTypes(nestedType))
                {
                    yield return nestedNestedType;
                }
            }
        }
    }
}
