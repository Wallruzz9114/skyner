using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync(string id);
        Task<Cart> CreateOrUpdateCartAsync(Cart cart);
        Task<bool> EmptyCartAsync(string id);
    }
}