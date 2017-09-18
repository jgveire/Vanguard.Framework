using System.Threading.Tasks;
using Vanguard.Framework.Core.Cqrs;

namespace Vanguard.Framework.Core.Tests.Cqrs
{
    public class TestAsyncQueryHandler : IAsyncQueryHandler<string, TestQuery>
    {
        public async Task<string> Retrieve(TestQuery query)
        {
            await Task.Delay(10);
            return "Success";
        }
    }
}
