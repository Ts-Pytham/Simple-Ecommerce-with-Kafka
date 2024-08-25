using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SimpleResults;

namespace Ecommerce.Models.Exceptions;
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> Logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        Logger.LogError(exception, "An unexpected error occurred");

        Result result = exception switch
        {
            ValidationException validationException => Result.Failure(validationException.Errors.Select(e => e.ErrorMessage)),
            _ => Result.Failure(exception.Message)
        };

        await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
        return true;
    }
}
