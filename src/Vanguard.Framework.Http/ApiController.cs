﻿namespace Vanguard.Framework.Http
{
    using Microsoft.AspNetCore.Mvc;
    using Vanguard.Framework.Core.Cqrs;

    /// <summary>
    /// The API controller base class.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    public class ApiController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiController"/> class.
        /// </summary>
        /// <param name="commandDispatcher">The command dispatcher.</param>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        public ApiController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            CommandDispatcher = Guard.ArgumentNotNull(commandDispatcher, nameof(commandDispatcher));
            QueryDispatcher = Guard.ArgumentNotNull(queryDispatcher, nameof(queryDispatcher));
        }

        /// <summary>
        /// Gets the command dispatcher.
        /// </summary>
        /// <value>
        /// The command dispatcher.
        /// </value>
        protected ICommandDispatcher CommandDispatcher { get; }

        /// <summary>
        /// Gets the query dispatcher.
        /// </summary>
        /// <value>
        /// The query dispatcher.
        /// </value>
        protected IQueryDispatcher QueryDispatcher { get; }

        /// <summary>
        /// Creates an <see cref="OkObjectResult" /> object that produces an
        /// <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status200OK" /> response
        /// when the value is not null otherwise an <see cref="NotFoundObjectResult" /> object that produces an
        /// <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound" />.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>
        /// An <see cref="OkObjectResult" /> response when the value is not null
        /// otherwise an <see cref="NotFoundObjectResult" /> response.
        /// </returns>
        protected ActionResult<TResult> OkOrNotFound<TResult>(TResult value)
        {
            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);
        }
    }
}
