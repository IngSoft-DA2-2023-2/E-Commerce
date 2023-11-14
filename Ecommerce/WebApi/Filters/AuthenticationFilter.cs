using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Filters
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string header = context.HttpContext.Request.Headers["Authorization"];
            if (header is null)
            {
                context.Result = new ObjectResult("Authorization header is required.")
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }
        }
    }
}