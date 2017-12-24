namespace ExampleService.Controllers
{
    using ExampleModels;
    using Microsoft.AspNetCore.Mvc;
    using Vanguard.Framework.Core.Cqrs;
    using Vanguard.Framework.Http;

    [Route("api/v1/garages")]
    public class GaragerController : CrudController<GarageModel>
    {
        public GaragerController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }
    }
}
