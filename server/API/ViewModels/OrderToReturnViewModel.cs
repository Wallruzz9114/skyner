using System;
using System.Collections.Generic;
using Core.Models;

namespace API.ViewModels
{
    public class OrderToReturnViewModel
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Total { get; set; }
        public string OrderStatus { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItemViewModel> OrderItems { get; set; }
    }
}