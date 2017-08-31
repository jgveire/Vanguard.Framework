using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Vanguard.Framework.Core.Exceptions;
using Vanguard.Framework.Http.Resources;

namespace Vanguard.Framework.Http.Filters
{
    /// <summary>
    /// The exception filter class.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter" />
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Called after an action has thrown an <see cref="T:System.Exception" />.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>
        public void OnException(ExceptionContext context)
        {
            var innerExceptionType = context.Exception.InnerException?.GetType();
            if (innerExceptionType == typeof(ValidationException))
            {
                var exception = context.Exception.InnerException as ValidationException;
                var status = HttpStatusCode.BadRequest;
                var errorResponse = new ErrorResponse(status.ToString(), exception.Message, exception.Target);
                context.HttpContext.Response.StatusCode = (int)status;
                context.Result = new JsonResult(errorResponse);
            }
        }
    }
}
