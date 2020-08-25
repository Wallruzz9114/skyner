using Core.Models;

namespace Core.Interfaces
{
    public interface IJWTTokenService
    {
        string CreateToken(AppUser appUser);
    }
}