namespace Vanguard.Framework.Core.Tests.DomainEvents
{
    using Vanguard.Framework.Core.DomainEvents;

    public class TestEventHandler : IEventHandler<TestEvent>
    {
        public void Handle(TestEvent domainEvent)
        {
            domainEvent.IsHandlerExecuted = true;
        }
    }
}
