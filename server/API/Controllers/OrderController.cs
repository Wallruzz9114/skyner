using System.Collections.Generic;
using System.Threading.Tasks;
using API.Errors;
using API.Extensions;
using API.ViewModels;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Order>> CreateOrderAsync(OrderViewModel orderViewModel)
        {
            var customerEmail = HttpContext.User.GetCustomerEmail();
            var shippingAddress = _mapper.Map<AddressViewModel, ShippingAddress>(orderViewModel.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(customerEmail, orderViewModel.DeliveryMethodId, orderViewModel.CartId, shippingAddress);

            if (order == null) return BadRequest(new APIResponse(StatusCodes.Status400BadRequest, "Problem creating order"));

            return Ok(order);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IReadOnlyList<OrderViewModel>>> GetOrdersForAppUserAsync()
        {
            var customerEmail = HttpContext.User.GetCustomerEmail();
            var orders = await _orderService.GetOrdersForAppUserAsync(customerEmail);

            return Ok(_mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnViewModel>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnViewModel>> GetAppUserOrderByIdAsync(int id)
        {
            var customerEmail = HttpContext.User.GetCustomerEmail();
            var order = await _orderService.GetOrderByIdAsync(id, customerEmail);

            if (order == null) return NotFound(new APIResponse(StatusCodes.Status404NotFound));

            return _mapper.Map<Order, OrderToReturnViewModel>(order);
        }

        [HttpGet("deliverymethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethodsAsync() =>
            Ok(await _orderService.GetDeliveryMethodsAsync());
    }
}