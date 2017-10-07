using System.Threading.Tasks;
using Vanguard.Framework.Core.DomainEvents;

namespace Vanguard.Framework.Core.Tests.DomainEvents
{
    public class TestAsyncEventHandler : IAsyncEventHandler<TestAsyncEvent>
    {
        public async Task Handle(TestAsyncEvent domainEvent)
        {
            await Task.Delay(10);
            domainEvent.IsHandlerExecuted = true;
        }
    }
}
