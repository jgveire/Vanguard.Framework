using Microsoft.AspNetCore.Mvc;

namespace Vanguard.Framework.Http
{
    /// <summary>
    /// The controller base class.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class ControllerBase : Controller
    {
        /// <summary>
        /// Creates an <see cref="OkObjectResult"/> object that produces an
        /// <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status200OK"/> response
        /// when the value is not null otherwise an <see cref="NotFoundObjectResult"/> object that produces an
        /// <see cref="Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound"/>.
        /// </summary>
        /// <param name="value">The content value to format in the entity body.</param>
        /// <returns>
        /// An <see cref="OkObjectResult"/> response when the value is not null
        /// otherwise an <see cref="NotFoundObjectResult"/> response.
        /// </returns>
        protected IActionResult OkOrNotFound(object value)
        {
            if (value == null)
            {
                return NotFound();
            }

            return Ok(value);
        }
    }
}
