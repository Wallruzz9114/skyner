using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;

namespace Middleware.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartService _cartService;

        public OrderService(IUnitOfWork unitOfWork, ICartService cartService)
        {
            _unitOfWork = unitOfWork;
            _cartService = cartService;
        }

        public async Task<Order> CreateOrderAsync(
            string customerEmail, int deliveryMethodId, string cartId, ShippingAddress shippingAddress)
        {
            // Get cart
            var cart = await _cartService.GetCartAsync(cartId);

            // Get order items
            var orderItems = new List<OrderItem>();
            foreach (var cartItem in cart.Items)
            {
                var productItem = await _unitOfWork.Service<Product>().GetByIdAsync(cartItem.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureURL);
                var orderItem = new OrderItem(itemOrdered, productItem.Price, cartItem.Quantity);

                orderItems.Add(orderItem);
            }

            // Get delivery method
            var deliveryMethod = await _unitOfWork.Service<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // Calculate subttotal
            var subTotal = orderItems.Sum(orderitem => orderitem.Price * orderitem.Quantity);

            // Create order
            var order = new Order(customerEmail, subTotal, shippingAddress, deliveryMethod, orderItems);

            // Save to database
            _unitOfWork.Service<Order>().Add(order);
            var orderSaved = await _unitOfWork.SaveChangesToDatabase();

            // Sanity check
            if (orderSaved <= 0) return null;

            // Empty cart
            await _cartService.EmptyCartAsync(cartId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync() =>
            await _unitOfWork.Service<DeliveryMethod>().ListAllAsync();

        public async Task<Order> GetOrderByIdAsync(int orderId, string customerEmail)
        {
            var specification = new SortedOrdersWithItemsSpecification(orderId, customerEmail);
            return await _unitOfWork.Service<Order>().GetByIdWithObjectsAsync(specification);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForAppUserAsync(string customerEmail)
        {
            var specification = new SortedOrdersWithItemsSpecification(customerEmail);
            return await _unitOfWork.Service<Order>().ListAllWithObjectsAsync(specification);
        }
    }
}