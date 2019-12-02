namespace Vanguard.Framework.Http
{
    using Vanguard.Framework.Core.Cqrs;

    /// <summary>
    /// The controller base class.
    /// </summary>
    public class ControllerBase : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBase"/> class.
        /// </summary>
        /// <param name="commandDispatcher">The command dispatcher.</param>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        public ControllerBase(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
            : base(commandDispatcher, queryDispatcher)
        {
        }
    }
}
