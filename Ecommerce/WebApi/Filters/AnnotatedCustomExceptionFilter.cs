using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using LogicInterface.Exceptions;
namespace WebApi.Filters
{
    public class AnnotatedCustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is LogicException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = 400
                };
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new ObjectResult(new { ErrorMessage = context.Exception.Message })
                {
                    StatusCode = 403
                };
            }
            else
            {
                context.Result = new ObjectResult(new { ErrorMessage = "Something went wrong." })
                {
                    StatusCode = 500
                };
            }
            

        }
    }
}
