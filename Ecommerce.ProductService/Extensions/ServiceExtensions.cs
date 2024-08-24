using Ecommerce.Models.Providers;

namespace Ecommerce.ProductService.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection service)
    {
        service.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return service;
    }
}
