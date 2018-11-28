namespace Vanguard.Framework.Http
{
    using Microsoft.AspNetCore.Mvc;
    using Vanguard.Framework.Core.Cqrs;

    /// <summary>
    /// The controller base class.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class ControllerBase : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBase"/> class.
        /// </summary>
        /// <param name="commandDispatcher">The command dispatcher.</param>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        public ControllerBase(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            Guard.ArgumentNotNull(commandDispatcher, nameof(commandDispatcher));
            Guard.ArgumentNotNull(queryDispatcher, nameof(queryDispatcher));

            CommandDispatcher = commandDispatcher;
            QueryDispatcher = queryDispatcher;
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
