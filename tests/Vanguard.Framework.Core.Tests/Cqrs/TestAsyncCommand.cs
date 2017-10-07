using Vanguard.Framework.Core.Cqrs;

namespace Vanguard.Framework.Core.Tests.Cqrs
{
    public class TestAsyncCommand : IAsyncCommand
    {
        public bool IsHandlerExecuted { get; set; }
    }
}
