using AutoMapper;
using WEBSHOP_API.Models;
using WEBSHOP_API.DTOs;

namespace WEBSHOP_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<Stock, StockDTO>();
            CreateMap<StockDTO, Stock>();
            CreateMap<StorageLogger, StorageLoggerDTO>();
        }
    }
}