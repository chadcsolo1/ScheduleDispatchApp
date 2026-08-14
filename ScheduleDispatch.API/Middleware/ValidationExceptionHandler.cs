using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ScheduleDispatch.API.Middleware
{
    //public sealed class ValidationExceptionHandler : IExceptionHandler
    //{
    //    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    //    {
    //        if (exception is not ValidationException validationException)
    //        {
    //            return false;
    //        }

    //        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    //        var context = new ProblemDetailsContext
    //        {
    //            HttpContext = httpContext,
    //            Exception = exception,
    //            ProblemDetails = new ProblemDetails
    //            {
    //                Title = "Validation Error",
    //                //Detail = validationException.Message,
    //                Status = StatusCodes.Status400BadRequest
    //            }
    //        };

    //        var errors = validationException.Errors
    //            .GroupBy(e => e.PropertyName)
    //            .ToDictionary(
    //                g => g.Key.ToLowerVariant(),
    //                g => g.Select(e => e.ErrorMessage).ToArray()
    //            );

    //        context.ProblemDetails.Extensions.Add("errors", errors);

    //        return await problemDetailsService.TryWriteAsync(context);

    //    }
    //}
}
