using Vanguard.Framework.Core.DomainEvents;

namespace Vanguard.Framework.Core.Tests.DomainEvents
{
    public class TestEvent : IDomainEvent
    {
        public bool IsHandlerExecuted { get; set; }
    }
}
