using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;


namespace Services.AutoMapperProfile
{
    public class ExampleProfile : AutoMapper.Profile
    {
        //        public ExampleProfile() 
        //        {
        //            CreateMap<Product, ProductDto>()
        //                .ForMember(dest => dest.ProductBrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
        //                .ForMember(dest => dest.ProductTypeName, opt => opt.MapFrom(src => src.ProductType.Name))
        //                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());

        //            CreateMap<ProductBrand, BrandDto>();
    }
}

