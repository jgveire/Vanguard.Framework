namespace Vanguard.Framework.Http.Filters
{
    using System.Collections.Generic;
    using System.Net;
    using System.Text.RegularExpressions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Vanguard.Framework.Http.Resources;

    /// <summary>
    /// The validate model attribute.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var status = HttpStatusCode.BadRequest;
                var errorResponse = new ErrorResponse
                {
                    Error = new Error(ErrorCode.ValidationError, ExceptionResource.ValidationError),
                };
                errorResponse.Error.Details = GetErrorDetails(context.ModelState);
                context.HttpContext.Response.StatusCode = (int)status;
                context.Result = new JsonResult(errorResponse);
            }
        }

        private List<Error> GetErrorDetails(ModelStateDictionary modelState)
        {
            var result = new List<Error>();
            foreach (var modelStateItem in modelState)
            {
                foreach (var error in modelStateItem.Value.Errors)
                {
                    string target = GetTargetName(modelStateItem.Key);
                    result.Add(new Error(ErrorCode.ValidationError, error.ErrorMessage, target));
                }
            }

            return result;
        }

        private string GetTargetName(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return key;
            }

            // Converts C# property name to Json property name.
            return Regex.Replace(key, @"^([A-Z]{1})|\.([A-Z]{1})", match => match.ToString().ToLower());
        }
    }
}
