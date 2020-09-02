using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string customerEmail, int deliveryMethodId, string cartId, ShippingAddress shippingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForAppUserAsync(string customerEmail);
        Task<Order> GetOrderByIdAsync(int orderId, string customerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}