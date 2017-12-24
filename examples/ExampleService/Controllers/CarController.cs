namespace ExampleService.Controllers
{
    using System;
    using ExampleBusiness.Commands;
    using ExampleModels;
    using Microsoft.AspNetCore.Mvc;
    using Vanguard.Framework.Core.Cqrs;
    using Vanguard.Framework.Http;

    /// <summary>
    /// The car controller.
    /// </summary>
    /// <seealso cref="Vanguard.Framework.Http.CrudController{TIdentifier, TModel}" />
    [Route("api/v1/cars")]
    public class CarController : CrudController<Guid, CarModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarController"/> class.
        /// </summary>
        /// <param name="commandDispatcher">The command dispatcher.</param>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        public CarController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }

        /// <summary>
        /// Reports the car stolen.
        /// </summary>
        /// <param name="id">The car identifier.</param>
        /// <returns>An action result.</returns>
        [HttpPut("{id}/reportstolen")]
        public IActionResult ReportCarStolen(Guid id)
        {
            var command = new ReportCarStolenCommand(id);
            CommandDispatcher.Dispatch(command);
            return new NoContentResult();
        }
    }
}
