namespace Vanguard.Framework.Core.Tests.Cqrs
{
    using System.Threading.Tasks;
    using Vanguard.Framework.Core.Cqrs;

    public class TestAsyncQueryHandler : IAsyncQueryHandler<string, TestAsyncQuery>
    {
        public async Task<string> Retrieve(TestAsyncQuery query)
        {
            await Task.Delay(10);
            return "Success";
        }
    }
}
