namespace Vanguard.Framework.Http.Filters
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Vanguard.Framework.Core.Exceptions;

    /// <summary>
    /// The validation exception filter.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter" />
    public class ValidationExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Called after an action has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception.InnerException as ValidationException ?? context.Exception as ValidationException;
            if (exception == null)
            {
                return;
            }

            var status = HttpStatusCode.BadRequest;
            var errorResponse = new ErrorResponse(status.ToString(), exception.Message, exception.Target);
            context.HttpContext.Response.StatusCode = (int)status;
            context.Result = new JsonResult(errorResponse);
        }
    }
}
