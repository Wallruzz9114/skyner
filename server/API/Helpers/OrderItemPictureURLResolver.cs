using API.ViewModels;
using AutoMapper;
using Core.Models;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class OrderItemPictureURLResolver : IValueResolver<OrderItem, OrderItemViewModel, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPictureURLResolver(IConfiguration configuration) => _configuration = configuration;

        public string Resolve(
            OrderItem source, OrderItemViewModel destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureURL))
                return _configuration["ApiURL"] + source.ItemOrdered.PictureURL;

            return null;
        }
    }
}