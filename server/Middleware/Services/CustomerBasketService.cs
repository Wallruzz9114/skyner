using System;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using StackExchange.Redis;

namespace Middleware.Services
{
    public class CustomerBasketService : ICustomerBasketService
    {
        private readonly IDatabase _database;

        public CustomerBasketService(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<CustomerBasket> CreateOrUpdateCustomerBasketAsync(CustomerBasket customerBasket)
        {
            var customerBasketCreated = await _database.StringSetAsync(
                customerBasket.Id,
                JsonSerializer.Serialize(customerBasket),
                TimeSpan.FromDays(30)
            );

            if (!customerBasketCreated) return null;

            return await GetCustomerBasketAsync(customerBasket.Id);
        }

        public async Task<bool> DeleteCustomerBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetCustomerBasketAsync(string basketId)
        {
            var redisValue = await _database.StringGetAsync(basketId);
            return redisValue.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(redisValue);
        }
    }
}