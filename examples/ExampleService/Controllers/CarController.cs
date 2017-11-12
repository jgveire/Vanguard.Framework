﻿using ExampleBusiness.Commands;

namespace ExampleService.Controllers
{
    using System;
    using ExampleService.Models;
    using Microsoft.AspNetCore.Mvc;
    using Vanguard.Framework.Core.Cqrs;
    using Vanguard.Framework.Http;

    [Route("v1/cars")]
    public class CarController : CrudController<Guid, CarModel>
    {
        public CarController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }

        [HttpPut("{id}/reportstolen")]
        public IActionResult ReportCarStolen(Guid id)
        {
            var command = new ReportCarStolenCommand(id);
            CommandDispatcher.Dispatch(command);
            return new NoContentResult();
        }
    }
}
