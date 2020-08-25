using API.ViewModels;
using AutoMapper;
using Core.Models;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(pvm => pvm.ProductBrand, e => e.MapFrom(p => p.ProductBrand.Name))
                .ForMember(pvm => pvm.ProductType, e => e.MapFrom(p => p.ProductType.Name))
                .ForMember(pvm => pvm.PictureURL, e => e.MapFrom<ProductURLResolver>());

            CreateMap<Address, AddressViewModel>();
            CreateMap<AddressViewModel, Address>();
            CreateMap<CartViewModel, Cart>();
            CreateMap<CartItemViewModel, CartItem>();
        }
    }
}