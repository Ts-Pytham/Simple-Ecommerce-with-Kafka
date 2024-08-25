using FluentValidation;
using MediatR;
using SimpleResults;

namespace Ecommerce.Models.PipelineBehaviours;
public class ValidationBehaviourResult<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> Validators)
    : IPipelineBehavior<TRequest, Result<TResponse>> where TRequest : IRequest<Result<TResponse>>
{
    public async Task<Result<TResponse>> Handle(
        TRequest request, 
        RequestHandlerDelegate<Result<TResponse>> next, 
        CancellationToken cancellationToken)
    {
        if (!Validators.Any()) return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(Validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        if(failures.Count != 0)
        {
            return Result.Failure(failures.Select(f => f.ErrorMessage));
        }

        return await next();
    }
}
