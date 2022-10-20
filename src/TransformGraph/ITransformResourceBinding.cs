using System;
namespace TransformGraph.Contracts
{
    public interface ITransformResourceBinding
    {
        Type ResourceType { get; }

        string Name { get; }

        ITransformInputPredicateSet InputPredicateSet { get; }

        ITransformOutputPredicateSet OutputPredicateSet { get; }
    }
}
