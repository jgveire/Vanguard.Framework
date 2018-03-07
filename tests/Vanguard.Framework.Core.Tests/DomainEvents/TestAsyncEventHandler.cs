namespace Vanguard.Framework.Core.Tests.DomainEvents
{
    using System.Threading.Tasks;
    using Vanguard.Framework.Core.DomainEvents;

    public class TestAsyncEventHandler : IAsyncEventHandler<TestAsyncEvent>
    {
        public async Task HandleAsync(TestAsyncEvent domainEvent)
        {
            await Task.Delay(10);
            domainEvent.IsHandlerExecuted = true;
        }
    }
}
