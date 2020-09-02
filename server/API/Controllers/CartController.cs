using System.Threading.Tasks;
using API.ViewModels;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Cart>> GetCartAsync(string id) =>
            Ok(await _cartService.GetCartAsync(id) ?? new Cart(id));

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateOrUpdateCartAsync(CartViewModel cartViewModel)
        {
            var cart = _mapper.Map<CartViewModel, Cart>(cartViewModel);
            var updatedCart = await _cartService.CreateOrUpdateCartAsync(cart);
            return Ok(updatedCart);
        }

        [HttpDelete]
        public async Task EmptyCartAsync(string id) => await _cartService.EmptyCartAsync(id);
    }
}