namespace Vanguard.Framework.Core.Tests.Cqrs
{
    using System.Threading.Tasks;
    using Vanguard.Framework.Core.Cqrs;

    public class TestAsyncCommandHandler : IAsyncCommandHandler<TestAsyncCommand>
    {
        public async Task ExecuteAsync(TestAsyncCommand command)
        {
            await Task.Delay(10);
            command.IsHandlerExecuted = true;
        }
    }
}
