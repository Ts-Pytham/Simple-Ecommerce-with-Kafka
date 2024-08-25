using FluentValidation;
using MediatR;
using SimpleResults;

namespace Ecommerce.Models.PipelineBehaviours;
public class ValidationBehaviourResponse<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> Validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
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
            throw new ValidationException(failures);
        }

        return await next();
    }
}
