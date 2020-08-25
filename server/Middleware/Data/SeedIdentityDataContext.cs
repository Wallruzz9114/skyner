using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Middleware.Data
{
    public class SeedIdentityDataContext
    {
        public static async Task SeedIdentityDataAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@email.com",
                    UserName = "bob@email.com",
                    Address = new Address
                    {
                        FirstName = "Bob",
                        LastName = "Marley",
                        Street = "9916 111 St",
                        City = "Edmonton",
                        Province = "AB",
                        PostalCode = "T5H 0C5"
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}