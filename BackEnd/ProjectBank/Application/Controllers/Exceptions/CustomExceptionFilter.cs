using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ProjectBank.Application.Controllers.Exceptions
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is FluentValidation.ValidationException vex)
            {
                var errorResponse = new { Message = "Validation failed", Errors = vex.Errors.Select(e => e.ErrorMessage) };
                context.Result = new BadRequestObjectResult(errorResponse);
                context.ExceptionHandled = true;
            }
            else if (context.Exception is UnauthorizedAccessException uax)
            {
                var errorResponse = new { Message = "Unauthorized", Details = uax.Message };
                context.Result = new UnauthorizedObjectResult(errorResponse);
                context.ExceptionHandled = true;
            }
            else if (context.Exception is KeyNotFoundException knf)
            {
                var errorResponse = new { Message = "Not Found", Details = knf.Message };
                context.Result = new NotFoundObjectResult(errorResponse);
                context.ExceptionHandled = true;
            }
            //else if (context.Exception is ForbiddenAccessException fax)
            //{
            //    var errorResponse = new { Message = "Forbidden", Details = fax.Message };
            //    context.Result = new ForbidResult();
            //    context.ExceptionHandled = true;
            //}
            //else if (context.Exception is ResourceNotFoundException rnf)
            //{
            //    var errorResponse = new { Message = "Not Found", Details = rnf.Message };
            //    context.Result = new NotFoundObjectResult(errorResponse);
            //    context.ExceptionHandled = true;
            //}
            //else if (context.Exception is ConflictException cex)
            //{
            //    var errorResponse = new { Message = "Conflict", Details = cex.Message };
            //    context.Result = new ConflictObjectResult(errorResponse);
            //    context.ExceptionHandled = true;
            //}
            else
            {
                var errorResponse = new { Message = "Internal server error", Details = context.Exception.Message };
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = 500
                };
                context.ExceptionHandled = true;
            }
        }
    }
}