using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Ecommerce.Shared.Extensions;
public static class HttpContextExtensions
{
    public static string? GetUserId(this IHttpContextAccessor httpContextAccessor)
    {
        return httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }

    public static string? GetUserName(this IHttpContextAccessor httpContextAccessor)
    {
        return httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    }

    public static IEnumerable<string> GetRoles(this IHttpContextAccessor httpContextAccessor)
    {
        return httpContextAccessor.HttpContext?.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value) ?? [];
    }
}
