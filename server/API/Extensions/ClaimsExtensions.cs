using System.Linq;
using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetCustomerEmail(this ClaimsPrincipal claimsPrincipal) =>
            claimsPrincipal?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;
    }
}