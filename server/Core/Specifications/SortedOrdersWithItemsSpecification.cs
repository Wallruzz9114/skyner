using Core.Models;

namespace Core.Specifications
{
    public class SortedOrdersWithItemsSpecification : Specification<Order>
    {
        public SortedOrdersWithItemsSpecification(string customerEmail)
            : base(order => order.CustomerEmail == customerEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
            AddOrderByDescending(order => order.OrderDate);
        }

        public SortedOrdersWithItemsSpecification(int orderId, string customerEmail)
            : base(order => order.Id == orderId && order.CustomerEmail == customerEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
        }
    }
}