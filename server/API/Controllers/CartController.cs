using System.Threading.Tasks;
using API.ViewModels;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CCartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CCartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Cart>> GetCartAsync(string id)
        {
            var cart = await _cartService.GetCartAsync(id);
            return Ok(cart ?? new Cart(id));
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> CreateOrUpdateCartAsync(CartViewModel cartViewModel)
        {
            var cart = _mapper.Map<CartViewModel, Cart>(cartViewModel);
            var updatedCart = await _cartService.CreateOrUpdateCartAsync(cart);
            return Ok(updatedCart);
        }

        [HttpDelete]
        public async Task EmptyCartAsync(string id)
        {
            await _cartService.EmptyCartAsync(id);
        }
    }
}