using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CustomerBasketController : BaseController
    {
        private readonly ICustomerBasketService _customerBasketService;

        public CustomerBasketController(ICustomerBasketService customerBasketService)
        {
            _customerBasketService = customerBasketService;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetCustomerBasketByIdAsync(string id)
        {
            var customerBasket = await _customerBasketService.GetCustomerBasketAsync(id);
            return Ok(customerBasket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateCustomerBasket(CustomerBasket customerBasket)
        {
            var updatedCustomerBasket = await _customerBasketService.CreateOrUpdateCustomerBasketAsync(customerBasket);
            return Ok(updatedCustomerBasket);
        }

        [HttpDelete]
        public async Task DeleteCustomerBasketAsync(string id)
        {
            await _customerBasketService.DeleteCustomerBasketAsync(id);
        }
    }
}