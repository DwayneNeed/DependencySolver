using TransformProvider.Reflection.Contracts;

namespace Example.Resources
{
    [Resource]
    public class VirtualMachine
    {
        private PowerState PowerState { get; set; }

        [Predicate]
        bool IsRunning => this.PowerState == PowerState.Running;

        [Predicate]
        bool IsStopped => this.PowerState == PowerState.Stopped;

        [Predicate]
        bool IsDeallocated => this.PowerState == PowerState.Deallocated;
    }
}
