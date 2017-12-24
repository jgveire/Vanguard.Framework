namespace Vanguard.Framework.Core.Tests.Cqrs
{
    using Vanguard.Framework.Core.Cqrs;

    public class TestQueryHandler : IQueryHandler<string, TestQuery>
    {
        public string Retrieve(TestQuery query)
        {
            return "Success";
        }
    }
}
