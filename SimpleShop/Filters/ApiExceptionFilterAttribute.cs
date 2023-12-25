using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SimpleShop.Application.Modify.Behaviours;
using SimpleShop.Application.Modify.Exceptions;

namespace SimpleShop.ModifyApi.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var type = context.Exception.GetType();

            if (type == typeof(NotFoundException))
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);

                context.ExceptionHandled = true;
            }

            if (type == typeof(RequestValidationException))
            {
                context.Result = new BadRequestObjectResult(((RequestValidationException)context.Exception).Errors);

                context.ExceptionHandled = true;
            }

            base.OnException(context);
        }
    }
}
