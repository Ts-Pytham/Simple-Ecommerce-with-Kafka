namespace Ecommerce.Shared.Providers;
public class Claims
{
    public string UserId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public IEnumerable<string> Roles { get; set; } = [];
}
