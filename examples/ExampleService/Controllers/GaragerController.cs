namespace ExampleService.Controllers
{
    using ExampleService.Models;
    using Microsoft.AspNetCore.Mvc;
    using Vanguard.Framework.Core.Cqrs;
    using Vanguard.Framework.Http;

    [Route("v1/garages")]
    public class GaragerController : CrudController<GarageModel>
    {
        public GaragerController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }
    }
}
