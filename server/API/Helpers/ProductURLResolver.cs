using API.ViewModels;
using AutoMapper;
using Core.Models;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class ProductURLResolver : IValueResolver<Product, ProductViewModel, string>
    {
        private readonly IConfiguration _configuration;

        public ProductURLResolver(IConfiguration configuration) => _configuration = configuration;

        public string Resolve(
            Product source,
            ProductViewModel destination,
            string destMember,
            ResolutionContext context
        ) => !string.IsNullOrEmpty(source.PictureURL) ? _configuration["ApiURL"] + source.PictureURL : null;
    }
}