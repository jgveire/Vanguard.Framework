using Vanguard.Framework.Core.DomainEvents;

namespace Vanguard.Framework.Core.Tests.DomainEvents
{
    public class TestAsyncEvent : IAsyncDomainEvent
    {
        public bool IsHandlerExecuted { get; set; }
    }
}
