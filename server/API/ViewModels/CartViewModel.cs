using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class CartViewModel
    {
        [Required]
        public string Id { get; set; }

        public List<CartItemViewModel> Items { get; set; }
    }
}