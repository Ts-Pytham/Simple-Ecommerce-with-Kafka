using Ecommerce.Models.Providers;
using Ecommerce.Models.PipelineBehaviours;
using MediatR;
using System.Reflection;
using FluentValidation;
using Ecommerce.ProductService.Application.CreateProduct;

namespace Ecommerce.ProductService.Extensions;

public static class ServiceExtensions
{
    /// <summary>
    /// Add services to the service collection
    /// </summary>
    /// <param name="service"> The service collection </param>
    /// <returns> The service collection </returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining<Program>();
            cfg.AddOpenBehavior(typeof(ValidationBehaviourResult<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehaviourResponse<,>));
        });
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}
