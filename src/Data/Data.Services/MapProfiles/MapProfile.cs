namespace Data.Services.MapProfiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using AutoMapper;

    using Data.Models.Entities;
    using Data.Services.DtoModels;

    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ImageFile, ImageDto>().ReverseMap();
            CreateMap<Model, ModelDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Specification, SpecificationDto>().ReverseMap();
        }
    }
}
