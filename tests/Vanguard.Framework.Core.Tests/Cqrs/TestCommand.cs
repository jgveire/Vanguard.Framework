namespace Vanguard.Framework.Core.Tests.Cqrs
{
    using Vanguard.Framework.Core.Cqrs;

    public class TestCommand : ICommand
    {
        public bool IsHandlerExecuted { get; set; }
    }
}
