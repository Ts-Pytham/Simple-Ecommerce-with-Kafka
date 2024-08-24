using Ecommerce.Models.Providers;

namespace Ecommerce.ProductService.Extensions;

public static class ServiceExtensions
{
    /// <summary>
    /// Add services to the service collection
    /// </summary>
    /// <param name="service"> The service collection </param>
    /// <returns> The service collection </returns>
    public static IServiceCollection AddServices(this IServiceCollection service)
    {
        service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        service.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return service;
    }
}
