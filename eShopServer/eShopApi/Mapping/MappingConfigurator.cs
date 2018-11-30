using AutoMapper;
using BusinessEntities.Products;
using EntityDTO.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopApi.Mapping
{
    public static class MappingConfigurator
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<Product, ProductInfoDTO>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name));

                cfg.CreateMap<Product, ProductDetailDTO>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ImageBytes, opt => opt.Ignore())
                .ForMember(dest => dest.ImageBigBytes, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.ImageBig, opt => opt.Ignore());

                cfg.CreateMap<ProductDetailDTO, Product>()
                .ForMember(dest => dest.Brand, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.ImageBig, opt => opt.Ignore())
                .ForMember(dest => dest.ImageID, opt => opt.Ignore())
                .ForMember(dest => dest.ImageBigID, opt => opt.Ignore());

                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();

            });
            Mapper.AssertConfigurationIsValid();
        }
    }
}
