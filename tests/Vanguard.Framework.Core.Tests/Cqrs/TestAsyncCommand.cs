namespace Vanguard.Framework.Core.Tests.Cqrs
{
    using Vanguard.Framework.Core.Cqrs;

    public class TestAsyncCommand : IAsyncCommand
    {
        public bool IsHandlerExecuted { get; set; }
    }
}
