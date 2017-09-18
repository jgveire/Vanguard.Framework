using System.Threading.Tasks;
using Vanguard.Framework.Core.Cqrs;

namespace Vanguard.Framework.Core.Tests.Cqrs
{
    public class TestAsyncCommandHandler : IAsyncCommandHandler<TestCommand>
    {
        public async Task Execute(TestCommand command)
        {
            await Task.Delay(10);
            command.IsHandlerExecuted = true;
        }
    }
}
