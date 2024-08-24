namespace Ecommerce.Models.Providers;
public interface IDateTimeProvider
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
}
