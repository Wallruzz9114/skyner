using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserByCaimPrincipalWithAddressAsync(
            this UserManager<AppUser> userManager,
            ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;
            return await userManager.Users
                .Include(appUser => appUser.Address)
                .SingleOrDefaultAsync(appUser => appUser.Email == email);
        }

        public static async Task<AppUser> FindByClaimsPrincipal(
            this UserManager<AppUser> userManager,
            ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal?.Claims?.FirstOrDefault(claim => claim.Type == ClaimTypes.Email).Value;
            return await userManager.Users.SingleOrDefaultAsync(appUser => appUser.Email == email);
        }
    }
}