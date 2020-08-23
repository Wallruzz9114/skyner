using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface ICustomerBasketService
    {
        Task<CustomerBasket> GetCustomerBasketAsync(string basketId);
        Task<CustomerBasket> CreateOrUpdateCustomerBasketAsync(CustomerBasket customerBasket);
        Task<bool> DeleteCustomerBasketAsync(string basketId);
    }
}