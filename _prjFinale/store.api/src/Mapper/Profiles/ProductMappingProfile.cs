using AutoMapper;
using store.api.src.Dto.Product;
using store.core.src.Domain.Entity.Catalog;
namespace store.api.src.Mapper.Profiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductCreateRequest>().ReverseMap();
        CreateMap<Product, ProductUpdateRequest>().ReverseMap();
    }
}