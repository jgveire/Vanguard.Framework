using Vanguard.Framework.Core.Cqrs;

namespace Vanguard.Framework.Core.Tests.Cqrs
{
    public class TestQueryHandler : IQueryHandler<string, TestQuery>
    {
        public string Retrieve(TestQuery query)
        {
            return "Success";
        }
    }
}
