using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(d=> d.ProductBrand,o=> o.MapFrom(s=>s.ProductBrand.Name))
            .ForMember(d=> d.ProductType,o=> o.MapFrom(s=>s.ProductType.Name))
            .ForMember(d=>d.PictureUrl,o=> o.MapFrom<ProductUrlResolver>());
        }
    }

    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config=config;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
           {
              return _config["ApiUrl"] + source.PictureUrl;
           }
              return null;   
        }
    }
}