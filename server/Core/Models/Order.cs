using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class Order : BaseModel
    {
        public Order() { }

        public Order(
            string customerEmail,
            decimal subTotal,
            ShippingAddress shippingAddress,
            DeliveryMethod deliveryMethod,
            IReadOnlyList<OrderItem> orderItems)
        {
            CustomerEmail = customerEmail;
            SubTotal = subTotal;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
        }

        public string CustomerEmail { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public string PaymentMethodId { get; set; }

        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;
    }
}