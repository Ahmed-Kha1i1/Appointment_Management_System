using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AMS.Application.Common.Extensions
{
    public static class HttpContextExtensions
    {
        public static int GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            string userId = httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            return int.Parse(userId);
        }
    }
}
