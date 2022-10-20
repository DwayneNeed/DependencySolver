using TransformProvider.Reflection.Contracts;
using Example.Resources;

namespace Example.Transforms
{
    [Transform]
    class StartVmTransform
    {
        [ResourceBinding(ResourceBindingDirection.Input | ResourceBindingDirection.Output)]
        [InputPredicateMatch("IsRunning", false)]
        [OutputPredicateMatch("IsRunning", true)]
        public VirtualMachine VirtualMachine { get; set; }
    }
}
