using Vanguard.Framework.Core.DomainEvents;

namespace Vanguard.Framework.Core.Tests.DomainEvents
{
    public class TestEventHandler : IEventHandler<TestEvent>
    {
        public void Handle(TestEvent domainEvent)
        {
            domainEvent.IsHandlerExecuted = true;
        }
    }
}
