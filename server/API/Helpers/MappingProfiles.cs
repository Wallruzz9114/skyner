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
            CreateMap<AddressViewModel, ShippingAddress>();

            CreateMap<Order, OrderToReturnViewModel>()
                .ForMember(otrvm => otrvm.DeliveryMethod, e => e.MapFrom(o => o.DeliveryMethod.Name))
                .ForMember(otrvm => otrvm.ShippingPrice, e => e.MapFrom(o => o.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(oivm => oivm.ProductId, e => e.MapFrom(oi => oi.ItemOrdered.ProductItemId))
                .ForMember(oivm => oivm.ProductName, e => e.MapFrom(oi => oi.ItemOrdered.ProductName))
                .ForMember(oivm => oivm.PictureURL, e => e.MapFrom(oi => oi.ItemOrdered.PictureURL))
                .ForMember(oivm => oivm.PictureURL, e => e.MapFrom<OrderItemPictureURLResolver>());
        }
    }
}