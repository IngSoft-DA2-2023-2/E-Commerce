using Domain.Exceptions;
using LogicInterface.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace WebApi.Filters
{
    [ExcludeFromCodeCoverage]
    public class AnnotatedCustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is LogicException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                };
            }
            else if (context.Exception is DomainException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                };
            }
            else
            {

                context.Result = new ObjectResult(new { ErrorMessage = "Something went wrong." })
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
            }
        }
    }
}