namespace Vanguard.Framework.Core.Tests.Cqrs
{
    using Vanguard.Framework.Core.Cqrs;

    public class TestCommandHandler : ICommandHandler<TestCommand>
    {
        public void Execute(TestCommand command)
        {
            command.IsHandlerExecuted = true;
        }
    }
}
