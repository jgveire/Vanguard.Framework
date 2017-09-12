using Vanguard.Framework.Core.Cqrs;

namespace Vanguard.Framework.Core.Tests.Cqrs
{
    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public void Execute(TestCommand command)
        {
            command.IsHandlerExecuted = true;
        }
    }
}
