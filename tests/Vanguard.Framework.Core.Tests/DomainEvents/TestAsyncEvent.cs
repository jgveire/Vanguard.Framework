namespace Vanguard.Framework.Core.Tests.DomainEvents
{
    using Vanguard.Framework.Core.DomainEvents;

    public class TestAsyncEvent : IAsyncDomainEvent
    {
        public bool IsHandlerExecuted { get; set; }
    }
}
