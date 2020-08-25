using System.Collections.Generic;

namespace Core.Models
{
    public class Cart
    {
        public Cart() { }

        public Cart(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}