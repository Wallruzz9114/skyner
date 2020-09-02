using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Middleware.Data
{
    public class IdentityDataContext : IdentityDbContext<AppUser>
    {
        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
    }
}