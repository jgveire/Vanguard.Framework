namespace Vanguard.Framework.Core.Tests.DomainEvents
{
    using Vanguard.Framework.Core.DomainEvents;

    public class TestEvent : IDomainEvent
    {
        public bool IsHandlerExecuted { get; set; }
    }
}
