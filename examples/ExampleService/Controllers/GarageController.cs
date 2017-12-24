﻿namespace ExampleService.Controllers
{
    using ExampleModels;
    using Microsoft.AspNetCore.Mvc;
    using Vanguard.Framework.Core.Cqrs;
    using Vanguard.Framework.Http;

    /// <summary>
    /// The garage controller.
    /// </summary>
    /// <seealso cref="Vanguard.Framework.Http.CrudController{TModel}" />
    [Route("api/v1/garages")]
    public class GarageController : CrudController<GarageModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GarageController"/> class.
        /// </summary>
        /// <param name="commandDispatcher">The command dispatcher.</param>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        public GarageController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }
    }
}
